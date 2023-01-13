using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbPension
    {
        public int PensionPkid { get; set; }
        public string BranchCode { get; set; }
        public string DepartmentCode { get; set; }
        public string RankTypeCode { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string RankType { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PensionReportNo { get; set; }
        public DateTime? PensionDate { get; set; }
        public string PensionTypeCode { get; set; }
        public string LatestSalary { get; set; }
        public string MonthlyPension { get; set; }
        public string Saving { get; set; }
        public DateTime? PensionStartDate { get; set; }
        public string PensionBank { get; set; }
        public string Remark { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UploadForTownship { get; set; }
    }
}
