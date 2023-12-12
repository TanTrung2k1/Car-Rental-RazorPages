using BusinessObjects.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class StaffAccount
    {
        
        public string StaffId { get; set; }

        [Required(ErrorMessage = "Please enter an Full name.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [Email]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter an Password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter an role.")]
        public int? Role { get; set; }
    }
}
