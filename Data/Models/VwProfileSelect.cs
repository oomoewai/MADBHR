using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwProfileSelect
    {
        public string Name { get; set; }
        public string Nrcnumber { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Race { get; set; }
        public string Religion { get; set; }
        public string JoinDate { get; set; }
        public string CurrentRankDate { get; set; }
        public string CurrentRank { get; set; }
        public string LatestSalary { get; set; }
    }
}
