using System;

namespace EMBC.DFA.Dynamics
{
    public class DfaContext : Microsoft.Dynamics.CRM.System
    {
        public DfaContext(Uri serviceRoot) : base(serviceRoot)
        {
        }
    }
}
