using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace NguyenTanTrungRazorPages.Pages.Customers
{
    public class CreateCarRentalModel : PageModel
    {
        private ICarRepo _carRepo;
        private ICarProducerRepo _carProducerRepo;


        public CreateCarRentalModel()
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
            if (IsAuthorized(userLogin) == false)
            {
                Response.Redirect("/Login");
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
            if (searchString != null)
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
