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
    public class TrainingHistoryController : Controller
    {
        private readonly MADBAdminSolutionContext _context;
        private readonly ITrainingHistoryServices _trainingHistoryServices;
        private readonly Pagination _pagination;

        public TrainingHistoryController(MADBAdminSolutionContext context, ITrainingHistoryServices trainingHistoryServices, IOptions<Pagination> pagination)
        {
            _context = context;
            _trainingHistoryServices = trainingHistoryServices;
            _pagination = pagination.Value;
        }
        public void Initialize(TbTrainingHistory traingHistory = null)
        {
            var userId = HttpContext.User.Identity.Name;
            ViewBag.lstLogIn = _context.TbUserLogin.Where(x => x.Status == "Enable" && x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
            List<string> lstLocation = new List<string>();
            lstLocation.Add("ပြည်တွင်း");
            lstLocation.Add("ပြည်ပ");
            ViewData["Location"] = new SelectList(lstLocation, traingHistory?.Location);

        }
        public IActionResult Index(string? SerialNumber = null, string SchoolName = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
            var trainingHistories = _trainingHistoryServices.GetTrainingHistory(SchoolName, EmployeeCode).ToList();
            return View(trainingHistories.OrderByDescending(x => x.CreatedDate).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbTrainingHistory trainingHistory)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var userInfo = _context.TbUserLogin.Where(x => x.UserPkid == Convert.ToInt32(userId)).FirstOrDefault();
                    trainingHistory.UploadForTownship = userInfo.TownshipId == null || userInfo.TownshipId == "" ? userInfo.StateDivisionId : userInfo.TownshipId;

                    trainingHistory.EmployeeCode = _context.TbEmployee.Where(x => x.SerialNumber == trainingHistory.SerialNumber).Select(x => x.EmployeeCode).FirstOrDefault();
                    var emp = await _trainingHistoryServices.SaveTrainingHistory(trainingHistory, Convert.ToInt32(userId), 0);

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
            var trainingHistory = _context.TbTrainingHistory.Where(x => x.TrainingHistoryPkid == Id).FirstOrDefault();         
            trainingHistory.SerialNumber = _context.TbEmployee.Where(x => x.EmployeeCode == trainingHistory.EmployeeCode).Select(x => x.SerialNumber).FirstOrDefault();
            Initialize(trainingHistory);
            return View(trainingHistory);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TbTrainingHistory history, bool? RedirectToSonAndDaughter = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    //if (ModelState.IsValid)
                    //{
                    var userId = HttpContext.User.Identity.Name;
                    var emp = await _trainingHistoryServices.SaveTrainingHistory(history, Convert.ToInt32(userId), history.TrainingHistoryPkid);

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
                _trainingHistoryServices.DeleteTrainingHistory(id, Convert.ToInt32(userId));
                //TempData["notice"] = StatusEnum.NoticeStatus.Delete;

            }
            catch (Exception e)
            {

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
