using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbVillage
    {
        public int VillagePkid { get; set; }
        public string TownshipId { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
