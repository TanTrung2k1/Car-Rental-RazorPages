using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Implement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenTanTrungRazorPages.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private IStaffAccountRepo _staffAccountRepo;
        private CarRentalSystemDBContext _context;

        public IndexModel()
        {
            _staffAccountRepo = new StaffAccountRepoImpl();
            _context = new CarRentalSystemDBContext();
            
        }

        [BindProperty(SupportsGet = true)]
        public string? searchString { get; set; }

        
        public List<StaffAccount> listStaff { get; set; }

        
        public bool IsAuthorized(string email)
        {
            if(email == null)
            {
                return false;
            }

            string admin = _context.getDefaultAccount().Email;
            if(email != admin)
            {
                return false;
            }

            return true;
        }


        public void OnGet()
        {
            //check role login
            string userLogin = HttpContext.Session.GetString("USERLOGIN");
            if (!IsAuthorized(userLogin))
            {
                // khong phai la admin
                Response.Redirect("/");
            }

            var list = _staffAccountRepo.GetAll();
            listStaff = list.ToList();

        }

        public void OnPost()
        {
            if(searchString != null)
            {
                var list = _staffAccountRepo.GetListStaffByName(searchString);
                listStaff = list.ToList();
            }
            else
            {
                var list = _staffAccountRepo.GetAll();
                listStaff = list.ToList();
            }
            
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear(); // xóa thông tin đăng nhập trong Session
            return RedirectToPage("/Login"); // chuyển hướng người dùng về trang chủ
        }


    }
}
