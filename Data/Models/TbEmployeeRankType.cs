using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbEmployeeRankType
    {
        public string RankPkId { get; set; }
        public string RankTypeCode { get; set; }
        public string RankType { get; set; }
        public string UploadForTownship { get; set; }
    }
}
