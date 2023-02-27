using System;
using System.Threading;
using System.Threading.Tasks;
using EMBC.DFA.Managers.Intake;
using EMBC.DFA.Resources.Submissions;
using EMBC.DFA.Services.CHEFS;

namespace EMBC.DFA.Services
{
    public class SmbBackgroundTask : IBackgroundTask
    {
        private readonly ICHEFSAPIService _chefsAPI;
        private readonly ISubmissionsRepository _submissionsRepository;
        private readonly IIntakeManager _intakeManager;

        public string Schedule => "0 */1 * * * *";

        public int DegreeOfParallelism => 1;

        public TimeSpan InitialDelay => TimeSpan.FromSeconds(5);

        public TimeSpan InactivityTimeout => TimeSpan.FromMinutes(5);

        public SmbBackgroundTask(ICHEFSAPIService chefsAPI, ISubmissionsRepository submissionsRepository, IIntakeManager intakeManager)
        {
            _chefsAPI = chefsAPI;
            _submissionsRepository = submissionsRepository;
            _intakeManager = intakeManager;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var submissions = await _chefsAPI.GetSmbSubmissions();
            var existingSubmissions = await _submissionsRepository.QuerySubmissionIdsByForm(FormType.SMB);
            foreach (var submission in submissions)
            {
                Console.WriteLine(submission.SubmissionId);
            }
        }
    }
}
