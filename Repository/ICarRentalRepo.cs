using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICarRentalRepo
    {
        IEnumerable<CarRental> GetAllAndDescending();
        List<CarRental> GetListByCarId(string cardId);
        List<CarRental> getListByCustomerId(string customerId);
        bool AvailableForRental(string carID, DateTime startDate, DateTime endDate);
        void Add(CarRental carRental);
        List<CarRental> Report(DateTime startDate, DateTime endDate);
    }
}
