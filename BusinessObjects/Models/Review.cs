using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Review
    {
        public string CustomerId { get; set; }
        public string CarId { get; set; }
        public int? ReviewStar { get; set; }
        public string Comment { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
