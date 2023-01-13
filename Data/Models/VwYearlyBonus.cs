using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwYearlyBonus
    {
        public int YearlyBonusPkid { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedNo { get; set; }
        public string YearlyBonusCount { get; set; }
        public string YearlyBonusSalary { get; set; }
        public DateTime? YearlyBonusDate { get; set; }
    }
}
