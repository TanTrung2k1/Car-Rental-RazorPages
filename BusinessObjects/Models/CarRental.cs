using BusinessObjects.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace BusinessObjects.Models
{
    public partial class CarRental
    {
        public string CustomerId { get; set; }
        public string CarId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [TodayDate]
        public DateTime PickupDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReturnDate { get; set; }
        public decimal? RentPrice { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
