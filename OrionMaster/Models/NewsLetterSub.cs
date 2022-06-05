using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OrionMaster.Models
{
    public partial class NewsLetterSub
    {
        public int Id { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "enter your email")]
        [DataType(DataType.EmailAddress,ErrorMessage ="enter a valid email")]
        public string Email { get; set; }
        public DateTime Datein { get; set; }
        public bool Deleted { get; set; }
        [NotMapped]
        public string returnUrl { get; set; } = "/";
    }
}
