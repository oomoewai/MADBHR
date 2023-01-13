using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwTrainingHistory
    {
        public int TrainingHistoryPkid { get; set; }
        public string EmployeeCode { get; set; }
        public string BranchCode { get; set; }
        public string DepartmentCode { get; set; }
        public string RankTypeCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TrainingTitle { get; set; }
        public string SchoolName { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
