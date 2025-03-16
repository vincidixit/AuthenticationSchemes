using CookieAuthScheme.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthScheme.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if(loginModel.Username == "cisco.ramon" && loginModel.Password == "Admin@123")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }   
        }

        public IActionResult Logout()
        {
            return View();
        }


    }
}
