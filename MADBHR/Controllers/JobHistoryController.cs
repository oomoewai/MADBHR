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
    public class JobHistoryController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly IJobHistoryServices _jobHistoryServices;
        private readonly Pagination _pagination;
        public JobHistoryController(MADBAdminSolutionContext context, IJobHistoryServices jobHistoryServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _jobHistoryServices = jobHistoryServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbJobHistory tbJobHistory = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            var currentJobTownship = _context.TbCurrentJobTownship.Where(x => x.Active == true).ToList();
            ViewData["TownshipCode"] = new SelectList(currentJobTownship, "TownshipCode", "Township", tbJobHistory?.UploadForTownship);
            var rankType = _context.TbRankType.Select(x => new { x.RankTypeCode,x.RankType }).ToList();
            ViewData["RankType"] = new SelectList(rankType, "RankTypeCode", "RankType", tbJobHistory?.RankTypeCode);

        }
        public IActionResult Index(string? SerialNumber = null, DateTime? FromDate = null, DateTime? ToDate = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var jobHistories = _jobHistoryServices.GetJobHistory(EmployeeCode, FromDate, ToDate).ToList();
            return View(jobHistories.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbJobHistory jobHistory)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    //jobHistory.UploadForTownship =userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    jobHistory.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == jobHistory.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _jobHistoryServices.SaveJobHistory(jobHistory, Convert.ToInt32(userId), 0);

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
            var jobHistory = _context.TbJobHistory.Where(x => x.JobHistoryPkid == Id).FirstOrDefault();
            jobHistory.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == jobHistory.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(jobHistory);
            return View(jobHistory);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbJobHistory jobHistory, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    jobHistory.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;
                    var emp = await _jobHistoryServices.SaveJobHistory(jobHistory, Convert.ToInt32(userId), jobHistory.JobHistoryPkid);

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
                _jobHistoryServices.DeleteJobHistory(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
