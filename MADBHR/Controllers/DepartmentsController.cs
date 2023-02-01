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
    public class DepartmentsController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IDepartmentServices _departmentServices;
        private readonly Pagination _pagination;
        public DepartmentsController(MADBAdminSolutionContext context, IDepartmentServices departmentServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _departmentServices = departmentServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbDepartment tbDepartment = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            

        }
        public IActionResult Index(string? Department = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var departments = _departmentServices.GetDepartment(Department).ToList();
            return View(departments.OrderByDescending(x => x.DepartmentPkid).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbDepartment department)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    department.Department = department.DepartmentName;
                    //department.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    //department.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == award.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _departmentServices.SaveDepartment(department, Convert.ToInt32(userId), 0);

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
            var department = _context.TbDepartment.Where(x => x.DepartmentPkid == Id).FirstOrDefault();
            Initialize(department);
            return View(department);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbDepartment department)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    department.Department = department.DepartmentName;
                    var emp = await _departmentServices.SaveDepartment(department, Convert.ToInt32(userId), department.DepartmentPkid);

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
                var department = _context.TbDepartment.Where(x => x.DepartmentPkid == id).FirstOrDefault();
                _departmentServices.DeleteDepartment(department.DepartmentCode);
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
