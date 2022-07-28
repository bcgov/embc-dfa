namespace EMBC.DFA.Api.Models
{
    public class SmbForm
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Data data { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Data
    {
        public string pleaseSelectTheAppropriateOption { get; set; }
        public string yes7 { get; set; } //Indigenous Status
        public string yes6 { get; set; } //On First Nations Reserve?
        public string nameOfFirstNationsReserve { get; set; }
        public string primaryContactNameLastFirst { get; set; } //Applicant First name
        public string primaryContactNameLastFirst3 { get; set; } //Applicant Last name
        public string primaryContactNameLastFirst4 { get; set; } //Applicant Initial
        public DateTime dateOfDamage { get; set; } //From
        public string dateOfDamage1 { get; set; } //To
        public string primaryContactNameLastFirst1 { get; set; } //Business, Farm or Organization Legal Name
        public string primaryContactNameLastFirst2 { get; set; } //Name of Contact Person
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
        public string eMailAddress1 { get; set; }
        public string alternateContactNameWhereYouCanBeReachedIfApplicable { get; set; }
        public string alternatePhoneNumber { get; set; }
        public Causeofdamageloss causeOfDamageLoss { get; set; }
        public string pleaseSpecify { get; set; } //Other
        public string provideABriefDescriptionOfDamage { get; set; }
        public string pleaseDecideToChooseAOrB { get; set; }
        public string yes { get; set; } //Is your business managed by all owners of the business on a day to day basis?
        public string yes1 { get; set; } //Are the gross revenues of the business more than $10,000 but less than $2 million in the year before the disaster?
        public string yes2 { get; set; } //Does the business employ less than 50 employees at any one time?
        public bool yes3 { get; set; } //Is the farm operation identified in the current assessment of the British Columbia Assessment Authority as a developing or established agricultural operation?
        public bool yes4 { get; set; } //Is the farm operation owned and operated by a person(s) who full-time employment is as a farmer?
        public bool yes5 { get; set; } //Is the farm operation the means by which the owner(s) derives the majority of that person’s income?
        public bool yes8 { get; set; } //Written confirmation from your insurance broker/agent that you could not have purchased insurance to cover the loss to your small business, farm or charitable organization
        public bool yes9 { get; set; } //A copy of a rental agreement or lease, if applicable.
        public bool yes10 { get; set; } //If you have invoices/receipts for cleanup or repairs, please have them available during the site meeting to help the evaluator identify eligible costs.
        public bool yes11 { get; set; } //The most recently filed financial statements (income statement and balance sheet) used for income tax purposes
        public bool yes12 { get; set; } //The most recently filed complete corporate income tax return, with all supporting schedules.
        public bool yes13 { get; set; } //Proof of ownership (Central Securities Register listing all shareholders or Partnership Agreement)
        public bool yes14 { get; set; } //A listing of the Directors, including their contact and address information
        public bool yes15 { get; set; } //Proof of the organization’s registration (must include registration date) under the BC Society Act
        public bool yes16 { get; set; } //A statement outlining the organization’s structure and purpose, and any other documentation supporting how the organization meets the eligibility criteria for Disaster Financial Assistance
        public string signature1 { get; set; }
        public string printName1 { get; set; }
        public DateTime dateYyyyMDay1 { get; set; }
        public string signature2 { get; set; }
        public string printName2 { get; set; }
        public DateTime dateYyyyMDay2 { get; set; }
        public string applicantFirstName2 { get; set; } //Appendix A Applicant First Name
        public string applicantFirstName3 { get; set; } //Appendix A Applicant Last Name
        public Cleanuplogdetail[] cleanupLogDetails { get; set; }
        public string applicantFirstName { get; set; } //Appendix B Applicant First Name
        public string applicantFirstName1 { get; set; } //Appendix B Applicant Last Name
        public Listbyroomitemssubmittedfordamageassessment[] listByRoomItemsSubmittedForDamageAssessment { get; set; }
        public bool submit1 { get; set; }
    }

    public class Causeofdamageloss
    {
        public bool flooding { get; set; }
        public bool landslide { get; set; }
        public bool windstorm { get; set; }
        public bool other { get; set; }
    }

    public class Cleanuplogdetail
    {
        public DateTime dateYyyyMDay { get; set; }
        public string nameOfFamilyMemberVolunteer { get; set; }
        public string hoursWorked { get; set; }
        public string descriptionOfWork { get; set; }
        //public string embcOfficeUseOnly { get; set; } //
    }

    public class Listbyroomitemssubmittedfordamageassessment
    {
        public string listByRoomItemsSubmittedForDamageAssessment1 { get; set; }
        //public string embcOfficeUseOnlyComments { get; set; }
    }

    public class Metadata
    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
