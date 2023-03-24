using System;
using System.Threading.Tasks;
using EMBC.DFA.Dynamics;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using static EMBC.DFA.Resources.Submissions.Mappings;
using System.Threading;

namespace EMBC.DFA.Resources.Submissions
{
    public class SubmissionsRepository : ISubmissionsRepository
    {
        private readonly IDfaContextFactory dfaContextFactory;
        private readonly IConfiguration configuration;

        public SubmissionsRepository(IDfaContextFactory dfaContextFactory, IConfiguration configuration)
        {
            this.dfaContextFactory = dfaContextFactory;
            this.configuration = configuration;
        }

        public async Task<string> Manage(Command form) => await (form switch
        {
            SubmitSmbFormCommand f => Handle(f),
            SubmitIndFormCommand f => Handle(f),
            SubmitGovFormCommand f => Handle(f),
            _ => throw new NotImplementedException($"type {form.GetType().FullName}")
        });

        public async Task<IEnumerable<string>> QueryConfirmationIdsByForm(FormType type)
        {
            try
            {
                var ct = new CancellationTokenSource().Token;
                var ctx = dfaContextFactory.Create();
                var query = ctx.dfa_appapplications.AsQueryable();
                switch (type)
                {
                    case FormType.SMB:
                        {
                            query = query.Where(app => app.dfa_applicanttype == (int)ApplicantTypeOptionSet.SmallBusinessOwner ||
                            app.dfa_applicanttype == (int)ApplicantTypeOptionSet.FarmOwner ||
                            app.dfa_applicanttype == (int)ApplicantTypeOptionSet.CharitableOrganization);
                            break;
                        }
                    case FormType.IND:
                        {
                            query = query.Where(app => app.dfa_applicanttype == (int)ApplicantTypeOptionSet.HomeOwner ||
                            app.dfa_applicanttype == (int)ApplicantTypeOptionSet.ResidentialTenant);
                            break;
                        }
                    case FormType.GOV:
                        {
                            query = query.Where(app => app.dfa_applicanttype == (int)ApplicantTypeOptionSet.GovernmentBody);
                            break;
                        }
                    default:
                        {
                            //SMB - should not happen!
                            query = query.Where(app => app.dfa_applicanttype == (int)ApplicantTypeOptionSet.SmallBusinessOwner ||
                            app.dfa_applicanttype == (int)ApplicantTypeOptionSet.FarmOwner ||
                            app.dfa_applicanttype == (int)ApplicantTypeOptionSet.CharitableOrganization);
                            break;
                        }
                }
                return (await query.GetAllPagesAsync(ct)).Select(app => app.dfa_chefconfirmationnumber);
            }
            catch (Exception ex)
            {
                throw new CRMQueryException($"Error while querying CRM Confirmation IDs for type {type.ToString()}");
            }
        }

        private async Task<string> Handle(SubmitGovFormCommand f)
        {
            var ctx = dfaContextFactory.Create();
            var application = Mappings.Map(f.Form);
            application.dfa_appapplicationid = Guid.NewGuid();
            ctx.AddTodfa_appapplications(application);

            var applicant = application.dfa_Applicant;
            applicant.dfa_appcontactid = Guid.NewGuid();
            ctx.AddTodfa_appcontacts(applicant);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_Applicant), applicant);

            AddSecondaryApplicants(ctx, application);

            await ctx.SaveChangesAsync();
            ctx.DetachAll();

            return application.dfa_appapplicationid.ToString();
        }

        private async Task<string> Handle(SubmitIndFormCommand f)
        {
            var ctx = dfaContextFactory.Create();
            var application = Mappings.Map(f.Form);
            application.dfa_appapplicationid = Guid.NewGuid();
            ctx.AddTodfa_appapplications(application);

            var applicant = application.dfa_Applicant;
            applicant.dfa_appcontactid = Guid.NewGuid();
            ctx.AddTodfa_appcontacts(applicant);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_Applicant), applicant);

            var buildingOwner = application.dfa_BuildingOwnerLandlord;
            buildingOwner.dfa_appbuildingownerlandlordid = Guid.NewGuid();
            ctx.AddTodfa_appbuildingownerlandlords(buildingOwner);
            ctx.SetLink(application, nameof(dfa_appapplication.dfa_BuildingOwnerLandlord), buildingOwner);

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
            var ctx = dfaContextFactory.Create();
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
            var chefApiBaseUri = configuration.GetValue<string>("CHEFS_API_BASE_URI", string.Empty) + "/app/form/view?s=";
            foreach (var doc in application.dfa_appapplication_dfa_appdocumentlocations_ApplicationId)
            {
                doc.dfa_url = chefApiBaseUri + doc.dfa_url;
                ctx.AddTodfa_appdocumentlocationses(doc);
                ctx.AddLink(application, nameof(dfa_appapplication.dfa_appapplication_dfa_appdocumentlocations_ApplicationId), doc);
            }
        }
    }
}
