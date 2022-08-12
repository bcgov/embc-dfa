using AutoMapper;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;

namespace EMBC.DFA.Api.Resources.Forms
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Form, incident>(MemberList.None)
                .IncludeAllDerived()
                .ForMember(d => d.dfa_applicanttype, opts => opts.MapFrom(s => (int?)Enum.Parse<ApplicantTypeOptionSet>(s.ApplicantType.ToString())))
                .ForMember(d => d.dfa_dateofdamage, opts => opts.MapFrom(s => s.DamageFrom))
                .ForMember(d => d.dfa_dateofdamageto, opts => opts.MapFrom(s => s.DamageTo))
                .ForMember(d => d.dfa_indigenousstatus, opts => opts.MapFrom(s => s.IndigenousStatus))
                .ForMember(d => d.dfa_onfirstnationsreserve, opts => opts.MapFrom(s => s.OnFirstNationReserve))
                .ForMember(d => d.dfa_nameoffirstnationsreserve, opts => opts.MapFrom(s => s.NameOfFirstNationsReserve))
                .ForMember(d => d.customerid_contact, opts => opts.MapFrom(s => s.Applicant))

                //.ForMember(d => d., opts => opts.MapFrom(s => s.BusinessLegalName))
                //.ForMember(d => d., opts => opts.MapFrom(s => s.ContactName))

                .ForMember(d => d.dfa_damagedpropertystreet1, opts => opts.MapFrom(s => s.DamagePropertyAddress != null ? s.DamagePropertyAddress.AddressLine1 : string.Empty))
                .ForMember(d => d.dfa_damagedpropertystreet2, opts => opts.MapFrom(s => s.DamagePropertyAddress != null ? s.DamagePropertyAddress.AddressLine2 : string.Empty))
                .ForMember(d => d.dfa_damagedpropertycity, opts => opts.MapFrom(s => s.DamagePropertyAddress != null ? s.DamagePropertyAddress.City : string.Empty))
                .ForMember(d => d.dfa_damagedpropertyprovince, opts => opts.MapFrom(s => s.DamagePropertyAddress != null ? s.DamagePropertyAddress.Province : string.Empty))
                .ForMember(d => d.dfa_damagedpropertypostalcode, opts => opts.MapFrom(s => s.DamagePropertyAddress != null ? s.DamagePropertyAddress.PostalCode : string.Empty))

                .ForMember(d => d.dfa_mailingaddressstreet1, opts => opts.MapFrom(s => s.MailingAddress != null ? s.MailingAddress.AddressLine1 : string.Empty))
                .ForMember(d => d.dfa_mailingaddressstreet2, opts => opts.MapFrom(s => s.MailingAddress != null ? s.MailingAddress.AddressLine2 : string.Empty))
                .ForMember(d => d.dfa_mailingaddresscity, opts => opts.MapFrom(s => s.MailingAddress != null ? s.MailingAddress.City : string.Empty))
                .ForMember(d => d.dfa_mailingaddressprovince, opts => opts.MapFrom(s => s.MailingAddress != null ? s.MailingAddress.Province : string.Empty))
                .ForMember(d => d.dfa_mailingaddresspostalcode, opts => opts.MapFrom(s => s.MailingAddress != null ? s.MailingAddress.PostalCode : string.Empty))

                .ForMember(d => d.dfa_issamemailingaddress, opts => opts.MapFrom(s =>
                s.MailingAddress != null && s.DamagePropertyAddress != null ?
                    s.MailingAddress.AddressLine1.Equals(s.DamagePropertyAddress.AddressLine1, StringComparison.OrdinalIgnoreCase) &&
                    s.MailingAddress.AddressLine2.Equals(s.DamagePropertyAddress.AddressLine2, StringComparison.OrdinalIgnoreCase) &&
                    s.MailingAddress.City.Equals(s.DamagePropertyAddress.City, StringComparison.OrdinalIgnoreCase) &&
                    s.MailingAddress.Province.Equals(s.DamagePropertyAddress.Province, StringComparison.OrdinalIgnoreCase) &&
                    s.MailingAddress.PostalCode.Equals(s.DamagePropertyAddress.PostalCode, StringComparison.OrdinalIgnoreCase)
                    : false))


                .ForMember(d => d.emailaddress, opts => opts.MapFrom(s => s.Applicant.Email))

                //.ForMember(d => d., opts => opts.MapFrom(s => s.OtherEmail))
                //.ForMember(d => d., opts => opts.MapFrom(s => s.AlternateContact))
                //.ForMember(d => d., opts => opts.MapFrom(s => s.AlternatePhoneNumber))
                //.ForMember(d => d., opts => opts.MapFrom(s => s.AlternateCellNumber))

                //.ForMember(d => d., opts => opts.MapFrom(s => s.BuildingOwner))

                .ForMember(d => d.dfa_manufacturedhome, opts => opts.MapFrom(s => s.DamageInfo.ManufacturedHome))
                .ForMember(d => d.dfa_causeofdamageloss, opts => opts.MapFrom(s => (int?)Enum.Parse<ApplicantTypeOptionSet>(s.DamageInfo.DamageType.ToString())))
                .ForMember(d => d.dfa_causeofdamagelossother, opts => opts.MapFrom(s => s.DamageInfo.OtherDescription))
                .ForMember(d => d.description, opts => opts.MapFrom(s => s.DamageInfo.DamageDescription))

                //.ForMember(d => d., opts => opts.MapFrom(s => s.SmbInsuranceDetails))

                .ForMember(d => d.dfa_doyouhaveinsurancecoverage, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.HasInsurance : false))
                .ForMember(d => d.dfa_isthispropertyyourprimaryresidence, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.IsPrimaryResidence : false))
                .ForMember(d => d.dfa_eligibleforbchomegrantonthisproperty, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.EligibleForGrant : false))
                .ForMember(d => d.dfa_doyourlossestotalmorethan1000, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.LossesOverOneThousand : false))
                .ForMember(d => d.dfa_wereyouevacuatedduringtheevent, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.WasEvacuated : false))
                .ForMember(d => d.dfa_areyounowresidingintheresidence, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.InResidence : false))
                .ForMember(d => d.dfa_datereturntotheresidence, opts => opts.MapFrom(s => s.IndInsuranceDetails != null ? s.IndInsuranceDetails.DateReturned : null))

                .ForMember(d => d.dfa_rentalorleaseagreement, opts => opts.MapFrom(s => s.HasRentalAgreement))
                .ForMember(d => d.dfa_receiptsorinvoicespaidbyapplicant, opts => opts.MapFrom(s => s.HasReceipts))

                .ForMember(d => d.dfa_incident_occupant, opts => opts.MapFrom(s => s.Occupants))
                .ForMember(d => d.dfa_incident_cleanuplog, opts => opts.MapFrom(s => s.CleanUpLogs))
                .ForMember(d => d.dfa_incident_damageditem, opts => opts.MapFrom(s => s.DamagedItems))
                .ForMember(d => d.dfa_listofdamageditemsassessed, opts => opts.MapFrom(s => s.DamagedItems.Any()))
                ;

            CreateMap<Applicant, contact>(MemberList.None)
                .ForMember(d => d.firstname, opts => opts.MapFrom(s => s.FirstName))
                .ForMember(d => d.lastname, opts => opts.MapFrom(s => s.LastName))
                .ForMember(d => d.jobtitle, opts => opts.MapFrom(s => s.Title))
                ;

            CreateMap<Occupant, dfa_occupant>(MemberList.None)
                .ForMember(d => d.dfa_name, opts => opts.MapFrom(s => s.FirstName + " " + s.LastName))
                ;

            CreateMap<CleanUpLog, dfa_cleanuplog>(MemberList.None)
                .ForMember(d => d.dfa_hoursworked, opts => opts.MapFrom(s => s.HoursWorked))
                .ForMember(d => d.dfa_date, opts => opts.MapFrom(s => s.Date))
                .ForMember(d => d.dfa_name, opts => opts.MapFrom(s => s.DescriptionOfWork))
                .ForMember(d => d.dfa_embcofficeonly, opts => opts.MapFrom(s => s.EmbcOfficeUseOnly))
                //.ForMember(d => d.dfa_ContactId, opts => opts.MapFrom(s => s.ContactName))
                ;

            CreateMap<DamageItem, dfa_damageditem>(MemberList.None)
                .ForMember(d => d.dfa_damageditemname, opts => opts.MapFrom(s => s.ItemName))
                ;
        }
    }

    public enum ApplicantTypeOptionSet
    {
        CharitableOrganization = 222710004,
        FarmOwner = 222710002,
        HomeOwner = 222710000,
        ResidentialTenant = 222710001,
        SmallBusinessOwner = 222710003,
        GovernmentBody = 222710005,
        Incorporated = 222710006
    }

    public enum DamageTypeOptionSet
    {
        Flooding = 222710000,
        Landslide = 222710001,
        Windstorm = 222710002,
        Other = 222710003,
    }
}
