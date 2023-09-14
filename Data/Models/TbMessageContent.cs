using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class TbMessageContent
    {
        public int MessageContentPkid { get; set; }
        public string TransactionId { get; set; }
        public string MessageId { get; set; }
        public string Title { get; set; }
        public string MainContent { get; set; }
        public string AttachFileId { get; set; }
        public string IsUploaded { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
