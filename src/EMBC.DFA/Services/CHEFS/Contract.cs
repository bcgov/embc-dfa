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
        public string id { get; set; }
        public string formVersionId { get; set; }
        public string confirmationId { get; set; }
        public bool draft { get; set; }
        public bool deleted { get; set; }
        public IndForm submission { get; set; }
    }

    public class CHEFGovResponse
    {
        public string id { get; set; }
        public string formVersionId { get; set; }
        public string confirmationId { get; set; }
        public bool draft { get; set; }
        public bool deleted { get; set; }
        public GovForm submission { get; set; }
    }

    public class CHEFVersionResponse
    {
        public string id { get; set; }
        public IEnumerable<CHEFVersionInfo> versions { get; set; }
    }

    public class CHEFVersionInfo
    {
        public string id { get; set; }
        public bool published { get; set; }
    }
}
