﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMeb2spersonalInformation
    {
        public int PersonPkid { get; set; }
        public string TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string MemberNumber { get; set; }
        public string AccountType { get; set; }
        public string MemberType { get; set; }
        public string Name { get; set; }
        public string Nrc { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FatherName { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string RegionId { get; set; }
        public string TownshipId { get; set; }
        public string Phone { get; set; }
        public string StateDivisionId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool? IsMainPerson { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
