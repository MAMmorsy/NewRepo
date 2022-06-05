using System;
using System.Collections.Generic;

#nullable disable

namespace OrionMaster.Models
{
    public partial class UserMessage
    {
        public int MsgId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string MsgDetails { get; set; }
        public byte Status { get; set; }
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }
    }
}
