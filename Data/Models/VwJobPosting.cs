using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJobPosting
    {
        public int JobHistoryPkid { get; set; }
        public string EmployeeCode { get; set; }
        public string DepartmentName { get; set; }
        public string RankTypeCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RankType { get; set; }
        public string JobYear { get; set; }
        public string JobMonth { get; set; }
        public string JobDay { get; set; }
        public string DepartmentName1 { get; set; }
        public string Remark { get; set; }
        public bool? IsCurrent { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
