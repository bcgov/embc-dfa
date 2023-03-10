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
using Serilog;

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
                    Log.Information("starting {0}: initial delay {1}, schedule: {2}, parallelism: {3}", typeof(T).Name, this.startupDelay, this.schedule.ToString(), task.DegreeOfParallelism);
                }
                else
                {
                    Log.Information($"background task is disabled, check configuration flag 'backgroundTask:{typeof(T).Name}'");
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

                    Log.Information("next {0} run in {1}s", typeof(T).Name, nextExecutionDelay.TotalSeconds);

                    try
                    {
                        // get a lock
                        handle = await semaphore.TryAcquireAsync(TimeSpan.FromSeconds(5), stoppingToken);

                        // wait in the lock
                        await Task.Delay(nextExecutionDelay, stoppingToken);
                        if (handle == null)
                        {
                            // no lock
                            Log.Information("skipping {0} run {1}", typeof(T).Name, runNumber);
                            continue;
                        }
                        try
                        {
                            // do work
                            Log.Information("executing {0} run # {1}", typeof(T).Name, runNumber);
                            await task.ExecuteAsync(stoppingToken);
                        }
                        catch (Exception e)
                        {
                            Log.Error("error in {0} run # {1}: {2}", typeof(T).Name, runNumber, e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error("unhandled error in {0}: {1}", typeof(T).Name, e.Message);
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
