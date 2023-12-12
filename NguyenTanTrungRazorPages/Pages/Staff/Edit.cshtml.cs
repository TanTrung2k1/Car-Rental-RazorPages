using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NguyenTanTrungRazorPages.Pages.Staff
{
    public class EditModel : PageModel
    {
        private ICarRepo _repo;
        private ICarProducerRepo _producerRepo;
        public EditModel()
        {
            _producerRepo = new CarProducerRepoImpl();
            _repo = new CarRepoImpl();
            var listProducer = _producerRepo.GetAll();
            producers = listProducer.ToList();
        }

        [BindProperty]
        public Car car { get; set; }
        public List<CarProducer> producers { get; set; }
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
            car = _repo.GetCarByID(id);
            if (car == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            DateTime today = DateTime.Now.Date;
            if (car.ImportDate > today)
            {
                ModelState.AddModelError("car.ImportDate", "The Import date cannot greeter than today " + (today.ToString("dd/MM/yyyy")));
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (car != null)
            {
                _repo.UpdateCar(car);
            }


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
