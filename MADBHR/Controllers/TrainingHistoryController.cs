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
    public class TrainingHistoryController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly ITrainingHistoryServices _trainingHistoryServices;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        private readonly Pagination _pagination;

        public TrainingHistoryController(MADBAdminSolutionContext context, ITrainingHistoryServices trainingHistoryServices, IOptions<Pagination> pagination, IEmployeeDisposalServices employeeDisposalServices)
        {
            _context = context;
            _trainingHistoryServices = trainingHistoryServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
        }
        public void Initialize(TbTrainingHistory traingHistory = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            List<string> lstLocation = new List<string>();
            lstLocation.Add("ပြည်တွင်း");
            lstLocation.Add("ပြည်ပ");
            ViewData["Location"] = new SelectList(lstLocation, traingHistory?.Location);

        }
        public IActionResult Index(string? SerialNumber = null, string Name = null, string? StateDivisionCode = null, string? TownshipCode = null, int? page = 1)
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
            TempData["SerialNumber"] = SerialNumber;
            TempData["TownshipCode"] = TownshipCode;
            TempData["StateDivisionCode"] = StateDivisionCode;
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
            var trainingHistories = _trainingHistoryServices.GetTrainingHistoryForAdmin(StateDivisionCode, TownshipCode,SerialNumber,Name).ToList();
            return View(trainingHistories.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode, string? SchoolName = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["EmployeeCode"] = EmployeeCode;
            TempData["SchoolName"] = SchoolName;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var trainingHistories = _trainingHistoryServices.GetTrainingHistory(SchoolName, EmployeeCode).ToList();
            return View(trainingHistories.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbTrainingHistory trainingHistory)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var didUploaded = true;
                    if (trainingHistory.CertificateImageFile != null)
                    {
                        var filename = trainingHistory.CertificateImageFile != null ? FtpHelper.ftpImageFolderPath + trainingHistory.CertificateImageFile.GetUniqueName() : "";
                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(trainingHistory.CertificateImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            didUploaded = true;
                            trainingHistory.CertificatePic = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                    if (didUploaded)
                    {
                        var userId = HttpContext.User.Identity.Name;
                        var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                        trainingHistory.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                        trainingHistory.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == trainingHistory.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
                        var emp = await _trainingHistoryServices.SaveTrainingHistory(trainingHistory, Convert.ToInt32(userId), 0);

                        return RedirectToAction("Index");

                    }
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
            var trainingHistory = _context.TbTrainingHistory.Where(x => x.TrainingHistoryPkid == Id).FirstOrDefault();
            trainingHistory.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == trainingHistory.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(trainingHistory.SerialNumber);
            trainingHistory.CertificateImageContent = trainingHistory.CertificatePic.GetBase64();
            Initialize(trainingHistory);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
            return View(trainingHistory);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbTrainingHistory history, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var emp = await _trainingHistoryServices.SaveTrainingHistory(history, Convert.ToInt32(userId), history.TrainingHistoryPkid);

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
                _trainingHistoryServices.DeleteTrainingHistory(id, Convert.ToInt32(userId));
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
            string? SerialNumber = TempData["SerialNumber"] == null ? null : TempData["SerialNumber"].ToString();
            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var trainingHistories = _trainingHistoryServices.GetTrainingHistoryForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "သင်တန်းအမည်/အပတ်စဉ်";
            worksheet.Cells[1, 7].Value = "သင်တန်း/ကျောင်းအမည်";

            for (int i = 0; i < trainingHistories.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = trainingHistories[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = trainingHistories[i].Township;
                worksheet.Cells[i + 2, 3].Value = trainingHistories[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = trainingHistories[i].RankType;
                worksheet.Cells[i + 2, 5].Value = trainingHistories[i].Department;
                worksheet.Cells[i + 2, 6].Value = trainingHistories[i].TrainingTitle;
                worksheet.Cells[i + 2, 7].Value = trainingHistories[i].SchoolName;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrainingHistoryForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {


            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();
            string? SchoolName = TempData["SchoolName"] == null ? null : TempData["SchoolName"].ToString();
            var trainingHistories = _trainingHistoryServices.GetTrainingHistory(SchoolName, EmployeeCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "သင်တန်းအမည်/အပတ်စဉ်";
            worksheet.Cells[1, 7].Value = "သင်တန်း/ကျောင်းအမည်";
            worksheet.Cells[1, 8].Value = "ကာလ (မှ)";
            worksheet.Cells[1, 9].Value = "ကာလ (ထိ)";


            for (int i = 0; i < trainingHistories.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = trainingHistories[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = trainingHistories[i].Township;
                worksheet.Cells[i + 2, 3].Value = trainingHistories[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = trainingHistories[i].RankType;
                worksheet.Cells[i + 2, 5].Value = trainingHistories[i].Department;
                worksheet.Cells[i + 2, 6].Value = trainingHistories[i].TrainingTitle;
                worksheet.Cells[i + 2, 7].Value = trainingHistories[i].SchoolName;
                worksheet.Cells[i + 2, 8].Value = trainingHistories[i].StartDateStr;
                worksheet.Cells[i + 2, 9].Value = trainingHistories[i].EndDateStr;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrainingHistoryForDetail.xlsx");
        }
    }
}
