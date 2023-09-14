using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbTransaction
    {
        public int TransactionPkid { get; set; }
        public string TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public string InterestRate { get; set; }
        public string InterestTotal { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
