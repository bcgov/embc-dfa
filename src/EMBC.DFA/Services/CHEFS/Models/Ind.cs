using System;

namespace EMBC.DFA.Services.CHEFS
{
    public class IndForm
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? SubmissionId { get; set; }
        public string? ConfirmationId { get; set; }
        public IndFormData data { get; set; }
        public Metadata metadata { get; set; }
    }

    public class IndFormData
    {
        public string pleaseCheckAppropriateBox { get; set; } //Applicant Type
        public string yes7 { get; set; } //Indigenous Status
        public string yes6 { get; set; } //On First Nations Reserve?

        public string primaryContactNameLastFirst { get; set; } //Applicant First name
        public string primaryContactNameLastFirst1 { get; set; } //Applicant Last name
        public string primaryContactNameLastFirst2 { get; set; } //Applicant Initial
        public string cellularTelephoneNumber { get; set; } //Applicant Cell Number
        public string businessTelephoneNumber { get; set; } //Applicant Residence Telephone Number
        public string eMailAddress { get; set; } //Applicant Email
        public string AlternatePhoneNumberofPrimarycontact { get; set; } //Applicant Alt Phone

        public IND_SecondaryApplicant[] SecondaryApplicant { get; set; } = Array.Empty<IND_SecondaryApplicant>();
        public IND_OtherContac[] otherContacts { get; set; } = Array.Empty<IND_OtherContac>();

        public string street1 { get; set; } //Damaged Property Address street 1
        public string street3 { get; set; } //Damaged Property Address Street 2
        public string cityTown1 { get; set; } //Damaged Property Address
        public string province1 { get; set; } //Damaged Property Address
        public string postalCode1 { get; set; } //Damaged Property Address

        public string mailingAddress { get; set; } //Mailing address street 1
        public string street2 { get; set; } //Mailing addres street 2
        public string cityTown { get; set; } //Mailing addres 
        public string province { get; set; } //Mailing addres
        public string postalCode { get; set; } //Mailing addres

        public string provideRegisteredBuildingOwnerSAndOrLandlordSNameS { get; set; }
        public string contactTelephoneNumberS { get; set; }
        public string emailOfLandlord { get; set; }

        public string dateOfDamageFrom { get; set; } //From
        public string dateOfDamageTo { get; set; } //To
        public ManufacturedHome manufacturedHome { get; set; }
        public string causeOfDamageLoss1 { get; set; }
        public string pleaseSpecifyIfOthers { get; set; } //Other damage loss
        public string provideABriefDescriptionOfDamage { get; set; }

        public string yes { get; set; } //Do you have insurance coverage for the damage/loss that incurred?
        public string yes1 { get; set; } //As the Home Owner or Tenant, do you occupy this property as your principal residence?
        public string yes2 { get; set; } //As the Home Owner, are you eligible for a BC Home Owner Grant for this property?
        public string yes3 { get; set; } //Excluding luxury/non-essential items and landscaping, do your losses total more than $1,000?
        public string yes4 { get; set; } //Were you evacuated during the event?
        public string yes5 { get; set; } //Are you now residing in the residence?

        public Occupant[] occupants { get; set; } = Array.Empty<Occupant>();

        public bool aCopyOfARentalAgreementOrLeaseIfApplicableForResidentialTenantApplication { get; set; }
        public bool ifYouHaveInvoicesReceiptsForCleanupOrRepairsPleaseHaveThemAvailableDuringTheSiteMeetingToHelpTheEvaluatorIdentifyEligibleCosts { get; set; }

        public string signature1 { get; set; }
        public string printName1 { get; set; }
        public string dateYyyyMDay1 { get; set; }

        public string signature2 { get; set; }
        public string printName2 { get; set; }
        public string dateYyyyMDay2 { get; set; }

        public CleanUpDetail[] cleanupLogDetails { get; set; } = Array.Empty<CleanUpDetail>();
        public DamagedItem[] listByRoomItemsSubmittedForDamageAssessment { get; set; } = Array.Empty<DamagedItem>();
        public CHEF_Attachment[] completedInsuranceTemplate { get; set; } = Array.Empty<CHEF_Attachment>();
        public CHEF_Attachment[] governmentIssuedIdDlServiceCardBcId { get; set; } = Array.Empty<CHEF_Attachment>();
        public CHEF_Attachment[] signedTenancyAgreementIfNoTenancyAgreementPleaseProvideTheLandlordContactInformationAndAPieceOfMailWithTheAddress { get; set; } = Array.Empty<CHEF_Attachment>();

        public bool submit1 { get; set; }
        public string? nameOfFirstNationsReserve { get; set; }
        public string? Comments { get; set; }
        public string? date { get; set; }
    }

    public class CHEF_Attachment
    {
        public string storage { get; set; }
        public string url { get; set; }
        public int size { get; set; }
        public CEHF_Data data { get; set; }
        public string originalName { get; set; }
    }

    public class CEHF_Data
    {
        public string id { get; set; }
    }

    public class IND_SecondaryApplicant
    {
        public string? applicantType { get; set; }
        public string FirstNameofsecondary { get; set; }
        public string? LastNameofSecondary { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }

        public string? hoursWorked { get; set; }
        public string? dateYyyyMDay { get; set; }
        public string? descriptionOfWork { get; set; }
        public string? embcOfficeUseOnly { get; set; }
        public string? nameOfFamilyMemberVolunteer { get; set; }
    }

    public class IND_OtherContac
    {
        public string AlternateContactName { get; set; } //First Name
        public string AlternateContactName1 { get; set; } //Last Name
        public string AlternateEmailAddress { get; set; }
        public string AlternateContactPhone { get; set; }
    }

    public class ManufacturedHome
    {
        public bool yes { get; set; }
        public bool no { get; set; }
    }

    public class Occupant
    {
        public string listTheNamesOfAllFullTimeOccupantsWhoResidedInTheHomeAtTheTimeOfTheEvent { get; set; } //Occupant First Name
        public string listTheNamesOfAllFullTimeOccupantsWhoResidedInTheHomeAtTheTimeOfTheEvent1 { get; set; } //Occupant Last Name
        public string relationshipTitle { get; set; } //Relationship to Occupant
    }


#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
