namespace EMBC.DFA.Api.Services.Intake
{
    public class IntakeManager : IIntakeManager
    {
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
            return await Task.FromResult("smb");
        }

        private async Task<string> HandleSubmitGovForm(NewGovFormSubmissionCommand cmd)
        {
            return await Task.FromResult("gov");
        }

        private async Task<string> HandleSubmitIndForm(NewIndFormSubmissionCommand cmd)
        {
            return await Task.FromResult("ind");
        }
    }
}
