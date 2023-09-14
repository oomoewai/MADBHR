using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwPassBookFrontSelectByAccNo
    {
        public int PersonPkid { get; set; }
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string FirstMemberName { get; set; }
        public string SecondMemberName { get; set; }
        public string FirstNrc { get; set; }
        public string SecondNrc { get; set; }
        public string FirstMemberFatherName { get; set; }
        public string SecondMemberFatherName { get; set; }
        public string Address { get; set; }
        public string FirstPhone { get; set; }
        public string SecondPhone { get; set; }
        public string SpecialInstruction { get; set; }
        public string BranchName { get; set; }
        public string RegistrationDate { get; set; }
        public string NrcofAdan { get; set; }
        public string TodayDate { get; set; }
        public bool? IsMainPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AccountSummaryPkid { get; set; }
        public string AccountStatus { get; set; }
    }
}
