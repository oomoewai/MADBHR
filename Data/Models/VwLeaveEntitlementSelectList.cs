using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwLeaveEntitlementSelectList
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string CurrentRank { get; set; }
        public string Township { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Period { get; set; }
        public string LeaveTypeCode { get; set; }
        public string UploadForTownship { get; set; }
    }
}
