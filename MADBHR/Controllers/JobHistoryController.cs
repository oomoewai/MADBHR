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
    public class JobHistoryController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IJobHistoryServices _jobHistoryServices;
        private readonly Pagination _pagination;
        public JobHistoryController(MADBAdminSolutionContext context, IJobHistoryServices jobHistoryServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _jobHistoryServices = jobHistoryServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbJobHistory tbJobHistory = null)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            if (userInfo.AccountType == "Head Admin")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true).ToList();
                ViewData["TownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbJobHistory?.DepartmentName);
            }
            else if (userInfo.AccountType == "Super Admin")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId).ToList();
                ViewData["TownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbJobHistory?.DepartmentName);
            }
            else if (userInfo.AccountType == "User")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId && x.UploadForTownship == userInfo.TownshipId).ToList();
                ViewData["TownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbJobHistory?.DepartmentName);
            }

            var rankType = _context.TbRankType.Select(x => new { x.RankTypeCode,x.RankType }).ToList();
            ViewData["RankType"] = new SelectList(rankType, "RankTypeCode", "RankType", tbJobHistory?.RankTypeCode1);

        }
        public IActionResult Index(string? SerialNumber = null, DateTime? FromDate = null, DateTime? ToDate = null,string? StateDivisionCode=null,string? TownshipCode=null, int? page = 1)
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
            else if(userInfo.AccountType == "Head Admin")
            {
                var stateDivisionCodes = _context.TbStateDivision.Select(x => new { x.StateDivision, x.StateDivisionCode }).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision");
            }
            else if(userInfo.AccountType=="User")
            {
                TownshipCode = _context.TbCurrentJobTownship.Where(x => x.UploadForTownship == userInfo.TownshipId).Select(x => x.TownshipCode).FirstOrDefault() ;
                StateDivisionCode = userInfo.StateDivisionId;
                TownshipCode = TownshipCode == null ? "0" : TownshipCode;
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);

            }
            TempData["SerialNumber"] = SerialNumber;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["TownshipCode"] = TownshipCode;
            TempData["StateDivisionCode"] = StateDivisionCode;
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var jobHistories = _jobHistoryServices.GetCurrentJobHistory(EmployeeCode,StateDivisionCode,TownshipCode).ToList();
            return View(jobHistories.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode, DateTime? FromDate = null, DateTime? ToDate = null,  int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["EmployeeCode"] = EmployeeCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var jobHistories = _jobHistoryServices.GetJobHistory(EmployeeCode,FromDate,ToDate).ToList();
            return View(jobHistories.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbJobHistory jobHistory)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    jobHistory.UploadForTownship =userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    jobHistory.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == jobHistory.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _jobHistoryServices.SaveJobHistory(jobHistory, Convert.ToInt32(userId), 0);

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
            var jobHistory = _context.TbJobHistory.Where(x => x.JobHistoryPkid == Id).FirstOrDefault();
            jobHistory.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == jobHistory.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(jobHistory);
            return View(jobHistory);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbJobHistory jobHistory, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    jobHistory.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _jobHistoryServices.SaveJobHistory(jobHistory, Convert.ToInt32(userId), jobHistory.JobHistoryPkid);

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
                _jobHistoryServices.DeleteJobHistory(id, Convert.ToInt32(userId));
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
            string? SerialNumber= TempData["SerialNumber"]==null?null: TempData["SerialNumber"].ToString() ;
            DateTime? FromDate=Convert.ToDateTime(TempData["FromDate"]);
            DateTime? ToDate=Convert.ToDateTime(TempData["ToDate"]);
            if (TempData["FromDate"] == null)
            {
                FromDate = null;
                ToDate = null;

            }
            string? TownshipCode=TempData["TownshipCode"]==null?null: TempData["TownshipCode"].ToString();
            string? StateDivisionCode= TempData["StateDivisionCode"]==null?null: TempData["StateDivisionCode"].ToString();
            var jobHistories = _jobHistoryServices.GetCurrentJobHistory(null, StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ဌာနအမည်";
            worksheet.Cells[1, 5].Value = "ရာထူး အဆင့်";
            worksheet.Cells[1, 6].Value = "ရာထူး အခြေအနေ";
            

            for (int i = 0; i < jobHistories.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = jobHistories[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = jobHistories[i].Township;
                worksheet.Cells[i + 2, 3].Value = jobHistories[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = jobHistories[i].Department_Name;
                worksheet.Cells[i + 2, 5].Value = jobHistories[i].RankType;
                if(jobHistories[i].IsCurrent==true)
                {
                    worksheet.Cells[i + 2, 6].Value = "လက်ရှိရာထူး";
                }
                else
                {
                    worksheet.Cells[i + 2, 6].Value = "ယခင်ရာထူး";
                }
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JobHistoryForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {
           
            DateTime? FromDate =Convert.ToDateTime(TempData["FromDate"]);
            DateTime? ToDate = Convert.ToDateTime(TempData["ToDate"]);
            if (TempData["FromDate"] == null)
            {
                FromDate = null;
                ToDate = null;

            }
            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();       
            var jobHistories = _jobHistoryServices.GetJobHistory(EmployeeCode, FromDate, ToDate).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ဌာနအမည်";
            worksheet.Cells[1, 5].Value = "ရာထူး အဆင့်";
            worksheet.Cells[1, 6].Value = "ရာထူး အခြေအနေ";
            worksheet.Cells[1, 7].Value = "အချိန်ကာလ (မှ)";
            worksheet.Cells[1, 8].Value = "အချိန်ကာလ (ထိ)";


            for (int i = 0; i < jobHistories.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = jobHistories[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = jobHistories[i].Township;
                worksheet.Cells[i + 2, 3].Value = jobHistories[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = jobHistories[i].Department_Name;
                worksheet.Cells[i + 2, 5].Value = jobHistories[i].RankType;
                if (jobHistories[i].IsCurrent == true)
                {
                    worksheet.Cells[i + 2, 6].Value = "လက်ရှိရာထူး";
                }
                else
                {
                    worksheet.Cells[i + 2, 6].Value = "ယခင်ရာထူး";
                }
                worksheet.Cells[i + 2, 7].Value = jobHistories[i].FromDateStr;
                worksheet.Cells[i + 2, 8].Value = jobHistories[i].ToDateStr;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JobHistoryForDetail.xlsx");
        }
    }
}
