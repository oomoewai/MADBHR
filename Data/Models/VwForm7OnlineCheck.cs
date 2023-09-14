using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwForm7OnlineCheck
    {
        public int Form7Pkid { get; set; }
        public int? PersonPkid { get; set; }
        public string Township { get; set; }
        public string FarmLocation { get; set; }
        public string FarmNo { get; set; }
        public string FarmName { get; set; }
        public string UpaingNo { get; set; }
        public string FarmType { get; set; }
        public string FarmArea { get; set; }
        public string ConfirmLetterNo { get; set; }
        public string ConfirmLetterDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecordEdited { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
