using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbSubjects
    {
        public int SubjectPkid { get; set; }
        public string SubjectCode { get; set; }
        public string Subject { get; set; }
        public int? TotalMark { get; set; }
    }
}
