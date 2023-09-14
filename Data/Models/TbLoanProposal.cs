using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbLoanProposal
    {
        public int ApplyPkid { get; set; }
        public string TransactionId { get; set; }
        public string BankAccountNumber { get; set; }
        public string Name { get; set; }
        public string Nrc { get; set; }
        public string FatherName { get; set; }
        public string RegionId { get; set; }
        public string TownshipId { get; set; }
        public string StateDivisionId { get; set; }
        public string Phone { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string ApplyStatus { get; set; }
        public string QueueNumber { get; set; }
        public string LoanName { get; set; }
        public string LoanType { get; set; }
        public decimal? LoanAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
