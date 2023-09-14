using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwMeb2sloanMasterForBalancingChecking
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
        public bool? IsMainPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? PersonRecordDeleted { get; set; }
        public bool? PersonRecordEdited { get; set; }
        public int? LoanReturnPkid { get; set; }
        public string LoanYearType { get; set; }
        public string LoanBusinessType { get; set; }
        public string LoanType { get; set; }
        public string LoanDesc { get; set; }
        public string Duration { get; set; }
        public string PendingYear { get; set; }
        public string FirstLoanDate { get; set; }
        public string FirstLoanAmount { get; set; }
        public string SecondLoanDate { get; set; }
        public string SecondLoanAmount { get; set; }
        public string TotalLoanAmount { get; set; }
        public string Balance { get; set; }
        public string ReturnAmount { get; set; }
        public bool? LoanRecordDeleted { get; set; }
        public bool? LoanReturnEdited { get; set; }
        public string CreatedBy { get; set; }
    }
}
