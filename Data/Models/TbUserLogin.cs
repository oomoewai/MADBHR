using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbUserLogin
    {
        public int UserPkid { get; set; }
        public string Name { get; set; }
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public string Department { get; set; }
        public string Office { get; set; }
        public string StateDivisionId { get; set; }
        public string TownshipId { get; set; }
        public string Status { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
