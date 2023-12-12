using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;

namespace NguyenTanTrungRazorPages.Pages.Staff
{
    public class ProfileModel : PageModel
    {
        private IStaffAccountRepo _repo;
        public ProfileModel()
        {
            _repo = new StaffAccountRepoImpl();
        }

        public StaffAccount staffAccount { get; set; }
        public void OnGet()
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (!IsAuthorized(userLogin))
            {
                Response.Redirect("/");
            }
            
            staffAccount = _repo.getStaffAccountByEmail(userLogin);
        }

        public bool IsAuthorized(string email)
        {
            if (email == null)
            {
                return false;
            }

            IStaffAccountRepo staffRepo = new StaffAccountRepoImpl();
            var staff = staffRepo.getStaffAccountByEmail(email);
            if (staff == null)
            {
                return false;
            }
            return true;
        }
    }
}
