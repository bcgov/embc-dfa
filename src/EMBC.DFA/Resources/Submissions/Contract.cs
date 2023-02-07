using System;
using System.Threading.Tasks;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Resources.Submissions
{
    public interface ISubmissionsRepository
    {
        Task<string> Manage(Command form);
        Task<string> Query();
    }

    public abstract class Command
    {
    }

    public class SubmitSmbFormCommand : Command
    {
        public SmbForm Form { get; set; } = null!;
    }

    public class SubmitIndFormCommand : Command
    {
        public IndForm Form { get; set; } = null!;
    }

    public class SubmitGovFormCommand : Command
    {
        public GovForm Form { get; set; } = null!;
    }
}
