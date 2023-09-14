using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbLoanTransaction
    {
        public int LoanPkid { get; set; }
        public string LoanTransactionId { get; set; }
        public string CustomerId { get; set; }
        public string TransactionType { get; set; }
        public string LoanItem { get; set; }
        public string Amount { get; set; }
        public string InterestRate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsDataSynced { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
