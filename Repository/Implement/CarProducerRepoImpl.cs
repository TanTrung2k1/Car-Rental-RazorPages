using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class CarProducerRepoImpl : ICarProducerRepo
    {
        private CarProducerDAO _dao;
        public CarProducerRepoImpl()
        {
            _dao = new CarProducerDAO();
        }
        public IEnumerable<CarProducer> GetAll() => _dao.GetAll();
    }
}
