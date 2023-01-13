using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbPensionType
    {
        public int PensionTypePkid { get; set; }
        public string PensionTypeCode { get; set; }
        public string PensionType { get; set; }
        public bool? IsRecordEdited { get; set; }
        public bool Active { get; set; }
        public string UploadForTownship { get; set; }
    }
}
