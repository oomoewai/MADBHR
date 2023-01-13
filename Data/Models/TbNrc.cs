using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbNrc
    {
        public int Nrcpkid { get; set; }
        public string Nrccode { get; set; }
        public string NrcenglishCode { get; set; }
        public string NrcmyanmarCode { get; set; }
        public int? Nrcnumber { get; set; }
    }
}
