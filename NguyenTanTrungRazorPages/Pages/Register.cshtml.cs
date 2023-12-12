using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenTanTrungRazorPages.GenerateId;
using Repository;
using Repository.Implement;
using System;

namespace NguyenTanTrungRazorPages.Pages
{
    public class RegisterModel : PageModel
    {
        private ICustomerRepo _customerRepo;
        public RegisterModel()
        {
            _customerRepo = new CustomerRepoImpl();
        }

        [BindProperty]
        public Customer customer { get; set; }

        [BindProperty]
        public string confirmPass { get; set; }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            DateTime today = DateTime.Now.Date;
            if (customer.LicenceDate > today)
            {
                ModelState.AddModelError("customer.LicenceDate", "The Import date cannot greeter than today " + (today.ToString("dd/MM/yyyy")));
                return Page();
            }
            var flag = _customerRepo.getCustomerByEmail(customer.CustomerEmail);
            if (flag != null)
            {
                ModelState.AddModelError("customer.CustomerEmail", "Email has been used");
                return Page();
            }
            if(customer.CustomerPassword != confirmPass)
            {
                ModelState.AddModelError("confirmPass", "Not match with password");
                return Page();
            }
            GenerateNewId idGenerator = new GenerateNewId();
            customer.CustomerId = idGenerator.GenerateCustomerId();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _customerRepo.AddCustomer(customer);


            return RedirectToPage("/Login");
        }
    }
}
