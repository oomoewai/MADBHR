using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbLoanGroup
    {
        public int LoanGroupPkid { get; set; }
        public string TransactionId { get; set; }
        public string FirstAccount { get; set; }
        public string SecondAccount { get; set; }
        public string ThirdAccount { get; set; }
    }
}
