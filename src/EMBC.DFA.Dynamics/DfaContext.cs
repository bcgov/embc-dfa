using System;
using System.Linq;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;

namespace EMBC.DFA.Dynamics
{
    public class DfaContext : Microsoft.Dynamics.CRM.System
    {
        public DfaContext(Uri serviceRoot) : base(serviceRoot)
        {
        }
    }

    public static class DfaContextExtensions
    {
        public static dfa_areacommunities? LookupCommunityByName(this DfaContext context, string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            return context.dfa_areacommunitieses.Where(c => c.dfa_name == name).FirstOrDefault();
        }
    }
}
