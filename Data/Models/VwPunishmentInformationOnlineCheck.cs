using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwPunishmentInformationOnlineCheck
    {
        public int PunishmentPkid { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string CrimeYear { get; set; }
        public string PunishmentTypeCode { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UploadForTownship { get; set; }
    }
}
