using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwMebtslns10withSaving
    {
        public int PersonPkid { get; set; }
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string MemberNumber { get; set; }
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
        public bool? IsMainPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? PersonRecordDeleted { get; set; }
        public bool? PersonRecordEdited { get; set; }
        public int LoanReturnPkid { get; set; }
        public string Duration { get; set; }
        public string LoanYearType { get; set; }
        public string LoanBusinessType { get; set; }
        public string LoanType { get; set; }
        public string LoanDesc { get; set; }
        public string LoanAmount { get; set; }
        public string TotalReturnAmount { get; set; }
        public string Balance { get; set; }
        public string LoanDate { get; set; }
        public string ReturnDate { get; set; }
        public string ReturnAmount { get; set; }
        public string ActualReturn { get; set; }
        public string RemainingReturn { get; set; }
        public string InterestAmount { get; set; }
        public string ActualInterest { get; set; }
        public string RemainingInterest { get; set; }
        public string FineAmount { get; set; }
        public string ActualFine { get; set; }
        public string RemainingFine { get; set; }
        public string TotalReturn { get; set; }
        public bool? LoanRecordDeleted { get; set; }
        public bool? LoanReturnEdited { get; set; }
        public string CreatedBy { get; set; }
    }
}
