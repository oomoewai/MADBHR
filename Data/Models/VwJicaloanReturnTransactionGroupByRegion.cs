using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJicaloanReturnTransactionGroupByRegion
    {
        public string RegionId { get; set; }
        public string TownshipId { get; set; }
        public string StateDivisionId { get; set; }
        public string Manufacturer { get; set; }
        public string MachineType { get; set; }
        public string TotalLoan { get; set; }
        public string TotalReturn { get; set; }
        public string TotalBalance { get; set; }
        public string ReturnedPercentage { get; set; }
        public string BalancePercentage { get; set; }
    }
}
