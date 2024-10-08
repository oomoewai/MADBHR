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
    public class PunishmentController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IPunishmentServices _punishmentServices;
        private readonly Pagination _pagination;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        public PunishmentController(MADBAdminSolutionContext context, IPunishmentServices punishmentServices, IOptions<Pagination> pagination,IEmployeeDisposalServices employeeDisposalServices)
        {
            _context = context;
            _punishmentServices = punishmentServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
        }
        public void Initialize(TbPunishment tbPunishment = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var punishmentTypeCode = _context.TbPunishmentType.Where(x => x.Active == true).OrderBy(x=>x.PunishmentType).ToList();
            ViewData["PunishmentType"] = new SelectList(punishmentTypeCode, "PunishmentTypeCode", "PunishmentType", tbPunishment?.PunishmentTypeCode);

        }
        public IActionResult Index(string? StateDivisionCode = null, string? TownshipCode = null, string? Name = null, string? SerialNumber = null, int? page = 1)
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
            var awrds = _punishmentServices.GetPunishmentForAdmin(StateDivisionCode, TownshipCode,Name,SerialNumber).ToList();
            return View(awrds.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string? EmployeeCode = null, string PunishmentType = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["EmployeeCode"] = EmployeeCode;
            TempData["PunishmentType"] = PunishmentType;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
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

                    punishment.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == punishment.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
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
            punishment.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == punishment.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(punishment.SerialNumber);
            Initialize(punishment);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
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
        [HttpGet]
        public async Task<IActionResult> ExcelExportForIndex()
        {

            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var awrds = _punishmentServices.GetPunishmentForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "အမိန့်စာနေ့စွဲ";
            worksheet.Cells[1, 7].Value = "အမိန့်စာအမှတ်";


            for (int i = 0; i < awrds.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = awrds[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = awrds[i].Township;
                worksheet.Cells[i + 2, 3].Value = awrds[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = awrds[i].RankType;
                worksheet.Cells[i + 2, 5].Value = awrds[i].Department;
                worksheet.Cells[i + 2, 6].Value = awrds[i].OrderDateStr;
                worksheet.Cells[i + 2, 7].Value = awrds[i].OrderNo;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PunishmentForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {


            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();
            string? PunishmentType = TempData["PunishmentType"] == null ? null : TempData["PunishmentType"].ToString();
            var awrds = _punishmentServices.GetPunishment(PunishmentType, EmployeeCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "ကိုယ်ပိုင်အမှတ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "အမိန့်စာနေ့စွဲ";
            worksheet.Cells[1, 7].Value = "အမိန့်စာအမှတ်";
            worksheet.Cells[1, 8].Value = "ကျူးလွန်သည့်ခုနှစ်";
            worksheet.Cells[1, 9].Value = "ပြစ်မှုအမျိုးအစား";
            worksheet.Cells[1, 10].Value = "ဖြစ်စဉ်အကျဉ်း";
            for (int i = 0; i < awrds.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = awrds[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = awrds[i].Township;
                worksheet.Cells[i + 2, 2].Value = awrds[i].SerialNumber;
                worksheet.Cells[i + 2, 3].Value = awrds[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = awrds[i].RankType;
                worksheet.Cells[i + 2, 5].Value = awrds[i].Department;
                worksheet.Cells[i + 2, 6].Value = awrds[i].OrderDateStr;
                worksheet.Cells[i + 2, 7].Value = awrds[i].OrderNo;
                worksheet.Cells[i + 2, 8].Value = awrds[i].CrimeYear;
                worksheet.Cells[i + 2, 9].Value = awrds[i].PunishmentType;
                worksheet.Cells[i + 2, 10].Value = awrds[i].Description;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PunishmentForDetail.xlsx");
        }
    }
}
