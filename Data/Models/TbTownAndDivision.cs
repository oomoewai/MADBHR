using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbTownAndDivision
    {
        public int TownId { get; set; }
        public string TownCode { get; set; }
        public string TownName { get; set; }
        public string DiviSionName { get; set; }
        public string GroupNumber { get; set; }
        public string DivisionCode { get; set; }
        public string TownOrderCode { get; set; }
    }
}
