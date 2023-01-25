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

        public IActionResult Index(string? SerialNumber = null, string? DisposalTypeCode = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var pensions = _pensionServices.GetPension(EmployeeCode).ToList();
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
    }
}
