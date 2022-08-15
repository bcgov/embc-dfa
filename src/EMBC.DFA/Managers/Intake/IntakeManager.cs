using EMBC.DFA.Resources.Submissions;
using System;
using System.Threading.Tasks;

namespace EMBC.DFA.Managers.Intake
{
    public class IntakeManager : IIntakeManager
    {
        private readonly ISubmissionsRepository submissionsRepository;

        public IntakeManager(ISubmissionsRepository submissionsRepository)
        {
            this.submissionsRepository = submissionsRepository;
        }

        public async Task<string> Handle(IntakeCommand cmd)
        {
            return cmd switch
            {
                NewSmbFormSubmissionCommand c => await Handle(c),
                NewGovFormSubmissionCommand c => await Handle(c),
                NewIndFormSubmissionCommand c => await Handle(c),
                _ => throw new NotSupportedException($"{cmd.GetType().Name} is not supported")
            };
        }

        public Task<IntakeQueryResults> Handle(IntakeQuery query)
        {
            throw new NotImplementedException();
        }

        private async Task<string> Handle(NewSmbFormSubmissionCommand cmd)
        {
            //add any validations


            var caseId = await submissionsRepository.Manage(new SubmitSmbFormCommand { Form = cmd.Form });
            return await Task.FromResult(caseId);
        }

        private async Task<string> Handle(NewGovFormSubmissionCommand cmd)
        {
            var caseId = await submissionsRepository.Manage(new SubmitGovFormCommand { Form = cmd.Form });
            return await Task.FromResult(caseId);
        }

        private async Task<string> Handle(NewIndFormSubmissionCommand cmd)
        {
            var caseId = await submissionsRepository.Manage(new SubmitIndFormCommand { Form = cmd.Form });
            return await Task.FromResult(caseId);
        }
    }
}
