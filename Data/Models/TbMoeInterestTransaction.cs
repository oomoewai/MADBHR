using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMoeInterestTransaction
    {
        public int MoeInterestPkid { get; set; }
        public string TransactionId { get; set; }
        public string BankAccountNumber { get; set; }
        public string TownshipId { get; set; }
        public string RegionId { get; set; }
        public string StateDivisionId { get; set; }
        public string LoanStartDate { get; set; }
        public string LoanYear { get; set; }
        public string LoanSeason { get; set; }
        public string LoanAmount { get; set; }
        public string ReturnAmount { get; set; }
        public string InterestRate { get; set; }
        public string InterestAmount { get; set; }
        public string InterestCalculationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
