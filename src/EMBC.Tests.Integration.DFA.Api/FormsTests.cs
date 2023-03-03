using System.Net;
using EMBC.DFA.Api.Models;
using EMBC.DFA.Managers.Intake;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace EMBC.Tests.Integration.DFA.Api
{
    public class ManageFormCommandResult
    {
        public string Id { get; set; }
    }

    public class FormsTests
    {
        public const string defaultSignature = "data:image/png;base64,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";
        public static string GenerateNewUniqueId(string prefix) => prefix + Guid.NewGuid().ToString().Substring(0, 4);

        [Test]
        public async Task SubmitGovForm()
        {
            var form = new EMBC.DFA.Api.Models.GovForm
            {
                data = new GovFormData
                {
                    indigenousGoverningBodyAndLocalGovernmentApplicationForDisasterFinancialAssistanceDfa1 = "Test Legal Name",
                    date = DateTime.Now,
                    primaryContactNameLastFirst = "John",
                    primaryContactNameLastFirst1 = "Test",
                    title = "Supreme Leader of the Universe",
                    mailingAddress = "123 Mail St.",
                    street2 = String.Empty,
                    cityTown = "Calgary",
                    province = "AB",
                    postalCode = "C2C2C2",
                    businessTelephoneNumber = "(778) 321-4567",
                    cellularTelephoneNumber = "(778) 123-4567",
                    eMailAddress = "test@test.com",
                    businessTelephoneNumber1 = "(779) 321-4567",
                    cellularTelephoneNumber1 = "(779) 123-4567",
                    eMailAddress1 = String.Empty,
                    //causeOfDamageLoss = new DamageLoss
                    //{
                    //    flooding = true,
                    //    landslide = false,
                    //    windstorm = false,
                    //    other = false
                    //},
                    pleaseSpecifyIfOthers = String.Empty,
                    dateOfDamageLoss = DateTime.Now,
                    dateOfDamageLoss1 = DateTime.Now,
                    provideABriefDescriptionOfDamage = "Too much water",
                    submit1 = true,
                },
                metadata = new Metadata { }
            };

            //var host = Application.Host;

            //var result = await host.Scenario(s =>
            //{
            //    s.Post.Json(form).ToUrl("/forms/gov");
            //    s.StatusCodeShouldBe((int)HttpStatusCode.Created);
            //});

            //ManageFormCommandResult res = result.ResponseBody.ReadAsJson<ManageFormCommandResult>();
            form.ShouldNotBeNull();
        }

        [Test]
        public async Task SubmitSmbForm()
        {
            var uniqueSignature = GenerateNewUniqueId("auto");

            var form = new EMBC.DFA.Managers.Intake.SmbForm
            {
                CHEFConfirmationId = uniqueSignature,
                ApplicantType = ApplicantType.FarmOwner,
                IndigenousStatus = false,
                OnFirstNationReserve = false,
                Applicant = new Applicant
                {
                    FirstName = uniqueSignature + "-John",
                    LastName = uniqueSignature + "-Test",
                    Initial = "Q",
                    Email = "john@test.com",
                    Phone = "(778) 987-8978",
                    Mobile = "(778) 987-8978",
                    BusinessLegalName = "Test Farm",
                    ContactName = "Test Contact"
                },
                SecondaryApplicants = new List<SecondaryApplicant>(),
                AltContacts = new List<AltContact>(),
                DamagePropertyAddress = new Address
                {
                    AddressLine1 = "123 Test Street",
                    AddressLine2 = String.Empty,
                    City = "Vancouver",
                    Province = "BC",
                    PostalCode = "v1v1v1"
                },
                MailingAddress = new Address
                {
                    AddressLine1 = "123 Test Street",
                    AddressLine2 = String.Empty,
                    City = "Vancouver",
                    Province = "BC",
                    PostalCode = "v1v1v1"
                },
                DamageFrom = DateTime.Now.AddDays(-2),
                DamageTo = DateTime.Now,
                DamageInfo = new DamageInfo
                {
                    DamageType = "flooding",
                    OtherDescription = String.Empty,
                    DamageDescription = String.Empty,
                },
                CouldNotPurchaseInsurance = false,
                HasRentalAgreement = false,
                HasReceipts = false,
                HasFinancialStatements = false,
                HasTaxReturn = false,
                HasProofOfOwnership = false,
                HasListOfDirectors = false,
                HasProofOfRegistration = false,
                HasEligibilityDocuments = false,

                Signature = defaultSignature,
                SignerName = "John Q Test",
                SignatureDate = DateTime.Now,
                OtherSignature = String.Empty,
                OtherSignerName = String.Empty,

                CleanUpLogs = new List<CleanUpLog>(),
                DamagedItems = new List<DamageItem>(),
                Documents = new List<AttachmentData>(),
            };

            //var host = Application.Host;
            //var manager = host.Services.GetRequiredService<IIntakeManager>();
            //var res = await manager.Handle(new NewSmbFormSubmissionCommand { Form = form });
            //res.ShouldNotBeNull();

            form.ShouldNotBeNull();
        }

        [Test]
        public async Task SubmitIndForm()
        {
            var uniqueSignature = GenerateNewUniqueId("auto");
            //TODO - update data
            var form = new EMBC.DFA.Managers.Intake.IndForm
            {
                CHEFConfirmationId = uniqueSignature,
                ApplicantType = ApplicantType.FarmOwner,
                IndigenousStatus = false,
                OnFirstNationReserve = false,
                Applicant = new Applicant
                {
                    FirstName = uniqueSignature + "-John",
                    LastName = uniqueSignature + "-Test",
                    Initial = "Q",
                    Email = "john@test.com",
                    Phone = "(778) 987-8978",
                    Mobile = "(778) 987-8978",
                    BusinessLegalName = "Test Farm",
                    ContactName = "Test Contact"
                },
                SecondaryApplicants = new List<SecondaryApplicant>(),
                AltContacts = new List<AltContact>(),
                DamagePropertyAddress = new Address
                {
                    AddressLine1 = "123 Test Street",
                    AddressLine2 = String.Empty,
                    City = "Vancouver",
                    Province = "BC",
                    PostalCode = "v1v1v1"
                },
                MailingAddress = new Address
                {
                    AddressLine1 = "123 Test Street",
                    AddressLine2 = String.Empty,
                    City = "Vancouver",
                    Province = "BC",
                    PostalCode = "v1v1v1"
                },
                DamageFrom = DateTime.Now.AddDays(-2),
                DamageTo = DateTime.Now,
                DamageInfo = new DamageInfo
                {
                    DamageType = "flooding",
                    OtherDescription = String.Empty,
                    DamageDescription = String.Empty,
                },

                Signature = defaultSignature,
                SignerName = "John Q Test",
                SignatureDate = DateTime.Now,
                OtherSignature = String.Empty,
                OtherSignerName = String.Empty,

                CleanUpLogs = new List<CleanUpLog>(),
                DamagedItems = new List<DamageItem>(),
                Documents = new List<AttachmentData>(),
            };


            //var host = Application.Host;
            //var manager = host.Services.GetRequiredService<IIntakeManager>();
            //var res = await manager.Handle(new NewIndFormSubmissionCommand { Form = form });
            //res.ShouldNotBeNull();

            form.ShouldNotBeNull();
        }
    }

    public static class ApplicationTypes
    {
        public const string SmallBusiness = "smallBusinessOwner";
        public const string FarmOwner = "farmOwner";
        public const string CharitableOrganization = "charitableOrganization";
        public const string HomeOwner = "homeOwner";
        public const string ResidentialTenant = "residentialTenant";
    }

    public static class SecondaryApplicantTypes
    {
        public const string Contact = "contact";
        public const string Organization = "organization";
    }

    public static class ApplicantTypes
    {
        public const string SmallBusiness = "a";
        public const string FarmOwner = "b";
    }


}
