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
        public string yes7 { get; set; }
        public string yes6 { get; set; }
        public string primaryContactNameLastFirst { get; set; }
        public string primaryContactNameLastFirst3 { get; set; }
        public string primaryContactNameLastFirst4 { get; set; }
        public DateTime dateOfDamage { get; set; }
        public string dateOfDamage1 { get; set; }
        public string primaryContactNameLastFirst1 { get; set; }
        public string primaryContactNameLastFirst2 { get; set; }
        public string street1 { get; set; }
        public string street3 { get; set; }
        public string cityTown1 { get; set; }
        public string province1 { get; set; }
        public string postalCode1 { get; set; }
        public string mailingAddress { get; set; }
        public string street2 { get; set; }
        public string cityTown { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string businessTelephoneNumber { get; set; }
        public string cellularTelephoneNumber { get; set; }
        public string eMailAddress { get; set; }
        public string eMailAddress1 { get; set; }
        public string alternateContactNameWhereYouCanBeReachedIfApplicable { get; set; }
        public string alternatePhoneNumber { get; set; }
        public Causeofdamageloss causeOfDamageLoss { get; set; }
        public string pleaseSpecify { get; set; }
        public string provideABriefDescriptionOfDamage { get; set; }
        public string pleaseDecideToChooseAOrB { get; set; }
        public bool yes8 { get; set; }
        public bool yes9 { get; set; }
        public bool yes10 { get; set; }
        public bool yes11 { get; set; }
        public bool yes12 { get; set; }
        public bool yes13 { get; set; }
        public bool yes14 { get; set; }
        public bool yes15 { get; set; }
        public bool yes16 { get; set; }
        public string signature1 { get; set; }
        public string printName1 { get; set; }
        public DateTime dateYyyyMDay1 { get; set; }
        public string signature2 { get; set; }
        public string printName2 { get; set; }
        public DateTime dateYyyyMDay2 { get; set; }
        public string applicantFirstName2 { get; set; }
        public string applicantFirstName3 { get; set; }
        public Cleanuplogdetail[] cleanupLogDetails { get; set; }
        public string applicantFirstName { get; set; }
        public string applicantFirstName1 { get; set; }
        public Listbyroomitemssubmittedfordamageassessment[] listByRoomItemsSubmittedForDamageAssessment { get; set; }
        public bool submit1 { get; set; }
        public string nameOfFirstNationsReserve { get; set; }
        public string yes { get; set; }
        public string yes1 { get; set; }
        public string yes2 { get; set; }
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
        public string hoursWorked { get; set; }
        public DateTime dateYyyyMDay { get; set; }
        public string descriptionOfWork { get; set; }
        public string embcOfficeUseOnly { get; set; }
        public string nameOfFamilyMemberVolunteer { get; set; }
    }

    public class Listbyroomitemssubmittedfordamageassessment
    {
        public string embcOfficeUseOnlyComments { get; set; }
        public string listByRoomItemsSubmittedForDamageAssessment1 { get; set; }
    }

    public class Metadata
    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
