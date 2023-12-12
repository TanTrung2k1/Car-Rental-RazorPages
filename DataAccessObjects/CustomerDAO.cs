using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private CarRentalSystemDBContext _context;
        public CustomerDAO()
        {
            _context = new CarRentalSystemDBContext();
        }

        // get customer by email
        public Customer getCustomerByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerEmail == email);
        }

        // get customer by ID
        public Customer GetCustomerByID(string customerID)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == customerID);
        }

        // add customer
        public void AddCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        // get all customer
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        // update customer
        public void UpdateCustomer(Customer customer)
        {
            var flag = GetCustomerByID(customer.CustomerId);
            if (flag != null)
            {
                flag.CustomerId = customer.CustomerId;
                flag.CustomerName = customer.CustomerName;
                flag.Mobile = customer.Mobile;
                flag.CustomerEmail = customer.CustomerEmail;
                flag.CustomerPassword = customer.CustomerPassword;
                flag.IdentityCard = customer.IdentityCard;
                flag.LicenceNumber = customer.LicenceNumber;
                flag.LicenceDate = customer.LicenceDate;


                _context.SaveChanges();
            }
        }

        // check customer login
        public Customer checkCustomerLogin(string email, string password)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerEmail == email && c.CustomerPassword == password);
        }

    }
}
