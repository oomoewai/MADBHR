using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMeb2sloanReturnTransactionDetail
    {
        public int LoanReturnTransactionDetailPkid { get; set; }
        public string TransactionId { get; set; }
        public int? LoanReturnPkid { get; set; }
        public string BankAccountNumber { get; set; }
        public string MemberNumber { get; set; }
        public string LoanYearType { get; set; }
        public string LoanBusinessType { get; set; }
        public string LoanType { get; set; }
        public string SerialNumber { get; set; }
        public string ChalenNumber { get; set; }
        public string NoOfInstallment { get; set; }
        public DateTime? DateOfInstallment { get; set; }
        public decimal? ReturnAmount { get; set; }
        public decimal? ActualReturn { get; set; }
        public decimal? RemainingReturn { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? ActualInterest { get; set; }
        public decimal? RemainingInterest { get; set; }
        public decimal? FineAmount { get; set; }
        public decimal? ActualFine { get; set; }
        public decimal? RemainingFine { get; set; }
        public decimal? ProfitSavingAmount { get; set; }
        public decimal? SavingAmount { get; set; }
        public decimal? ShareAmount { get; set; }
        public decimal? FormPrice { get; set; }
        public decimal? Other { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string PaidStatus { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
