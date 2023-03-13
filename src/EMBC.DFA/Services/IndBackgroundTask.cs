using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EMBC.DFA.Managers.Intake;
using EMBC.DFA.Resources.Submissions;
using EMBC.DFA.Services.CHEFS;
using Serilog;

namespace EMBC.DFA.Services
{
    public class IndBackgroundTask : IBackgroundTask
    {
        private readonly ICHEFSAPIService _chefsAPI;
        private readonly ISubmissionsRepository _submissionsRepository;
        private readonly IIntakeManager _intakeManager;

        public string Schedule => "0 5-59/15 * * * *"; //Every 15 minutes, staggered by 5 minutes
        public TimeSpan InitialDelay => TimeSpan.FromSeconds(30);

        public string TestSchedule => "20 * * * * *"; //every minute on second 20
        public TimeSpan FastDelay => TimeSpan.FromSeconds(3);

        public int DegreeOfParallelism => 1;

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
            var existingConfirmationIds = (await _submissionsRepository.QueryConfirmationIdsByForm(FormType.IND)).ToList();
            var newSubmissions = submissions.Where(s => !existingConfirmationIds.Any(id => !string.IsNullOrEmpty(id) && id.Equals(s.ConfirmationId, StringComparison.OrdinalIgnoreCase))).ToList();
            var count = 0;
            foreach (var submission in newSubmissions)
            {
                try
                {
                    var submissionId = await _intakeManager.Handle(new NewIndFormSubmissionCommand { Form = Mappings.Map(submission) });
                    ++count;
                }
                catch (Exception ex)
                {
                    Log.Error($"Error in IND submission {submission.ConfirmationId}: " + ex.Message);
                }
            }

            if (count > 0) Log.Information($"Successfully created {count} IND Applications");
            else Log.Information($"No new IND Applications");
        }
    }
}
