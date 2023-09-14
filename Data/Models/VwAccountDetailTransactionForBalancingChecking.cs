using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwAccountDetailTransactionForBalancingChecking
    {
        public string RegionId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string TotalBalance { get; set; }
    }
}
