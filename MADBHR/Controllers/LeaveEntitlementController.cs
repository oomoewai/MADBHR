using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        public LeaveEntitlementController(MADBAdminSolutionContext context, ILeaveEntitlementServices leaveEntitlementServices, IOptions<Pagination> pagination,IEmployeeDisposalServices employeeDisposalServices)
        {
            _context = context;
            _leaveEntitlementServices = leaveEntitlementServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
        }
        public void Initialize(TbLeaveEntitlement tbLeaveEntitlement = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var LeaveType = _context.TbLeaveType.Select(x => new { x.LeaveTypeCode, x.LeaveType }).ToList();
            ViewData["LeaveType"] = new SelectList(LeaveType, "LeaveTypeCode", "LeaveType", tbLeaveEntitlement?.LeaveTypeCode);
            
        }
        public IActionResult Index(string? StateDivisionCode = null,string? TownshipCode=null, string? Name = null, string? SerialNumber = null, int? page = 1)
        {
            Initialize();
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            ViewBag.AccountType = userInfo.AccountType;
            ViewBag.TownshipId = userInfo.TownshipId;
            if (userInfo.AccountType == "Super Admin")
            {
                StateDivisionCode = userInfo.StateDivisionId;
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).OrderBy(x => x.StateDivision).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);
            }
            else if (userInfo.AccountType == "Head Admin")
            {
                var stateDivisionCodes = _context.TbStateDivision.Select(x => new { x.StateDivision, x.StateDivisionCode }).OrderBy(x => x.StateDivision).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision");
            }
            else if (userInfo.AccountType == "User")
            {
                if (userInfo.TownshipId == "0010")
                {
                    StateDivisionCode = userInfo.StateDivisionId;
                }
                else
                {
                    TownshipCode = _context.TbCurrentJobTownship.Where(x => x.UploadForTownship == userInfo.TownshipId).Select(x => x.TownshipCode).FirstOrDefault();
                    StateDivisionCode = userInfo.StateDivisionId;
                    TownshipCode = TownshipCode == null ? "0" : TownshipCode;
                }
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).OrderBy(x => x.StateDivision).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);

            }
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["TownshipCode"] = TownshipCode;
            TempData["StateDivisionCode"] = StateDivisionCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var leaves = _leaveEntitlementServices.GetLeaveEntitlementForAdmin(StateDivisionCode, TownshipCode,Name,SerialNumber).ToList();
            return View(leaves.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode, DateTime? FromDate = null, DateTime? ToDate = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["EmployeeCode"] = EmployeeCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
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

                    leaveEntitlement.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == leaveEntitlement.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
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
            leaveEntitlement.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == leaveEntitlement.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(leaveEntitlement.SerialNumber);
            Initialize(leaveEntitlement);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
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
        [HttpGet]
        public async Task<IActionResult> ExcelExportForIndex()
        {

            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var leaves = _leaveEntitlementServices.GetLeaveEntitlementForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "အမိန့်စာရက်စွဲ";
            worksheet.Cells[1, 7].Value = "အမိန့်စာအမှတ်";

            for (int i = 0; i < leaves.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = leaves[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = leaves[i].Township;
                worksheet.Cells[i + 2, 3].Value = leaves[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = leaves[i].RankType;
                worksheet.Cells[i + 2, 5].Value = leaves[i].Department;
                worksheet.Cells[i + 2, 6].Value = leaves[i].ApproveDateStr;
                worksheet.Cells[i + 2, 7].Value = leaves[i].ApprovedNo;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IntKnowledgeForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {


            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();
            DateTime? FromDate = Convert.ToDateTime(TempData["FromDate"]);
            DateTime? ToDate = Convert.ToDateTime(TempData["ToDate"]);
            if (TempData["FromDate"] == null)
            {
                FromDate = null;
                ToDate = null;

            }
            var leaves = _leaveEntitlementServices.GetLeaveEntitlement(EmployeeCode, FromDate, ToDate).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "အမိန့်စာရက်စွဲ";
            worksheet.Cells[1, 7].Value = "အမိန့်စာအမှတ်";
            worksheet.Cells[1, 8].Value = "ခွင့်ခံစားမည့်ရက်စွဲ (မှ)";
            worksheet.Cells[1, 9].Value = "ခွင့်ခံစားမည့်ရက်စွဲ (ထိ)";
            worksheet.Cells[1, 10].Value = "ကာလ";
            worksheet.Cells[1, 11].Value = "ခွင့်အမျိုးအစား";

            for (int i = 0; i < leaves.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = leaves[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = leaves[i].Township;
                worksheet.Cells[i + 2, 3].Value = leaves[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = leaves[i].RankType;
                worksheet.Cells[i + 2, 5].Value = leaves[i].Department;
                worksheet.Cells[i + 2, 6].Value = leaves[i].ApproveDateStr;
                worksheet.Cells[i + 2, 7].Value = leaves[i].ApprovedNo;
                worksheet.Cells[i + 2, 8].Value = leaves[i].StartDateStr;
                worksheet.Cells[i + 2, 9].Value = leaves[i].EndDateStr;
                worksheet.Cells[i + 2, 10].Value = leaves[i].Period;
                worksheet.Cells[i + 2, 11].Value = leaves[i].LeaveType;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IntKnowledgeForDetail.xlsx");
        }
    }
}
