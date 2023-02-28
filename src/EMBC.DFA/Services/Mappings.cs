using System;
using System.Collections.Generic;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Services
{
    public static partial class Mappings
    {
        public static EMBC.DFA.Managers.Intake.SmbForm Map(EMBC.DFA.Services.CHEFS.SmbForm payload)
        {
            var source = payload.data;
            var ret = new EMBC.DFA.Managers.Intake.SmbForm
            {
                CHEFConfirmationId = payload.ConfirmationId ?? string.Empty,
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

                DamageFrom = DateTime.Parse(source.dateOfDamage),
                DamageTo = DateTime.Parse(source.dateOfDamage1),

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
                SignatureDate = DateTime.Parse(source.dateYyyyMDay1),

                OtherSignature = source.signature2,
                OtherSignerName = source.printName2,
                OtherSignatureDate = !string.IsNullOrEmpty(source.dateYyyyMDay2) ? DateTime.Parse(source.dateYyyyMDay2) : null,

                CleanUpLogs = new List<CleanUpLog>(),
                DamagedItems = new List<DamageItem>(),
                Documents = new List<AttachmentData>(),
            };

            foreach (var otherApplicant in source.cleanupLogDetails1)
            {
                ret.SecondaryApplicants.Add(new OtherApplicant
                {
                    ApplicantType = (SecondaryApplicantType)Enum.Parse(typeof(SecondaryApplicantType), otherApplicant.applicantType, true),
                    FirstName = otherApplicant.FirstNameofSecondary,
                    LastName = otherApplicant.FirstNameofSecondary1,
                    Email = otherApplicant.emailAddress,
                    Phone = otherApplicant.phoneNumber,
                });
            }

            foreach (var contact in source.otherContacts)
            {
                ret.AltContacts.Add(new AltContact
                {
                    FirstName = contact.alternateContactNameWhereYouCanBeReachedIfApplicable,
                    LastName = contact.alternateContactNameWhereYouCanBeReachedIfApplicable1,
                    Email = contact.eMailAddress1,
                    Phone = contact.alternatePhoneNumber,
                });
            }

            foreach (var log in source.cleanupLogDetails)
            {
                ret.CleanUpLogs.Add(new CleanUpLog
                {
                    HoursWorked = log.hoursWorked,
                    Date = !string.IsNullOrEmpty(log.dateYyyyMDay) ? DateTime.Parse(log.dateYyyyMDay) : null,
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

            foreach (var attachment in source.completedInsuranceTemplate1)
            {
                ret.Documents.Add(new AttachmentData
                {
                    FileName = attachment.originalName,
                    FileType = "Insurance Template",
                    Url = attachment.url,
                    CHEFSubmissionId = payload.SubmissionId ?? string.Empty
                });
            }

            foreach (var attachment in source.leaseAgreementsIfApplicable)
            {
                ret.Documents.Add(new AttachmentData
                {
                    FileName = attachment.originalName,
                    FileType = "Lease Agreement",
                    Url = attachment.url,
                    CHEFSubmissionId = payload.SubmissionId ?? string.Empty
                });
            }

            foreach (var attachment in source.financialDocuments)
            {
                ret.Documents.Add(new AttachmentData
                {
                    FileName = attachment.originalName,
                    FileType = "Financial Document",
                    Url = attachment.url,
                    CHEFSubmissionId = payload.SubmissionId ?? string.Empty
                });
            }

            return ret;
        }

        public static EMBC.DFA.Managers.Intake.IndForm Map(EMBC.DFA.Services.CHEFS.IndForm payload)
        {
            var source = payload.data;
            var ret = new EMBC.DFA.Managers.Intake.IndForm
            {
                CHEFConfirmationId = payload.ConfirmationId ?? string.Empty,
                ApplicantType = (ApplicantType)Enum.Parse(typeof(ApplicantType), source.pleaseCheckAppropriateBox, true),
                IndigenousStatus = source.yes7 == "yes",
                OnFirstNationReserve = source.yes6 == "yes",
                NameOfFirstNationsReserve = source.nameOfFirstNationsReserve,
                FirstNationsComments = source.nameOfFirstNationsReserve,

                Applicant = new Applicant
                {
                    FirstName = source.primaryContactNameLastFirst,
                    LastName = source.primaryContactNameLastFirst1,
                    Initial = source.primaryContactNameLastFirst2,
                    Mobile = source.cellularTelephoneNumber,
                    Phone = source.businessTelephoneNumber,
                    Email = source.eMailAddress,
                    AlternatePhone = source.AlternatePhoneNumberofPrimarycontact,
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
                BuildingOwner = new BuildingOwner
                {
                    Name = source.provideRegisteredBuildingOwnerSAndOrLandlordSNameS,
                    Phone = source.contactTelephoneNumberS,
                    Email = source.emailOfLandlord
                },

                DamageFrom = source.dateOfDamageFrom,
                DamageTo = source.dateOfDamageTo,

                DamageInfo = new DamageInfo
                {
                    ManufacturedHome = source.manufacturedHome.yes,
                    DamageType = source.causeOfDamageLoss1,
                    OtherDescription = source.pleaseSpecifyIfOthers,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                },
                HasInsurance = source.yes == "yes",
                IsPrimaryResidence = source.yes1 == "yes",
                EligibleForGrant = source.yes2 == "yes",
                LossesOverOneThousand = source.yes3 == "yes",
                WasEvacuated = source.yes4 == "yes",
                InResidence = source.yes5 == "yes",

                Occupants = new List<Managers.Intake.Occupant>(),

                HasRentalAgreement = source.aCopyOfARentalAgreementOrLeaseIfApplicableForResidentialTenantApplication,
                HasReceipts = source.ifYouHaveInvoicesReceiptsForCleanupOrRepairsPleaseHaveThemAvailableDuringTheSiteMeetingToHelpTheEvaluatorIdentifyEligibleCosts,

                Signature = source.signature1,
                SignerName = source.printName1,
                SignatureDate = DateTime.Parse(source.dateYyyyMDay1),

                OtherSignature = source.signature2,
                OtherSignerName = source.printName2,
                OtherSignatureDate = !string.IsNullOrEmpty(source.dateYyyyMDay2) ? DateTime.Parse(source.dateYyyyMDay2) : null,

                CleanUpLogs = new List<CleanUpLog>(),
                DamagedItems = new List<DamageItem>(),
                Documents = new List<AttachmentData>(),
            };

            foreach (var otherApplicant in source.SecondaryApplicant)
            {
                ret.SecondaryApplicants.Add(new OtherApplicant
                {
                    ApplicantType = (SecondaryApplicantType)Enum.Parse(typeof(SecondaryApplicantType), otherApplicant.applicantType, true),
                    FirstName = otherApplicant.FirstNameofsecondary,
                    LastName = otherApplicant.lastName,
                    Email = otherApplicant.emailAddress,
                    Phone = otherApplicant.phoneNumber,
                });
            }

            foreach (var contact in source.otherContacts)
            {
                ret.AltContacts.Add(new AltContact
                {
                    FirstName = contact.AlternateContactName,
                    LastName = contact.AlternateContactName1,
                    Email = contact.AlternateEmailAddress,
                    Phone = contact.AlternateContactPhone,
                });
            }

            foreach (var occupant in source.occupants)
            {
                ret.Occupants.Add(new Managers.Intake.Occupant
                {
                    FirstName = occupant.listTheNamesOfAllFullTimeOccupantsWhoResidedInTheHomeAtTheTimeOfTheEvent,
                    LastName = occupant.listTheNamesOfAllFullTimeOccupantsWhoResidedInTheHomeAtTheTimeOfTheEvent1,
                    Relationship = occupant.relationshipTitle
                });
            }

            foreach (var log in source.cleanupLogDetails)
            {
                ret.CleanUpLogs.Add(new CleanUpLog
                {
                    HoursWorked = log.hoursWorked,
                    Date = !string.IsNullOrEmpty(log.dateYyyyMDay) ? DateTime.Parse(log.dateYyyyMDay) : null,
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

            foreach (var attachment in source.completedInsuranceTemplate)
            {
                ret.Documents.Add(new AttachmentData
                {
                    FileName = attachment.originalName,
                    FileType = "Insurance Template",
                    Url = attachment.url,
                    CHEFSubmissionId = payload.SubmissionId ?? string.Empty
                });
            }

            foreach (var attachment in source.governmentIssuedIdDlServiceCardBcId)
            {
                ret.Documents.Add(new AttachmentData
                {
                    FileName = attachment.originalName,
                    FileType = "Government ID",
                    Url = attachment.url,
                    CHEFSubmissionId = payload.SubmissionId ?? string.Empty
                });
            }

            foreach (var attachment in source.signedTenancyAgreementIfNoTenancyAgreementPleaseProvideTheLandlordContactInformationAndAPieceOfMailWithTheAddress)
            {
                ret.Documents.Add(new AttachmentData
                {
                    FileName = attachment.originalName,
                    FileType = "Tenancy Agreement / Mail Indicating Address",
                    Url = attachment.url,
                    CHEFSubmissionId = payload.SubmissionId ?? string.Empty
                });
            }

            return ret;
        }

        public static EMBC.DFA.Managers.Intake.GovForm Map(EMBC.DFA.Services.CHEFS.GovForm payload)
        {
            var source = payload.data;
            return new EMBC.DFA.Managers.Intake.GovForm
            {
                CHEFConfirmationId = payload.ConfirmationId ?? string.Empty,
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
