using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;
using EMBC.DFA.Managers.Intake;
using Serilog;

namespace EMBC.DFA.Resources.Submissions
{
    public static class Mappings
    {
        public static dfa_appapplication Map(SmbForm form)
        {
            try
            {
                return new dfa_appapplication
                {
                    dfa_chefconfirmationnumber = form.CHEFConfirmationId,
                    dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                    dfa_indigenousstatus = form.IndigenousStatus,
                    dfa_indigenousreserve = form.OnFirstNationReserve,
                    dfa_nameoffirstnationsr = form.NameOfFirstNationsReserve,
                    dfa_comments = form.FirstNationsComments,

                    dfa_dateofdamage = form.DamageFrom,
                    dfa_dateofdamageto = form.DamageTo,

                    dfa_damagedpropertystreet1 = form.DamagePropertyAddress.AddressLine1,
                    dfa_damagedpropertystreet2 = form.DamagePropertyAddress.AddressLine2,
                    dfa_damagedpropertycitytext = form.DamagePropertyAddress.City,
                    dfa_damagedpropertyprovince = form.DamagePropertyAddress.Province,
                    dfa_damagedpropertypostalcode = form.DamagePropertyAddress.PostalCode,

                    dfa_mailingaddressstreet1 = form.MailingAddress.AddressLine1,
                    dfa_mailingaddressstreet2 = form.MailingAddress.AddressLine2,
                    dfa_mailingaddresscitytext = form.MailingAddress.City,
                    dfa_mailingaddressprovince = form.MailingAddress.Province,
                    dfa_mailingaddresspostalcode = form.MailingAddress.PostalCode,

                    dfa_manufacturedhom = form.DamageInfo.ManufacturedHome,
                    dfa_causeofdamagelos = !string.IsNullOrEmpty(form.DamageInfo.DamageType) ? (int?)Enum.Parse<DamageTypeOptionSet>(form.DamageInfo.DamageType, true) : null,
                    dfa_causeofdamageloss = form.DamageInfo.OtherDescription,
                    dfa_description = form.DamageInfo.DamageDescription,

                    dfa_accountlegalname = form.Applicant.BusinessLegalName,

                    dfa_Applicant = Map(form.Applicant),
                    dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId = Map(form.SecondaryApplicants),
                    dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId = Map(form.AltContacts),

                    dfa_businessmanagedbyallownersondaytodaybasis = (int?)(form.IsBusinessManaged.HasValue && form.IsBusinessManaged.Value ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_grossrevenues100002000000beforedisaster = (int?)(form.AreRevenuesInRange.HasValue && form.AreRevenuesInRange.Value ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_employlessthan50employeesatanyonetime = (int?)(form.EmployLessThanFifty.HasValue && form.EmployLessThanFifty.Value ? DFATwoOptions.Yes : DFATwoOptions.No),

                    dfa_farmoperation = (int?)(form.DevelopingOperaton.HasValue && form.DevelopingOperaton.Value ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_ownedandoperatedbya = (int?)(form.FullTimeFarmer.HasValue && form.FullTimeFarmer.Value ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_farmoperationderivesthatpersonsmajorincom = (int?)(form.MajorityIncome.HasValue && form.MajorityIncome.Value ? DFATwoOptions.Yes : DFATwoOptions.No),

                    dfa_writtenconfirmationofinsurancenotpurchase = form.CouldNotPurchaseInsurance,
                    dfa_acopyofarentalagreementorlease = form.HasRentalAgreement,
                    dfa_haveinvoicesreceiptsforcleanuporrepairs = form.HasReceipts,
                    dfa_mostrecentlyfiledfinancialstatements = form.HasFinancialStatements,
                    dfa_mostrecentcompletecorporateincometaxretur = form.HasTaxReturn,
                    dfa_proofofownership = form.HasProofOfOwnership,
                    dfa_alistingofthedirectors = form.HasListOfDirectors,
                    dfa_orgregistrationproofunderbcsocietya = form.HasProofOfRegistration,
                    dfa_organizationstructureandpurposestatemente = form.HasEligibilityDocuments,

                    dfa_primaryapplicantsigned = (int?)(!string.IsNullOrEmpty(form.Signature) ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_primaryapplicantprintname = form.SignerName,
                    dfa_primaryapplicantsigneddate = form.SignatureDate,
                    entityimage = Convert.FromBase64String(form.Signature.Substring(form.Signature.IndexOf(',') + 1)),
                    dfa_secondaryapplicantsigned = (int?)(!string.IsNullOrEmpty(form.OtherSignature) ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_secondaryapplicantprintname = form.OtherSignerName,
                    dfa_secondaryapplicantsigneddate = form.OtherSignatureDate,
                    //dfa_secondaryapplicantsignature = form.OtherSignature,
                    //signature
                    dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId = Map(form.CleanUpLogs),
                    dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId = Map(form.DamagedItems),
                    dfa_appapplication_dfa_appdocumentlocations_ApplicationId = Map(form.Documents),
                };
            }
            catch
            {
                Log.Error("Error mapping SmbForm to dfa_appapplication");
                throw;
            }
        }

        public static dfa_appapplication Map(IndForm form)
        {
            try
            {
                return new dfa_appapplication
                {
                    dfa_chefconfirmationnumber = form.CHEFConfirmationId,
                    dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                    dfa_indigenousstatus = form.IndigenousStatus,
                    dfa_indigenousreserve = form.OnFirstNationReserve,
                    dfa_nameoffirstnationsr = form.NameOfFirstNationsReserve,
                    dfa_comments = form.FirstNationsComments,

                    dfa_Applicant = Map(form.Applicant),

                    dfa_dateofdamage = form.DamageFrom,
                    dfa_dateofdamageto = form.DamageTo,

                    dfa_damagedpropertystreet1 = form.DamagePropertyAddress.AddressLine1,
                    dfa_damagedpropertystreet2 = form.DamagePropertyAddress.AddressLine2,
                    dfa_damagedpropertycitytext = form.DamagePropertyAddress.City,
                    dfa_damagedpropertyprovince = form.DamagePropertyAddress.Province,
                    dfa_damagedpropertypostalcode = form.DamagePropertyAddress.PostalCode,

                    dfa_mailingaddressstreet1 = form.MailingAddress.AddressLine1,
                    dfa_mailingaddressstreet2 = form.MailingAddress.AddressLine2,
                    dfa_mailingaddresscitytext = form.MailingAddress.City,
                    dfa_mailingaddressprovince = form.MailingAddress.Province,
                    dfa_mailingaddresspostalcode = form.MailingAddress.PostalCode,

                    dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId = Map(form.SecondaryApplicants),
                    dfa_dfa_appapplication_dfa_appothercontact_AppApplicationId = Map(form.AltContacts),

                    //dfa_accountlegalname = form.BuildingOwner.FirstName,
                    dfa_BuildingOwnerLandlord = Map(form.BuildingOwner),

                    dfa_manufacturedhom = form.DamageInfo.ManufacturedHome,
                    dfa_causeofdamagelos = !string.IsNullOrEmpty(form.DamageInfo.DamageType) ? (int?)Enum.Parse<DamageTypeOptionSet>(form.DamageInfo.DamageType, true) : null,
                    dfa_causeofdamageloss = form.DamageInfo.OtherDescription,
                    dfa_description = form.DamageInfo.DamageDescription,

                    dfa_doyouhaveinsurancecoverage = form.HasInsurance,
                    dfa_isthispropertyyourp = form.IsPrimaryResidence,
                    dfa_eligibleforbchomegrantonthisproperty = form.EligibleForGrant,
                    dfa_doyourlossestotalmorethan1000 = form.LossesOverOneThousand,
                    dfa_wereyouevacuatedduringtheevent = form.WasEvacuated,
                    dfa_datereturntotheresidence = form.DateReturned,
                    dfa_areyounowresidingintheresidence = form.InResidence,

                    dfa_dfa_appapplication_dfa_appoccupant_ApplicationId = Map(form.Occupants),

                    dfa_acopyofarentalagreementorlease = form.HasRentalAgreement,
                    dfa_haveinvoicesreceiptsforcleanuporrepairs = form.HasReceipts,

                    dfa_primaryapplicantsigned = (int?)(!string.IsNullOrEmpty(form.Signature) ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_primaryapplicantprintname = form.SignerName,
                    dfa_primaryapplicantsigneddate = form.SignatureDate,
                    entityimage = Convert.FromBase64String(form.Signature.Substring(form.Signature.IndexOf(',') + 1)),
                    dfa_secondaryapplicantsigned = (int?)(!string.IsNullOrEmpty(form.OtherSignature) ? DFATwoOptions.Yes : DFATwoOptions.No),
                    dfa_secondaryapplicantprintname = form.OtherSignerName,
                    dfa_secondaryapplicantsigneddate = form.OtherSignatureDate,
                    //dfa_secondaryapplicantsignature = form.OtherSignature,

                    dfa_dfa_appapplication_dfa_appcleanuplog_ApplicationId = Map(form.CleanUpLogs),
                    dfa_dfa_appapplication_dfa_appdamageditem_ApplicationId = Map(form.DamagedItems),
                    dfa_appapplication_dfa_appdocumentlocations_ApplicationId = Map(form.Documents),
                };
            }
            catch
            {
                Log.Error("Error mapping IndForm to dfa_appapplication");
                throw;
            }
        }

        public static dfa_appapplication Map(GovForm form)
        {
            try
            {
                return new dfa_appapplication
                {
                    dfa_chefconfirmationnumber = form.CHEFConfirmationId,
                    dfa_applicanttype = (int?)Enum.Parse<ApplicantTypeOptionSet>(form.ApplicantType.ToString()),
                    dfa_governmentbodylegalname = form.GovLegalName,
                    dfa_datereceived = form.Date,

                    dfa_Applicant = Map(form.Applicant),

                    dfa_mailingaddressstreet1 = !string.IsNullOrEmpty(form.MailingAddress.AddressLine1) ? form.MailingAddress.AddressLine1.Substring(0, Math.Min(100, form.MailingAddress.AddressLine1.Length)) : string.Empty,
                    dfa_mailingaddressstreet2 = form.MailingAddress.AddressLine2,
                    dfa_mailingaddresscitytext = form.MailingAddress.City,
                    dfa_mailingaddressprovince = form.MailingAddress.Province,
                    dfa_mailingaddresspostalcode = form.MailingAddress.PostalCode,

                    dfa_dfa_appapplication_dfa_appsecondaryapplicant_AppApplicationId = Map(form.SecondaryApplicants),

                    dfa_causeofdamagelos = !string.IsNullOrEmpty(form.DamageInfo.DamageType) ? (int?)Enum.Parse<DamageTypeOptionSet>(form.DamageInfo.DamageType, true) : null,
                    dfa_dateofdamage = form.DamageFrom,
                    dfa_dateofdamageto = form.DamageTo,
                    dfa_causeofdamageloss = form.DamageInfo.OtherDescription,
                    dfa_description = form.DamageInfo.DamageDescription,
                    dfa_toreceivesupportaccessingdamage = (int?)(form.WouldLikeToReceiveSupport ? DFATwoOptions.Yes : DFATwoOptions.No),
                };
            }
            catch
            {
                Log.Error("Error mapping GovForm to dfa_appapplication");
                throw;
            }
        }

        public static Collection<dfa_appsecondaryapplicant> Map(List<SecondaryApplicant> applicants)
        {
            var ret = new Collection<dfa_appsecondaryapplicant>();

            foreach (var applicant in applicants)
            {
                ret.Add(new dfa_appsecondaryapplicant
                {
                    dfa_applicanttype = (int?)Enum.Parse<SecondaryApplicantTypeOptionSet>(applicant.ApplicantType.ToString()),
                    dfa_firstname = applicant.FirstName,
                    dfa_lastname = applicant.LastName,
                    dfa_title = applicant.Title ?? string.Empty,
                    dfa_emailaddress = applicant.Email,
                    dfa_phonenumber = applicant.Phone,
                    dfa_organizationname = applicant.ApplicantType == SecondaryApplicantType.Organization ? applicant.LastName + ", " + applicant.FirstName : String.Empty,
                });
            }

            return ret;
        }

        public static Collection<dfa_appothercontact> Map(List<AltContact> contacts)
        {
            var ret = new Collection<dfa_appothercontact>();

            foreach (var contact in contacts)
            {
                ret.Add(new dfa_appothercontact
                {
                    dfa_name = contact.FirstName + " " + contact.LastName,
                    dfa_firstname = contact.FirstName,
                    dfa_lastname = contact.LastName,
                    dfa_emailaddress = contact.Email,
                    dfa_phonenumber = contact.Phone
                });
            }

            return ret;
        }

        public static Collection<dfa_appoccupant> Map(List<Occupant> occupants)
        {
            var ret = new Collection<dfa_appoccupant>();

            foreach (var occupant in occupants)
            {
                ret.Add(new dfa_appoccupant
                {
                    dfa_name = occupant.LastName + ", " + occupant.FirstName,
                    dfa_ContactId = new dfa_appcontact
                    {
                        dfa_firstname = occupant.FirstName,
                        dfa_lastname = occupant.LastName,
                        dfa_title = occupant.Relationship
                    },
                });
            }

            return ret;
        }

        public static Collection<dfa_appcleanuplog> Map(List<CleanUpLog> cleanUpLogs)
        {
            var ret = new Collection<dfa_appcleanuplog>();

            foreach (var log in cleanUpLogs)
            {
                ret.Add(new dfa_appcleanuplog
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

            return ret;
        }

        public static Collection<dfa_appdamageditem> Map(List<DamageItem> damagedItems)
        {
            var ret = new Collection<dfa_appdamageditem>();

            foreach (var item in damagedItems)
            {
                ret.Add(new dfa_appdamageditem
                {
                    dfa_roomname = item.RoomName,
                    dfa_damagedescription = item.Description
                });
            }

            return ret;
        }

        public static Collection<dfa_appdocumentlocations> Map(List<AttachmentData> documents)
        {
            var ret = new Collection<dfa_appdocumentlocations>();

            foreach (var doc in documents)
            {
                ret.Add(new dfa_appdocumentlocations
                {
                    dfa_name = doc.FileName,
                    dfa_documenttype = doc.FileType,
                    dfa_url = doc.CHEFSubmissionId,
                });
            }

            return ret;
        }

        public static dfa_appcontact Map(Applicant applicant)
        {
            var ret = new dfa_appcontact
            {
                dfa_firstname = applicant.FirstName,
                dfa_lastname = applicant.LastName,
                dfa_initial = applicant.Initial,
                dfa_title = applicant.Title,
                dfa_residencetelephonenumber = applicant.Phone,
                dfa_cellphonenumber = applicant.Mobile,
                dfa_emailaddress = applicant.Email,
                dfa_alternatephonenumber = applicant.AlternatePhone,
            };

            if (!string.IsNullOrEmpty(applicant.ContactName))
            {
                ret.dfa_dfa_appcontact_dfa_apporganization_ContactId = new Collection<dfa_apporganization>
                {
                    new dfa_apporganization
                    {
                        dfa_ContactId = new dfa_appcontact
                        {
                            dfa_name = applicant.ContactName
                        }
                    }
                };
            }

            return ret;
        }

        public static dfa_appbuildingownerlandlord Map(BuildingOwner owner)
        {
            return new dfa_appbuildingownerlandlord
            {
                dfa_contactfirstname = owner.FirstName,
                dfa_contactlastname = owner.LastName,
                dfa_contactphone1 = owner.Phone,
                dfa_contactemail = owner.Email,
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


        public enum ApplicantTypeOptionSet
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
            Wildfire = 222710004,
        }
    }
}
