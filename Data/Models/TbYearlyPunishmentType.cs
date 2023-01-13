using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbYearlyPunishmentType
    {
        public int YearlyPunishmentPkid { get; set; }
        public string YearlyPunishmentTypeCode { get; set; }
        public string YearlyPunishmentType { get; set; }
        public string UploadForTownship { get; set; }
    }
}
