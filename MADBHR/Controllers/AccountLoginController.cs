using MADBHR.Helper;
using MADBHR_Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MADB.Controllers
{
    public class AccountLoginController : Controller
    {
        private readonly MADBAdminSolutionContext _context;

        public AccountLoginController(MADBAdminSolutionContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            TimeSpan.FromDays(-30);
            return RedirectToAction("Login", "AccountLogin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)//string username, string password
        {
            try
            {
                var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UsernameOrEmail == username && x.Password == password).FirstOrDefault();
                //var pass = MADBHR.Helper.EncryptAndDecrypt.Decrypt(userInfo.Password, username.Trim() + "MADB").Equals(password);
                if (userInfo != null)
                {
                    var claims = new List<Claim>
                    {
                         new Claim(ClaimTypes.NameIdentifier, "NameIdentifire"),
                         new Claim(ClaimTypes.Name, username),

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Dashboard", "Home");

                }
                else
                {
                    ViewBag.Error = "Your UserName or Password Wrong!";

                }
            }
            catch (Exception ex)
            {

            }

            return View();

        }
    }
}