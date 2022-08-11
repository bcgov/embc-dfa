using AutoMapper;
using EMBC.DFA.Api.Resources.Forms;

namespace EMBC.DFA.Api.Services.Intake
{
    public class IntakeManager : IIntakeManager
    {
        private readonly IMapper mapper;
        private readonly IFormsRepository formsRepository;

        public IntakeManager (IMapper mapper, IFormsRepository formsRepository)
        {
            this.mapper = mapper;
            this.formsRepository = formsRepository;
        }

        public async Task<string> Handle(IntakeCommand cmd)
        {
            return cmd switch
            {
                NewSmbFormSubmissionCommand c => await HandleSubmitSmbForm(c),
                NewGovFormSubmissionCommand c => await HandleSubmitGovForm(c),
                NewIndFormSubmissionCommand c => await HandleSubmitIndForm(c),
                _ => throw new NotSupportedException($"{cmd.GetType().Name} is not supported")
            };
        }

        public Task<IntakeQueryResults> Handle(IntakeQuery query)
        {
            throw new NotImplementedException();
        }

        private async Task<string> HandleSubmitSmbForm(NewSmbFormSubmissionCommand cmd)
        {
            //add any validations

            var smbForm = mapper.Map<Form>(cmd.Form.data);

            var caseId = (await formsRepository.Manage(new SubmitNewForm { Form = smbForm })).Id;
            return await Task.FromResult(caseId);
        }

        private async Task<string> HandleSubmitGovForm(NewGovFormSubmissionCommand cmd)
        {
            var govForm = mapper.Map<Form>(cmd.Form.data);

            var caseId = (await formsRepository.Manage(new SubmitNewForm { Form = govForm })).Id;
            return await Task.FromResult(caseId);
        }

        private async Task<string> HandleSubmitIndForm(NewIndFormSubmissionCommand cmd)
        {
            var indForm = mapper.Map<Form>(cmd.Form.data);

            var caseId = (await formsRepository.Manage(new SubmitNewForm { Form = indForm })).Id;
            return await Task.FromResult(caseId);
        }
    }
}
