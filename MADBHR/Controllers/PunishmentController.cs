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
    public class PunishmentController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IPunishmentServices _punishmentServices;
        private readonly Pagination _pagination;
        public PunishmentController(MADBAdminSolutionContext context, IPunishmentServices punishmentServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _punishmentServices = punishmentServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbPunishment tbPunishment = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var punishmentTypeCode = _context.TbPunishmentType.Where(x => x.Active == true).ToList();
            ViewData["PunishmentType"] = new SelectList(punishmentTypeCode, "PunishmentTypeCode", "PunishmentType", tbPunishment?.PunishmentTypeCode);

        }
        public IActionResult Index(string? SerialNumber = null, string PunishmentType = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var awrds = _punishmentServices.GetPunishment(PunishmentType, EmployeeCode).ToList();
            return View(awrds.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbPunishment punishment)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    punishment.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    punishment.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == punishment.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _punishmentServices.SavePunishment(punishment, Convert.ToInt32(userId), 0);

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
            var punishment = _context.TbPunishment.Where(x => x.PunishmentPkid == Id).FirstOrDefault();
            punishment.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == punishment.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(punishment);
            return View(punishment);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbPunishment punishment, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    punishment.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _punishmentServices.SavePunishment(punishment, Convert.ToInt32(userId), punishment.PunishmentPkid);

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
                _punishmentServices.DeletePunishment(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
