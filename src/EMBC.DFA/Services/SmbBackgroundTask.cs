using System;
using System.Threading;
using System.Threading.Tasks;
using EMBC.DFA.Services.CHEFS;

namespace EMBC.DFA.Services
{
    public class SmbBackgroundTask : IBackgroundTask
    {
        private readonly ICHEFSAPIService _chefsAPI;

        public string Schedule => "0 */1 * * * *";

        public int DegreeOfParallelism => 1;

        public TimeSpan InitialDelay => TimeSpan.FromSeconds(5);

        public TimeSpan InactivityTimeout => TimeSpan.FromMinutes(5);

        public SmbBackgroundTask(ICHEFSAPIService chefsAPI)
        {
            _chefsAPI = chefsAPI;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var submissions = await _chefsAPI.GetSmbSubmissions();
            foreach (var submission in submissions)
            {
                Console.WriteLine(submission.SubmissionId);
            }
        }
    }
}
