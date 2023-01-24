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
    public class YearlyBonusController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IYearlyBonusServices _yearlyBonusServices;
        private readonly Pagination _pagination;
        public YearlyBonusController(MADBAdminSolutionContext context, IYearlyBonusServices yearlyBonusServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _yearlyBonusServices = yearlyBonusServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbYearlyBonus tbYearlyBonus = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
        }
        public IActionResult Index(string? SerialNumber = null, DateTime? ApprovedDate = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var yearlyBonus = _yearlyBonusServices.GetYearlyBonus( EmployeeCode,ApprovedDate).ToList();
            return View(yearlyBonus.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbYearlyBonus yearlyBonus)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    yearlyBonus.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    yearlyBonus.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == yearlyBonus.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _yearlyBonusServices.SaveYearlyBonus(yearlyBonus, Convert.ToInt32(userId), 0);

                    return RedirectToAction("Index");

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
            var yearlyBonus = _context.TbYearlyBonus.Where(x => x.YearlyBonusPkid == Id).FirstOrDefault();
            yearlyBonus.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == yearlyBonus.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(yearlyBonus);
            return View(yearlyBonus);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbYearlyBonus yearlyBonus, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    yearlyBonus.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _yearlyBonusServices.SaveYearlyBonus(yearlyBonus, Convert.ToInt32(userId), yearlyBonus.YearlyBonusPkid);

                    return RedirectToAction("Index");

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
                _yearlyBonusServices.DeleteYearlyBonus(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
