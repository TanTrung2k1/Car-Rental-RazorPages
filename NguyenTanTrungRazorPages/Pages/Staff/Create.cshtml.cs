using BusinessObjects.Models;
using DataAccessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenTanTrungRazorPages.GenerateId;
using Repository;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;


namespace NguyenTanTrungRazorPages.Pages.Staff
{
    public class CreateModel : PageModel
    {
        private ICarProducerRepo _carProducerRepo;
        private ICarRepo _carRepo;
        public CreateModel()
        {
            _carProducerRepo = new CarProducerRepoImpl();
            _carRepo = new CarRepoImpl();
            var listProducer = _carProducerRepo.GetAll();
            producers = listProducer.ToList();
        }

        [BindProperty]
        public Car car { get; set; }
        public List<CarProducer> producers { get; set; }
        public void OnGet()
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (!IsAuthorized(userLogin))
            {
                Response.Redirect("/");
            }
        }

        public IActionResult OnPost()
        {
            DateTime today = DateTime.Now.Date;
            if (car.ImportDate > today)
            {
                ModelState.AddModelError("car.ImportDate", "The Import date cannot greeter than today "+(today.ToString("dd/MM/yyyy")));
                return Page();
            }
            GenerateNewId idGenerator = new GenerateNewId();
            car.CarId = idGenerator.GenerateCarId();
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _carRepo.AddCar(car);


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
