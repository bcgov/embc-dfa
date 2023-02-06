using EMBC.DFA.Api.Models;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Api
{
    public static partial class Mappings
    {
        public static EMBC.DFA.Managers.Intake.SmbForm Map(Models.SmbForm payload)
        {
            var source = payload.data;
            var ret = new EMBC.DFA.Managers.Intake.SmbForm
            {
                ApplicantType = (ApplicantType)Enum.Parse(typeof(ApplicantType), source.pleaseSelectTheAppropriateOption, true),
                IndigenousStatus = source.yes7 == "yes",
                OnFirstNationReserve = source.yes6 == "yes",
                NameOfFirstNationsReserve = source.nameOfFirstNationsReserve,
                FirstNationsComments = source.comments,

                Applicant = new Applicant
                {
                    FirstName = source.primaryContactNameLastFirst,
                    LastName = source.primaryContactNameLastFirst3,
                    Initial = source.primaryContactNameLastFirst4,
                    Email = source.eMailAddress2,
                    Phone = source.businessTelephoneNumber1,
                    Mobile = source.cellularTelephoneNumber1,
                    AlternatePhone = source.alternatePhoneNumber1,
                    BusinessLegalName = source.primaryContactNameLastFirst1,
                    ContactName = source.primaryContactNameLastFirst2,
                },

                SecondaryApplicants = new List<OtherApplicant>(),
                AltContacts = new List<AltContact>(),

                DamagePropertyAddress = new Address
                {
                    AddressLine1 = source.street1,
                    AddressLine2 = source.street3,
                    City = source.cityTown1,
                    Province = source.province1,
                    PostalCode = source.postalCode1
                },

                MailingAddress = new Address
                {
                    AddressLine1 = source.mailingAddress,
                    AddressLine2 = source.street2,
                    City = source.cityTown,
                    Province = source.province,
                    PostalCode = source.postalCode
                },

                DamageFrom = source.dateOfDamage,
                DamageTo = source.dateOfDamage1,

                DamageInfo = new DamageInfo
                {
                    DamageType = source.causeOfDamageLoss1,
                    OtherDescription = source.pleaseSpecify,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                },

                IsBusinessManaged = !string.IsNullOrEmpty(source.yes) ? source.yes == "yes" : false,
                AreRevenuesInRange = !string.IsNullOrEmpty(source.yes1) ? source.yes1 == "yes" : false,
                EmployLessThanFifty = !string.IsNullOrEmpty(source.yes2) ? source.yes2 == "yes" : false,

                DevelopingOperaton = !string.IsNullOrEmpty(source.yes3) ? source.yes3 == "yes" : false,
                FullTimeFarmer = !string.IsNullOrEmpty(source.yes4) ? source.yes4 == "yes" : false,
                MajorityIncome = !string.IsNullOrEmpty(source.yes5) ? source.yes5 == "yes" : false,

                CouldNotPurchaseInsurance = source.yes8,
                HasRentalAgreement = source.yes9,
                HasReceipts = source.yes10,
                HasFinancialStatements = source.yes11,
                HasTaxReturn = source.yes12,
                HasProofOfOwnership = source.yes13,
                HasListOfDirectors = source.yes14,
                HasProofOfRegistration = source.yes15,
                HasEligibilityDocuments = source.yes16,

                Signature = source.signature1,
                SignerName = source.printName1,
                SignatureDate = source.dateYyyyMDay1,

                OtherSignature = source.signature2,
                OtherSignerName = source.printName2,
                OtherSignatureDate = source.dateYyyyMDay2,

                CleanUpLogs = new List<CleanUpLog>(),
                DamagedItems = new List<DamageItem>(),
            };

            foreach (var otherApplicant in source.cleanupLogDetails1)
            {
                ret.SecondaryApplicants.Add(new OtherApplicant
                {
                    ApplicantType = (SecondaryApplicantType)Enum.Parse(typeof(SecondaryApplicantType), otherApplicant.applicantType, true),
                    FirstName = otherApplicant.FirstNameofSecondary,
                    LastName = otherApplicant.LastNameofSecondary,
                    Email = otherApplicant.emailAddress,
                    Phone = otherApplicant.phoneNumber,
                });
            }

            foreach (var contact in source.otherContacts)
            {
                ret.AltContacts.Add(new AltContact
                {
                    Name = contact.alternateContactNameWhereYouCanBeReachedIfApplicable,
                    Email = contact.eMailAddress1,
                    Phone = contact.alternatePhoneNumber,
                });
            }

            foreach (var log in source.cleanupLogDetails)
            {
                ret.CleanUpLogs.Add(new CleanUpLog
                {
                    HoursWorked = log.hoursWorked,
                    Date = log.dateYyyyMDay,
                    DescriptionOfWork = log.descriptionOfWork,
                    ContactName = log.nameOfFamilyMemberVolunteer,
                });
            }

            foreach (var roomInfo in source.listByRoomItemsSubmittedForDamageAssessment)
            {
                ret.DamagedItems.Add(new DamageItem
                {
                    RoomName = roomInfo.roomName,
                    Description = roomInfo.listByRoomItemsSubmittedForDamageAssessment1,
                });
            }

            return ret;
        }

        public static EMBC.DFA.Managers.Intake.IndForm Map(Models.IndForm payload)
        {
            var source = payload.data;
            return new EMBC.DFA.Managers.Intake.IndForm
            {
                ApplicantType = (ApplicantType)Enum.Parse(typeof(ApplicantType), source.pleaseCheckAppropriateBox, true),
                IndigenousStatus = source.yes7 == "yes",
                OnFirstNationReserve = source.yes6 == "yes",
                NameOfFirstNationsReserve = source.nameOfFirstNationsReserve,
                Applicant = new Applicant
                {
                    FirstName = source.primaryContactNameLastFirst,
                    LastName = source.primaryContactNameLastFirst1,
                    Initial = source.primaryContactNameLastFirst2,
                    Email = source.eMailAddress,
                    Phone = source.businessTelephoneNumber,
                    Mobile = source.cellularTelephoneNumber,
                },
                DamageFrom = source.dateOfDamage,
                DamageTo = source.dateOfDamage1,
                DamagePropertyAddress = new Address
                {
                    AddressLine1 = source.street1,
                    AddressLine2 = source.street3,
                    City = source.cityTown1,
                    Province = source.province1,
                    PostalCode = source.postalCode1
                },
                MailingAddress = new Address
                {
                    AddressLine1 = source.mailingAddress,
                    AddressLine2 = source.street2,
                    City = source.cityTown,
                    Province = source.province,
                    PostalCode = source.postalCode
                },
                AlternateContact = source.alternateContactNameWhereYouCanBeReachedIfApplicable,
                AlternatePhoneNumber = source.alternatePhoneNumber,
                DamageInfo = new DamageInfo
                {
                    ManufacturedHome = source.manufacturedHome.yes,
                    DamageType = source.causeOfDamageLoss1,
                    OtherDescription = source.pleaseSpecifyIfOthers,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                },
                CleanUpLogs = Array.Empty<CleanUpLog>(),
                HasInsurance = source.yes == "yes",
                IsPrimaryResidence = source.yes1 == "yes",
                EligibleForGrant = source.yes2 == "yes",
                LossesOverOneThousand = source.yes3 == "yes",
                WasEvacuated = source.yes4 == "yes",
                InResidence = source.yes5 == "yes",

                Occupants = Array.Empty<Managers.Intake.Occupant>(),
                DamagedItems = Array.Empty<DamageItem>(),
            };
        }

        public static EMBC.DFA.Managers.Intake.GovForm Map(Models.GovForm payload)
        {
            var source = payload.data;
            return new EMBC.DFA.Managers.Intake.GovForm
            {
                ApplicantType = ApplicantType.GovernmentBody,
                Applicant = new Applicant
                {
                    FirstName = source.primaryContactNameLastFirst,
                    LastName = source.primaryContactNameLastFirst1,
                    Title = source.title,
                    Email = source.eMailAddress,
                    Phone = source.businessTelephoneNumber,
                    Mobile = source.cellularTelephoneNumber,
                },
                DamageFrom = source.dateOfDamageLoss,
                DamageTo = source.dateOfDamageLoss1,
                MailingAddress = new Address
                {
                    AddressLine1 = source.mailingAddress,
                    AddressLine2 = source.street2,
                    City = source.cityTown,
                    Province = source.province,
                    PostalCode = source.postalCode
                },
                AlternatePhoneNumber = source.businessTelephoneNumber1,
                AlternateCellNumber = source.cellularTelephoneNumber1,
                DamageInfo = new DamageInfo
                {
                    DamageType = source.causeOfDamageLoss1,
                    OtherDescription = source.pleaseSpecifyIfOthers,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                }
            };
        }
    }
}
