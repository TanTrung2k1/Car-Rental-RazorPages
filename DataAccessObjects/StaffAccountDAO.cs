using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class StaffAccountDAO
    {
        private CarRentalSystemDBContext _context;
        public StaffAccountDAO()
        {
            _context = new CarRentalSystemDBContext();
        }

        // get staff by ID
        public StaffAccount getStaffByID(string StaffID)
        {
            return _context.StaffAccounts.FirstOrDefault(s => s.StaffId == StaffID);
        }

        // get staff by email
        public StaffAccount getStaffAccountByEmail(string email)
        {
            return _context.StaffAccounts.FirstOrDefault(s => s.Email == email);
        }

        // get list staff by name
        public List<StaffAccount> GetListStaffByName(string name)
        {
            return _context.StaffAccounts.Where(s => s.FullName.ToLower().Contains(name) 
            || s.Email.ToLower().Contains(name)).ToList();
        }

        // add staff
        public void AddStaff(StaffAccount account)
        {
            _context.Add(account);
            _context.SaveChanges(); 
        }

        // update staff
        public void UpdateStaff(StaffAccount account)
        {
            var getStaff = getStaffByID(account.StaffId);
            if (getStaff != null)
            {
                getStaff.StaffId = account.StaffId;
                getStaff.FullName = account.FullName;
                getStaff.Email = account.Email;
                getStaff.Password = account.Password;
                getStaff.Role = account.Role;

                _context.SaveChanges();
            }
        }

        // delete staff
        public void DeleteStaff(string StaffID)
        {
            var getStaff = getStaffByID(StaffID);
            if (getStaff != null)
            {
                _context.StaffAccounts.Remove(getStaff);
                _context.SaveChanges();
            }
        }

        // get all staff
        public IEnumerable<StaffAccount> GetAll()
        {
            return _context.StaffAccounts.ToList();
        }

        // check staff login
        public StaffAccount checkStaffLogin(string email, string password)
        {
            return _context.StaffAccounts.FirstOrDefault(s => s.Email == email && s.Password == password);
        }

        //getAdmin
        public StaffAccount checkAdminLogin(string email, string password)
        {
            StaffAccount admin = _context.getDefaultAccount();
            if(email == admin.Email && password == admin.Password)
            {
                return admin;
            }
            return null;
        }
    }
}
