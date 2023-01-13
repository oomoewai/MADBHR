using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJobExperience
    {
        public int EmployeePkid { get; set; }
        public string SerialNumber { get; set; }
        public string SerialNumberMyan { get; set; }
        public string Name { get; set; }
        public string Nrcnumber { get; set; }
        public string RankType { get; set; }
        public string DepartmentName { get; set; }
        public string JoinDate { get; set; }
        public DateTime? FromDate { get; set; }
        public decimal? JobYear { get; set; }
        public decimal? JobMonth { get; set; }
        public decimal? JobDay { get; set; }
        public decimal? CurrentJobYear { get; set; }
        public int TotalJobYear { get; set; }
        public int TotalJobMonth { get; set; }
        public int TotalJobDay { get; set; }
        public bool? IsCurrent { get; set; }
    }
}
