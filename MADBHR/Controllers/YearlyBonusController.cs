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
    public class YearlyBonusController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IYearlyBonusServices _yearlyBonusServices;
        private readonly Pagination _pagination;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        public YearlyBonusController(MADBAdminSolutionContext context, IYearlyBonusServices yearlyBonusServices, IOptions<Pagination> pagination,IEmployeeDisposalServices employeeDisposalServices)
        {
            _context = context;
            _yearlyBonusServices = yearlyBonusServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
        }
        public void Initialize(TbYearlyBonus tbYearlyBonus = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
        }
        public IActionResult Index(string? StateDivisionCode = null, string? TownshipCode = null, int? page = 1)
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
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var yearlyBonus = _yearlyBonusServices.GetYearlyBonusForAdmin( StateDivisionCode,TownshipCode).ToList();
            return View(yearlyBonus.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode = null, DateTime? ApprovedDate = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["ApprovedDate"] = ApprovedDate;
            TempData["EmployeeCode"] = EmployeeCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var yearlyBonus = _yearlyBonusServices.GetYearlyBonus(EmployeeCode, ApprovedDate).ToList();
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

                    yearlyBonus.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == yearlyBonus.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
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
            yearlyBonus.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == yearlyBonus.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(yearlyBonus.SerialNumber);
            Initialize(yearlyBonus);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
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
        [HttpGet]
        public async Task<IActionResult> ExcelExportForIndex()
        {
            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var yearlyBonus = _yearlyBonusServices.GetYearlyBonusForAdmin(StateDivisionCode, TownshipCode).ToList();
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


            for (int i = 0; i < yearlyBonus.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = yearlyBonus[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = yearlyBonus[i].Township;
                worksheet.Cells[i + 2, 3].Value = yearlyBonus[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = yearlyBonus[i].RankType;
                worksheet.Cells[i + 2, 5].Value = yearlyBonus[i].Department;
                worksheet.Cells[i + 2, 6].Value = yearlyBonus[i].ApproveDateStr;
                worksheet.Cells[i + 2, 7].Value = yearlyBonus[i].ApprovedNo;

            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "YearlyBonusForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {


            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();
            DateTime? ApprovedDate = Convert.ToDateTime(TempData["ApprovedDate"]);
            if (TempData["FromDate"] == null)
            {
                ApprovedDate = null;

            }
            var yearlyBonus = _yearlyBonusServices.GetYearlyBonus(EmployeeCode, ApprovedDate).ToList();
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
            worksheet.Cells[1, 8].Value = "နှစ်တိုးအကြိမ်";
            worksheet.Cells[1, 9].Value = "နှစ်တိုးလစာငွေကျပ်";
            worksheet.Cells[1, 10].Value = "နှစ်တိုးရက်စွဲ";


            for (int i = 0; i < yearlyBonus.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = yearlyBonus[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = yearlyBonus[i].Township;
                worksheet.Cells[i + 2, 3].Value = yearlyBonus[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = yearlyBonus[i].RankType;
                worksheet.Cells[i + 2, 5].Value = yearlyBonus[i].Department;
                worksheet.Cells[i + 2, 6].Value = yearlyBonus[i].ApproveDateStr;
                worksheet.Cells[i + 2, 7].Value = yearlyBonus[i].ApprovedNo;
                worksheet.Cells[i + 2, 8].Value = yearlyBonus[i].YearlyBonusCount;
                worksheet.Cells[i + 2, 9].Value = yearlyBonus[i].YearlyBonusSalary;
                worksheet.Cells[i + 2, 10].Value = yearlyBonus[i].YearlyBonusDate;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "YearlyBonusForDetail.xlsx");
        }
    }
}
