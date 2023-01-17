using MADBHR.Models;
using MADBHR_Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MADBHR.Controllers
{
    public class HomeController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
       
        public HomeController(MADBAdminSolutionContext context)  
        {
            _context = context;
           
        }

        public IActionResult Index()
        {
            
           
            return View();
        }
        public IActionResult Dashboard()
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn =_context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid==Convert.ToInt64(userId) ).FirstOrDefault();
         
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
