using CookieAuthScheme.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;

namespace CookieAuthScheme.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login([FromQuery] string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl = null)
        {
            if (ValidateUser(loginModel.Username, loginModel.Password))
            {
                var claims = GetUserClaims(loginModel.Username);

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProps = new AuthenticationProperties { IsPersistent = loginModel.RememberMe };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        private static List<Claim> GetUserClaims(string username)
        {
            if (username == "cisco.ramon@starlabs.com")
            {
                return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Cisco Ramon"),
                    new Claim(ClaimTypes.Role, "Engineer"),
                    new Claim(ClaimTypes.Email, username)
                };
            }

            if (username == "harrison.wells@starlabs.com")
            {
                return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Harrison Wells"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, username)
                };
            }

            return null;
        }

        private bool ValidateUser(string username, string password)
        {
            if(!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                if ((username.Equals("cisco.ramon@starlabs.com") && password.Equals("Tech@123")) ||
                    (username.Equals("harrison.wells@starlabs.com") && password.Equals("Admin@123")))
                {
                    return true;
                }
            }

            return false;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
