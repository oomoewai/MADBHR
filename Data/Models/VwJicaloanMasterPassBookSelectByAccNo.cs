using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJicaloanMasterPassBookSelectByAccNo
    {
        public int LoanReturnPkid { get; set; }
        public string LoanAmount { get; set; }
        public string ReturnAmount { get; set; }
        public string TotalBalance { get; set; }
        public string Source { get; set; }
        public string LoanShortDate { get; set; }
        public DateTime? LoanDate { get; set; }
        public string TransactionType { get; set; }
    }
}
