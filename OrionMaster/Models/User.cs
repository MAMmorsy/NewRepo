using System;
using System.Collections.Generic;

#nullable disable

namespace OrionMaster.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Provider { get; set; }
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }
    }
}
