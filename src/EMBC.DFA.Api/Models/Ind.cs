namespace EMBC.DFA.Api.Models
{
    public class IndForm
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
        public DateTime dateOfDamage { get; set; } //From
        public DateTime dateOfDamage1 { get; set; } //To
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
        public string businessTelephoneNumber { get; set; } //Residence Telephone Number
        public string cellularTelephoneNumber { get; set; }
        public string eMailAddress { get; set; }
        public string alternateContactNameWhereYouCanBeReachedIfApplicable { get; set; }
        public string alternatePhoneNumber { get; set; }
        public string provideRegisteredBuildingOwnerSAndOrLandlordSNameS { get; set; }
        public string contactTelephoneNumberS { get; set; }
        public string contactTelephoneNumberS1 { get; set; }
        public ManufacturedHome manufacturedHome { get; set; }
        //public DamageLoss causeOfDamageLoss { get; set; }
        public string causeOfDamageLoss1 { get; set; }
        public string pleaseSpecifyIfOthers { get; set; } //Other damage loss
        public string provideABriefDescriptionOfDamage { get; set; }
        public string yes { get; set; } //Do you have insurance coverage for the damage/loss that incurred?
        public string yes1 { get; set; } //As the Home Owner or Tenant, do you occupy this property as your principal residence?
        public string yes2 { get; set; } //As the Home Owner, are you eligible for a BC Home Owner Grant for this property?
        public string yes3 { get; set; } //Excluding luxury/non-essential items and landscaping, do your losses total more than $1,000?
        public string yes4 { get; set; } //Were you evacuated during the event?
        public string yes5 { get; set; } //Are you now residing in the residence?
        public Occupant[] occupants { get; set; }
        public bool aCopyOfARentalAgreementOrLeaseIfApplicableForResidentialTenantApplication { get; set; }
        public bool ifYouHaveInvoicesReceiptsForCleanupOrRepairsPleaseHaveThemAvailableDuringTheSiteMeetingToHelpTheEvaluatorIdentifyEligibleCosts { get; set; }
        public string signature1 { get; set; }
        public string printName1 { get; set; }
        public DateTime dateYyyyMDay1 { get; set; }
        public string signature2 { get; set; }
        public string printName2 { get; set; }
        public DateTime dateYyyyMDay2 { get; set; }
        public string applicantFirstName { get; set; } //Appendix A Applicant First Name
        public string applicantFirstName1 { get; set; } //Appendix A Applicant Last Name
        public CleanUpDetail[] cleanupLogDetails { get; set; }
        public string applicantFirstName2 { get; set; } //Appendix B Applicant First Name
        public string applicantFirstName3 { get; set; } //Appendix B Applicant Last Name
        public DamagedItem[] listByRoomItemsSubmittedForDamageAssessment { get; set; }
        public bool submit1 { get; set; }
        public string? nameOfFirstNationsReserve { get; set; }
        public DateTime date { get; set; }
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
    }


#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
