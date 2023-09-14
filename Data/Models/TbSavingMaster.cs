using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbSavingMaster
    {
        public int SavingMasterPkid { get; set; }
        public string TransactionId { get; set; }
        public string CustomerId { get; set; }
        public string SavingAmount { get; set; }
        public string TotalInterest { get; set; }
        public string TotalAmount { get; set; }
    }
}
