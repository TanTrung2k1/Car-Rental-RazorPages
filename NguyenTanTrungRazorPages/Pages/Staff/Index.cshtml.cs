using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace NguyenTanTrungRazorPages.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private ICarRepo _carRepo;
        private ICarProducerRepo _carProducerRepo;
        

        public IndexModel()
        {
            _carRepo = new CarRepoImpl();
            _carProducerRepo = new CarProducerRepoImpl();
        }

        public List<Car> listCar { get; set; }
        public List<CarProducer> producers { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? searchString { get; set; }

        public void OnGet()
        {
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (!IsAuthorized(userLogin))
            {
                Response.Redirect("/");
            }

            //if (searchString != null)
            //{
                //var list = _carRepo.GetListByName(searchString);
                //listCar = list.ToList();
            //}
            //else
            //{
                var list = _carRepo.GetAll();
                listCar = list.ToList();
            //}
        }

        public void OnPost()
        {
            if(searchString != null)
            {
                var list = _carRepo.GetListByName(searchString);
                listCar = list.ToList();
            }
            else
            {
                var list = _carRepo.GetAll();
                listCar = list.ToList();
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

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear(); // xóa thông tin đăng nhập trong Session
            return RedirectToPage("/Login"); // chuyển hướng người dùng về trang chủ
        }
    }
}
