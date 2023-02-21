using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MADBHR.Controllers
{
    public class SonAndDaughterController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly ISonAndDaughterServices _sonAndDaughterServices;
        private readonly Pagination _pagination;

        public SonAndDaughterController(MADBAdminSolutionContext context, ISonAndDaughterServices sonAndDaughterServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _sonAndDaughterServices = sonAndDaughterServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbSonAndDaughter sonAndDaughter = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            

        }
        public IActionResult Index(string? EmployeeCode = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var sonAndDaughters = _context.TbSonAndDaughter.Where(x => x.EmployeeCode == EmployeeCode && (x.IsDeleted == false||x.IsDeleted==null)).ToList();
            return View(sonAndDaughters.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create(string? serialNumber = null,string? EmployeeCode=null)
        {
            Initialize();
            if (serialNumber != null)
            {
                ViewData["SerialNumber"] = serialNumber;
                TempData["EmployeeCode"] = EmployeeCode;
                ViewBag.EmployeeCode = EmployeeCode;
            }
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbSonAndDaughter sonAndDaughter)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    sonAndDaughter.EmployeeCode = TempData["EmployeeCode"].ToString();
                    var emp = await _sonAndDaughterServices.SaveSonAndDaughter(sonAndDaughter, Convert.ToInt32(userId), 0);

                    if (userInfo.AccountType == "Head Admin" || userInfo.AccountType == "Super Admin")
                        return RedirectToAction("AdminDivisionIndex", "Employee");
                    else
                        return RedirectToAction("AdminIndex", "Employee");

                    //}
                }
                catch (Exception e)
                {

                    await transaction.RollbackAsync();
                }
            }

            return View();
        }
        public IActionResult Edit(int Id)
        {
            var sonAndDaughter = _context.TbSonAndDaughter.Where(x => x.SonAndDaughterPkid == Id).FirstOrDefault();
            ViewData["SerialNumber"] = _context.TbEmployee.Where(x => x.EmployeeCode == sonAndDaughter.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(sonAndDaughter);
            ViewBag.EmployeeCode = sonAndDaughter.EmployeeCode;
            return View(sonAndDaughter);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbSonAndDaughter sonAndDaughter)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var emp = await _sonAndDaughterServices.SaveSonAndDaughter(sonAndDaughter, Convert.ToInt32(userId), sonAndDaughter.SonAndDaughterPkid);

                    return RedirectToAction("Index", new { EmployeeCode = emp.EmployeeCode });

                    //}
                }
                catch (Exception e)
                {

                    await transaction.RollbackAsync();
                }
            }

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                _sonAndDaughterServices.DeleteSonAndDaughter(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
