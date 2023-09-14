using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwLoanReturnTransactionDetail
    {
        public int PersonPkid { get; set; }
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Nrc { get; set; }
        public string DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string RegionId { get; set; }
        public string TownshipId { get; set; }
        public string Phone { get; set; }
        public string StateDivisionId { get; set; }
        public string RegistrationDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? PersonRecordEdited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LoanReturnTransactionDetailPkid { get; set; }
        public int? LoanReturnPkid { get; set; }
        public string LoanDate { get; set; }
        public string LoanYear { get; set; }
        public string LoanSeason { get; set; }
        public string SerialNumber { get; set; }
        public string ChalenNumber { get; set; }
        public string ReturnAmount { get; set; }
        public string InterestAmount { get; set; }
        public string FineInterestAmount { get; set; }
        public string ProfitSavingAmount { get; set; }
        public string SavingAmount { get; set; }
        public string ShareAmount { get; set; }
        public string FormPrice { get; set; }
        public string Other { get; set; }
        public string TotalAmount { get; set; }
        public string ReturnDate { get; set; }
        public bool? ReturnTransactionDeleted { get; set; }
        public bool? ReturnTransactionEdited { get; set; }
        public string ReturnTransactionCreatedBy { get; set; }
    }
}
