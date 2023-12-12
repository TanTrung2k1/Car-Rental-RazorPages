using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NguyenTanTrungRazorPages.GenerateId;
using Repository;
using Repository.Implement;
using System.Threading.Tasks;

namespace NguyenTanTrungRazorPages.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private IStaffAccountRepo _staffAccountRepo;
        public CreateModel()
        {
            _staffAccountRepo = new StaffAccountRepoImpl();
        }

        [BindProperty]
        public StaffAccount staffAccount { get; set; }
        public void OnGet()
        {
            if (!IsAuthorized(HttpContext.Session.GetString("USERLOGIN")))
            {
                Response.Redirect("/Login");
            }
        }
        public IActionResult OnPost()
        {
            var flag = _staffAccountRepo.getStaffAccountByEmail(staffAccount.Email);
            if (flag != null)
            {
                ModelState.AddModelError("staffAccount.Email", "Duplicate email");
                return Page();
            }
            GenerateNewId idGenerator = new GenerateNewId();
            staffAccount.StaffId = idGenerator.GenerateStaffId();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _staffAccountRepo.AddStaff(staffAccount);
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
