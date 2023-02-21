using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using MADBHR_Models.JobExperience;
using MADBHR_Services.Base;
using MADBHR_Services.Options;
using MADBHR_Services.SqlDataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services
{
    public class JobHistoryServices:IJobHistoryServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly JobHistoryDAO _jobHistoryDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public JobHistoryServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _jobHistoryDAO = new JobHistoryDAO();
            _context = context;
        }
        public async Task<dynamic> SaveJobHistory(TbJobHistory jobHistory, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _jobHistoryDAO.SaveJobHistory(cmd, jobHistory, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return jobHistory;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbJobHistory> GetJobHistory(string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var jobHistories = _jobHistoryDAO.GetJobHistory(cmd, EmployeeCode, FromDate, ToDate);

                return jobHistories;
            }
            catch (Exception ex)
            {
                List<TbJobHistory> emps = new List<TbJobHistory>();
                return emps;
            }
        }
        public List<TbJobHistory> GetCurrentJobHistory(string? EmployeeCode = null, string? StateDivisionCode = null, string? TownshipCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var jobHistories = _jobHistoryDAO.GetCurrentJobHistory(cmd, EmployeeCode, StateDivisionCode, TownshipCode);

                return jobHistories;
            }
            catch (Exception ex)
            {
                List<TbJobHistory> emps = new List<TbJobHistory>();
                return emps;
            }
        }
        public List<VMJobExperience> GetTotalJobExperience(string? EmployeeCode = null,string? StateDivisionCode=null,string? TownshipCode=null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var jobexperiences = _jobHistoryDAO.GetTotalJobExperience(cmd, EmployeeCode,StateDivisionCode,TownshipCode);

                return jobexperiences;
            }
            catch (Exception ex)
            {
                List<VMJobExperience> emps = new List<VMJobExperience>();
                return emps;
            }
        }
        public List<VMJobExperience> GetCurrentJobExperience(string? EmployeeCode = null, string? StateDivisionCode = null, string? TownshipCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var jobexperiences = _jobHistoryDAO.GetCurrentJobExperience(cmd, EmployeeCode,  StateDivisionCode ,TownshipCode);

                return jobexperiences;
            }
            catch (Exception ex)
            {
                List<VMJobExperience> emps = new List<VMJobExperience>();
                return emps;
            }
        }
        public void DeleteJobHistory(int jobHistoryPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _jobHistoryDAO.DeleteJobHistory(cmd, jobHistoryPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
