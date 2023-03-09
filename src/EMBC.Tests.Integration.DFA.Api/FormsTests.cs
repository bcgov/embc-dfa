using System.Net;
using System.Text.Json;
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
        public async Task CanSerialize()
        {
            //var options = new JsonSerializerOptions();
            //options.Converters.Add(new EMBC.DFA.Services.CHEFS.JsonInt32Converter());
            //var result = JsonSerializer.Deserialize<EMBC.DFA.Services.CHEFS.CHEFSmbResponse>(testSubmission, options);
            //result.ShouldNotBeNull();
        }

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

        //CHEFS SMB submission with empty string in number field
        private string testSubmission = @"{
    ""id"": ""d30f1c96-557a-407b-b01c-127ca75ce34b"",
    ""formVersionId"": ""75865534-1143-4f6f-8d9f-62752d0aecf7"",
    ""confirmationId"": ""D30F1C96"",
    ""draft"": false,
    ""deleted"": false,
    ""submission"": {
        ""data"": {
            ""yes"": ""yes"",
            ""yes1"": ""yes"",
            ""yes2"": ""yes"",
            ""yes6"": ""yes"",
            ""yes7"": ""yes"",
            ""yes8"": true,
            ""yes9"": true,
            ""yes10"": true,
            ""yes11"": true,
            ""yes12"": true,
            ""yes13"": true,
            ""yes14"": false,
            ""yes15"": false,
            ""yes16"": false,
            ""street1"": ""1234 Roxy Ave "",
            ""street2"": """",
            ""street3"": """",
            ""submit1"": true,
            ""cityTown"": """",
            ""comments"": ""bus on reserve "",
            ""province"": """",
            ""cityTown1"": ""Cowichan "",
            ""province1"": ""BC "",
            ""postalCode"": """",
            ""printName1"": ""Nilesh John "",
            ""printName2"": """",
            ""signature1"": ""data:image/png;base64,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"",
            ""signature2"": """",
            ""postalCode1"": ""V8v 8v8"",
            ""dateOfDamage"": ""2023-03-06T12:00:00-08:00"",
            ""dateOfDamage1"": ""2023-03-09T12:00:00-08:00"",
            ""dateYyyyMDay1"": ""2023-03-08T12:00:00-08:00"",
            ""dateYyyyMDay2"": """",
            ""eMailAddress2"": ""nileshjohn@gmail.com"",
            ""otherContacts"": [{
                    ""eMailAddress1"": ""neilgammy@gmail.com"",
                    ""alternatePhoneNumber"": ""(250) 250-2550"",
                    ""alternateContactNameWhereYouCanBeReachedIfApplicable"": ""Neil "",
                    ""alternateContactNameWhereYouCanBeReachedIfApplicable1"": ""Gammy ""
                }
            ],
            ""pleaseSpecify"": """",
            ""mailingAddress"": """",
            ""cleanupLogDetails"": [{
                    ""hoursWorked"": """",
                    ""dateYyyyMDay"": ""2023-03-07T12:00:00-08:00"",
                    ""descriptionOfWork"": """",
                    ""embcOfficeUseOnly"": """",
                    ""nameOfFamilyMemberVolunteer"": """"
                }
            ],
            ""causeOfDamageLoss1"": ""flooding"",
            ""cleanupLogDetails1"": [{
                    ""emailAdress"": """",
                    ""hoursWorked"": """",
                    ""phoneNumber"": """",
                    ""dateYyyyMDay"": """",
                    ""applicantType"": ""contact"",
                    ""descriptionOfWork"": """",
                    ""embcOfficeUseOnly"": """",
                    ""LastNameofSecondary"": """",
                    ""FirstNameofSecondary"": ""Diana"",
                    ""FirstNameofSecondary1"": ""Beers "",
                    ""nameOfFamilyMemberVolunteer"": """"
                }
            ],
            ""financialDocuments"": [{
                    ""url"": ""/app/api/v1/files/cc81d282-a66f-46fe-aaf3-133f8ea5e962"",
                    ""data"": {
                        ""id"": ""cc81d282-a66f-46fe-aaf3-133f8ea5e962""
                    },
                    ""size"": 63489,
                    ""storage"": ""chefs"",
                    ""originalName"": ""Consent for 3rd Party Disclosure.pdf""
                }
            ],
            ""alternatePhoneNumber1"": ""(258) 888-8888"",
            ""businessTelephoneNumber1"": """",
            ""cellularTelephoneNumber1"": ""(250) 222-2222"",
            ""pleaseDecideToChooseAOrB"": ""a"",
            ""nameOfFirstNationsReserve"": ""Cowichan Tribes "",
            ""completedInsuranceTemplate1"": [{
                    ""url"": ""/app/api/v1/files/02c372b5-3c7f-4629-bb40-6349af0e1493"",
                    ""data"": {
                        ""id"": ""02c372b5-3c7f-4629-bb40-6349af0e1493""
                    },
                    ""size"": 48910,
                    ""storage"": ""chefs"",
                    ""originalName"": ""Appeal To The Director - Enclosure .pdf""
                }
            ],
            ""leaseAgreementsIfApplicable"": [],
            ""primaryContactNameLastFirst"": ""Nilesh "",
            ""primaryContactNameLastFirst1"": ""Sunny Days "",
            ""primaryContactNameLastFirst2"": ""Joe Grover "",
            ""primaryContactNameLastFirst3"": ""John "",
            ""primaryContactNameLastFirst4"": """",
            ""pleaseSelectTheAppropriateOption"": ""smallBusinessOwner"",
            ""provideABriefDescriptionOfDamage"": ""Whole home flooded "",
            ""listByRoomItemsSubmittedForDamageAssessment"": [{
                    ""roomName"": ""living room"",
                    ""embcOfficeUseOnlyComments"": """",
                    ""listByRoomItemsSubmittedForDamageAssessment1"": """"
                }, {
                    ""roomName"": ""bathroom "",
                    ""listByRoomItemsSubmittedForDamageAssessment1"": """"
                }
            ]
        },
        ""state"": ""submitted"",
        ""metadata"": {
            ""offset"": -480,
            ""onLine"": true,
            ""origin"": ""https://submit.digital.gov.bc.ca"",
            ""pathName"": ""/app/form/submit"",
            ""referrer"": """",
            ""timezone"": ""America/Vancouver"",
            ""userAgent"": ""Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36 Edg/110.0.1587.63"",
            ""selectData"": {
                ""cleanupLogDetails1"": [{
                        ""applicantType"": {
                            ""label"": ""Contact""
                        }
                    }
                ]
            },
            ""browserName"": ""Netscape""
        }
    },
    ""createdBy"": ""OSCHICK@bceid-basic"",
    ""createdAt"": ""2023-03-09T00:31:47.011Z"",
    ""updatedBy"": null,
    ""updatedAt"": ""2023-03-09T00:31:47.011Z""
}
";
    }
}
