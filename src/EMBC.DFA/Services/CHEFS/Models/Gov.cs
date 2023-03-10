namespace EMBC.DFA.Services.CHEFS
{
    public class GovForm
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? SubmissionId { get; set; }
        public string? ConfirmationId { get; set; }
        public GovFormData data { get; set; }
        public Metadata metadata { get; set; }
    }

    public class GovFormData
    {
        public string indigenousGoverningBodyAndLocalGovernmentApplicationForDisasterFinancialAssistanceDfa1 { get; set; } //Legal Name
        public string date { get; set; }
        public string primaryContactNameLastFirst { get; set; } //Primary Contact First name
        public string primaryContactNameLastFirst1 { get; set; } //Primary Contact Last name
        public string title { get; set; } //Primary Contact title
        public string cellularTelephoneNumber2 { get; set; } //Primary Contact Cell
        public string businessTelephoneNumber2 { get; set; } //Primary Contact Business number
        public string eMailAddress2 { get; set; } //Primary Contact email
        public string businessTelephoneNumber1 { get; set; } //alt business number
        public string mailingAddress { get; set; } //Mailing address street 1
        public string street2 { get; set; } //Mailing addres street 2
        public string cityTown { get; set; } //Mailing addres 
        public string province { get; set; } //Mailing addres
        public string postalCode { get; set; } //Mailing addres
        public GOV_AlternateContact[] alternateContacts { get; set; }
        public string flooding { get; set; } //Cause of damage type...
        public string pleaseSpecifyIfSelectedOthers { get; set; } //Other damage loss
        public string dateOfDamageLoss { get; set; } //From
        public string dateOfDamageLoss1 { get; set; } //To
        public string provideABriefDescriptionOfDamage { get; set; }
        public string ifThereWasOpportunityToReceiveGuidanceAndSupportInAssessingYourDamagedInfrastructureWouldYouLikeToReceiveThisSupport { get; set; }
        public bool submit1 { get; set; }
    }

    public class GOV_AlternateContact
    {
        public string eMailAddress1 { get; set; }
        public string? nameOfAdditionalContact { get; set; }
        public string cellularTelephoneNumber1 { get; set; }
        public string applicantType { get; set; }
        public string FirstnameOfAdditionalContact { get; set; }
        public string lastNameOfContact { get; set; }
        public string title { get; set; }
    }
}
