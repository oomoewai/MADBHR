using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwAccountSummaryOnlineCheck
    {
        public int AccountSummaryPkid { get; set; }
        public string TransactionId { get; set; }
        public string BankAccountNumber { get; set; }
        public string AccountStatus { get; set; }
        public string LastBalance { get; set; }
        public string AmountUpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string RecordCreatedBy { get; set; }
        public DateTime? RecordCreatedDate { get; set; }
    }
}
