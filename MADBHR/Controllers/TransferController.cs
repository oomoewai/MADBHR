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
    public class TransferController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly ITransferServices _transferServices;
        private readonly Pagination _pagination;
        private readonly IEmployeeDisposalServices _employeeDisposalServices;
        private readonly IJobHistoryServices _jobHistoryServices;
        public TransferController(MADBAdminSolutionContext context, ITransferServices transferServices,IEmployeeDisposalServices employeeDisposalServices, IOptions<Pagination> pagination,IJobHistoryServices jobHistoryServices)
        {
            _context = context;
            _transferServices = transferServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
            _jobHistoryServices = jobHistoryServices;
        }
        public void Initialize(TbTransfer tbTransfer = null)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true).OrderBy(x => x.Township).ToList();
            ViewData["FromTownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbTransfer?.FromTownshipCode);
            ViewData["ToTownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbTransfer?.ToTownshipCode);
            var rankTypeCode = _context.TbRankType.Select(x => new { x.RankTypeCode, x.RankType }).OrderBy(x=>x.RankType).ToList();
            ViewData["RankTypeCode"] = new SelectList(rankTypeCode, "RankTypeCode", "RankType", tbTransfer?.RankTypeCode);
        }
        public IActionResult Index(string? StateDivisionCode = null, string? TownshipCode = null, string? SerialNumber = null, string Name = null, int? page = 1)
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
            var transfers = _transferServices.GetTransferForAdmin(StateDivisionCode, TownshipCode,SerialNumber,Name).ToList();
            return View(transfers.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Detail(string EmployeeCode = null, string? FromTownshipCode = null, string? ToTownshipCode = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["EmployeeCode"] = EmployeeCode;
            TempData["FromTownshipCode"] = FromTownshipCode;
            TempData["ToTownshipCode"] = ToTownshipCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var disposals = _transferServices.GetTransfer(EmployeeCode,FromTownshipCode,ToTownshipCode).ToList();
            return View(disposals.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbTransfer transfer)
        {
            var empSerialNumberinfo = _context.TbEmployee.Where(x => x.SerialNumber == transfer.SerialNumber && x.Status == "Pending").FirstOrDefault();
            if (empSerialNumberinfo != null)
            {
                return View();
            }
            else
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {

                        //if (ModelState.IsValid)
                        //{
                        var userId = HttpContext.User.Identity.Name;
                        var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();

                        transfer.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == transfer.SerialNumber && x.IsDeleted == false).Select(x => x.EmployeeCode).FirstOrDefault();
                        var emp = await _transferServices.SaveTransfer(transfer, Convert.ToInt32(userId), 0);
                        TbDisposal tbDisposal = new TbDisposal();
                        tbDisposal.EmployeeCode = transfer.EmployeeCode;
                        tbDisposal.DisposalTypeCode = "101";
                        tbDisposal.DisposalDate = DateTime.Now;
                        tbDisposal.IsDeleted = false;
                        tbDisposal.IsRecordEdited = true;
                        tbDisposal.CreatedBy = userInfo.UserPkid;
                        tbDisposal.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                        var transferId = _context.TbTransfer.Where(x => x.IsDeleted == false).OrderByDescending(x => x.TransferPkid).Select(x => x.TransferPkid).FirstOrDefault();
                        tbDisposal.TransferId = Convert.ToInt32(transferId);
                        var disposal = await _employeeDisposalServices.SaveEmployeeDisposal(tbDisposal, Convert.ToInt32(userId), 0, true);
                        //var jobHistoryInfo = _context.TbJobHistory.Where(x => x.RankTypeCode == transfer.RankTypeCode && x.DepartmentCode == transfer.FromTownshipCode && x.DepartmentName == transfer.FromTownshipCode && x.IsCurrent == true).ToList();
                        //if(jobHistoryInfo.Count>0)
                        //{
                        TbJobHistory tbJobHistory = new TbJobHistory();
                        tbJobHistory.EmployeeCode = transfer.EmployeeCode;
                        tbJobHistory.DepartmentCode = transfer.ToTownshipCode;
                        tbJobHistory.DepartmentName = transfer.ToTownshipCode;
                        tbJobHistory.RankTypeCode1 = transfer.RankTypeCode;
                        tbJobHistory.FromDate = DateTime.Now;
                        tbJobHistory.ToDate = DateTime.Now;
                        tbJobHistory.Remark = "";
                        tbJobHistory.IsCurrent = true;
                        tbJobHistory.IsDeleted = false;
                        tbJobHistory.IsRecordEdited = true;
                        tbJobHistory.CreatedBy = userInfo.UserPkid;
                        tbJobHistory.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                        var jobHistory = await _jobHistoryServices.SaveJobHistory(tbJobHistory, Convert.ToInt32(userId), 0);
                        //}

                        return RedirectToAction("Index");

                        //}
                    }
                    catch (Exception e)
                    {

                        await transaction.RollbackAsync();
                    }
                }
            }
            return View();
        }
        public IActionResult Edit(int Id)
        {
            var transferEmployeeCode = _context.TbTransfer.Where(x => x.TransferPkid == Id).Select(x => x.EmployeeCode).FirstOrDefault();
            var transfer = _transferServices.GetTransfer(transferEmployeeCode,null,null,Id).FirstOrDefault();
            transfer.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == transferEmployeeCode && x.IsDeleted == false).Select(x => x.SerialNumber).FirstOrDefault();
            var empInfo = _employeeDisposalServices.GetEmployeeInfo(transfer.SerialNumber);
            ViewBag.Name = empInfo.Name;
            ViewBag.Rank = empInfo.RankType;
            ViewBag.FromTownshipCode = transfer.FromTownshipCode;
            ViewBag.FromTownship = transfer.FromTownship;

            Initialize(transfer);
            return View(transfer);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbTransfer transfer, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    //transfer.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _transferServices.SaveTransfer(transfer, Convert.ToInt32(userId), transfer.TransferPkid);
                    TbDisposal tbDisposal = new TbDisposal();
                    tbDisposal.EmployeeCode = transfer.EmployeeCode;
                    tbDisposal.DisposalTypeCode = "101";
                    tbDisposal.DisposalDate = DateTime.Now;
                    tbDisposal.IsDeleted = false;
                    tbDisposal.IsRecordEdited = true;
                    tbDisposal.CreatedBy = userInfo.UserPkid;
                    tbDisposal.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    //var transferId = _context.TbTransfer.Where(x => x.IsDeleted == false).OrderByDescending(x => x.TransferPkid).Select(x => x.TransferPkid).FirstOrDefault();
                    tbDisposal.TransferId = transfer.TransferPkid;
                    tbDisposal.DisposalPkid = Convert.ToInt32(_context.TbDisposal.Where(x => x.TransferId == transfer.TransferPkid).Select(x => x.TransferId).FirstOrDefault());
                    var disposal = await _employeeDisposalServices.SaveEmployeeDisposal(tbDisposal, Convert.ToInt32(userId), tbDisposal.DisposalPkid, true);
                    TbJobHistory tbJobHistory = new TbJobHistory();
                    tbJobHistory.EmployeeCode = transfer.EmployeeCode;
                    tbJobHistory.DepartmentCode = transfer.ToTownshipCode;
                    tbJobHistory.DepartmentName = transfer.ToTownshipCode;
                    tbJobHistory.RankTypeCode1 = transfer.RankTypeCode;
                    tbJobHistory.FromDate = DateTime.Now;
                    tbJobHistory.ToDate = DateTime.Now;
                    tbJobHistory.Remark = "";
                    tbJobHistory.IsCurrent = true;
                    tbJobHistory.IsDeleted = false;
                    tbJobHistory.IsRecordEdited = true;
                    tbJobHistory.CreatedBy = userInfo.UserPkid;
                    tbJobHistory.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    tbJobHistory.JobHistoryPkid = _context.TbJobHistory.Where(x => x.DepartmentCode == transfer.ToTownshipCode && x.EmployeeCode == transfer.EmployeeCode && x.IsCurrent == true).Select(x => x.JobHistoryPkid).FirstOrDefault();
                    var jobHistory = await _jobHistoryServices.SaveJobHistory(tbJobHistory, Convert.ToInt32(userId), tbJobHistory.JobHistoryPkid);
                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {

                    await transaction.RollbackAsync();
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForIndex()
        {

            string? TownshipCode = TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            string? StateDivisionCode = TempData["StateDivisionCode"] == null ? null : TempData["StateDivisionCode"].ToString();
            var awrds = _transferServices.GetTransferForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "အမည်";
            worksheet.Cells[1, 3].Value = "ရာထူး";
            worksheet.Cells[1, 4].Value = "မြို့နယ် (မှ)";
            worksheet.Cells[1, 5].Value = "မြို့နယ် (သို့)";
            worksheet.Cells[1, 5].Value = "ပြောင်းရွှေ့ရက်စွဲ";

            for (int i = 0; i < awrds.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = awrds[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = awrds[i].EmployeeName;
                worksheet.Cells[i + 2, 3].Value = awrds[i].RankType;
                worksheet.Cells[i + 2, 4].Value = awrds[i].FromTownship;
                worksheet.Cells[i + 2, 5].Value = awrds[i].ToTownship;
                worksheet.Cells[i + 2, 6].Value = awrds[i].TransferDateStr;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransferForIndex.xlsx");
        }
        [HttpGet]
        public async Task<IActionResult> ExcelExportForDetail()
        {


            string? EmployeeCode = TempData["EmployeeCode"] == null ? null : TempData["EmployeeCode"].ToString();
            string? FromTownshipCode = TempData["FromTownshipCode"] == null ? null : TempData["FromTownshipCode"].ToString();
            string? ToTownshipCode = TempData["ToTownshipCode"] == null ? null : TempData["ToTownshipCode"].ToString();

            var awrds = _transferServices.GetTransfer( EmployeeCode,FromTownshipCode,ToTownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "အမည်";
            worksheet.Cells[1, 3].Value = "ရာထူး";
            worksheet.Cells[1, 4].Value = "မြို့နယ် (မှ)";
            worksheet.Cells[1, 5].Value = "မြို့နယ် (သို့)";
            worksheet.Cells[1, 6].Value = "ပြောင်းရွှေ့ရက်စွဲ";
            worksheet.Cells[1, 7].Value = "အကြောင်းအရာ";


            for (int i = 0; i < awrds.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = awrds[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = awrds[i].EmployeeName;
                worksheet.Cells[i + 2, 3].Value = awrds[i].RankType;
                worksheet.Cells[i + 2, 4].Value = awrds[i].FromTownship;
                worksheet.Cells[i + 2, 5].Value = awrds[i].ToTownship;
                worksheet.Cells[i + 2, 6].Value = awrds[i].TransferDateStr;
                worksheet.Cells[i + 2, 7].Value = awrds[i].Remark;
            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransferForDetail.xlsx");
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                _transferServices.DeleteTransfer(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
