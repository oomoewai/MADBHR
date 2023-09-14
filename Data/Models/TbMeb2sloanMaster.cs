using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMeb2sloanMaster
    {
        public int LoanReturnPkid { get; set; }
        public string TransactionId { get; set; }
        public string BankAccountNumber { get; set; }
        public string MemberNumber { get; set; }
        public string LoanYearType { get; set; }
        public string LoanBusinessType { get; set; }
        public string LoanType { get; set; }
        public string LoanDesc { get; set; }
        public int? Duration { get; set; }
        public int? PendingYear { get; set; }
        public DateTime? FirstLoanDate { get; set; }
        public decimal? FirstLoanAmount { get; set; }
        public DateTime? SecondLoanDate { get; set; }
        public decimal? SecondLoanAmount { get; set; }
        public decimal? CustomReturn { get; set; }
        public decimal? ActualReturn { get; set; }
        public decimal? RemainingReturn { get; set; }
        public decimal? CustomInterest { get; set; }
        public decimal? ActualInterest { get; set; }
        public decimal? RemainingInterest { get; set; }
        public decimal? CustomFine { get; set; }
        public decimal? ActualFine { get; set; }
        public decimal? RemainingFine { get; set; }
        public decimal? FirstInstallment { get; set; }
        public decimal? SecondInstallment { get; set; }
        public decimal? ThirthInstallment { get; set; }
        public decimal? FourthInstallment { get; set; }
        public decimal? FifthInstallment { get; set; }
        public decimal? SixthInstallment { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
