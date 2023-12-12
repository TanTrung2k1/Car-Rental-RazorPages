using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class CarRentalRepoImpl : ICarRentalRepo
    {
        private CarRentalDAO _dao;
        public CarRentalRepoImpl()
        {
            _dao = new CarRentalDAO();
        }
        public void Add(CarRental carRental) => _dao.Add(carRental);

        public bool AvailableForRental(string carID, DateTime startDate, DateTime endDate) => _dao.AvailableForRental(carID, startDate, endDate);

        public IEnumerable<CarRental> GetAllAndDescending() => _dao.GetAllAndDescending();
        public List<CarRental> GetListByCarId(string cardId) => _dao.GetListByCarId(cardId);
        public List<CarRental> getListByCustomerId(string customerId) => _dao.getListByCustomerId(customerId);

        

        public List<CarRental> Report(DateTime startDate, DateTime endDate) => _dao.Report(startDate, endDate);
    }
}
