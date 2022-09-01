using System;
using System.Threading.Tasks;
using EMBC.DFA.Dynamics;
using EMBC.Utilities.Runtime;
using Microsoft.Extensions.DependencyInjection;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using System.Linq;

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
            var contact = Mappings.Map(f.Form.Applicant);
            ctx.AddTocontacts(contact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            var addedContact = ctx.contacts.Where(c => c.firstname == contact.firstname && c.lastname == contact.lastname).FirstOrDefault();
            var incident = Mappings.Map(f.Form);
            ctx.AddToincidents(incident);
            ctx.SetLink(incident, nameof(incident.customerid_contact), addedContact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return incident.incidentid.ToString();
        }

        private async Task<string> Handle(SubmitIndFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            var contact = Mappings.Map(f.Form.Applicant);
            ctx.AddTocontacts(contact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            var addedContact = ctx.contacts.Where(c => c.firstname == contact.firstname && c.lastname == contact.lastname).FirstOrDefault();
            var incident = Mappings.Map(f.Form);
            ctx.AddToincidents(incident);
            ctx.SetLink(incident, nameof(incident.customerid_contact), addedContact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return incident.incidentid.ToString();
        }

        private async Task<string> Handle(SubmitSmbFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            var contact = Mappings.Map(f.Form.Applicant);
            ctx.AddTocontacts(contact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            var addedContact = ctx.contacts.Where(c => c.firstname == contact.firstname && c.lastname == contact.lastname).FirstOrDefault();
            var incident = Mappings.Map(f.Form);
            ctx.AddToincidents(incident);
            ctx.SetLink(incident, nameof(incident.customerid_contact), addedContact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return incident.incidentid.ToString();
        }
    }
}
