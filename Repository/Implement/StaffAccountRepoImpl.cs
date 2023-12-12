using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class StaffAccountRepoImpl : IStaffAccountRepo
    {
        private StaffAccountDAO _dao;
        public StaffAccountRepoImpl()
        {
            _dao = new StaffAccountDAO();
        }
        public void AddStaff(StaffAccount account) => _dao.AddStaff(account);
        public StaffAccount checkStaffLogin(string email, string password) => _dao.checkStaffLogin(email, password);
        public void DeleteStaff(string StaffID) => _dao.DeleteStaff(StaffID);

        public StaffAccount checkAdminLogin(string email, string password) => _dao.checkAdminLogin(email, password);
        public IEnumerable<StaffAccount> GetAll() => _dao.GetAll();
        public List<StaffAccount> GetListStaffByName(string name) => _dao.GetListStaffByName(name);
        public StaffAccount getStaffAccountByEmail(string email) => _dao.getStaffAccountByEmail(email);
        public StaffAccount getStaffByID(string StaffID) => _dao.getStaffByID(StaffID);
        public void UpdateStaff(StaffAccount account) => _dao.UpdateStaff(account);
    }
}
