using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MADBHR.Controllers
{
    public class PensionAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
