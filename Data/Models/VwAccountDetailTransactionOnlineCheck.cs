using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwAccountDetailTransactionOnlineCheck
    {
        public int AccountDetailPkid { get; set; }
        public string TransactionId { get; set; }
        public string BankAccountNumber { get; set; }
        public string AccountType { get; set; }
        public string TransactionType { get; set; }
        public string ChalenNumber { get; set; }
        public string Amount { get; set; }
        public string TotalBalance { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Source { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
