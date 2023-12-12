using BusinessObjects.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Car
    {
        public Car()
        {
            CarRentals = new HashSet<CarRental>();
            Reviews = new HashSet<Review>();
        }

        
        public string CarId { get; set; }

        [Required(ErrorMessage = "Please enter an Car Name.")]
        public string CarName { get; set; }

        [Required(ErrorMessage = "Please enter an Car Model Year.")]
        [ModelYear]
        public int? CarModelYear { get; set; }

        [Required(ErrorMessage = "Please enter an Color.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Please enter an Capacity.")]
        public int? Capacity { get; set; }

        [Required(ErrorMessage = "Please enter an Decs.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter an Date.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ImportDate { get; set; }

        [Required(ErrorMessage = "Please enter an Price.")]
        public decimal? RentPrice { get; set; }

        public int? Status { get; set; }
        public string ProducerId { get; set; }

        public virtual CarProducer Producer { get; set; }
        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
