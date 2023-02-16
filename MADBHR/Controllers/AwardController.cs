using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly ILogger<AwardController> _logger;
        public AwardController(MADBAdminSolutionContext context, IAwardServices awardServices, IOptions<Pagination> pagination,ILogger<AwardController> logger)
        {
            _context = context;
            _awardServices = awardServices;
            _pagination = pagination.Value;
            _logger = logger;
        }
        public void Initialize(TbAward tbAward = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var awardTypeCode = _context.TbAwardType.Where(x => x.Active == true).ToList();
            ViewData["AwardType"] = new SelectList(awardTypeCode,"AwardTypeCode","AwardType" ,tbAward?.AwardTypeCode);

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
           // var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var awrds = _awardServices.GetAwardForAdmin(StateDivisionCode,TownshipCode).ToList();
            return View(awrds.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode, string AwardType = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["EmployeeCode"] = EmployeeCode;
            TempData["AwardType"] = AwardType;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
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
                var userId = HttpContext.User.Identity.Name;
                var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                MappedDiagnosticsLogicalContext.Set("userId", userInfo.UserPkid);
                try
                {

                    //if (ModelState.IsValid)
                    //{
                
                    award.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    award.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == award.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _awardServices.SaveAward(award, Convert.ToInt32(userId), 0);
                    _logger.LogInformation("Successfully Create");
                    return RedirectToAction("Index");

                    //}
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
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
                var userId = HttpContext.User.Identity.Name;
                var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                MappedDiagnosticsLogicalContext.Set("userId", userInfo.UserPkid);
                try
                {

                    //if (ModelState.IsValid)
                    //{
                  
                    award.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _awardServices.SaveAward(award, Convert.ToInt32(userId), award.AwardPkid);
                    _logger.LogInformation("Successfully Edit");
                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    await transaction.RollbackAsync();
                }
            }

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.User.Identity.Name;
            MappedDiagnosticsLogicalContext.Set("userId",Convert.ToInt32(userId));
            try
            {
                
                _awardServices.DeleteAward(id, Convert.ToInt32(userId));
                _logger.LogInformation("Successfully Deleted");

                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForIndex()
        {
            
            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var awrds = _awardServices.GetAwardForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "ချီးမြင့်ခံရသည့်ဘွဲ့တံဆိပ်";


            for (int i = 0; i < awrds.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = awrds[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = awrds[i].Township;
                worksheet.Cells[i + 2, 3].Value = awrds[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = awrds[i].RankType;
                worksheet.Cells[i + 2, 5].Value = awrds[i].Department;
                worksheet.Cells[i + 2, 6].Value = awrds[i].AwardType;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AwardForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {


            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();
            string? AwardType = TempData["AwardType"] == null ? null : TempData["AwardType"].ToString();
            var awrds = _awardServices.GetAward(AwardType, EmployeeCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "ချီးမြင့်ခံရသည့်ဘွဲ့တံဆိပ်";
            worksheet.Cells[1, 7].Value = "ချီးမြင့်ခံရသည့်နေ့စွဲ";
            worksheet.Cells[1, 8].Value = "ချီးမြင့်ခံရသည့်နှစ်";
            worksheet.Cells[1, 9].Value = "အကြောင်းအရင်း";


            for (int i = 0; i < awrds.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = awrds[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = awrds[i].Township;
                worksheet.Cells[i + 2, 3].Value = awrds[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = awrds[i].RankType;
                worksheet.Cells[i + 2, 5].Value = awrds[i].Department;
                worksheet.Cells[i + 2, 6].Value = awrds[i].AwardType;
                worksheet.Cells[i + 2, 7].Value = awrds[i].AwardDateStr;
                worksheet.Cells[i + 2, 8].Value = awrds[i].AwardYear;
                worksheet.Cells[i + 2, 9].Value = awrds[i].Reason;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AwardForDetail.xlsx");
        }
    }
}
