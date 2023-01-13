using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwRelationInformationOnlineCheck
    {
        public int RelationshipPkid { get; set; }
        public string EmployeeCode { get; set; }
        public string RelationName { get; set; }
        public DateTime? RelationDob { get; set; }
        public string RelationType { get; set; }
        public string RelationOccupation { get; set; }
        public string RelationAddress { get; set; }
        public string RelationFatherName { get; set; }
        public string RelationMotherName { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UploadForTownship { get; set; }
    }
}
