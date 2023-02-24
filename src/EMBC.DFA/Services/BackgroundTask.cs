using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Medallion.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EMBC.DFA.Services
{
    public interface IBackgroundTask
    {
        public string Schedule { get; }
        public int DegreeOfParallelism { get; }
        public TimeSpan InitialDelay { get; }
        TimeSpan InactivityTimeout { get; }

        public Task ExecuteAsync(CancellationToken cancellationToken);
    }

    internal class BackgroundTask<T> : BackgroundService
        where T : IBackgroundTask
    {
        private readonly IServiceProvider serviceProvider;
        private readonly CronExpression schedule;
        private readonly TimeSpan startupDelay;
        private readonly bool enabled;
        private readonly IDistributedSemaphore semaphore;
        private long runNumber = 0;

        public BackgroundTask(IServiceProvider serviceProvider, IDistributedSemaphoreProvider distributedSemaphoreProvider)
        {
            this.serviceProvider = serviceProvider;
            using (var scope = serviceProvider.CreateScope())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>().GetSection($"backgroundtask:{typeof(T).Name}");
                var task = scope.ServiceProvider.GetRequiredService<T>();
                var appName = Environment.GetEnvironmentVariable("APP_NAME") ?? Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;

                schedule = CronExpression.Parse(configuration.GetValue("schedule", task.Schedule), CronFormat.IncludeSeconds);
                startupDelay = configuration.GetValue("initialDelay", task.InitialDelay);
                enabled = configuration.GetValue("enabled", true);
                var degreeOfParallelism = configuration.GetValue("degreeOfParallelism", task.DegreeOfParallelism);

                if (!string.IsNullOrEmpty(appName)) appName += "-";
                semaphore = distributedSemaphoreProvider.CreateSemaphore($"{appName}backgroundtask:{typeof(T).Name}", degreeOfParallelism);

                if (enabled)
                {
                    Console.WriteLine("starting background task: initial delay {0}, schedule: {1}, parallelism: {2}", this.startupDelay, this.schedule.ToString(), task.DegreeOfParallelism);
                }
                else
                {
                    Console.WriteLine($"background task is disabled, check configuration flag 'backgroundTask:{typeof(T).Name}'");
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!enabled) return;

            await Task.Delay(startupDelay, stoppingToken);

            var nextExecutionDelay = CalculateNextExecutionDelay(DateTime.UtcNow);

            IDistributedSynchronizationHandle? handle = null;

            while (!stoppingToken.IsCancellationRequested)
            {
                runNumber++;
                using (var scope = serviceProvider.CreateScope())
                {
                    var task = scope.ServiceProvider.GetRequiredService<T>();

                    Console.WriteLine("next run in {0}s", nextExecutionDelay.TotalSeconds);

                    try
                    {
                        // get a lock
                        handle = await semaphore.TryAcquireAsync(TimeSpan.FromSeconds(5), stoppingToken);

                        // wait in the lock
                        await Task.Delay(nextExecutionDelay, stoppingToken);
                        if (handle == null)
                        {
                            // no lock
                            Console.WriteLine("skipping run {0}", runNumber);
                            continue;
                        }
                        try
                        {
                            // do work
                            Console.WriteLine("executing run # {0}", runNumber);
                            await task.ExecuteAsync(stoppingToken);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("error in run # {0}: {1}", runNumber, e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("unhandled error in background job");
                    }
                    finally
                    {
                        nextExecutionDelay = CalculateNextExecutionDelay(DateTime.UtcNow);
                        // release the lock
                        if (handle != null) await handle.DisposeAsync();
                    }
                }
            }
        }

        private TimeSpan CalculateNextExecutionDelay(DateTime utcNow)
        {
            var nextDate = schedule.GetNextOccurrence(utcNow);
            if (nextDate == null) throw new InvalidOperationException("Cannot calculate the next execution date, stopping the background task");

            return nextDate.Value.Subtract(utcNow);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
