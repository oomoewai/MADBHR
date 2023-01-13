using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwLeaveEntitlement
    {
        public int LeaveEntitlementPkid { get; set; }
        public string EmployeeCode { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedNo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Period { get; set; }
        public string LeaveTypeCode { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
