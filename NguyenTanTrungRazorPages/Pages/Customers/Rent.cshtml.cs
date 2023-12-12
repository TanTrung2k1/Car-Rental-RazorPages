using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;
using System;

namespace NguyenTanTrungRazorPages.Pages.Customers
{
    public class RentModel : PageModel
    {
        private ICarRentalRepo _carRentalRepo;
        private ICustomerRepo _customerRepo;
        private ICarRepo _carRepo;

        public RentModel()
        {
            _carRentalRepo = new CarRentalRepoImpl();
            _customerRepo = new CustomerRepoImpl();
            _carRepo = new CarRepoImpl();
            carRental = new CarRental();
        }
        [BindProperty]
        public CarRental carRental { get; set; }

        [BindProperty]
        public string customerName { get; set; }
        [BindProperty]

        public string carName { get; set; }

        [BindProperty]
        public decimal? rentPrice { get; set; }

        public string errorMessage { get; set; }
        
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
            
            
            var user = _customerRepo.getCustomerByEmail(userLogin);
            var car = _carRepo.GetCarByID(id);
            carRental.CustomerId = user.CustomerId;
            carRental.CarId = car.CarId;
            carRental.RentPrice = car.RentPrice;
            carRental.PickupDate = DateTime.Today;
            carRental.ReturnDate = DateTime.Today.AddDays(1);

            customerName = user.CustomerName;
            carName = car.CarName;
            rentPrice = car.RentPrice;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (carRental != null)
            {
                DateTime startD = carRental.PickupDate;
                DateTime endD = carRental.ReturnDate;

                if (endD < startD)
                {
                    ModelState.AddModelError("carRental.ReturnDate", "The return date must be after the pickup date");
                    return Page();
                }
                else if (!_carRentalRepo.AvailableForRental(carRental.CarId, startD, endD))
                {
                    ModelState.AddModelError("carRental.ReturnDate", "The above time has been rented by someone else.");
                    return Page();
                }
                else
                {
                    var price = carRental.RentPrice;
                    TimeSpan time = endD - startD;
                    int days = time.Days + 1;

                    carRental.RentPrice = price * days;

                    _carRentalRepo.Add(carRental);
                }
            }
            return RedirectToPage("./Index");

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
