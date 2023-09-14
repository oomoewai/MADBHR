using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJicaloanMasterOnlineCheck
    {
        public int LoanReturnPkid { get; set; }
        public string TransactionId { get; set; }
        public string BankAccountNumber { get; set; }
        public string MachineId { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public string Dealer { get; set; }
        public string ProposalAmount { get; set; }
        public string LoanAmount { get; set; }
        public DateTime? LoanDate { get; set; }
        public int? Duration { get; set; }
        public decimal? CustomReturn { get; set; }
        public decimal? ActualReturn { get; set; }
        public decimal? RemainingReturn { get; set; }
        public decimal? CustomInterest { get; set; }
        public decimal? ActualInterest { get; set; }
        public decimal? RemainingInterest { get; set; }
        public decimal? CustomFine { get; set; }
        public decimal? ActualFine { get; set; }
        public decimal? RemainingFine { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
