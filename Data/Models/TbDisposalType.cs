using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbDisposalType
    {
        public int DisposalTypePkid { get; set; }
        public string DisposalTypeCode { get; set; }
        public string DisposalType { get; set; }
        public bool? IsRecordEdited { get; set; }
        public bool? Active { get; set; }
        public string UploadForTownship { get; set; }
    }
}
