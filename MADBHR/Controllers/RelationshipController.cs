using MADBHR.Helper;
using MADBHR_Data.Models;
using MADBHR_Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MADBHR.Controllers
{
    public class RelationshipController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IRelationshipServices _relationshipServices;
        private readonly Pagination _pagination;

        public RelationshipController(MADBAdminSolutionContext context, IRelationshipServices relationshipServices,IOptions<Pagination> pagination)
        {
            _context = context;
            _relationshipServices = relationshipServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbRelationship relationship = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            
        }
        public IActionResult Index(string? EmployeeCode = null,int?page=1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var relationshipInfo = _context.TbRelationship.Where(x => x.EmployeeCode == EmployeeCode && (x.IsDeleted == false || x.IsDeleted ==null)).ToList();
            return View(relationshipInfo.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create(string? serialNumber=null)
        {
            Initialize();
            if (serialNumber != null)
            {
                ViewData["SerialNumber"] = serialNumber;
                TempData["SerialNumber"] = serialNumber;
                TempData["EmployeeCode"] = _context.TbEmployee.Where(x => x.SerialNumber ==serialNumber).Select(x=>x.EmployeeCode).FirstOrDefault(); 
            }
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbRelationship relationship, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    relationship.EmployeeCode = TempData["EmployeeCode"].ToString();
                    var emp = await _relationshipServices.SaveRelationShip(relationship, Convert.ToInt32(userId),0);
                    if (RedirectToSonAndDaughter == true)
                    {
                        return RedirectToAction("Create", "SonAndDaughter", new { SerialNumber = TempData["SerialNumber"].ToString(), EmployeeCode=emp.EmployeeCode});
                    }
                    else
                    {
                        return RedirectToAction("Index","Employee");
                    }


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
            var relationInfo = _context.TbRelationship.Where(x => x.RelationshipPkid == Id).FirstOrDefault();
            ViewData["SerialNumber"] = _context.TbEmployee.Where(x => x.EmployeeCode == relationInfo.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(relationInfo);
            return View(relationInfo);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbRelationship relationship, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var emp = await _relationshipServices.SaveRelationShip(relationship, Convert.ToInt32(userId), relationship.RelationshipPkid);
                    if (RedirectToSonAndDaughter == true)
                    {
                        return RedirectToAction("Index", "SonAndDaughter", new { EmployeeCode = emp.EmployeeCode });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                    //}
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
                _relationshipServices.DeleteRelationship(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
