using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbEmployee
    {
        public int EmployeePkid { get; set; }
        public string EmployeeCode { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Race { get; set; }
        public string Nrcnumber { get; set; }
        public string PlaceOfBirth { get; set; }
        public string EducationTypeCode { get; set; }
        public string Occupation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EyeColor { get; set; }
        public string Height { get; set; }
        public string Mark { get; set; }
        public string OtherName { get; set; }
        public DateTime? JoinDate { get; set; }
        public string TownshipCode { get; set; }
        public string Address { get; set; }
        public string DearestPerson { get; set; }
        public string Ancestor { get; set; }
        public string IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UploadForTownship { get; set; }
    }
}
