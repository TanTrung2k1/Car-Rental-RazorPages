using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implement;
using System.Collections.Generic;
using System.Linq;

namespace NguyenTanTrungRazorPages.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private ICustomerRepo _customerRepo;
        private ICarRentalRepo _carRentalRepo;
        public IndexModel()
        {
            _carRentalRepo = new CarRentalRepoImpl();
            _customerRepo = new CustomerRepoImpl();
            carRent = new List<CarRental>();
        }

        [BindProperty]
        public List<CarRental> carRent { get; set; }

        
        public void OnGet()
        {

            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (IsAuthorized(userLogin) == false)
            {
                Response.Redirect("/Login");
            }
            else
            {
                Customer customer = _customerRepo.getCustomerByEmail(userLogin);
                var list = _carRentalRepo.getListByCustomerId(customer.CustomerId);
                carRent = list.ToList();
            }
            
            
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

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear(); // xóa thông tin đăng nhập trong Session
            return RedirectToPage("/Login"); // chuyển hướng người dùng về trang chủ
        }
    }
}
