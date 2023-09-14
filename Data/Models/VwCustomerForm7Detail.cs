using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwCustomerForm7Detail
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
        public int? Form7Pkid { get; set; }
        public string Township { get; set; }
        public string FarmLocation { get; set; }
        public string FarmNo { get; set; }
        public string FarmName { get; set; }
        public string UpaingNo { get; set; }
        public string FarmType { get; set; }
        public string FarmArea { get; set; }
        public string ConfirmLetterNo { get; set; }
        public string ConfirmLetterDate { get; set; }
        public bool? Form7RecordEdited { get; set; }
        public string Form7CreatedBy { get; set; }
    }
}
