using DocumentsManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel login)
        {
            LoginLogoutExampleContext context = new LoginLogoutExampleContext();
            var user = context.Userdetails.SingleOrDefault(u=>u.Email ==login.Email && u.Password == login.Password);
            if (user == null)
            {
                ViewBag.LoginFailed = "Login failed, please try again";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("userId", user.Id.ToString());
            }
            return RedirectToAction("index","home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            _13_NguyenQuangSu_TranLongKhanh_ProjectContext context=new _13_NguyenQuangSu_TranLongKhanh_ProjectContext();
            if (ModelState.IsValid)
            {   
                Userdetail u = new Userdetail
                {
                    Name = register.Name,
                    Email = register.Email,
                    Password = register.Password,
                    Mobile = register.Mobile,
                };


                var exist = context.Userdetails.SingleOrDefault(e=>e.Email==u.Email);
                
                if (exist == null)
                {
                    context.Userdetails.Add(u);
                    context.SaveChanges();
                    return RedirectToAction("index", "login");
                }
                else
                {
                    ViewBag.ExistEmail = "this Email already exist";
                    return View();
                }
                
            }


            else
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
