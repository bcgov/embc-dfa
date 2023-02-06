using System;
using System.Collections.ObjectModel;
using System.Linq;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Resources.Submissions
{
    public static class Mappings
    {
        public static incident Map(SmbForm form)
        {
            return new incident
            {
                dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                dfa_indigenousstatus = form.IndigenousStatus,
                dfa_onfirstnationsreserve = form.OnFirstNationReserve,
                dfa_nameoffirstnationsreserve = form.NameOfFirstNationsReserve,

                //customerid_contact = Map(form.Applicant),
                emailaddress = form.Applicant.Email,
                dfa_dateofdamage = form.DamageFrom,
                dfa_dateofdamageto = form.DamageTo,

                //dfa_RelatedBusiness = Map(form.BusinessLegalName),
                //ContactName

                dfa_damagedpropertystreet1 = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.AddressLine1 : String.Empty,
                dfa_damagedpropertystreet2 = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.AddressLine2 : String.Empty,
                //dfa_AreaCommunityId = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.City : String.Empty,
                dfa_damagedpropertyprovince = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.Province : String.Empty,
                dfa_damagedpropertypostalcode = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.PostalCode : String.Empty,
                dfa_mailingaddressstreet1 = form.MailingAddress != null ? form.MailingAddress.AddressLine1 : String.Empty,
                dfa_mailingaddressstreet2 = form.MailingAddress != null ? form.MailingAddress.AddressLine2 : String.Empty,
                //dfa_AreaCommunity2Id = form.MailingAddress != null ? form.MailingAddress.City : String.Empty,
                dfa_mailingaddressprovince = form.MailingAddress != null ? form.MailingAddress.Province : String.Empty,
                dfa_mailingaddresspostalcode = form.MailingAddress != null ? form.MailingAddress.PostalCode : String.Empty,
                dfa_issamemailingaddress = form.MailingAddress != null && form.DamagePropertyAddress != null ?
                    form.MailingAddress.AddressLine1.Equals(form.DamagePropertyAddress.AddressLine1, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.AddressLine2.Equals(form.DamagePropertyAddress.AddressLine2, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.City.Equals(form.DamagePropertyAddress.City, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.Province.Equals(form.DamagePropertyAddress.Province, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.PostalCode.Equals(form.DamagePropertyAddress.PostalCode, StringComparison.OrdinalIgnoreCase)
                    : false,

                dfa_alternatecontactname = form.AlternateContact,
                dfa_alternatephonenumber = form.AlternatePhoneNumber,

                dfa_causeofdamageloss = (int?)Enum.Parse<DamageTypeOptionSet>(form.DamageInfo.DamageType.ToString()),
                dfa_causeofdamagelossother = form.DamageInfo.OtherDescription,
                description = form.DamageInfo.DamageDescription,

                //IsBusinessManaged
                //AreRevenuesInRange
                //EmployLessThanFifty
                //DevelopingOperaton
                //FullTimeFarmer
                //MajorityIncome
                //CouldNotPurchaseInsurance
                dfa_rentalorleaseagreement = form.HasRentalAgreement,
                dfa_receiptsorinvoicespaidbyapplicant = form.HasReceipts,
                //HasFinancialStatements
                //HasTaxReturn
                //HasProofOfOwnership
                //HasListOfDirectors
                //HasProofOfRegistration
                //HasEligibilityDocuments

                dfa_incident_cleanuplog = Map(form.CleanUpLogs),
                dfa_incident_damageditem = Map(form.DamagedItems),
                dfa_listofdamageditemsassessed = form.DamagedItems.Any(),

            };
        }

        public static incident Map(IndForm form)
        {
            return new incident
            {
                dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                dfa_indigenousstatus = form.IndigenousStatus,
                dfa_onfirstnationsreserve = form.OnFirstNationReserve,
                dfa_nameoffirstnationsreserve = form.NameOfFirstNationsReserve,

                customerid_contact = Map(form.Applicant),
                emailaddress = form.Applicant.Email,
                dfa_dateofdamage = form.DamageFrom,
                dfa_dateofdamageto = form.DamageTo,
                //BusinessLegalName
                //ContactName

                dfa_damagedpropertystreet1 = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.AddressLine1 : String.Empty,
                dfa_damagedpropertystreet2 = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.AddressLine2 : String.Empty,
                //dfa_AreaCommunityId = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.City : String.Empty,
                dfa_damagedpropertyprovince = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.Province : String.Empty,
                dfa_damagedpropertypostalcode = form.DamagePropertyAddress != null ? form.DamagePropertyAddress.PostalCode : String.Empty,
                dfa_mailingaddressstreet1 = form.MailingAddress != null ? form.MailingAddress.AddressLine1 : String.Empty,
                dfa_mailingaddressstreet2 = form.MailingAddress != null ? form.MailingAddress.AddressLine2 : String.Empty,
                //dfa_AreaCommunity2Id = form.MailingAddress != null ? form.MailingAddress.City : String.Empty,
                dfa_mailingaddressprovince = form.MailingAddress != null ? form.MailingAddress.Province : String.Empty,
                dfa_mailingaddresspostalcode = form.MailingAddress != null ? form.MailingAddress.PostalCode : String.Empty,
                dfa_issamemailingaddress = form.MailingAddress != null && form.DamagePropertyAddress != null ?
                    form.MailingAddress.AddressLine1.Equals(form.DamagePropertyAddress.AddressLine1, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.AddressLine2.Equals(form.DamagePropertyAddress.AddressLine2, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.City.Equals(form.DamagePropertyAddress.City, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.Province.Equals(form.DamagePropertyAddress.Province, StringComparison.OrdinalIgnoreCase) &&
                    form.MailingAddress.PostalCode.Equals(form.DamagePropertyAddress.PostalCode, StringComparison.OrdinalIgnoreCase)
                    : false,

                dfa_alternatecontactname = form.AlternateContact,
                dfa_alternatephonenumber = form.AlternatePhoneNumber,

                //BuildingOwner

                dfa_causeofdamageloss = (int?)Enum.Parse<DamageTypeOptionSet>(form.DamageInfo.DamageType.ToString()),
                dfa_causeofdamagelossother = form.DamageInfo.OtherDescription,
                description = form.DamageInfo.DamageDescription,

                dfa_doyouhaveinsurancecoverage = form.HasInsurance,
                dfa_isthispropertyyourprimaryresidence = form.IsPrimaryResidence,
                dfa_eligibleforbchomegrantonthisproperty = form.EligibleForGrant,
                dfa_doyourlossestotalmorethan1000 = form.LossesOverOneThousand,
                dfa_wereyouevacuatedduringtheevent = form.WasEvacuated,
                dfa_datereturntotheresidence = form.DateReturned,
                dfa_areyounowresidingintheresidence = form.InResidence,

                dfa_incident_occupant = Map(form.Occupants),
                dfa_incident_cleanuplog = Map(form.CleanUpLogs),
                dfa_incident_damageditem = Map(form.DamagedItems),
                dfa_listofdamageditemsassessed = form.DamagedItems.Any(),

            };
        }

        public static incident Map(GovForm form)
        {
            return new incident
            {
                dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                //GovLegalName
                //Date

                customerid_contact = Map(form.Applicant),
                emailaddress = form.Applicant.Email,
                //BusinessLegalName
                //ContactName

                dfa_damagedpropertystreet1 = form.MailingAddress != null ? form.MailingAddress.AddressLine1 : String.Empty,
                dfa_damagedpropertystreet2 = form.MailingAddress != null ? form.MailingAddress.AddressLine2 : String.Empty,
                //dfa_AreaCommunityId = form.MailingAddress != null ? form.MailingAddress.City : String.Empty,
                dfa_damagedpropertyprovince = form.MailingAddress != null ? form.MailingAddress.Province : String.Empty,
                dfa_damagedpropertypostalcode = form.MailingAddress != null ? form.MailingAddress.PostalCode : String.Empty,
                dfa_mailingaddressstreet1 = form.MailingAddress != null ? form.MailingAddress.AddressLine1 : String.Empty,
                dfa_mailingaddressstreet2 = form.MailingAddress != null ? form.MailingAddress.AddressLine2 : String.Empty,
                //dfa_AreaCommunity2Id = form.MailingAddress != null ? form.MailingAddress.City : String.Empty,
                dfa_mailingaddressprovince = form.MailingAddress != null ? form.MailingAddress.Province : String.Empty,
                dfa_mailingaddresspostalcode = form.MailingAddress != null ? form.MailingAddress.PostalCode : String.Empty,
                dfa_issamemailingaddress = true,

                dfa_alternatephonenumber = form.AlternatePhoneNumber,
                //AlternateCellNumber

                dfa_dateofdamage = form.DamageFrom,
                dfa_dateofdamageto = form.DamageTo,

                dfa_causeofdamageloss = (int?)Enum.Parse<DamageTypeOptionSet>(form.DamageInfo.DamageType.ToString()),
                dfa_causeofdamagelossother = form.DamageInfo.OtherDescription,
                description = form.DamageInfo.DamageDescription,
            };
        }

        public static contact Map(Applicant applicant)
        {
            return new contact
            {
                firstname = applicant.FirstName,
                lastname = applicant.LastName,
                jobtitle = applicant.Title,
            };
        }

        public static Collection<dfa_occupant> Map(Occupant[] occupants)
        {
            var ret = new Collection<dfa_occupant>();
            foreach (var oc in occupants)
            {
                ret.Add(Map(oc));
            }
            return ret;
        }

        public static dfa_occupant Map(Occupant occupant)
        {
            return new dfa_occupant
            {
                dfa_name = occupant.FirstName + ' ' + occupant.LastName
            };
        }

        public static Collection<dfa_cleanuplog> Map(CleanUpLog[] logs)
        {
            var ret = new Collection<dfa_cleanuplog>();
            foreach (var log in logs)
            {
                ret.Add(Map(log));
            }
            return ret;
        }

        public static dfa_cleanuplog Map(CleanUpLog log)
        {
            return new dfa_cleanuplog
            {
                dfa_hoursworked = log.HoursWorked,
                dfa_date = log.Date,
                dfa_name = log.DescriptionOfWork,
                dfa_embcofficeonly = log.EmbcOfficeUseOnly
            };
        }

        public static Collection<dfa_damageditem> Map(DamageItem[] items)
        {
            var ret = new Collection<dfa_damageditem>();
            foreach (var item in items)
            {
                ret.Add(Map(item));
            }
            return ret;
        }

        public static dfa_damageditem Map(DamageItem item)
        {
            return new dfa_damageditem
            {
                dfa_damageditemname = item.ItemName
            };
        }


        private enum ApplicantTypeOptionSet
        {
            CharitableOrganization = 222710004,
            FarmOwner = 222710002,
            HomeOwner = 222710000,
            ResidentialTenant = 222710001,
            SmallBusinessOwner = 222710003,
            GovernmentBody = 222710005,
            Incorporated = 222710006
        }

        private enum DamageTypeOptionSet
        {
            Flooding = 222710000,
            Landslide = 222710001,
            Windstorm = 222710002,
            Other = 222710003,
        }
    }
}
