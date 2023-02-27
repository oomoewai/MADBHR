using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static MADBHR_Data.Models.Attributes.CustomAttribute;

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
        public string ProfilePic { get; set; }
        public string Form66Pic { get; set; }
        public string Nrcpic { get; set; }
        public string DegreePic { get; set; }
        public string Status { get; set; }
        public string RejectComment { get; set; }
        public string EditRequest { get; set; }
        public string EditComment { get; set; }
        public string DeleteRequest { get; set; }
        [NotMapped]
        [SkipProperty]
        public string ImageContent { get; set; }
        [NotMapped]
        [SkipProperty]
        public string NRCImageContent { get; set; }
        [NotMapped]
        [SkipProperty]
        public string Form66ImageContent { get; set; }
        [NotMapped]
        [SkipProperty]
        public string DegreeImageContent { get; set; }
        [NotMapped]
        [SkipProperty]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [SkipProperty]
        public IFormFile NRCImageFile { get; set; }
        [NotMapped]
        [SkipProperty]
        public IFormFile Form66ImageFile { get; set; }
        [NotMapped]
        [SkipProperty]
        public IFormFile DegreeImageFile { get; set; }
        [NotMapped]
        [SkipProperty]
        public string CurrentRank { get; set; }
        
        [NotMapped]
        [SkipProperty]
        public string SerialNumber_Myan { get; set; }
        [NotMapped]
        [SkipProperty]
        public string EducationType { get; set; }
        [NotMapped]
        [SkipProperty]
        public string PlaceOfBirthName { get; set; }
        [NotMapped]
        [SkipProperty]
        public string DateOfBirthString { get; set; }
        [NotMapped]
        [SkipProperty]
        public string JoinDateString { get; set; }
        [NotMapped]
        [SkipProperty]
        public string CurrentRankDate { get; set; }
        [NotMapped]
        [SkipProperty]
        public string StateDivision { get; set; }

        [NotMapped]
        [SkipProperty]
        public string Township { get; set; }
        [NotMapped]
        [SkipProperty]
        public string OccupationName { get; set; }
        [NotMapped]
        [SkipProperty]
        public int Age { get; set; }
    }
}
