using System;
using System.Text.Json.Serialization;

namespace EMBC.DFA.Services.CHEFS
{
    public class SmbForm
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? SubmissionId { get; set; }
        public string? ConfirmationId { get; set; }
        public SmbFormData data { get; set; }
        public Metadata metadata { get; set; }
    }

    public class SmbFormData
    {
        public string pleaseSelectTheAppropriateOption { get; set; } //Applicant Type

        public string yes7 { get; set; } //Indigenous Status
        public string yes6 { get; set; } //On First Nations Reserve?
        public string? nameOfFirstNationsReserve { get; set; }
        public string? comments { get; set; } //On First Nations Reserve Comments

        public string primaryContactNameLastFirst { get; set; } //Applicant First name
        public string primaryContactNameLastFirst3 { get; set; } //Applicant Last name
        public string primaryContactNameLastFirst4 { get; set; } //Applicant Initial
        public string businessTelephoneNumber1 { get; set; } //Applicant Residence Telephone Number
        public string cellularTelephoneNumber1 { get; set; } //Applicant Cell Number
        public string eMailAddress2 { get; set; } //Applicant Email
        public string alternatePhoneNumber1 { get; set; } //Applicant Alt Phone

        public string primaryContactNameLastFirst1 { get; set; } //Business, Farm or Organization Legal Name
        public string primaryContactNameLastFirst2 { get; set; } //Name of Contact Person

        public SMB_SecondaryApplicant[] cleanupLogDetails1 { get; set; } = Array.Empty<SMB_SecondaryApplicant>();
        public AlternateContact[] otherContacts { get; set; } = Array.Empty<AlternateContact>();

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

        public string dateOfDamage { get; set; } //From
        public string dateOfDamage1 { get; set; } //To

        public string causeOfDamageLoss1 { get; set; } //Damage loss string
        public string pleaseSpecify { get; set; } //Other damage loss
        public string provideABriefDescriptionOfDamage { get; set; }

        public string pleaseDecideToChooseAOrB { get; set; } //ApplicantType - Small Business or Farm Owner
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
        public string dateYyyyMDay1 { get; set; }
        public string signature2 { get; set; }
        public string printName2 { get; set; }
        public string? dateYyyyMDay2 { get; set; }

        public CleanUpDetail[] cleanupLogDetails { get; set; } = Array.Empty<CleanUpDetail>();

        public DamagedItem[] listByRoomItemsSubmittedForDamageAssessment { get; set; } = Array.Empty<DamagedItem>();
        public CHEF_Attachment[] completedInsuranceTemplate1 { get; set; } = Array.Empty<CHEF_Attachment>();
        public CHEF_Attachment[] leaseAgreementsIfApplicable { get; set; } = Array.Empty<CHEF_Attachment>();
        public CHEF_Attachment[] financialDocuments { get; set; } = Array.Empty<CHEF_Attachment>();

        public bool submit1 { get; set; }

        //Small Business Applicant only
        public string? yes { get; set; } //Is your business managed by all owners of the business on a day to day basis?
        public string? yes1 { get; set; } //Are the gross revenues of the business more than $10,000 but less than $2 million in the year before the disaster?
        public string? yes2 { get; set; } //Does the business employ less than 50 employees at any one time?

        //Farm Owner Applicant only
        public string? yes3 { get; set; } //Is the farm operation identified in the current assessment of the British Columbia Assessment Authority as a developing or established agricultural operation?
        public string? yes4 { get; set; } //Is the farm operation owned and operated by a person(s) who full-time employment is as a farmer?
        public string? yes5 { get; set; } //Is the farm operation the means by which the owner(s) derives the majority of that person’s income?
    }

    public class AlternateContact
    {
        public string alternateContactNameWhereYouCanBeReachedIfApplicable { get; set; } //First Name
        public string alternateContactNameWhereYouCanBeReachedIfApplicable1 { get; set; } //Last Name
        public string eMailAddress1 { get; set; }
        public string alternatePhoneNumber { get; set; }
    }

    public class SMB_SecondaryApplicant
    {
        public string? applicantType { get; set; }
        public string FirstNameofSecondary { get; set; }
        public string FirstNameofSecondary1 { get; set; } //Secondary Applicant Last Name!!!!
        public string? LastNameofSecondary { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }

        public string? hoursWorked { get; set; }
        public string? dateYyyyMDay { get; set; }
        public string? descriptionOfWork { get; set; }
        public string? embcOfficeUseOnly { get; set; }
        public string? nameOfFamilyMemberVolunteer { get; set; }
    }

    public class CleanUpDetail
    {
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? hoursWorked { get; set; }
        public string dateYyyyMDay { get; set; }
        public string descriptionOfWork { get; set; }
        public string nameOfFamilyMemberVolunteer { get; set; }
        public string? embcOfficeUseOnly { get; set; }
    }

    public class DamagedItem
    {
        public string listByRoomItemsSubmittedForDamageAssessment1 { get; set; }
        public string roomName { get; set; }
        public string? embcOfficeUseOnlyComments { get; set; }
    }

    public class Metadata
    {
    }


#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
