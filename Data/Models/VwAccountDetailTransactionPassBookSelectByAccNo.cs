using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwAccountDetailTransactionPassBookSelectByAccNo
    {
        public int AccountDetailPkid { get; set; }
        public string Amount { get; set; }
        public string TotalBalance { get; set; }
        public string Source { get; set; }
        public string TransactionShortDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionType { get; set; }
    }
}
