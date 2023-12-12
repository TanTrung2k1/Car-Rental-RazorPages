using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICustomerRepo
    {
        Customer getCustomerByEmail(string email);
        Customer GetCustomerByID(string customerID);
        void AddCustomer(Customer customer);
        IEnumerable<Customer> GetAll();
        void UpdateCustomer(Customer customer);
        Customer checkCustomerLogin(string email, string password);
    }
}
