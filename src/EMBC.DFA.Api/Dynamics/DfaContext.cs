namespace EMBC.DFA.Api.Dynamics
{
    public class DfaContext : DFA.Dynamics.Microsoft.Dynamics.CRM.System
    {
        public DfaContext(Uri serviceRoot) : base(serviceRoot)
        {
        }
    }
}
