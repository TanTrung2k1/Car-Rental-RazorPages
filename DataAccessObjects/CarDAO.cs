using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CarDAO
    {
        private CarRentalSystemDBContext _context;
        public CarDAO()
        {
            _context = new CarRentalSystemDBContext();
        }

        //get car by ID
        public Car GetCarByID(string carID)
        {
            return _context.Cars.Include(c => c.Producer).FirstOrDefault(c => c.CarId == carID);
        }
        
        //list car by name
        public List<Car> GetListByName(string name)
        {
            return _context.Cars.Where(c => c.CarName.ToLower().Contains(name)).ToList();
        }

        //all list car
        public IEnumerable<Car> GetAll()
        {
            return _context.Cars.Include(c => c.Producer).ToList();
        }

        //add
        public void AddCar(Car car)
        {
            _context.Add(car);
            _context.SaveChanges();
        }

        //update
        public void UpdateCar(Car car)
        {
            var flag = GetCarByID(car.CarId);
            if (flag != null)
            {
                flag.CarId = car.CarId;
                flag.CarName = car.CarName;
                flag.CarModelYear = car.CarModelYear;
                flag.Color = car.Color;
                flag.Capacity = car.Capacity;
                flag.Description = car.Description;
                flag.ImportDate = car.ImportDate;
                flag.RentPrice = car.RentPrice;
                flag.Status = car.Status;
                flag.ProducerId = car.ProducerId;

                _context.SaveChanges();
            }
        }

        //delete
        public void DeleteCarByID(string carID)
        {
            var flag = GetCarByID(carID);
            if (flag != null)
            {
                _context.Cars.Remove(flag);
                _context.SaveChanges();
            }
        }
        
    }
}
