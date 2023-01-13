using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwAwardList
    {
        public int EmployeePkid { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string JoinDate { get; set; }
        public string JobAddress { get; set; }
        public string DateOfBirth { get; set; }
        public string CurrentRank { get; set; }
        public string Nrcnumber { get; set; }
        public string AwardType { get; set; }
        public string AwardYear { get; set; }
    }
}
