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
    public class InternationalKnowledgeController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IInternationalKnowledgeServices _internationalKnowledgeServices;
        private readonly Pagination _pagination;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        public InternationalKnowledgeController(MADBAdminSolutionContext context, IInternationalKnowledgeServices internationalKnowledgeServices, IOptions<Pagination> pagination,IEmployeeDisposalServices employeeDisposalServices)
        {
            _context = context;
            _internationalKnowledgeServices = internationalKnowledgeServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
        }
        public void Initialize(TbIntKnowledge tbIntKnowledge = null)
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
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);

            }
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;

            TempData["TownshipCode"] = TownshipCode;
            TempData["StateDivisionCode"] = StateDivisionCode;
            // var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var knowledges = _internationalKnowledgeServices.GetIntKnowledgeForAdmin(StateDivisionCode,TownshipCode).ToList();
            return View(knowledges.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode,DateTime? FromDate=null,DateTime? ToDate=null,  int? page = 1)
        {
            Initialize();

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["EmployeeCode"] = EmployeeCode;
            // var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var knowledges = _internationalKnowledgeServices.GetIntKnowledge(EmployeeCode,FromDate,ToDate).ToList();
            return View(knowledges.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbIntKnowledge intKnowledge)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    intKnowledge.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    intKnowledge.EmployeeCode =  _context.TbEmployee.Where(x => x.SerialNumber == intKnowledge.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _internationalKnowledgeServices.SaveIntKnowledge(intKnowledge, Convert.ToInt32(userId), 0);

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
            var intKnowledge = _context.TbIntKnowledge.Where(x => x.IntKnowledgePkid == Id).FirstOrDefault();
            intKnowledge.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == intKnowledge.EmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(intKnowledge.SerialNumber);
            Initialize(intKnowledge);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
            return View(intKnowledge);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbIntKnowledge intKnowledge, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    intKnowledge.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _internationalKnowledgeServices.SaveIntKnowledge(intKnowledge, Convert.ToInt32(userId), intKnowledge.IntKnowledgePkid);

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
                _internationalKnowledgeServices.DeleteIntKnowledge(id, Convert.ToInt32(userId));
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
            var knowledges = _internationalKnowledgeServices.GetIntKnowledgeForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "ကာလ (မှ)";
            worksheet.Cells[1, 7].Value = "ကာလ (ထိ)";

            for (int i = 0; i < knowledges.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = knowledges[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = knowledges[i].Township;
                worksheet.Cells[i + 2, 3].Value = knowledges[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = knowledges[i].RankType;
                worksheet.Cells[i + 2, 5].Value = knowledges[i].Department;
                worksheet.Cells[i + 2, 6].Value = knowledges[i].FromDateStr;
                worksheet.Cells[i + 2, 7].Value = knowledges[i].ToDateStr;
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
            var knowledges = _internationalKnowledgeServices.GetIntKnowledge(EmployeeCode, FromDate, ToDate).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ရာထူး";
            worksheet.Cells[1, 5].Value = "ဌာန";
            worksheet.Cells[1, 6].Value = "ကာလ (မှ)";
            worksheet.Cells[1, 7].Value = "ကာလ (ထိ)";
            worksheet.Cells[1, 8].Value = "နိုင်ငံအမည်";
            worksheet.Cells[1, 9].Value = "သွားရောက်သည့်အကြောင်းအရာ";


            for (int i = 0; i < knowledges.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = knowledges[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = knowledges[i].Township;
                worksheet.Cells[i + 2, 3].Value = knowledges[i].EmployeeName;
                worksheet.Cells[i + 2, 4].Value = knowledges[i].RankType;
                worksheet.Cells[i + 2, 5].Value = knowledges[i].Department;
                worksheet.Cells[i + 2, 6].Value = knowledges[i].FromDateStr;
                worksheet.Cells[i + 2, 7].Value = knowledges[i].ToDateStr;
                worksheet.Cells[i + 2, 8].Value = knowledges[i].CountryName;
                worksheet.Cells[i + 2, 9].Value = knowledges[i].Description;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IntKnowledgeForDetail.xlsx");
        }
    }
}
