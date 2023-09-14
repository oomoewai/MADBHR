using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbAccountDetail
    {
        public int AccountDetailPkid { get; set; }
        public string TransactionId { get; set; }
        public string ChalenNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string RegionId { get; set; }
        public string TownshipId { get; set; }
        public string StateDivisionId { get; set; }
        public string LoanYear { get; set; }
        public string LoanSeason { get; set; }
        public string BasedAmount { get; set; }
        public string InterestInKyat { get; set; }
        public string FineInterestInKyat { get; set; }
        public string SavingAmount { get; set; }
        public string OtherAmount { get; set; }
        public string ExtraAmount { get; set; }
        public string TotalAmount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
