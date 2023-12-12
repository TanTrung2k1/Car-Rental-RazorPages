using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IStaffAccountRepo
    {
        StaffAccount getStaffByID(string StaffID);
        StaffAccount getStaffAccountByEmail(string email);
        List<StaffAccount> GetListStaffByName(string name);
        void AddStaff(StaffAccount account);
        void UpdateStaff(StaffAccount account);
        void DeleteStaff(string StaffID);
        IEnumerable<StaffAccount> GetAll();
        StaffAccount checkStaffLogin(string email, string password);
        StaffAccount checkAdminLogin(string email, string password);
    }
}
