using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CarProducerDAO
    {
        private CarRentalSystemDBContext _context;
        public CarProducerDAO()
        {
            _context = new CarRentalSystemDBContext();
        }

        //get all
        public IEnumerable<CarProducer> GetAll()
        {
            return _context.CarProducers.ToList();
        }
    }
}
