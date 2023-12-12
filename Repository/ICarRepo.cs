using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICarRepo
    {
        Car GetCarByID(string carID);
        List<Car> GetListByName(string name);

        
        IEnumerable<Car> GetAll();
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCarByID(string carID);
    }
}
