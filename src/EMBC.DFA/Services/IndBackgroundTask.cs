using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EMBC.DFA.Managers.Intake;
using EMBC.DFA.Resources.Submissions;
using EMBC.DFA.Services.CHEFS;

namespace EMBC.DFA.Services
{
    public class IndBackgroundTask : IBackgroundTask
    {
        private readonly ICHEFSAPIService _chefsAPI;
        private readonly ISubmissionsRepository _submissionsRepository;
        private readonly IIntakeManager _intakeManager;

        public string Schedule => "0 5-59/15 * * * *";
        //public string Schedule => "20 * * * * *"; //every minute on second 20

        public int DegreeOfParallelism => 1;

        public TimeSpan InitialDelay => TimeSpan.FromSeconds(30);

        public TimeSpan InactivityTimeout => TimeSpan.FromMinutes(5);

        public IndBackgroundTask(ICHEFSAPIService chefsAPI, ISubmissionsRepository submissionsRepository, IIntakeManager intakeManager)
        {
            _chefsAPI = chefsAPI;
            _submissionsRepository = submissionsRepository;
            _intakeManager = intakeManager;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var submissions = await _chefsAPI.GetIndSubmissions();
            var existingConfirmationIds = await _submissionsRepository.QueryConfirmationIdsByForm(FormType.IND);
            var newSubmissions = submissions.Where(s => !existingConfirmationIds.Any(id => !string.IsNullOrEmpty(id) && id.Equals(s.ConfirmationId, StringComparison.OrdinalIgnoreCase))).ToList();
            foreach (var submission in newSubmissions)
            {
                var submissionId = await _intakeManager.Handle(new NewIndFormSubmissionCommand { Form = Mappings.Map(submission) });
            }

            if (newSubmissions.Count() > 0) Console.WriteLine($"Successfully created {newSubmissions.Count()} IND Applications");
            else Console.WriteLine($"No new IND Applications");
        }
    }
}
