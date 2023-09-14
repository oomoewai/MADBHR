using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwMeb2sloanReturnTransactionDetail
    {
        public int PersonPkid { get; set; }
        public string TransactionId { get; set; }
        public string MemberNumber { get; set; }
        public string MemberType { get; set; }
        public string Name { get; set; }
        public string Nrc { get; set; }
        public string FatherName { get; set; }
        public string RegionId { get; set; }
        public string TownshipId { get; set; }
        public bool? IsActive { get; set; }
        public bool? PersonRecordDeleted { get; set; }
        public bool? PersonRecordEdited { get; set; }
        public int? LoanReturnTransactionDetailPkid { get; set; }
        public int? LoanReturnPkid { get; set; }
        public string LoanYearType { get; set; }
        public string LoanBusinessType { get; set; }
        public string LoanType { get; set; }
        public string LoanDesc { get; set; }
        public string FirstLoanDate { get; set; }
        public string FirstLoanAmount { get; set; }
        public string SecondLoanDate { get; set; }
        public string SecondLoanAmount { get; set; }
        public string TotalLoanAmount { get; set; }
        public string NoOfInstallment { get; set; }
        public string DateOfInstallment { get; set; }
        public string SerialNumber { get; set; }
        public string ChalenNumber { get; set; }
        public string ReturnAmount { get; set; }
        public string ActualReturn { get; set; }
        public string RemainingReturn { get; set; }
        public string InterestAmount { get; set; }
        public string ActualInterest { get; set; }
        public string RemainingInterest { get; set; }
        public string FineAmount { get; set; }
        public string ActualFine { get; set; }
        public string RemainingFine { get; set; }
        public string ProfitSavingAmount { get; set; }
        public string SavingAmount { get; set; }
        public string ShareAmount { get; set; }
        public string FormPrice { get; set; }
        public string Other { get; set; }
        public string TotalAmount { get; set; }
        public string ReturnDate { get; set; }
        public string PaidStatus { get; set; }
        public bool? ReturnTransactionDeleted { get; set; }
        public bool? ReturnTransactionEdited { get; set; }
        public string ReturnTransactionCreatedBy { get; set; }
    }
}
