using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMachine
    {
        public int MachinePkid { get; set; }
        public string MachineId { get; set; }
        public string Manufacturer { get; set; }
        public string MachineType { get; set; }
    }
}
