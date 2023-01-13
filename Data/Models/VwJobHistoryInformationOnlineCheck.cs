using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwJobHistoryInformationOnlineCheck
    {
        public int JobHistoryPkid { get; set; }
        public string EmployeeCode { get; set; }
        public string RankTypeCode { get; set; }
        public string RankTypeCode1 { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? JobYear { get; set; }
        public decimal? JobMonth { get; set; }
        public decimal? JobDay { get; set; }
        public decimal? Duration { get; set; }
        public string Remark { get; set; }
        public bool? IsCurrent { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UploadForTownship { get; set; }
    }
}
