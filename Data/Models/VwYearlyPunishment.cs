using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwYearlyPunishment
    {
        public int EmployeePkid { get; set; }
        public string SerialNumber { get; set; }
        public string StateDivision { get; set; }
        public string Name { get; set; }
        public string CrimeYear { get; set; }
        public string YearlyPunishmentType { get; set; }
        public string OrderDate { get; set; }
        public string CurrentRank { get; set; }
        public string JobAddress { get; set; }
        public string JoinDate { get; set; }
        public string DateOfBirth { get; set; }
        public string Nrcnumber { get; set; }
    }
}
