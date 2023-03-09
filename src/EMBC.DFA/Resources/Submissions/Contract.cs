using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Resources.Submissions
{
    public interface ISubmissionsRepository
    {
        Task<string> Manage(Command form);
        Task<IEnumerable<string>> QueryConfirmationIdsByForm(FormType type);
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

    public enum FormType
    {
        SMB,
        IND,
        GOV
    }

    public enum DFATwoOptions
    {
        Yes = 222710000,
        No = 222710001
    }

    public class CRMQueryException : Exception
    {
        public CRMQueryException(string message) : base(message)
        {
        }
    }
}
