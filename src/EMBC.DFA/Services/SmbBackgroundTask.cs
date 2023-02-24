using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMBC.DFA.Services
{
    public class SmbBackgroundTask : IBackgroundTask
    {

        public string Schedule => "0 */1 * * * *";

        public int DegreeOfParallelism => 1;

        public TimeSpan InitialDelay => TimeSpan.FromSeconds(5);

        public TimeSpan InactivityTimeout => TimeSpan.FromMinutes(5);

        public SmbBackgroundTask() { }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Test SMB Task");
            await Task.CompletedTask;
        }
    }
}
