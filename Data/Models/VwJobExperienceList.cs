using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJobExperienceList
    {
        public int EmployeePkid { get; set; }
        public string SerialNumber { get; set; }
        public string SerialNumberMyan { get; set; }
        public string Name { get; set; }
        public string Nrcnumber { get; set; }
        public string CurrentRank { get; set; }
        public string DepartmentName { get; set; }
        public string JoinDate { get; set; }
        public string FromDate { get; set; }
        public string JobYear { get; set; }
        public string JobMonth { get; set; }
        public string JobDay { get; set; }
    }
}
