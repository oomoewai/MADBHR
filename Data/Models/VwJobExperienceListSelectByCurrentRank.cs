using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJobExperienceListSelectByCurrentRank
    {
        public int JobHistoryPkid { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Nrcnumber { get; set; }
        public string CurrentRank { get; set; }
        public string AllTrc { get; set; }
        public int? CurrentRankLevel { get; set; }
        public string Township { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? JobYear { get; set; }
        public decimal? JobMonth { get; set; }
        public decimal? JobDay { get; set; }
    }
}
