using System;
using System.Threading.Tasks;
using EMBC.DFA.Dynamics;
using EMBC.Utilities.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace EMBC.DFA.Resources.Submissions
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        //private readonly IDfaContextFactory dfaContextFactory;

        public SubmissionsRepository()
        {
            //this.dfaContextFactory = dfaContextFactory;
        }

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
            return await Task.FromResult("caseId");
        }

        private async Task<string> Handle(SubmitIndFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            return await Task.FromResult("caseId");
        }

        private async Task<string> Handle(SubmitSmbFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();

            //throw new NotImplementedException();

            //var ctx = dfaContextFactory.Create();
            //var incident = Mapper.Map<incident>(cmd.Form);
            //incident.incidentid = Guid.NewGuid();

            //ctx.AddToincidents(incident);

            //await ctx.SaveChangesAsync();
            //ctx.DetachAll();

            //return incident.incidentid.ToString();
            return await Task.FromResult("caseId");
        }
    }
}
