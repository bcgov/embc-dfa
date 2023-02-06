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
            //ctx.AddTocontacts(contact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            //var addedContact = ctx.contacts.Where(c => c.firstname == contact.firstname && c.lastname == contact.lastname).FirstOrDefault();
            var incident = Mappings.Map(f.Form);
            ctx.AddToincidents(incident);
            //ctx.SetLink(incident, nameof(incident.customerid_contact), addedContact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return incident.incidentid.ToString();
        }

        private async Task<string> Handle(SubmitIndFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            var contact = Mappings.Map(f.Form.Applicant);
            //ctx.AddTocontacts(contact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            //var addedContact = ctx.contacts.Where(c => c.firstname == contact.firstname && c.lastname == contact.lastname).FirstOrDefault();
            var incident = Mappings.Map(f.Form);
            ctx.AddToincidents(incident);
            //ctx.SetLink(incident, nameof(incident.customerid_contact), addedContact);
            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return incident.incidentid.ToString();
        }

        private async Task<string> Handle(SubmitSmbFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            var application = Mappings.Map(f.Form);
            ctx.AddTodfa_appapplications(application);

            var applicant = application.dfa_Applicant;
            applicant.dfa_appcontactid = Guid.NewGuid();
            ctx.AddTodfa_appcontacts(applicant);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_Applicant), applicant);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_areacommunityid), ctx.LookupCommunityByName(f.Form.DamagePropertyAddress.City));

            foreach (var secondary in application.dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId)
            {
                ctx.AddTodfa_appsecondaryapplicants(secondary);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId), secondary);
                ctx.SetLink(secondary, nameof(dfa_appsecondaryapplicant.dfa_AppApplicationId), application);
            }

            foreach (var contact in application.dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId)
            {
                ctx.AddTodfa_appothercontacts(contact);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId), contact);
                ctx.SetLink(contact, nameof(dfa_appothercontact.dfa_AppApplicationId), application);
            }

            foreach (var log in application.dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId)
            {
                ctx.AddTodfa_appcleanuplogs(log);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId), log);
                //ctx.SetLink(log, nameof(dfa_appcleanuplog.dfa_AppApplicationId), application); //missing link?

                //add link to the log dfa_appcontact
                ctx.AddTodfa_appcontacts(log.dfa_contactid);
                ctx.SetLink(log, nameof(dfa_appcleanuplog.dfa_contactid), log.dfa_contactid);
            }

            foreach (var item in application.dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId)
            {
                ctx.AddTodfa_appdamageditems(item);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId), item);
                //ctx.SetLink(item, nameof(dfa_appdamageditem.dfa_AppApplicationId), application);  //missing link?
            }

            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return application.dfa_appapplicationid.ToString();
        }
    }
}
