using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbRegion
    {
        public int RegionPkid { get; set; }
        public string TownshipId { get; set; }
        public string RegionCode { get; set; }
        public string Region { get; set; }
    }
}
