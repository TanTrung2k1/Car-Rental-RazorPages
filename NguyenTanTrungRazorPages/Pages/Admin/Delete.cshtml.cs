using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;

namespace NguyenTanTrungRazorPages.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private IStaffAccountRepo _repo;
        public DeleteModel()
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
            if(staffAccount == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            _repo.DeleteStaff(id);
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
