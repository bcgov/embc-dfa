using System;
using System.Collections.ObjectModel;
using System.Linq;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using EMBC.DFA.Managers.Intake;

namespace EMBC.DFA.Resources.Submissions
{
    public static class Mappings
    {
        public static dfa_appapplication Map(SmbForm form)
        {
            var ret = new dfa_appapplication
            {
                dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                dfa_indigenousstatus = form.IndigenousStatus,
                dfa_indigenousreserve = form.OnFirstNationReserve,
                dfa_nameoffirstnationsr = form.NameOfFirstNationsReserve,
                dfa_comments = form.FirstNationsComments,

                dfa_Applicant = Map(form.Applicant),

                dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId = new Collection<dfa_appsecondaryapplicant>(),
                dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId = new Collection<dfa_appothercontact>(),

                dfa_damagedpropertystreet1 = form.DamagePropertyAddress.AddressLine1,
                dfa_damagedpropertystreet2 = form.DamagePropertyAddress.AddressLine2,
                //dfa_areacommunityid = get community from city name...,
                dfa_damagedpropertyprovince = form.DamagePropertyAddress.Province,
                dfa_damagedpropertypostalcode = form.DamagePropertyAddress.PostalCode,

                dfa_mailingaddressstreet1 = form.MailingAddress.AddressLine1,
                dfa_mailingaddressstreet2 = form.MailingAddress.AddressLine2,
                //dfa_areacommunity2id = form.MailingAddress.City, need to get community
                dfa_mailingaddressprovince = form.MailingAddress.Province,
                dfa_mailingaddresspostalcode = form.MailingAddress.PostalCode,

                dfa_dateofdamage = form.DamageFrom,
                dfa_dateofdamageto = form.DamageTo,

                dfa_businessmanagedbyallownersondaytodaybasis = form.IsBusinessManaged,
                dfa_grossrevenues100002000000beforedisaster = form.AreRevenuesInRange,
                dfa_employlessthan50employeesatanyonetime = form.EmployLessThanFifty,

                dfa_farmoperation = form.DevelopingOperaton.ToString(),
                dfa_ownedandoperatedbya = form.FullTimeFarmer,
                dfa_farmoperationderivesthatpersonsmajorincom = form.MajorityIncome,

                dfa_writtenconfirmationofinsurancenotpurchase = form.CouldNotPurchaseInsurance,
                dfa_acopyofarentalagreementorlease = form.HasRentalAgreement,
                dfa_haveinvoicesreceiptsforcleanuporrepairs = form.HasReceipts,
                dfa_mostrecentlyfiledfinancialstatements = form.HasFinancialStatements,
                dfa_mostrecentcompletecorporateincometaxretur = form.HasTaxReturn,
                dfa_proofofownership = form.HasProofOfOwnership,
                dfa_alistingofthedirectors = form.HasListOfDirectors,
                dfa_orgregistrationproofunderbcsocietya = form.HasProofOfRegistration,
                dfa_organizationstructureandpurposestatemente = form.HasEligibilityDocuments,

                dfa_primaryapplicantprintname = form.SignerName,
                dfa_primaryapplicantsigneddate = form.SignatureDate,
                //signature
                dfa_secondaryapplicantprintname = form.OtherSignerName,
                dfa_secondaryapplicantsigneddate = form.OtherSignatureDate,
                //signature
            };

            foreach (var applicant in form.SecondaryApplicants)
            {
                ret.dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId.Add(new dfa_appsecondaryapplicant
                {
                    dfa_applicanttype = (int?)Enum.Parse<SecondaryApplicantTypeOptionSet>(applicant.ApplicantType.ToString()),
                    dfa_firstname = applicant.FirstName,
                    dfa_lastname = applicant.LastName,
                    dfa_emailaddress = applicant.Email,
                    dfa_phonenumber = applicant.Phone
                });
            }

            foreach (var contact in form.AltContacts)
            {
                ret.dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId.Add(new dfa_appothercontact
                {
                    dfa_name = contact.Name,
                    dfa_emailaddress = contact.Email,
                    dfa_phonenumber = contact.Phone
                });
            }

            foreach (var log in form.CleanUpLogs)
            {
                ret.dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId.Add(new dfa_appcleanuplog
                {
                    dfa_date = log.Date,
                    dfa_contactid = new dfa_appcontact
                    {
                        dfa_name = log.ContactName
                    },
                    dfa_hoursworked = log.HoursWorked,
                    dfa_name = log.DescriptionOfWork,
                });
            }

            foreach (var item in form.DamagedItems)
            {
                ret.dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId.Add(new dfa_appdamageditem
                {
                    dfa_roomname = item.RoomName,
                    dfa_damagedescription = item.Description
                });
            }

            return ret;
        }

        public static incident Map(IndForm form)
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

                //customerid_contact = Map(form.Applicant),
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

        public static dfa_appcontact Map(Applicant applicant)
        {
            return new dfa_appcontact
            {
                dfa_firstname = applicant.FirstName,
                dfa_lastname = applicant.LastName,
                dfa_initial = applicant.Title,
                dfa_residencetelephonenumber = applicant.Phone,
                dfa_cellphonenumber = applicant.Mobile,
                dfa_emailaddress = applicant.Email,
                dfa_alternatephonenumber = applicant.AlternatePhone,
                dfa_dfa_appcontact_dfa_apporganization_ContactId = new Collection<dfa_apporganization>
                {
                    new dfa_apporganization
                    {
                        dfa_name = applicant.BusinessLegalName,
                        dfa_ContactId = new dfa_appcontact
                        {
                            dfa_name = applicant.ContactName
                        }
                    }
                }
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
                dfa_damageditemname = item.Description
            };
        }


        private enum ApplicantTypeOptionSet
        {
            CharitableOrganization = 222710000,
            FarmOwner = 222710001,
            HomeOwner = 222710002,
            ResidentialTenant = 222710003,
            SmallBusinessOwner = 222710004,
            GovernmentBody = 222710005,
            Incorporated = 222710006
        }

        private enum SecondaryApplicantTypeOptionSet
        {
            Organization = 222710000,
            Contact = 222710006,
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
