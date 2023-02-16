using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class Nlogs
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public DateTime? Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Property { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }
        public int? UserPkid { get; set; }
    }
}
