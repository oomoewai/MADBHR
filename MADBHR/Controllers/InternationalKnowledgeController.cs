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
    public class InternationalKnowledgeController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IInternationalKnowledgeServices _internationalKnowledgeServices;
        private readonly Pagination _pagination;
        public InternationalKnowledgeController(MADBAdminSolutionContext context, IInternationalKnowledgeServices internationalKnowledgeServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _internationalKnowledgeServices = internationalKnowledgeServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbIntKnowledge tbIntKnowledge = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
          
        }
        public IActionResult Index(string? SerialNumber = null,DateTime? FromDate=null,DateTime? ToDate=null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
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

                    intKnowledge.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == intKnowledge.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
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
            intKnowledge.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == intKnowledge.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(intKnowledge);
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
    }
}
