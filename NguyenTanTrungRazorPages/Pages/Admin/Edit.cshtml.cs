using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace NguyenTanTrungRazorPages.Pages.Admin
{
    public class EditModel : PageModel
    {
        private IStaffAccountRepo _repo;
        public EditModel()
        {
            _repo = new StaffAccountRepoImpl();
        }
        [BindProperty]
        public StaffAccount staffAccount { get; set; }
        public IActionResult OnGet(string id)
        {
            if (!IsAuthorized(HttpContext.Session.GetString("USERLOGIN")))
            {
                return RedirectToPage("/Login");
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
            
            if(staffAccount != null)
            {
                _repo.UpdateStaff(staffAccount);
            }

            
            return RedirectToPage("./Index");

        }

        public bool IsAuthorized(string email)
        {
            if (email == null)
            {
                return false;
            }
            CarRentalSystemDBContext _context = new CarRentalSystemDBContext();
            string admin = _context.getDefaultAccount().Email;
            if (email != admin)
            {
                return false;
            }

            return true;
        }
    }
}
