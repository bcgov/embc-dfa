using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Resources.Submissions
{
    public static class Mappings
    {
        public static incident Map(GovForm form)
        {
            return new incident();
        }
    }
}
