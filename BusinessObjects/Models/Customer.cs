using BusinessObjects.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CarRentals = new HashSet<CarRental>();
            Reviews = new HashSet<Review>();
        }

        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter an Customer name.")]
        public string CustomerName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter an Mobile.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter an Customer Email.")]
        [Email]
        public string CustomerEmail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter an Customer Password.")]
        public string CustomerPassword { get; set; }

        [Required(ErrorMessage = "Please enter an Identity Card.")]
        public string IdentityCard { get; set; }

        [Required(ErrorMessage = "Please enter an Licence Number.")]
        public string LicenceNumber { get; set; }

        [Required(ErrorMessage = "Please enter an Licence Date.")]
        public DateTime? LicenceDate { get; set; }

        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
