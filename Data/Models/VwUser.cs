using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class VwUser
    {
        public int UserPkid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DepartmentCode { get; set; }
        public string Department { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
