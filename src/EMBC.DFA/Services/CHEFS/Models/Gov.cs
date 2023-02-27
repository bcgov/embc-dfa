using System;

namespace EMBC.DFA.Services.CHEFS
{
    public class GovForm
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? SubmissionId { get; set; }
        public GovFormData data { get; set; }
        public Metadata metadata { get; set; }
    }

    public class GovFormData
    {
        public string indigenousGoverningBodyAndLocalGovernmentApplicationForDisasterFinancialAssistanceDfa1 { get; set; } //Legal Name
        public DateTime date { get; set; }
        public string primaryContactNameLastFirst { get; set; } //Primary Contact First name
        public string primaryContactNameLastFirst1 { get; set; } //Primary Contact Last name
        public string title { get; set; } //Primary Contact title
        public string mailingAddress { get; set; } //Mailing address street 1
        public string street2 { get; set; } //Mailing addres street 2
        public string cityTown { get; set; } //Mailing addres 
        public string province { get; set; } //Mailing addres
        public string postalCode { get; set; } //Mailing addres
        public string businessTelephoneNumber { get; set; }
        public string cellularTelephoneNumber { get; set; }
        public string eMailAddress { get; set; }
        public string businessTelephoneNumber1 { get; set; } //alt business number
        public string cellularTelephoneNumber1 { get; set; } //alt cell number
        public string eMailAddress1 { get; set; } //alt email
        //public DamageLoss causeOfDamageLoss { get; set; }
        public string causeOfDamageLoss1 { get; set; }
        public string pleaseSpecifyIfOthers { get; set; } //Other damage loss
        public DateTime dateOfDamageLoss { get; set; } //From
        public DateTime dateOfDamageLoss1 { get; set; } //To
        public string provideABriefDescriptionOfDamage { get; set; }
        public bool submit1 { get; set; }
    }
}
