using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Models.Employee;
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
    public class EmployeeController : Controller
    {

        private readonly MADBAdminSolutionContext _context;
        private readonly IEmployeeServices _employeeServices;
        private readonly Pagination _pagination;

        public EmployeeController(MADBAdminSolutionContext context, IEmployeeServices employeeServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _employeeServices = employeeServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbEmployee employee = null)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            var placeOfBirths = _context.TbTownAndDivision.Select(x => new { x.TownCode, x.TownName }).ToList();
            ViewData["PlaceOfBirth"] = new SelectList(placeOfBirths, "TownCode", "TownName", employee?.PlaceOfBirth);
            if (userInfo.AccountType == "Head Admin")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true).ToList();
                ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.PlaceOfBirth);
            }
            else if (userInfo.AccountType == "Super Admin")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId).ToList();
                ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.PlaceOfBirth);
            }
            else if (userInfo.AccountType == "User")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId && x.UploadForTownship == userInfo.TownshipId).ToList();
                ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.PlaceOfBirth);
            }
            var educationTypeCodes = _context.TbEducationType.Where(x => x.Active == true).ToList();
            ViewData["EducationType"] = new SelectList(educationTypeCodes, "EducationTypeCode", "EducationType", employee?.EducationTypeCode);


        }
        public IActionResult Index(DateTime? FromDate = null, DateTime? ToDate = null, string? Name = null, string? SerialNumber = null, string? FatherName = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var employees = _employeeServices.GetEmployee(Name, FromDate, ToDate, SerialNumber).ToList();
            return View(employees.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));

        }
        public IActionResult AdminIndex(string? StateDivisionCode = null, string? TownshipCode = null, int? page = 1)
        {
            Initialize();
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            //StateDivisionCode = userInfo.StateDivisionId;            
            if (userInfo.AccountType == "User")
            {
                TownshipCode = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.UploadForTownship == userInfo.TownshipId).Select(x => x.TownshipCode).FirstOrDefault();
                var townshipCodes = _context.TbCurrentJobTownship.Where(x => x.StateDivisionId == userInfo.StateDivisionId && x.UploadForTownship == userInfo.TownshipId).ToList();
                ViewData["TownshipCode"] = new SelectList(townshipCodes, "TownshipCode", "Township");
            }
            else
            {
                var townshipCodes = _context.TbCurrentJobTownship.Where(x => x.StateDivisionId == StateDivisionCode).ToList();
                ViewData["TownshipCode"] = new SelectList(townshipCodes, "TownshipCode", "Township");
            }
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var employees = _employeeServices.GetEmployeeForAdmin(StateDivisionCode, TownshipCode).ToList();
            StateDivisionCode = null;
            TownshipCode = null;
            return View(employees.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));

        }
        public IActionResult AdminDivisionIndex(string? StateDivisionCode = null, int? page = 1)
        {
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
            var employees = _employeeServices.GetEmployeeCount(StateDivisionCode).ToList();

            return View(employees.ToPagedList((int)page, pageSize));

        }

        [HttpGet]
        public IActionResult GetTownhsipByDivision(string divisonCode)
        {
            var townships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == divisonCode).ToList();
            //ViewData["PlaceOfBirth"] = new SelectList(townships, "TownshipCode", "Township");
            return Json(townships);
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbEmployee employee, bool? RedirectToRelationship = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    //if (ModelState.IsValid)
                    //{
                    var didUploaded = true;
                    if (employee.ImageFile != null)
                    {
                        var filename = employee.ImageFile != null ? FtpHelper.ftpImageFolderPath + employee.ImageFile.GetUniqueName() : "";
                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(employee.ImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            didUploaded = true;
                            employee.ProfilePic = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                    if (employee.NRCImageFile != null)
                    {
                        var filename = employee.NRCImageFile != null ? FtpHelper.ftpImageFolderPath + employee.NRCImageFile.GetUniqueName() : "";
                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(employee.NRCImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            didUploaded = true;
                            employee.Nrcpic = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                    if (employee.Form66ImageFile != null)
                    {
                        var filename = employee.Form66ImageFile != null ? FtpHelper.ftpImageFolderPath + employee.Form66ImageFile.GetUniqueName() : "";

                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(employee.Form66ImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            didUploaded = true;
                            employee.Form66Pic = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                    if (didUploaded)
                    {
                        employee.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                        var emp = await _employeeServices.SaveEmployee(employee, Convert.ToInt32(userId), 0);
                        if (RedirectToRelationship == true)
                        {
                            return RedirectToAction("Create", "Relationship", new { SerialNumber = emp.SerialNumber });
                        }
                        else
                        {
                            if (userInfo.AccountType == "Head Admin" || userInfo.AccountType == "Super Admin")
                                return RedirectToAction("AdminDivisionIndex");
                            else
                                return RedirectToAction("AdminIndex");
                        }
                    }
                    throw new Exception();
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
            var employeeInfo = _context.TbEmployee.Where(x => x.EmployeePkid == Id).FirstOrDefault();
            employeeInfo.ImageContent = employeeInfo.ProfilePic.GetBase64();
            employeeInfo.NRCImageContent = employeeInfo.Nrcpic.GetBase64();
            employeeInfo.Form66ImageContent = employeeInfo.Form66Pic.GetBase64();
            Initialize(employeeInfo);
            return View(employeeInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TbEmployee employee, bool? RedirectToRelationship = null)
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    var didUploaded = true;
                    if (employee.ImageFile != null)
                    {
                        didUploaded = false;
                        var filename = employee.ImageFile != null ? FtpHelper.ftpImageFolderPath + employee.ImageFile.GetUniqueName() : "";
                        var uploadRes = FtpHelper.UploadFileToServer(employee.ImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            var didDeleted = true;
                            if (FtpHelper.CheckIfFileExistsOnServer(employee.ProfilePic))
                            {
                                didDeleted = false;
                                var deleteRes = FtpHelper.DeleteFileOnServer(employee.ProfilePic);
                                if (deleteRes.IsSucceed())
                                {
                                    didDeleted = true;
                                }
                            }
                            if (didDeleted)
                            {
                                didUploaded = true;
                                employee.ProfilePic = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                    }
                    if (employee.NRCImageFile != null)
                    {
                        didUploaded = false;
                        var filename = employee.NRCImageFile != null ? FtpHelper.ftpImageFolderPath + employee.NRCImageFile.GetUniqueName() : "";
                        var uploadRes = FtpHelper.UploadFileToServer(employee.NRCImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            var didDeleted = true;
                            if (FtpHelper.CheckIfFileExistsOnServer(employee.Nrcpic))
                            {
                                didDeleted = false;
                                var deleteRes = FtpHelper.DeleteFileOnServer(employee.Nrcpic);
                                if (deleteRes.IsSucceed())
                                {
                                    didDeleted = true;
                                }
                            }
                            if (didDeleted)
                            {
                                didUploaded = true;
                                employee.Nrcpic = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                    }
                    if (employee.Form66ImageFile != null)
                    {
                        didUploaded = false;
                        var filename = employee.Form66ImageFile != null ? FtpHelper.ftpImageFolderPath + employee.Form66ImageFile.GetUniqueName() : "";
                        var uploadRes = FtpHelper.UploadFileToServer(employee.Form66ImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            var didDeleted = true;
                            if (FtpHelper.CheckIfFileExistsOnServer(employee.Form66Pic))
                            {
                                didDeleted = false;
                                var deleteRes = FtpHelper.DeleteFileOnServer(employee.Form66Pic);
                                if (deleteRes.IsSucceed())
                                {
                                    didDeleted = true;
                                }
                            }
                            if (didDeleted)
                            {
                                didUploaded = true;
                                employee.Form66Pic = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                    }
                    if (didUploaded)
                    {
                        employee.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                        var emp = await _employeeServices.SaveEmployee(employee, Convert.ToInt32(userId), employee.EmployeePkid);
                        if (RedirectToRelationship == true)
                        {
                            return RedirectToAction("Index", "Relationship", new { EmployeeCode = employee.EmployeeCode });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }

                    }
                }
            }
            catch (Exception e)
            {

            }
            Initialize(employee);
            return View(employee);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;
                _employeeServices.DeleteEmployee(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
