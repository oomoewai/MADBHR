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
    public class LeaveEntitlementController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly ILeaveEntitlementServices _leaveEntitlementServices;
        private readonly Pagination _pagination;
        public LeaveEntitlementController(MADBAdminSolutionContext context, ILeaveEntitlementServices leaveEntitlementServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _leaveEntitlementServices = leaveEntitlementServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbLeaveEntitlement tbLeaveEntitlement = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var LeaveType = _context.TbLeaveType.Select(x => new { x.LeaveTypeCode, x.LeaveType }).ToList();
            ViewData["LeaveType"] = new SelectList(LeaveType, "LeaveTypeCode", "LeaveType", tbLeaveEntitlement?.LeaveTypeCode);
            
        }
        public IActionResult Index(string? SerialNumber = null, DateTime? FromDate = null, DateTime? ToDate = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var leaves = _leaveEntitlementServices.GetLeaveEntitlement(EmployeeCode, FromDate, ToDate).ToList();
            return View(leaves.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbLeaveEntitlement leaveEntitlement)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    leaveEntitlement.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    leaveEntitlement.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == leaveEntitlement.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _leaveEntitlementServices.SaveLeaveEntitlement(leaveEntitlement, Convert.ToInt32(userId), 0);

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
            var leaveEntitlement = _context.TbLeaveEntitlement.Where(x => x.LeaveEntitlementPkid == Id).FirstOrDefault();
            leaveEntitlement.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == leaveEntitlement.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(leaveEntitlement);
            return View(leaveEntitlement);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbLeaveEntitlement leaveEntitlement, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    leaveEntitlement.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _leaveEntitlementServices.SaveLeaveEntitlement(leaveEntitlement, Convert.ToInt32(userId), leaveEntitlement.LeaveEntitlementPkid);

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
                _leaveEntitlementServices.DeleteLeaveEntitlement(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
