using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class CustomerRepoImpl : ICustomerRepo
    {
        private CustomerDAO _dao;
        public CustomerRepoImpl()
        {
            _dao = new CustomerDAO();
        }

        public void AddCustomer(Customer customer) => _dao.AddCustomer(customer);      
        public Customer checkCustomerLogin(string email, string password) => _dao.checkCustomerLogin(email, password);
        public IEnumerable<Customer> GetAll() => _dao.GetAll();
        public Customer getCustomerByEmail(string email) => _dao.getCustomerByEmail(email);
        public Customer GetCustomerByID(string customerID) => _dao.GetCustomerByID(customerID);
        public void UpdateCustomer(Customer customer) => _dao.UpdateCustomer(customer);
    }
}
