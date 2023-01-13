using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbAge60Full
    {
        public int Age60FullPkid { get; set; }
        public string BranchCode { get; set; }
        public string DepartmentCode { get; set; }
        public string RankTypeCode { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Nrcno { get; set; }
        public string RankType { get; set; }
        public string DepartmentPlace { get; set; }
        public string RaceReligion { get; set; }
        public DateTime? Dob { get; set; }
        public string LatestSalary { get; set; }
        public DateTime? PermanentDate { get; set; }
        public DateTime? CurrentRankDate { get; set; }
        public string Remark { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
