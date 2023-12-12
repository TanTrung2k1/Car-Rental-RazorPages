using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CarRentalDAO
    {
        private CarRentalSystemDBContext _context;

        public CarRentalDAO()
        {
            _context = new CarRentalSystemDBContext();
        }

        // get all car rental descending
        public IEnumerable<CarRental> GetAllAndDescending()
        {
            return _context.CarRentals.Include(c => c.Car).Include(c => c.Customer).OrderByDescending(c => c.RentPrice).ToList();
        }

        // get list car rental by car ID
        public List<CarRental> GetListByCarId(string cardId)
        {
            return _context.CarRentals.Where(c => c.CarId == cardId).ToList();
        }

        // get list car rental by customer ID
        public List<CarRental> getListByCustomerId(string customerId)
        {
            return _context.CarRentals.Include(c => c.Car).Where(c => c.CustomerId == customerId).OrderByDescending(c => c.PickupDate).ToList();
        }

        //add car rental
        public void Add(CarRental carRental)
        {
            _context.Add(carRental);
            _context.SaveChanges();
        }

        // report statistics by the period from StartDate to EndDate, and sort sales in descending order.
        public List<CarRental> Report(DateTime startDate, DateTime endDate)
        {
            return _context.CarRentals.Include(c=>c.Car).Include(c=>c.Customer).Where(c => c.PickupDate >= startDate && c.ReturnDate <= endDate).OrderByDescending(c => c.RentPrice).ToList();
        }

        //check overlap of car
        public bool AvailableForRental(string carID, DateTime startDate, DateTime endDate)
        {
            var carRental = _context.CarRentals.Where(c => c.CarId == carID);
            return !carRental.Any(c => (startDate >= c.PickupDate && startDate <= c.ReturnDate) ||
                                        (endDate >= c.PickupDate && endDate <= c.ReturnDate) ||
                                        (startDate <= c.PickupDate && endDate >= c.ReturnDate));
        }

        
    }
}
