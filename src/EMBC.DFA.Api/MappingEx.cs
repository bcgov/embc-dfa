using EMBC.DFA.Api.Models;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Api
{
    public static class Mapper
    {
        public static T? Map<T>(object? source)
        {
            //placeholder for future mapping
            return default(T);
        }

        public static EMBC.DFA.Managers.Intake.SmbForm? Map<T>(Models.SmbForm? payload)
        {
            if (payload == null) throw new ArgumentNullException(nameof(payload));
            var source = payload.data;
            return new EMBC.DFA.Managers.Intake.SmbForm
            {
                ApplicantType = (ApplicantType)Enum.Parse(typeof(ApplicantType), source.pleaseSelectTheAppropriateOption, true),
                IndigenousStatus = source.yes7 == "yes",
                OnFirstNationReserve = source.yes6 == "yes",
                NameOfFirstNationsReserve = source.nameOfFirstNationsReserve,
                Applicant = new Applicant
                {
                    FirstName = source.primaryContactNameLastFirst,
                    LastName = source.primaryContactNameLastFirst3,
                    Initial = source.primaryContactNameLastFirst4,
                    Email = source.eMailAddress,
                    Phone = source.businessTelephoneNumber,
                    Mobile = source.cellularTelephoneNumber,
                },
                DamageFrom = source.dateOfDamage,
                DamageTo = source.dateOfDamage1,
                BusinessLegalName = source.primaryContactNameLastFirst1,
                ContactName = source.primaryContactNameLastFirst2,
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
                    DamageType = GetDamageType(source.causeOfDamageLoss),
                    OtherDescription = source.pleaseSpecify,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                },
                CleanUpLogs = Array.Empty<CleanUpLog>(),
                IsBusinessManaged = source.yes == "yes",
                AreRevenuesInRange = source.yes1 == "yes",
                EmployLessThanFifty = source.yes2 == "yes",
                DevelopingOperaton = source.yes3 == "yes",
                FullTimeFarmer = source.yes4 == "yes",
                MajorityIncome = source.yes5 == "yes",
                CouldNotPurchaseInsurance = source.yes8,
                HasRentalAgreement = source.yes9,
                HasReceipts = source.yes10,
                HasFinancialStatements = source.yes11,
                HasTaxReturn = source.yes12,
                HasProofOfOwnership = source.yes13,
                HasListOfDirectors = source.yes14,
                HasProofOfRegistration = source.yes15,
                HasEligibilityDocuments = source.yes16,
                DamagedItems = Array.Empty<DamageItem>(),
            };
        }

        public static EMBC.DFA.Managers.Intake.IndForm? Map<T>(Models.IndForm? payload)
        {
            if (payload == null) throw new ArgumentNullException(nameof(payload));
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
                    DamageType = GetDamageType(source.causeOfDamageLoss),
                    OtherDescription = source.pleaseSpecifyIfOthers,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                },
                CleanUpLogs = Array.Empty<CleanUpLog>(),
                HasInsurance = source.yes == "yes",
                IsPrimaryResidence= source.yes1 == "yes",
                EligibleForGrant= source.yes2 == "yes",
                LossesOverOneThousand= source.yes3 == "yes",
                WasEvacuated= source.yes4 == "yes",
                InResidence = source.yes5 == "yes",

                Occupants = Array.Empty<Managers.Intake.Occupant>(),
                DamagedItems = Array.Empty<DamageItem>(),
            };
        }

        public static EMBC.DFA.Managers.Intake.GovForm? Map<T>(Models.GovForm? payload)
        {
            if (payload == null) throw new ArgumentNullException(nameof(payload));
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
                    DamageType = GetDamageType(source.causeOfDamageLoss),
                    OtherDescription = source.pleaseSpecifyIfOthers,
                    DamageDescription = source.provideABriefDescriptionOfDamage,
                }
            };
        }

        private static DamageType GetDamageType(DamageLoss s)
        {
            if (!s.flooding && !s.landslide && !s.windstorm && !s.other)
            {
                throw new ArgumentException();
            }

            return s.flooding ? DamageType.Flooding :
                s.landslide ? DamageType.Landslide :
                s.windstorm ? DamageType.Windstorm :
                s.other ? DamageType.Other : default(DamageType);
        }
    }
}
