using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbAwardType
    {
        public int AwardTypePkid { get; set; }
        public string AwardTypeCode { get; set; }
        public string AwardType { get; set; }
        public bool? Active { get; set; }
        public string UploadForTownship { get; set; }
    }
}
