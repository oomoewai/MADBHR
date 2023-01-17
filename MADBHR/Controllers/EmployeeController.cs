using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADBHR.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly MADBAdminSolutionContext _context;
        private readonly IEmployeeServices _employeeServices;
        public EmployeeController(MADBAdminSolutionContext context, IEmployeeServices employeeServices)
        {
            _context = context;
            _employeeServices = employeeServices;
        }
        public void Initialize(TbEmployee employee = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt64(userId)).FirstOrDefault();
            var placeOfBirths = _context.TbPlaceOfBirth.Where(x => x.Active == true).ToList();
            ViewData["PlaceOfBirth"] = new SelectList(placeOfBirths, "TownshipCode", "Township", employee?.PlaceOfBirth);
        }
        public IActionResult Index()
        {
            Initialize();
            return View();
        }
        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbEmployee employee, bool? RedirectToVisit = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
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
                    if (didUploaded)
                    {

                        var emp = await _employeeServices.SaveEmployee(employee);
                        return RedirectToAction(nameof(Index));

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
    }
}
