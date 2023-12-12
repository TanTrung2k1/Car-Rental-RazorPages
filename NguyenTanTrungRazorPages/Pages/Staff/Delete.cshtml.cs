using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;

namespace NguyenTanTrungRazorPages.Pages.Staff
{
    public class DeleteModel : PageModel
    {
        private ICarRepo _carRepo;
        public DeleteModel()
        {
            _carRepo = new CarRepoImpl();
        }

        [BindProperty]
        public Car car { get; set; }
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
            car = _carRepo.GetCarByID(id);
            if (car == null)
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

            _carRepo.DeleteCarByID(id);
            return RedirectToPage("./Index");

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
