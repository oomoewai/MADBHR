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
            var disposalType = _context.TbDisposalType.Where(x => x.Active == true).OrderBy(x=>x.DisposalType).ToList();
            ViewData["DisposalTypeCode"] = new SelectList(disposalType, "DisposalTypeCode", "DisposalType", tbDisposal?.DisposalTypeCode);
        }
        [HttpGet]
        public IActionResult GetEmployeeInformation(string SerialNumber)
        {
            var empInfo = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).FirstOrDefault();       
            return Json(empInfo);
        }
        [HttpGet]
        public IActionResult GetEmployeeDisposalInfo(string SerialNumber)
        {
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(SerialNumber);
            var disposalInfo = _context.TbDisposal.Where(x => x.EmployeeCode == empInfo.EmployeeCode && (x.DisposalTypeCode == "102" || x.DisposalTypeCode == "106" || x.DisposalTypeCode == "107") && x.IsDeleted == false).FirstOrDefault();
            if (disposalInfo != null)
            {
                empInfo.isDisposal = true;
            }
            else
            {
                empInfo.isDisposal = false;
            }
            return Json(empInfo);
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
            var disposals = _employeeDisposalServices.GetEmployeeDisposalForAdmin(StateDivisionCode, TownshipCode,Name,SerialNumber).ToList();
            return View(disposals.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode = null, string? DisposalTypeCode = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["EmployeeCode"] = EmployeeCode;
            TempData["DisposalTypeCode"] = DisposalTypeCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
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

                    disposal.EmployeeCode =  _context.TbEmployee.Where(x => x.SerialNumber == disposal.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _employeeDisposalServices.SaveEmployeeDisposal(disposal, Convert.ToInt32(userId), 0,false);

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
            disposal.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == disposal.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(disposal.SerialNumber);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
            ViewBag.Education = empInfo.EducationType;
            ViewBag.DateOfBirth = empInfo.DateOfBirth;
            ViewBag.JoinDate = empInfo.joindate;
            ViewBag.FromDate = empInfo.FromDate;
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
                    var emp = await _employeeDisposalServices.SaveEmployeeDisposal(disposal, Convert.ToInt32(userId), disposal.DisposalPkid,false);

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
        [HttpGet]
        public async Task<IActionResult> ExcelExportForIndex()
        {

            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var disposals = _employeeDisposalServices.GetEmployeeDisposalForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ပြုန်းတီးအမျိုးအစား";
            worksheet.Cells[1, 5].Value = "ပြုန်းတီးရက်စွဲ";
            worksheet.Cells[1, 6].Value = "မှတ်ချက်";

            for (int i = 0; i < disposals.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = disposals[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = disposals[i].Township;
                worksheet.Cells[i + 2, 3].Value = disposals[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = disposals[i].DisposalType;
                worksheet.Cells[i + 2, 5].Value = disposals[i].DisposalDateStr;
                worksheet.Cells[i + 2, 6].Value = disposals[i].Remark;
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
            string? DisposalTypeCode = TempData["DisposalTypeCode"] == null ? null : TempData["DisposalTypeCode"].ToString();
            var disposals = _employeeDisposalServices.GetEmployeeDisposal(DisposalTypeCode, EmployeeCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ပြုန်းတီးအမျိုးအစား";
            worksheet.Cells[1, 5].Value = "ပြုန်းတီးရက်စွဲ";
            worksheet.Cells[1, 6].Value = "မှတ်ချက်";


            for (int i = 0; i < disposals.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = disposals[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = disposals[i].Township;
                worksheet.Cells[i + 2, 3].Value = disposals[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = disposals[i].DisposalType;
                worksheet.Cells[i + 2, 5].Value = disposals[i].DisposalDateStr;
                worksheet.Cells[i + 2, 6].Value = disposals[i].Remark;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AwardForDetail.xlsx");
        }
    }
}
