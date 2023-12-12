using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace NguyenTanTrungRazorPages.Pages.Customers
{
    public class EditProfileModel : PageModel
    {
        private ICustomerRepo _customerRepo;
        public EditProfileModel()
        {
            _customerRepo = new CustomerRepoImpl();
        }
        [BindProperty]
        public Customer customer { get; set; }
        public IActionResult OnGet(string id)
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (IsAuthorized(userLogin) == false)
            {
                Response.Redirect("/Login");
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            customer = _customerRepo.GetCustomerByID(id);
            if (customer == null)
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

            if (customer != null)
            {
                _customerRepo.UpdateCustomer(customer);

               
            }


            return RedirectToPage("./Profile");

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
