using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using Microsoft.AspNetCore.Http;

namespace NguyenTanTrungRazorPages.Pages.Staff
{
    public class EditProfileModel : PageModel
    {
        private IStaffAccountRepo _repo;
        public EditProfileModel()
        {
            _repo = new StaffAccountRepoImpl();
        }
        [BindProperty]
        public StaffAccount staffAccount { get; set; }
        public IActionResult OnGet(string id)
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (!IsAuthorized(userLogin))
            {
                Response.Redirect("/");
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            staffAccount = _repo.getStaffByID(id);
            if (staffAccount == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (staffAccount != null)
            {
                _repo.UpdateStaff(staffAccount);
            }


            return RedirectToPage("./Profile");

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
