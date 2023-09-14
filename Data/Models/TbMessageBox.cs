using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMessageBox
    {
        public int MessagePkid { get; set; }
        public string TransactionId { get; set; }
        public string MessageId { get; set; }
        public string MessageTo { get; set; }
        public string MessageFrom { get; set; }
        public string MessageStatus { get; set; }
        public DateTime? MessageDate { get; set; }
    }
}
