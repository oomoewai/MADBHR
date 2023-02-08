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
    public class PensionController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        private readonly IPensionServices _pensionServices;
        private readonly Pagination _pagination;

        public PensionController(MADBAdminSolutionContext context, IEmployeeDisposalServices employeeDisposalServices,IPensionServices pensionServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _employeeDisposalServices = employeeDisposalServices;
            _pensionServices = pensionServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbPension? tbPension = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var pensionType = _context.TbPensionType.Where(x => x.Active == true).ToList();
            ViewData["PensionTypeCode"] = new SelectList(pensionType, "PensionTypeCode", "PensionType", tbPension?.PensionTypeCode);
        }
        [HttpGet]
        public IActionResult GetEmployeeInformation(string SerialNumber)
        {
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(SerialNumber);
            return Json(empInfo);
        }

        public IActionResult Index(string? StateDivisionCode = null, int? page = 1)
        {
            Initialize();
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;

            if (userInfo.AccountType == "Super Admin")
            {
                StateDivisionCode = userInfo.StateDivisionId;
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);
            }
            else
            {
                var stateDivisionCodes = _context.TbStateDivision.Select(x => new { x.StateDivision, x.StateDivisionCode }).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision");
            }
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var pensions = _pensionServices.GetPensionEmployeeCount(StateDivisionCode).ToList();
            return View(pensions.ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string? StateDivisionCode = null, string? TownshipCode = null, string? SerialNumber = null, string? DisposalTypeCode = null, int? page = 1)
        {
            Initialize();
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            //StateDivisionCode = userInfo.StateDivisionId;            
            if (userInfo.AccountType == "User")
            {
                TownshipCode = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.UploadForTownship == userInfo.TownshipId).Select(x => x.TownshipCode).FirstOrDefault();
                TownshipCode = TownshipCode == null ? "0" : TownshipCode;
                TempData["TownshipCode"] = TownshipCode;
                var townshipCodes = _context.TbCurrentJobTownship.Where(x => x.StateDivisionId == userInfo.StateDivisionId && x.UploadForTownship == userInfo.TownshipId).ToList();
                ViewData["TownshipCode"] = new SelectList(townshipCodes, "TownshipCode", "Township");
            }
            else
            {
                var townshipCodes = _context.TbCurrentJobTownship.Where(x => x.StateDivisionId == StateDivisionCode).ToList();
                TempData["TownshipCode"] = TownshipCode;
                ViewData["TownshipCode"] = new SelectList(townshipCodes, "TownshipCode", "Township");
            }
            TempData["StateDivisionCode"] = StateDivisionCode;
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var pensions = _pensionServices.GetPension(StateDivisionCode,TownshipCode,EmployeeCode).ToList();
            return View(pensions.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbPension pension)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    pension.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    pension.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == pension.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _pensionServices.SavePension(pension, Convert.ToInt32(userId), 0);

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
            var pension = _context.TbPension.Where(x => x.PensionPkid == Id).FirstOrDefault();
            pension.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == pension.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(pension.SerialNumber);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
            ViewBag.FatherName = empInfo.FatherName;
            ViewBag.DateOfBirth = empInfo.DateOfBirth;
            ViewBag.Department = empInfo.Township;
            Initialize(pension);
            return View(pension);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbPension pension, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    pension.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _pensionServices.SavePension(pension, Convert.ToInt32(userId), pension.PensionPkid);

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
                _pensionServices.DeletePension(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult ExcelExport()
        {

            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            var pensions = _pensionServices.GetPension(StateDivisionCode, TownshipCode, null).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ဌာနအမည်";
            worksheet.Cells[1, 5].Value = "ရာထူး";
            worksheet.Cells[1, 6].Value = "အစီရင်ခံစာအမှတ်";
            worksheet.Cells[1, 7].Value = "စာရင်းစစ်ရက်စွဲ";
            worksheet.Cells[1, 8].Value = "ပင်စင်အမျိုးအစား";
            worksheet.Cells[1, 9].Value = "နောက်ဆုံးထုတ်ယူခဲ့သည့်လစာ";
            worksheet.Cells[1, 10].Value = "လစဉ်ပင်စင်";
            worksheet.Cells[1, 11].Value = "ပင်စင်စတင်ခွင့်ပြုသည့်နေ့";
            worksheet.Cells[1, 12].Value = "ပင်စင်လစာထုတ်ယူသည့်ဘဏ်";
            worksheet.Cells[1, 13].Value = "မှတ်ချက်";
            

            for (int i = 0; i < pensions.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = pensions[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = pensions[i].Township;
                worksheet.Cells[i + 2, 3].Value = pensions[i].Name;
                worksheet.Cells[i + 2, 4].Value = pensions[i].Department;
                worksheet.Cells[i + 2, 5].Value = pensions[i].RankType;
                worksheet.Cells[i + 2, 6].Value = pensions[i].PensionReportNo;
                worksheet.Cells[i + 2, 7].Value = pensions[i].PensionDateStr;
                worksheet.Cells[i + 2, 8].Value = pensions[i].PensionTypeStr;
                worksheet.Cells[i + 2, 9].Value = pensions[i].LatestSalary;
                worksheet.Cells[i + 2, 10].Value = pensions[i].MonthlyPension;
                worksheet.Cells[i + 2, 11].Value = pensions[i].PensionStartDateStr;
                worksheet.Cells[i + 2, 12].Value = pensions[i].PensionBank;
                worksheet.Cells[i + 2, 13].Value = pensions[i].Remark;

            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PensionData.xlsx");
        }
    }
}
