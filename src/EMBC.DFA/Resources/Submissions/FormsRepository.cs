using System;
using System.Threading.Tasks;
using EMBC.DFA.Dynamics;
using EMBC.Utilities.Runtime;
using Microsoft.Extensions.DependencyInjection;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;

namespace EMBC.DFA.Resources.Submissions
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        public async Task<string> Manage(Command form) => await (form switch
        {
            SubmitGovFormCommand f => Handle(f),
            SubmitIndFormCommand f => Handle(f),
            SubmitSmbFormCommand f => Handle(f),
            _ => throw new NotImplementedException($"type {form.GetType().FullName}")
        });

        private async Task<string> Handle(SubmitGovFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();

            var incident = Mappings.Map(f.Form);
            incident.incidentid = Guid.NewGuid();
            ctx.AddToincidents(incident);
            //await ctx.SaveChangesAsync();
            //ctx.DetachAll();

            return await Task.FromResult("caseId");
        }

        private async Task<string> Handle(SubmitIndFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();

            var incident = Mappings.Map(f.Form);
            incident.incidentid = Guid.NewGuid();
            ctx.AddToincidents(incident);
            //await ctx.SaveChangesAsync();
            //ctx.DetachAll();

            return await Task.FromResult("caseId");
        }

        private async Task<string> Handle(SubmitSmbFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();

            var incident = Mappings.Map(f.Form);
            incident.incidentid = Guid.NewGuid();
            ctx.AddToincidents(incident);
            //await ctx.SaveChangesAsync();
            //ctx.DetachAll();

            //return incident.incidentid.ToString();
            return await Task.FromResult("caseId");
        }
    }
}
