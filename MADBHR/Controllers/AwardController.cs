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
    public class AwardController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IAwardServices _awardServices;
        private readonly Pagination _pagination;
        public AwardController(MADBAdminSolutionContext context, IAwardServices awardServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _awardServices = awardServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbAward tbAward = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var awardTypeCode = _context.TbAwardType.Where(x => x.Active == true).ToList();
            ViewData["AwardType"] = new SelectList(awardTypeCode,"AwardTypeCode","AwardType" ,tbAward?.AwardTypeCode);

        }
        public IActionResult Index(string? SerialNumber = null, string AwardType = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var awrds = _awardServices.GetAward(AwardType, EmployeeCode).ToList();
            return View(awrds.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbAward award)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    award.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    award.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == award.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _awardServices.SaveAward(award, Convert.ToInt32(userId), 0);

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
            var award = _context.TbAward.Where(x => x.AwardPkid == Id).FirstOrDefault();
            award.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == award.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(award);
            return View(award);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbAward award, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    award.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _awardServices.SaveAward(award, Convert.ToInt32(userId), award.AwardPkid);

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
                _awardServices.DeleteAward(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
