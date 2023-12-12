using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class CarProducer
    {
        public CarProducer()
        {
            Cars = new HashSet<Car>();
        }

        public string ProducerId { get; set; }
        public string ProcuderName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
