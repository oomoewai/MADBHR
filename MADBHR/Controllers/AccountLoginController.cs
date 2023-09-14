using MADBHR.Helper;
using MADBHR_Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
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
        private readonly ILogger<AccountLoginController> _logger;
        

        public AccountLoginController(MADBAdminSolutionContext context, ILogger<AccountLoginController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
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
                var userInfo = _context.TbUserLogin.Where(x => x.UsernameOrEmail == username && x.Password == password).FirstOrDefault();
                if(userInfo.AccountCloseStatus==true)
                {
                    ViewBag.Error = "Your Account Temporary Close bacause Monthly Account Closing Process Running!!!";
                }
                else
                {
                    if (userInfo != null)
                    {
                        MappedDiagnosticsLogicalContext.Set("userId", userInfo.UserPkid);

                        var userActivate = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UsernameOrEmail == username && x.Password == password).FirstOrDefault();
                        if (userActivate != null)
                        {
                            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Convert.ToString(userInfo.UserPkid),ClaimValueTypes.Integer64)
                    };

                            var claimsIdentity = new ClaimsIdentity(claims, "Login");
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            _logger.LogInformation("Successfully Login");
                            if (userInfo.AccountType == "Head Admin" || userInfo.AccountType == "Super Admin")
                                return RedirectToAction("AdminDivisionIndex", "Employee");
                            else
                                return RedirectToAction("AdminIndex", "Employee");
                        }
                        else
                        {
                            ViewBag.Error = "Your account is not activate!";
                        }


                    }
                    else
                    {
                        ViewBag.Error = "Your UserName or Password Wrong!";

                    }
                }
                //var pass = MADBHR.Helper.EncryptAndDecrypt.Decrypt(userInfo.Password, username.Trim() + "MADB").Equals(password);
                
            }
            catch (Exception ex)
            {

            }

            return View();

        }
       

    }
}