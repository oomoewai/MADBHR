using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbTownship
    {
        public int TownshipPkid { get; set; }
        public string StateDivisionId { get; set; }
        public string TownshipCode { get; set; }
        public string Township { get; set; }
    }
}
