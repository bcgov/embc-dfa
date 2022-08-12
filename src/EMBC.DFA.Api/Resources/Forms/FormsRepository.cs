using AutoMapper;
using EMBC.DFA.Api.Dynamics;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;

namespace EMBC.DFA.Api.Resources.Forms
{
    public class FormsRepository : IFormsRepository
    {
        private readonly IMapper mapper;
        private readonly IDfaContextFactory dfaContextFactory;

        public FormsRepository(IMapper mapper, IDfaContextFactory dfaContextFactory)
        {
            this.mapper = mapper;
            this.dfaContextFactory = dfaContextFactory;
        }

        public async Task<ManageFormCommandResult> Manage(ManageFormCommand cmd)
        {
            return cmd switch
            {
                SubmitNewForm c => await HandleSubmitNewForm(c),
                _ => throw new NotSupportedException($"{cmd.GetType().Name} is not supported")
            };
        }

        public async Task<ManageFormCommandResult> HandleSubmitNewForm(SubmitNewForm cmd)
        {
            return await Task.FromResult(new ManageFormCommandResult { Id = "caseId" });

            //var ctx = dfaContextFactory.Create();
            //var incident = mapper.Map<incident>(cmd.Form);
            //incident.incidentid = Guid.NewGuid();

            //ctx.AddToincidents(incident);

            //await ctx.SaveChangesAsync();
            //ctx.DetachAll();

            //return new ManageFormCommandResult { Id = incident.incidentid.ToString() };
        }
    }
}
