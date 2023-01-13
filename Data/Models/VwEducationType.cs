using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwEducationType
    {
        public string EducationTypeCode { get; set; }
        public string EducationType { get; set; }
        public int EducationTypePkid { get; set; }
        public bool Active { get; set; }
    }
}
