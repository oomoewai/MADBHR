using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Models.Employee;
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
    public class EmployeeController : Controller
    {

        private readonly MADBAdminSolutionContext _context;
        private readonly IEmployeeServices _employeeServices;
        private readonly Pagination _pagination;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(MADBAdminSolutionContext context, IEmployeeServices employeeServices, IOptions<Pagination> pagination,ILogger<EmployeeController> logger)
        {
            _context = context;
            _employeeServices = employeeServices;
            _pagination = pagination.Value;
            _logger = logger;
        }
        public void Initialize(TbEmployee employee = null)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            var placeOfBirths = _context.TbTownAndDivision.Select(x => new { x.TownCode, x.TownName }).OrderBy(x=>x.TownName).ToList();
            ViewData["PlaceOfBirth"] = new SelectList(placeOfBirths, "TownCode", "TownName", employee?.PlaceOfBirth);
            if (userInfo.AccountType == "Head Admin")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true).OrderBy(x=>x.Township).ToList();
                ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.Occupation);
            }
            else if (userInfo.AccountType == "Super Admin")
            {
                var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId).OrderBy(x => x.Township).ToList();
                ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.Occupation);
            }
            else if (userInfo.AccountType == "User")
            {
              //if(userInfo.StateDivisionId =="113")
              //  {
              //      var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId ).ToList();
              //      ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.Occupation);
              //  }
                //else
                //{
                    var currentJobTownships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId && x.UploadForTownship == userInfo.TownshipId).OrderBy(x => x.Township).ToList();
                    ViewData["CurrentJobTownship"] = new SelectList(currentJobTownships, "TownshipCode", "Township", employee?.Occupation);
                //}
               
            }
            var educationTypeCodes = _context.TbEducationType.Where(x => x.Active == true).OrderBy(x=>x.EducationType).ToList();
            ViewData["EducationType"] = new SelectList(educationTypeCodes, "EducationTypeCode", "EducationType", employee?.EducationTypeCode);


        }
        public IActionResult Index(DateTime? FromDate = null, DateTime? ToDate = null, string? Name = null, string? SerialNumber = null, string? FatherName = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;

            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["Name"] = Name;
            TempData["SerialNumber"] = SerialNumber;
            TempData["FatherName"] = FatherName;

            var employees = _employeeServices.GetRequestingEmployee().ToList();
            return View(employees.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));

        }
        public IActionResult AdminIndex(string StateDivisionCode = null, string TownshipCode = null, string? Name = null, string? SerialNumber = null, string? Status=null, int? page = 1)
        {
            Initialize();
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            //StateDivisionCode = userInfo.StateDivisionId;            
            if (userInfo.AccountType == "User")
            {
                if(userInfo.TownshipId=="0010")
                {
                    //TownshipCode = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == userInfo.StateDivisionId).Select(x => x.TownshipCode).FirstOrDefault();
                    //TownshipCode = TownshipCode == null ? "0" : TownshipCode;
                    StateDivisionCode = userInfo.StateDivisionId;
                }
                else
                {
                    TownshipCode = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.UploadForTownship == userInfo.TownshipId).Select(x => x.TownshipCode).FirstOrDefault();
                    TownshipCode = TownshipCode == null ? "0" : TownshipCode;
                }
                TempData["TownshipCode"] = TownshipCode;
                var townshipCodes = _context.TbCurrentJobTownship.Where(x => x.StateDivisionId == userInfo.StateDivisionId && x.UploadForTownship == userInfo.TownshipId).OrderBy(x => x.Township).ToList();
                ViewData["TownshipCode"] = new SelectList(townshipCodes, "TownshipCode", "Township");
            }
            else
            {
                TempData["TownshipCode"] = TownshipCode;
                var townshipCodes = _context.TbCurrentJobTownship.Where(x => x.StateDivisionId == StateDivisionCode).OrderBy(x => x.Township).ToList();
                ViewData["TownshipCode"] = new SelectList(townshipCodes, "TownshipCode", "Township");
            }
            List<string> status = new List<string>();
            status.Add("Pending");
            status.Add("Approve");
            status.Add("Reject");
            ViewData["Status"] = new SelectList(status);
            TempData["StateDivisionCode"] = StateDivisionCode;
            ViewBag.StateDivisionCode = StateDivisionCode;
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var employees = _employeeServices.GetEmployeeForAdmin(StateDivisionCode, TownshipCode,Status,Name,SerialNumber).ToList();
            StateDivisionCode = null;
            TownshipCode = null;
            return View(employees.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));

        }
        public IActionResult AdminDivisionIndex(string? StateDivisionCode = null, int? page = 1)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            ViewBag.lstLogIn = userInfo;
            //MappedDiagnosticsLogicalContext.Set("userId", userInfo.UserPkid);
            if (userInfo.AccountType == "Super Admin")
            {
                StateDivisionCode = userInfo.StateDivisionId;
                var stateDivisionCodes = _context.TbStateDivision.Where(x => x.StateDivisionCode == userInfo.StateDivisionId).OrderBy(x => x.StateDivision).ToList();
                ViewData["StateDivision"] = new SelectList(stateDivisionCodes, "StateDivisionCode", "StateDivision", stateDivisionCodes[0].StateDivisionCode);
            }
            else
            {
                var stateDivisionCodes = _context.TbStateDivision.Select(x => new { x.StateDivision, x.StateDivisionCode }).OrderBy(x => x.StateDivision).ToList();
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

        [HttpGet]
        public  IActionResult GetTownship(string divisonCode,string TownshipCode)
        {
            var townships = _context.TbCurrentJobTownship.Where(x => x.Active == true && x.StateDivisionId == divisonCode && x.UploadForTownship==TownshipCode).ToList();
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
                var userId = HttpContext.User.Identity.Name;
                var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                MappedDiagnosticsLogicalContext.Set("userId", userInfo.UserPkid);
                try
                {
                  
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
                    if (employee.DegreeImageFile != null)
                    {
                        var filename = employee.DegreeImageFile != null ? FtpHelper.ftpImageFolderPath + employee.DegreeImageFile.GetUniqueName() : "";

                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(employee.DegreeImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            didUploaded = true;
                            employee.DegreePic = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                    if (didUploaded)
                    {
                        employee.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                        if(userInfo.AccountType == "User" || userInfo.AccountType == "Super Admin")
                        {
                            employee.Status = "Pending";
                        }
                        else
                        {
                            employee.Status = "Approve";
                        }
                        var emp = await _employeeServices.SaveEmployee(employee, Convert.ToInt32(userId), 0);
                        
                        _logger.LogInformation("Successfully Create");
                        if (RedirectToRelationship == true)
                        {
                            return RedirectToAction("Create", "Relationship", new { SerialNumber = emp.SerialNumber,Address=emp.Address });
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
                    
                    _logger.LogError(e.Message);
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
            employeeInfo.DegreeImageContent = employeeInfo.DegreePic.GetBase64();
            Initialize(employeeInfo);
            return View(employeeInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TbEmployee employee, bool? RedirectToRelationship = null)
        {
            var userId = HttpContext.User.Identity.Name;
            var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            MappedDiagnosticsLogicalContext.Set("userId", userInfo.UserPkid);
            try
            {
               
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
                    if (employee.DegreeImageFile != null)
                    {
                        didUploaded = false;
                        var filename = employee.DegreeImageFile != null ? FtpHelper.ftpImageFolderPath + employee.DegreeImageFile.GetUniqueName() : "";
                        var uploadRes = FtpHelper.UploadFileToServer(employee.DegreeImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            var didDeleted = true;
                            if (FtpHelper.CheckIfFileExistsOnServer(employee.DegreePic))
                            {
                                didDeleted = false;
                                var deleteRes = FtpHelper.DeleteFileOnServer(employee.DegreePic);
                                if (deleteRes.IsSucceed())
                                {
                                    didDeleted = true;
                                }
                            }
                            if (didDeleted)
                            {
                                didUploaded = true;
                                employee.DegreePic = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                    }
                    if (didUploaded)
                    {
                        employee.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                        employee.EditRequest = null;
                        var emp = await _employeeServices.SaveEmployee(employee, Convert.ToInt32(userId), employee.EmployeePkid);
                        _logger.LogInformation("Successfully Edit");
                        if (RedirectToRelationship == true)
                        {
                            return RedirectToAction("Index", "Relationship", new { EmployeeCode = employee.EmployeeCode });
                        }
                        else
                        {
                            if (userInfo.AccountType == "Head Admin" || userInfo.AccountType == "Super Admin")
                                return RedirectToAction("AdminDivisionIndex");
                            else
                                return RedirectToAction("AdminIndex");
                        }

                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            Initialize(employee);
            return View(employee);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.User.Identity.Name;
            MappedDiagnosticsLogicalContext.Set("userId", Convert.ToInt32(userId));
            try
            {
               
                _employeeServices.DeleteEmployee(id, Convert.ToInt32(userId));
                _logger.LogInformation("Successfully Delete");
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ExcelExport()
        {
           
            string? StateDivisionCode = TempData["StateDivisionCode"]==null?null:TempData["StateDivisionCode"].ToString();
            string? TownshipCode= TempData["TownshipCode"] == null ? null : TempData["TownshipCode"].ToString();
            var employees = _employeeServices.GetEmployeeForAdmin(StateDivisionCode, TownshipCode).ToList();
            var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Data");
            worksheet.Cells[1, 1].Value = "တိုင်းဒေသကြီး";
            worksheet.Cells[1, 2].Value = "မြို့နယ်";
            worksheet.Cells[1, 3].Value = "အမည်";
            worksheet.Cells[1, 4].Value = "ဌာနအမည်";
            worksheet.Cells[1, 5].Value = "ရာထူး";
            worksheet.Cells[1, 6].Value = "အဖ အမည်";
            worksheet.Cells[1, 7].Value = "အမိ အမည်";
            worksheet.Cells[1, 8].Value = "ကိုးကွယ်သည့်ဘာသာ";
            worksheet.Cells[1, 9].Value = "လူမျိုး";
            worksheet.Cells[1, 10].Value = "အနီးစပ်ဆုံး ဆွေမျိုး";
            worksheet.Cells[1, 11].Value = "အမွေစား အမွေခံ";
            worksheet.Cells[1, 12].Value = "နိုင်ငံသားစီစစ်ရေးကဒ်ပြားအမှတ်";
            worksheet.Cells[1, 13].Value = "မွေးသက္ကရာဇ်";
            worksheet.Cells[1, 14].Value = "မွေးဖွားသည့်အရပ်";
            worksheet.Cells[1, 15].Value = "ပညာအရည်အချင်း";
            worksheet.Cells[1, 16].Value = "စတင်ဝင်ရောက်သည့်နေ့";
            worksheet.Cells[1, 17].Value = "နေရပ်လိပ်စာ";
            worksheet.Cells[1, 18].Value = "မျက်စိအရောင်";
            worksheet.Cells[1, 19].Value = "အရပ်";
            worksheet.Cells[1, 20].Value = "ထင်ရှားအမှတ်အသား";

            for (int i = 0; i < employees.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = employees[i].StateDivision;
                worksheet.Cells[i + 2, 2].Value = employees[i].Township;
                worksheet.Cells[i + 2, 3].Value = employees[i].Name;
                worksheet.Cells[i + 2, 4].Value = employees[i].OccupationName;
                worksheet.Cells[i + 2, 5].Value = employees[i].CurrentRank;
                worksheet.Cells[i + 2, 6].Value = employees[i].FatherName;
                worksheet.Cells[i + 2, 7].Value = employees[i].MotherName;
                worksheet.Cells[i + 2, 8].Value = employees[i].Religion;
                worksheet.Cells[i + 2, 9].Value = employees[i].Race;
                worksheet.Cells[i + 2, 10].Value = employees[i].DearestPerson;
                worksheet.Cells[i + 2, 11].Value = employees[i].Ancestor;
                worksheet.Cells[i + 2, 12].Value = employees[i].Nrcnumber;
                worksheet.Cells[i + 2, 13].Value = employees[i].DateOfBirthString;
                worksheet.Cells[i + 2, 14].Value = employees[i].PlaceOfBirthName;
                worksheet.Cells[i + 2, 15].Value = employees[i].EducationType;
                worksheet.Cells[i + 2, 16].Value = employees[i].JoinDateString;
                worksheet.Cells[i + 2, 17].Value = employees[i].Address;
                worksheet.Cells[i + 2, 18].Value = employees[i].EyeColor;
                worksheet.Cells[i + 2, 19].Value = employees[i].Height;
                worksheet.Cells[i + 2, 20].Value = employees[i].Mark;


            }
            var stream = new MemoryStream();
            package.SaveAs(stream);

            // Return the stream as a file
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeData.xlsx");
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeStatusChange(string EmployeeCode,string Status,string? Comment=null)
        {
            TbEmployee tbEmployee = _context.TbEmployee.Where(x => x.EmployeeCode == EmployeeCode).FirstOrDefault();
            tbEmployee.Status = Status;
            tbEmployee.RejectComment = Comment;
            var userId = HttpContext.User.Identity.Name;
            var emp = _employeeServices.SaveEmployee(tbEmployee, Convert.ToInt32(userId), tbEmployee.EmployeePkid);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeEditRequest(string EmployeeCode, string RequestType, string? Comment = null)
        {
            TbEmployee tbEmployee = _context.TbEmployee.Where(x => x.EmployeeCode == EmployeeCode).FirstOrDefault();
            if(RequestType=="Edit")
            {
                tbEmployee.EditRequest = "Requesting";
            }
            else
            {
                tbEmployee.DeleteRequest = "Requesting";
            }
            tbEmployee.EditComment = Comment;
            var userId = HttpContext.User.Identity.Name;
            ViewBag.StateDivisionCode = TempData["StateDivisionCode"];
            var emp = _employeeServices.SaveEmployee(tbEmployee, Convert.ToInt32(userId), tbEmployee.EmployeePkid);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeConfirm(string EmployeeCode,string RequestType)
        {
            TbEmployee tbEmployee = _context.TbEmployee.Where(x => x.EmployeeCode == EmployeeCode).FirstOrDefault();
            if(RequestType=="Edit")
            {
                tbEmployee.EditRequest = "Confirm";
            }
            else
            {
                tbEmployee.DeleteRequest = "Confirm";
            }
            var userId = HttpContext.User.Identity.Name;
            ViewBag.StateDivisionCode = TempData["StateDivisionCode"];
            var emp = _employeeServices.SaveEmployee(tbEmployee, Convert.ToInt32(userId), tbEmployee.EmployeePkid);
            return Ok();
        }
    }
}
