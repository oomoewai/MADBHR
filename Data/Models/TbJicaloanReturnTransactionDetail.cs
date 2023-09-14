using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbJicaloanReturnTransactionDetail
    {
        public int LoanReturnTransactionDetailPkid { get; set; }
        public string TransactionId { get; set; }
        public int? LoanReturnPkid { get; set; }
        public string AccountNumber { get; set; }
        public string SerialNumber { get; set; }
        public string ChalenNumber { get; set; }
        public string NoOfInstallment { get; set; }
        public DateTime? DateOfInstallment { get; set; }
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
        public DateTime? ReturnDate { get; set; }
        public string PaidStatus { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
