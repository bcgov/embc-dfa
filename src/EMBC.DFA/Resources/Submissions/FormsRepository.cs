﻿using System;
using System.Threading.Tasks;
using EMBC.DFA.Dynamics;
using EMBC.Utilities.Runtime;
using Microsoft.Extensions.DependencyInjection;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace EMBC.DFA.Resources.Submissions
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        private readonly IConfiguration configuration;

        public SubmissionsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> Manage(Command form) => await (form switch
        {
            SubmitGovFormCommand f => Handle(f),
            SubmitIndFormCommand f => Handle(f),
            SubmitSmbFormCommand f => Handle(f),
            _ => throw new NotImplementedException($"type {form.GetType().FullName}")
        });

        public async Task<string> Query()
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            var res = ctx.contacts.FirstOrDefault();
            return await Task.FromResult(res.contactid.ToString());
        }

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
            var application = Mappings.Map(f.Form);
            application.dfa_appapplicationid = Guid.NewGuid();
            ctx.AddTodfa_appapplications(application);

            var applicant = application.dfa_Applicant;
            applicant.dfa_appcontactid = Guid.NewGuid();
            ctx.AddTodfa_appcontacts(applicant);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_Applicant), applicant);

            AddSecondaryApplicants(ctx, application);
            AddOtherContacts(ctx, application);
            AddOccupants(ctx, application);
            AddCleanUpLogs(ctx, application);
            AddDamagedItems(ctx, application);
            AddDocuments(ctx, application);

            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return application.dfa_appapplicationid.ToString();
        }

        private async Task<string> Handle(SubmitSmbFormCommand f)
        {
            var ctx = CallContext.Current.Services.GetRequiredService<IDfaContextFactory>().Create();
            var application = Mappings.Map(f.Form);
            application.dfa_appapplicationid = Guid.NewGuid();
            ctx.AddTodfa_appapplications(application);

            var applicant = application.dfa_Applicant;
            applicant.dfa_appcontactid = Guid.NewGuid();
            ctx.AddTodfa_appcontacts(applicant);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_Applicant), applicant);

            AddSecondaryApplicants(ctx, application);
            AddOtherContacts(ctx, application);
            AddCleanUpLogs(ctx, application);
            AddDamagedItems(ctx, application);
            AddDocuments(ctx, application);

            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return application.dfa_appapplicationid.ToString();
        }

        private void AddSecondaryApplicants(DfaContext ctx, dfa_appapplication application)
        {
            foreach (var secondary in application.dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId)
            {
                ctx.AddTodfa_appsecondaryapplicants(secondary);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId), secondary);
                ctx.SetLink(secondary, nameof(dfa_appsecondaryapplicant.dfa_AppApplicationId), application);
            }
        }

        private void AddOtherContacts(DfaContext ctx, dfa_appapplication application)
        {
            foreach (var contact in application.dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId)
            {
                ctx.AddTodfa_appothercontacts(contact);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId), contact);
                ctx.SetLink(contact, nameof(dfa_appothercontact.dfa_AppApplicationId), application);
            }
        }

        private void AddOccupants(DfaContext ctx, dfa_appapplication application)
        {
            foreach (var occupant in application.dfa_dfa_appapplication_dfa_appoccupant_ApplicationId)
            {
                ctx.AddTodfa_appoccupants(occupant);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appoccupant_ApplicationId), occupant);
                //ctx.SetLink(occupant, nameof(dfa_appoccupant.dfa_appa), application);

                ctx.AddTodfa_appcontacts(occupant.dfa_ContactId);
                ctx.SetLink(occupant, nameof(dfa_appoccupant.dfa_ContactId), occupant.dfa_ContactId);
            }
        }

        private void AddCleanUpLogs(DfaContext ctx, dfa_appapplication application)
        {
            foreach (var log in application.dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId)
            {
                ctx.AddTodfa_appcleanuplogs(log);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId), log);

                ctx.AddTodfa_appcontacts(log.dfa_contactid);
                ctx.SetLink(log, nameof(dfa_appcleanuplog.dfa_contactid), log.dfa_contactid);
            }
        }

        private void AddDamagedItems(DfaContext ctx, dfa_appapplication application)
        {
            foreach (var item in application.dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId)
            {
                ctx.AddTodfa_appdamageditems(item);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId), item);
            }
        }

        private void AddDocuments(DfaContext ctx, dfa_appapplication application)
        {
            var chefApiBaseUri = configuration.GetValue<string>("CHEFS_API_BASE_URI", string.Empty);
            foreach (var doc in application.dfa_appapplication_dfa_appdocumentlocations_ApplicationId)
            {
                doc.dfa_url = chefApiBaseUri + doc.dfa_url;
                ctx.AddTodfa_appdocumentlocationses(doc);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_appapplication_dfa_appdocumentlocations_ApplicationId), doc);
            }
        }
    }
}
