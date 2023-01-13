using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwSonAndDaughterInformationOnlineCheck
    {
        public int SonAndDaughterPkid { get; set; }
        public string EmployeeCode { get; set; }
        public string SonAndDaughterName { get; set; }
        public DateTime? SonAndDaughterDob { get; set; }
        public string SonAndDaughterOccupation { get; set; }
        public string SonAndDaughterAddress { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UploadForTownship { get; set; }
    }
}
