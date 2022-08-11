using AutoMapper;
using EMBC.DFA.Api.Models;
using EMBC.DFA.Api.Resources.Forms;

namespace EMBC.DFA.Api.Services.Intake
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<SmbFormData, Form>()
                .ForMember(d => d.ApplicantType, opts => opts.MapFrom(s => (ApplicantType)Enum.Parse(typeof(ApplicantType), s.pleaseSelectTheAppropriateOption, true)))
                .ForMember(d => d.IndigenousStatus, opts => opts.MapFrom(s => s.yes7 == "yes"))
                .ForMember(d => d.OnFirstNationReserve, opts => opts.MapFrom(s => s.yes6 == "yes"))
                .ForMember(d => d.NameOfFirstNationsReserve, opts => opts.MapFrom(s => s.nameOfFirstNationsReserve))
                .ForMember(d => d.Applicant, opts => opts.MapFrom(s => new Applicant
                {
                    FirstName = s.primaryContactNameLastFirst,
                    LastName = s.primaryContactNameLastFirst3,
                    Initial = s.primaryContactNameLastFirst4,
                    Email = s.eMailAddress,
                    Phone = s.businessTelephoneNumber,
                    Mobile = s.cellularTelephoneNumber,
                }))
                .ForMember(d => d.DamageFrom, opts => opts.MapFrom(s => s.dateOfDamage))
                .ForMember(d => d.DamageTo, opts => opts.MapFrom(s => s.dateOfDamage1))
                .ForMember(d => d.BusinessLegalName, opts => opts.MapFrom(s => s.primaryContactNameLastFirst1))
                .ForMember(d => d.ContactName, opts => opts.MapFrom(s => s.primaryContactNameLastFirst2))
                .ForMember(d => d.DamagePropertyAddress, opts => opts.MapFrom(s => new Address
                {
                    AddressLine1 = s.street1,
                    AddressLine2 = s.street3,
                    City = s.cityTown1,
                    Province = s.province1,
                    PostalCode = s.postalCode1
                }))
                .ForMember(d => d.MailingAddress, opts => opts.MapFrom(s => new Address
                {
                    AddressLine1 = s.mailingAddress,
                    AddressLine2 = s.street2,
                    City = s.cityTown,
                    Province = s.province,
                    PostalCode = s.postalCode
                }))
                .ForMember(d => d.OtherEmail, opts => opts.MapFrom(s => s.eMailAddress1))
                .ForMember(d => d.AlternateContact, opts => opts.MapFrom(s => s.alternateContactNameWhereYouCanBeReachedIfApplicable))
                .ForMember(d => d.AlternatePhoneNumber, opts => opts.MapFrom(s => s.alternatePhoneNumber))
                .ForMember(d => d.DamageInfo, opts => opts.MapFrom(s => s.causeOfDamageLoss))
                .ForMember(d => d.CleanUpLogs, opts => opts.MapFrom(s => s.cleanupLogDetails))
                .ForMember(d => d.BuildingOwner, opts => opts.Ignore())

                .ForMember(d => d.SmbInsuranceDetails, opts => opts.MapFrom(s => new SmbInsuranceDetails
                {
                    IsBusinessManaged = s.yes == "yes",
                    AreRevenuesInRange = s.yes1 == "yes",
                    EmployLessThanFifty = s.yes2 == "yes",
                    DevelopingOperaton = s.yes3 == "yes",
                    FullTimeFarmer = s.yes4 == "yes",
                    MajorityIncome = s.yes5 == "yes",
                    CouldNotPurchaseInsurance = s.yes8,
                    HasRentalAgreement = s.yes9,
                    HasReceipts = s.yes10,
                    HasFinancialStatements = s.yes11,
                    HasTaxReturn = s.yes12,
                    HasProofOfOwnership = s.yes13,
                    HasListOfDirectors = s.yes14,
                    HasProofOfRegistration = s.yes15,
                    HasEligibilityDocuments = s.yes16,
                }))
                .ForMember(d => d.IndInsuranceDetails, opts => opts.Ignore())
                .ForMember(d => d.Occupants, opts => opts.MapFrom(s => Array.Empty<Resources.Forms.Occupant>()))

                //.ForMember(d => d.Signature, opts => opts.MapFrom(s => s.signature1))
                //.ForMember(d => d.SignerName, opts => opts.MapFrom(s => s.printName1))
                //.ForMember(d => d.SignatureDate, opts => opts.MapFrom(s => s.dateYyyyMDay1))
                //.ForMember(d => d.OtherSignature, opts => opts.MapFrom(s => s.signature2))
                //.ForMember(d => d.OtherSignerName, opts => opts.MapFrom(s => s.printName1))
                //.ForMember(d => d.OtherSignatureDate, opts => opts.MapFrom(s => s.dateYyyyMDay2))
                //.ForMember(d => d.AppendixAFirstName, opts => opts.MapFrom(s => s.applicantFirstName2))
                //.ForMember(d => d.AppendixALastName, opts => opts.MapFrom(s => s.applicantFirstName3))
                //.ForMember(d => d.AppendixBFirstName, opts => opts.MapFrom(s => s.applicantFirstName))
                //.ForMember(d => d.AppendixBLastName, opts => opts.MapFrom(s => s.applicantFirstName1))
                //.ForMember(d => d.DamagedItems, opts => opts.MapFrom(s => s.listByRoomItemsSubmittedForDamageAssessment))
                //.ForMember(d => d.IsSubmit, opts => opts.MapFrom(s => s.submit1))
                .AfterMap((s, d) =>
                {
                    d.DamageInfo.OtherDescription = s.pleaseSpecify;
                    d.DamageInfo.DamageDescription = s.provideABriefDescriptionOfDamage;
                })
                ;

            CreateMap<IndFormData, Form>()
                .ForMember(d => d.ApplicantType, opts => opts.MapFrom(s => (ApplicantType)Enum.Parse(typeof(ApplicantType), s.pleaseCheckAppropriateBox, true)))
                .ForMember(d => d.IndigenousStatus, opts => opts.MapFrom(s => s.yes7 == "yes"))
                .ForMember(d => d.OnFirstNationReserve, opts => opts.MapFrom(s => s.yes6 == "yes"))
                .ForMember(d => d.NameOfFirstNationsReserve, opts => opts.MapFrom(s => s.nameOfFirstNationsReserve))
                .ForMember(d => d.Applicant, opts => opts.MapFrom(s => new Applicant
                {
                    FirstName = s.primaryContactNameLastFirst,
                    LastName = s.primaryContactNameLastFirst1,
                    Initial = s.primaryContactNameLastFirst2,
                    Email = s.eMailAddress,
                    Phone = s.businessTelephoneNumber,
                    Mobile = s.cellularTelephoneNumber,
                }))
                .ForMember(d => d.DamageFrom, opts => opts.MapFrom(s => s.dateOfDamage))
                .ForMember(d => d.DamageTo, opts => opts.MapFrom(s => s.dateOfDamage1))
                .ForMember(d => d.BusinessLegalName, opts => opts.Ignore())
                .ForMember(d => d.ContactName, opts => opts.Ignore())
                .ForMember(d => d.DamagePropertyAddress, opts => opts.MapFrom(s => new Address
                {
                    AddressLine1 = s.street1,
                    AddressLine2 = s.street3,
                    City = s.cityTown1,
                    Province = s.province1,
                    PostalCode = s.postalCode1
                }))
                .ForMember(d => d.MailingAddress, opts => opts.MapFrom(s => new Address
                {
                    AddressLine1 = s.mailingAddress,
                    AddressLine2 = s.street2,
                    City = s.cityTown,
                    Province = s.province,
                    PostalCode = s.postalCode
                }))
                .ForMember(d => d.OtherEmail, opts => opts.Ignore())
                .ForMember(d => d.AlternateContact, opts => opts.MapFrom(s => s.alternateContactNameWhereYouCanBeReachedIfApplicable))
                .ForMember(d => d.AlternatePhoneNumber, opts => opts.MapFrom(s => s.alternatePhoneNumber))

                .ForMember(d => d.BuildingOwner, opts => opts.MapFrom(s => new BuildingOwner
                {
                    Name = s.provideRegisteredBuildingOwnerSAndOrLandlordSNameS,
                    phone1 = s.contactTelephoneNumberS,
                    phone2 = s.contactTelephoneNumberS1
                }))

                .ForMember(d => d.DamageInfo, opts => opts.MapFrom(s => s.causeOfDamageLoss))

                .ForMember(d => d.SmbInsuranceDetails, opts => opts.Ignore())
                .ForMember(d => d.IndInsuranceDetails, opts => opts.MapFrom(s => new IndInsuranceDetails
                {
                    HasInsurance = s.yes == "yes",
                    IsPrimaryResidence = s.yes1 == "yes",
                    EligibleForGrant = s.yes2 == "yes",
                    LossesOverOneThousand = s.yes3 == "yes",
                    WasEvacuated = s.yes4 == "yes",
                    InResidence = s.yes5 == "yes",

                }))

                .ForMember(d => d.Occupants, opts => opts.MapFrom(s => s.occupants))

                .ForMember(d => d.CleanUpLogs, opts => opts.MapFrom(s => s.cleanupLogDetails))

                .AfterMap((s, d) =>
                {
                    d.DamageInfo.OtherDescription = s.pleaseSpecifyIfOthers;
                    d.DamageInfo.ManufacturedHome = s.manufacturedHome.yes;
                    d.DamageInfo.DamageDescription = s.provideABriefDescriptionOfDamage;
                })
                ;

            CreateMap<GovFormData, Form>()
                .ForMember(d => d.ApplicantType, opts => opts.MapFrom(s => ApplicantType.GovernmentBody))
                .ForMember(d => d.Applicant, opts => opts.MapFrom(s => new Applicant
                {
                    FirstName = s.primaryContactNameLastFirst,
                    LastName = s.primaryContactNameLastFirst1,
                    Title = s.title,
                    Email = s.eMailAddress,
                    Phone = s.businessTelephoneNumber,
                    Mobile = s.cellularTelephoneNumber,
                }))
                .ForMember(d => d.DamageFrom, opts => opts.MapFrom(s => s.dateOfDamageLoss))
                .ForMember(d => d.DamageTo, opts => opts.MapFrom(s => s.dateOfDamageLoss1))
                .ForMember(d => d.DamagePropertyAddress, opts => opts.MapFrom(s => new Address
                {
                    AddressLine1 = s.mailingAddress,
                    AddressLine2 = s.street2,
                    City = s.cityTown,
                    Province = s.province,
                    PostalCode = s.postalCode
                }))
                .ForMember(d => d.MailingAddress, opts => opts.MapFrom(s => new Address
                {
                    AddressLine1 = s.mailingAddress,
                    AddressLine2 = s.street2,
                    City = s.cityTown,
                    Province = s.province,
                    PostalCode = s.postalCode
                }))
                .ForMember(d => d.OtherEmail, opts => opts.MapFrom(s => s.eMailAddress1))
                .ForMember(d => d.AlternatePhoneNumber, opts => opts.MapFrom(s => s.businessTelephoneNumber1))
                .ForMember(d => d.AlternateCellNumber, opts => opts.MapFrom(s => s.cellularTelephoneNumber1))
                .ForMember(d => d.DamageInfo, opts => opts.MapFrom(s => s.causeOfDamageLoss))
                
                .AfterMap((s, d) =>
                {
                    d.DamageInfo.OtherDescription = s.pleaseSpecifyIfOthers;
                    d.DamageInfo.DamageDescription = s.provideABriefDescriptionOfDamage;
                })
                ;

            CreateMap<DamageLoss, DamageInfo>()
                .ForMember(d => d.DamageType, opts => opts.MapFrom(s => GetDamageType(s)))
                .ForMember(d => d.ManufacturedHome, opts => opts.Ignore())
                .ForMember(d => d.OtherDescription, opts => opts.Ignore())
                .ForMember(d => d.DamageDescription, opts => opts.Ignore())
                ;

            CreateMap<CleanUpDetail, CleanUpLog>()
                .ForMember(d => d.HoursWorked, opts => opts.MapFrom(s => s.hoursWorked))
                .ForMember(d => d.Date, opts => opts.MapFrom(s => s.dateYyyyMDay))
                .ForMember(d => d.DescriptionOfWork, opts => opts.MapFrom(s => s.descriptionOfWork))
                .ForMember(d => d.EmbcOfficeUseOnly, opts => opts.MapFrom(s => s.embcOfficeUseOnly))
                .ForMember(d => d.ContactName, opts => opts.MapFrom(s => s.nameOfFamilyMemberVolunteer))
                ;

            CreateMap<DamagedItem, Item>()
                .ForMember(d => d.ItemName, opts => opts.MapFrom(s => s.listByRoomItemsSubmittedForDamageAssessment1))
                .ForMember(d => d.EmbcOfficeUseOnlyComments, opts => opts.MapFrom(s => s.embcOfficeUseOnlyComments))
                ;

            CreateMap<Models.Occupant, Resources.Forms.Occupant>()
                .ForMember(d => d.FirstName, opts => opts.MapFrom(s => s.listTheNamesOfAllFullTimeOccupantsWhoResidedInTheHomeAtTheTimeOfTheEvent))
                .ForMember(d => d.LastName, opts => opts.MapFrom(s => s.listTheNamesOfAllFullTimeOccupantsWhoResidedInTheHomeAtTheTimeOfTheEvent1))
                ;
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
