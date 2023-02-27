using System;
using System.Collections.Generic;
using System.Text;

namespace EMBC.DFA.Services.CHEFS
{
    public class CHEFSmbResponse
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string id { get; set; }
        public string formVersionId { get; set; }
        public string confirmationId { get; set; }
        public bool draft { get; set; }
        public bool deleted { get; set; }
        public SmbForm submission { get; set; }
    }

    public class CHEFIndResponse
    {
        public string Id { get; set; }
        public string FormVersionId { get; set; }
        public string ConfirmationId { get; set; }
        public string Draft { get; set; }
        public string Deleted { get; set; }
        public IndForm Submission { get; set; }
    }

    public class CHEFGovResponse
    {
        public string Id { get; set; }
        public string FormVersionId { get; set; }
        public string ConfirmationId { get; set; }
        public string Draft { get; set; }
        public string Deleted { get; set; }
        public GovForm Submission { get; set; }
    }
}
