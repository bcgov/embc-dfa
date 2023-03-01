using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EMBC.DFA.Managers.Intake;
using EMBC.DFA.Resources.Submissions;
using EMBC.DFA.Services.CHEFS;

namespace EMBC.DFA.Services
{
    public class GovBackgroundTask : IBackgroundTask
    {
        private readonly ICHEFSAPIService _chefsAPI;
        private readonly ISubmissionsRepository _submissionsRepository;
        private readonly IIntakeManager _intakeManager;

        public string Schedule => "0 10-59/15 * * * *"; //Every 15 minutes, staggered by 10 minutes
        public TimeSpan InitialDelay => TimeSpan.FromSeconds(30);

        //public string Schedule => "40 * * * * *"; //every minute on second 20
        //public TimeSpan InitialDelay => TimeSpan.FromSeconds(3);

        public int DegreeOfParallelism => 1;

        public TimeSpan InactivityTimeout => TimeSpan.FromMinutes(5);

        public GovBackgroundTask(ICHEFSAPIService chefsAPI, ISubmissionsRepository submissionsRepository, IIntakeManager intakeManager)
        {
            _chefsAPI = chefsAPI;
            _submissionsRepository = submissionsRepository;
            _intakeManager = intakeManager;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var submissions = await _chefsAPI.GetGovSubmissions();
            var existingConfirmationIds = (await _submissionsRepository.QueryConfirmationIdsByForm(FormType.GOV)).ToList();
            var newSubmissions = submissions.Where(s => !existingConfirmationIds.Any(id => !string.IsNullOrEmpty(id) && id.Equals(s.ConfirmationId, StringComparison.OrdinalIgnoreCase))).ToList();
            foreach (var submission in newSubmissions)
            {
                var submissionId = await _intakeManager.Handle(new NewGovFormSubmissionCommand { Form = Mappings.Map(submission) });
            }

            if (newSubmissions.Count() > 0) Console.WriteLine($"Successfully created {newSubmissions.Count()} GOV Applications");
            else Console.WriteLine($"No new GOV Applications");
        }
    }
}
