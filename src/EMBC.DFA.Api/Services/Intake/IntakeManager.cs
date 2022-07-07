namespace EMBC.DFA.Api.Services.Intake
{
    public class IntakeManager : IIntakeManager
    {
        public Task<string> Handle(IntakeCommand cmd)
        {
            throw new NotImplementedException();
        }

        public Task<IntakeQueryResults> Handle(IntakeQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
