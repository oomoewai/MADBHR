using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJicaloanReturnTransactionDetailPassBookSelectByAccNo
    {
        public int LoanReturnTransactionDetailPkid { get; set; }
        public int LoanReturnPkid { get; set; }
        public string LoanAmount { get; set; }
        public string ReturnAmount { get; set; }
        public string InterestAmount { get; set; }
        public string FineInterestAmount { get; set; }
        public string SavingAmount { get; set; }
        public string TotalBalance { get; set; }
        public string Source { get; set; }
        public string ReturnShortDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string TransactionType { get; set; }
    }
}
