using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MADBHR.Controllers
{
    public class AccountRegisterController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IAccountRegisterServices _accountRegisterServices;
        private readonly Pagination _pagination;
        public AccountRegisterController(MADBAdminSolutionContext context, IAccountRegisterServices accountRegisterServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _accountRegisterServices = accountRegisterServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbUserLogin tbUserLogin = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            List<string> accountType = new List<string>();
            accountType.Add("Head Admin");
            accountType.Add("Super Admin");
            accountType.Add("User");
            ViewData["AccountType"] = new SelectList(accountType, tbUserLogin?.AccountType);
            List<string> departments = new List<string>();
            departments.Add("Admin");
            departments.Add("Account");
            departments.Add("Loan");
            departments.Add("JICA TSL");
            departments.Add("HR");
            departments.Add("Banking");
            departments.Add("Budget");
            departments.Add("IT");
            departments.Add("RSD");
            departments.Add("MEB TSL");
            departments.Add("All");
            ViewData["Department"] = new SelectList(departments, tbUserLogin?.Department);
            List<string> office = new List<string>();
            office.Add("Head Office");
            office.Add("Head Region Office");
            office.Add("Region Office");
            ViewData["Office"] = new SelectList(office, tbUserLogin?.Office);
            var stateDivisions = _context.TbStateDivision.Select(x => new { x.StateDivisionCode, x.StateDivision }).ToList();
            ViewData["StateDivisionCode"] = new SelectList(stateDivisions, "StateDivisionCode", "StateDivision", tbUserLogin?.StateDivisionId);
            var stateDivisionsforHeadAdmin = _context.TbStateDivision.Where(x=>x.StateDivisionPkid>=18).Select(x => new { x.StateDivisionCode, x.StateDivision }).ToList();
            ViewData["StateDivisionCodeForHeadAdmin"] = new SelectList(stateDivisionsforHeadAdmin, "StateDivisionCode", "StateDivision", tbUserLogin?.StateDivisionId);
            var townships = _context.TbTownAndDivision.Select(x => new { x.TownCode, x.TownName }).ToList();
            ViewData["TownshipCode"] = new SelectList(townships, "TownCode", "TownName", tbUserLogin?.TownshipId);
        }
        public IActionResult Index(string? StateDivisionCode = null, string? TownshipCode = null, int? page = 1)
        {
            Initialize();
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            if (userInfo.AccountType == "Super Admin")
            {
                StateDivisionCode = userInfo.StateDivisionId;
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);
            }
            else if (userInfo.AccountType == "Head Admin")
            {
                var stateDivisionCodes = _context.TbStateDivision.Select(x => new { x.StateDivision, x.StateDivisionCode }).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision");
            }
            else if (userInfo.AccountType == "User")
            {
                TownshipCode = _context.TbCurrentJobTownship.Where(x => x.UploadForTownship == userInfo.TownshipId).Select(x => x.TownshipCode).FirstOrDefault();
                StateDivisionCode = userInfo.StateDivisionId;
                TownshipCode = TownshipCode == null ? "0" : TownshipCode;
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);

            }

            TempData["TownshipCode"] = TownshipCode;
            TempData["StateDivisionCode"] = StateDivisionCode;
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            // var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var accounts = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.StateDivisionId==StateDivisionCode && x.TownshipId==TownshipCode).ToList();
            return View(accounts.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbUserLogin userLogin)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {                 
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    userLogin.UserPkid = 0;
                    var emp = await _accountRegisterServices.SaveAccount(userLogin, Convert.ToInt32(userId), 0);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {

                    await transaction.RollbackAsync();
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult GetUserInfo(string useroremail)
        {
            var result = _context.TbUserLogin.Where(x => x.UsernameOrEmail == useroremail && x.Status == "Enable").FirstOrDefault();
            return Json(result);
        }
        [HttpGet]
        public IActionResult GetTownhsipByDivision(string divisonCode)
        {
            var townships = _context.TbTownAndDivision.Where(x => x.DivisionCode == divisonCode).ToList();
            //ViewData["PlaceOfBirth"] = new SelectList(townships, "TownshipCode", "Township");
            return Json(townships);
        }

    }
}
