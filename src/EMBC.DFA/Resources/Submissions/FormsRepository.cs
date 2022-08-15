using System;
using System.Threading.Tasks;

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

        private Task<string> Handle(SubmitGovFormCommand f)
        {
            throw new NotImplementedException();
        }

        private Task<string> Handle(SubmitIndFormCommand f)
        {
            throw new NotImplementedException();
        }

        private async Task<string> Handle(SubmitSmbFormCommand f)
        {
            return await Task.FromResult("caseId");

            //throw new NotImplementedException();

            //var ctx = dfaContextFactory.Create();
            //var incident = Mapper.Map<incident>(cmd.Form);
            //incident.incidentid = Guid.NewGuid();

            //ctx.AddToincidents(incident);

            //await ctx.SaveChangesAsync();
            //ctx.DetachAll();

            //return incident.incidentid.ToString();
        }
    }
}
