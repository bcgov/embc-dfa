using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMBC.DFA.Managers.Intake
{
    public interface IIntakeManager
    {
        Task<string> Handle(IntakeCommand cmd);

        Task<IntakeQueryResults> Handle(IntakeQuery query);
    }

    public abstract class IntakeCommand
    { }

    public abstract class IntakeQuery
    { }

    public abstract class IntakeQueryResults
    { }

    public class NewSmbFormSubmissionCommand : IntakeCommand
    {
        public SmbForm Form { get; set; } = null!;
    }

    public class NewGovFormSubmissionCommand : IntakeCommand
    {
        public GovForm Form { get; set; } = null!;
    }

    public class NewIndFormSubmissionCommand : IntakeCommand
    {
        public IndForm Form { get; set; } = null!;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class SmbForm
    {
        public ApplicantType ApplicantType { get; set; }
        public bool IndigenousStatus { get; set; }
        public bool OnFirstNationReserve { get; set; }
        public string? NameOfFirstNationsReserve { get; set; }
        public string? FirstNationsComments { get; set; }

        public Applicant Applicant { get; set; }

        public List<OtherApplicant> SecondaryApplicants { get; set; }
        public List<AltContact> AltContacts { get; set; }

        public Address DamagePropertyAddress { get; set; }
        public Address MailingAddress { get; set; }

        public DateTime DamageFrom { get; set; }
        public DateTime DamageTo { get; set; }

        public DamageInfo DamageInfo { get; set; }

        //Small Business Only
        public bool? IsBusinessManaged { get; set; }
        public bool? AreRevenuesInRange { get; set; }
        public bool? EmployLessThanFifty { get; set; }

        //Farm Owner Only
        public bool? DevelopingOperaton { get; set; }
        public bool? FullTimeFarmer { get; set; }
        public bool? MajorityIncome { get; set; }

        public bool CouldNotPurchaseInsurance { get; set; }
        public bool HasRentalAgreement { get; set; }
        public bool HasReceipts { get; set; }
        public bool HasFinancialStatements { get; set; }
        public bool HasTaxReturn { get; set; }
        public bool HasProofOfOwnership { get; set; }
        public bool HasListOfDirectors { get; set; }
        public bool HasProofOfRegistration { get; set; }
        public bool HasEligibilityDocuments { get; set; }

        public string Signature { get; set; }
        public string SignerName { get; set; }
        public DateTime SignatureDate { get; set; }
        public string OtherSignature { get; set; }
        public string OtherSignerName { get; set; }
        public DateTime? OtherSignatureDate { get; set; }

        public List<CleanUpLog> CleanUpLogs { get; set; }
        public List<DamageItem> DamagedItems { get; set; }
    }

    public class IndForm
    {
        public ApplicantType ApplicantType { get; set; }
        public bool IndigenousStatus { get; set; }
        public bool OnFirstNationReserve { get; set; }
        public string? NameOfFirstNationsReserve { get; set; }

        public Applicant Applicant { get; set; }
        public DateTime DamageFrom { get; set; }
        public DateTime DamageTo { get; set; }

        public Address? DamagePropertyAddress { get; set; }
        public Address? MailingAddress { get; set; }

        public string? AlternateContact { get; set; }
        public string? AlternatePhoneNumber { get; set; }

        public BuildingOwner BuildingOwner { get; set; }

        public DamageInfo DamageInfo { get; set; }

        public bool HasInsurance { get; set; }
        public bool IsPrimaryResidence { get; set; }
        public bool EligibleForGrant { get; set; }
        public bool LossesOverOneThousand { get; set; }
        public bool WasEvacuated { get; set; }
        public DateTime? DateReturned { get; set; }
        public bool InResidence { get; set; }

        public Occupant[] Occupants { get; set; }
        public CleanUpLog[] CleanUpLogs { get; set; }
        public DamageItem[] DamagedItems { get; set; }

        //Not seeing these fields anywhere in CRM
        //public string Signature { get; set; }
        //public string SignerName { get; set; }
        //public DateTime SignatureDate { get; set; }
        //public string OtherSignature { get; set; }
        //public string OtherSignerName { get; set; }
        //public DateTime OtherSignatureDate { get; set; }
        //public string AppendixAFirstName { get; set; }
        //public string AppendixALastName { get; set; }
        //public string AppendixBFirstName { get; set; }
        //public string AppendixBLastName { get; set; }
        //public bool IsSubmit { get; set; }
    }

    public class GovForm
    {
        public ApplicantType ApplicantType { get; set; }
        public string GovLegalName { get; set; }
        public DateTime Date { get; set; }

        public Applicant Applicant { get; set; }
        public Address? MailingAddress { get; set; }

        public string? AlternatePhoneNumber { get; set; }
        public string? AlternateCellNumber { get; set; }

        public DateTime DamageFrom { get; set; }
        public DateTime DamageTo { get; set; }
        public DamageInfo DamageInfo { get; set; }
    }

    public class Applicant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Initial { get; set; }
        public string? Title { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Mobile { get; set; }
        public string? AlternatePhone { get; set; }
        public string BusinessLegalName { get; set; }
        public string ContactName { get; set; }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }

    public class BuildingOwner
    {
        public string? Name { get; set; }
        public string? phone1 { get; set; }
        public string? phone2 { get; set; }
    }

    public class DamageInfo
    {
        public bool? ManufacturedHome { get; set; }
        public string DamageType { get; set; }
        public string OtherDescription { get; set; }
        public string DamageDescription { get; set; }
    }

    public class Occupant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class OtherApplicant
    {
        public SecondaryApplicantType ApplicantType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class AltContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class CleanUpLog
    {
        public int HoursWorked { get; set; }
        public DateTime Date { get; set; }
        public string DescriptionOfWork { get; set; }
        public string ContactName { get; set; }
    }

    public class DamageItem
    {
        public string RoomName { get; set; }
        public string Description { get; set; }
    }

    public enum ApplicantType
    {
        SmallBusinessOwner,
        FarmOwner,
        CharitableOrganization,
        HomeOwner,
        ResidentialTenant,
        GovernmentBody
    }

    public enum SecondaryApplicantType
    {
        Organization,
        Contact
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
