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
    public class ReportModel : PageModel
    {
        private ICarRentalRepo _carRentalRepo;

        public ReportModel()
        {
            _carRentalRepo = new CarRentalRepoImpl();
            startDate = DateTime.Today.Date;
            endDate = DateTime.Today.Date;
        }

        [BindProperty]
        public DateTime startDate { get; set; }
        [BindProperty]
        public DateTime endDate { get; set; }

        [BindProperty]
        public List<CarRental> carRent { get; set; }

        public void OnGet()
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (!IsAuthorized(userLogin))
            {
                Response.Redirect("/");
            }

            var list = _carRentalRepo.GetAllAndDescending();
            carRent = list.ToList();   
        }

        public void OnPost()
        {
            if(startDate > endDate)
            {
                ModelState.AddModelError("endDate", "The return end date must be after the start date");
            }
            else
            {
                carRent = _carRentalRepo.Report(startDate, endDate);
            }
            
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
