using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class CarRepoImpl : ICarRepo
    {
        private CarDAO _dao;
        public CarRepoImpl()
        {
            _dao = new CarDAO();
        }
        public void AddCar(Car car) => _dao.AddCar(car);
        public void DeleteCarByID(string carID) => _dao.DeleteCarByID(carID);
        public IEnumerable<Car> GetAll() => _dao.GetAll();
        public Car GetCarByID(string carID) => _dao.GetCarByID(carID);
        public List<Car> GetListByName(string name) => _dao.GetListByName(name);
        public void UpdateCar(Car car) => _dao.UpdateCar(car);

    }
}
