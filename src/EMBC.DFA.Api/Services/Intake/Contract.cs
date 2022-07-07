using EMBC.DFA.Api.Models;

namespace EMBC.DFA.Api.Services.Intake
{
    public interface IIntakeManager
    {
        Task<string> Handle(IntakeCommand cmd);

        Task<IntakeQueryResults> Handle(IntakeQuery query);
    }

    public abstract class IntakeCommand
    { }

    public abstract class IntakeQuery
    { }

    public abstract class IntakeQueryResults
    { }

    public class NewSmbFormSubmissionCommand : IntakeCommand
    {
        public SmbForm Form { get; set; } = null!;
    }

    public class NewGovFormSubmissionCommand : IntakeCommand
    {
        public GovForm Form { get; set; } = null!;
    }

    public class NewIndFormSubmissionCommand : IntakeCommand
    {
        public IndForm Form { get; set; } = null!;
    }
}
