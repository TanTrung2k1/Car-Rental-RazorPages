using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using Microsoft.AspNetCore.Http;

namespace NguyenTanTrungRazorPages.Pages.Customers
{
    public class ProfileModel : PageModel
    {

        private ICustomerRepo _customerRepo;
        public ProfileModel()
        {
            _customerRepo = new CustomerRepoImpl();
            
        }

        [BindProperty]
        public Customer customer { get; set; }
        public IActionResult OnGet()
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (IsAuthorized(userLogin) == false)
            {
                return RedirectToPage("/Login");
            }
            Customer cus = _customerRepo.getCustomerByEmail(userLogin);
            customer = cus;
            if(customer == null)
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
        public bool IsAuthorized(string email)
        {
            if (email == null)
            {
                return false;
            }

            ICustomerRepo customerRepo = new CustomerRepoImpl();
            var customer = customerRepo.getCustomerByEmail(email);
            if (customer == null)
            {
                return false;
            }
            return true;
        }
    }
}
