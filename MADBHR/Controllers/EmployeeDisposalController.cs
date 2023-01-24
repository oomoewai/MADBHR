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
    public class EmployeeDisposalController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        private readonly Pagination _pagination;

        public EmployeeDisposalController(MADBAdminSolutionContext context, IEmployeeDisposalServices employeeDisposalServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _employeeDisposalServices = employeeDisposalServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbDisposal? tbDisposal = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var disposalType = _context.TbDisposalType.Where(x => x.Active == true).ToList();
            ViewData["DisposalTypeCode"] = new SelectList(disposalType, "DisposalTypeCode", "DisposalType", tbDisposal?.DisposalTypeCode);
        }
        [HttpGet]
        public IActionResult GetEmployeeInformation(string SerialNumber)
        {
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(SerialNumber); 
            return Json(empInfo);
        }

        public IActionResult Index(string? SerialNumber = null, string DisposalTypeCode = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var disposals = _employeeDisposalServices.GetEmployeeDisposal(DisposalTypeCode, EmployeeCode).ToList();
            return View(disposals.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbDisposal disposal)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    disposal.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    disposal.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == disposal.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _employeeDisposalServices.SaveEmployeeDisposal(disposal, Convert.ToInt32(userId), 0);

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
            var disposal = _context.TbDisposal.Where(x => x.DisposalPkid == Id).FirstOrDefault();
            disposal.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == disposal.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(disposal);
            return View(disposal);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbDisposal disposal, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    disposal.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _employeeDisposalServices.SaveEmployeeDisposal(disposal, Convert.ToInt32(userId), disposal.DisposalPkid);

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
                var EmployeeCode = _context.TbDisposal.Where(x => x.DisposalPkid == id).Select(x => x.EmployeeCode).FirstOrDefault();
                _employeeDisposalServices.DeleteDisposal(EmployeeCode, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
