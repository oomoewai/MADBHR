using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
        public TransferController(MADBAdminSolutionContext context, ITransferServices transferServices,IEmployeeDisposalServices employeeDisposalServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _transferServices = transferServices;
            _pagination = pagination.Value;
            _employeeDisposalServices = employeeDisposalServices;
        }
        public void Initialize(TbTransfer tbTransfer = null)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true).ToList();
            ViewData["FromTownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbTransfer?.FromTownshipCode);
            ViewData["ToTownshipCode"] = new SelectList(currentJobTownships, "TownshipCode", "Township", tbTransfer?.ToTownshipCode);
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
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            TempData["TownshipCode"] = TownshipCode;
            TempData["StateDivisionCode"] = StateDivisionCode;
            //var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var transfers = _transferServices.GetTransferForAdmin(StateDivisionCode, TownshipCode).ToList();
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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();

                    transfer.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == transfer.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _transferServices.SaveTransfer(transfer, Convert.ToInt32(userId), 0);
                    TbDisposal tbDisposal = new TbDisposal();
                    tbDisposal.EmployeeCode = transfer.EmployeeCode;
                    tbDisposal.DisposalTypeCode = "101";
                    tbDisposal.DisposalDate = DateTime.Now;
                    tbDisposal.IsDeleted = false;
                    tbDisposal.IsRecordEdited = true;
                    tbDisposal.CreatedBy = userInfo.UserPkid;
                    tbDisposal.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var disposal = await _employeeDisposalServices.SaveEmployeeDisposal(tbDisposal, Convert.ToInt32(userId), 0,true);
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
            var transferEmployeeCode = _context.TbTransfer.Where(x => x.TransferPkid == Id).Select(x => x.EmployeeCode).FirstOrDefault();
            var transfer = _transferServices.GetTransfer(transferEmployeeCode,null,null,Id).FirstOrDefault();
            transfer.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == transferEmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
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

                    return RedirectToAction("Index");

                }
                catch (Exception e)
                {

                    await transaction.RollbackAsync();
                }
            }

            return View();
        }

    }
}
