using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using MADBHR_Services.Base;
using MADBHR_Services.Options;
using MADBHR_Services.SqlDataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services
{
    public class TrainingHistoryServices:ITrainingHistoryServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly TrainingHistoryDAO _trainingHistoryDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public TrainingHistoryServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _trainingHistoryDAO = new TrainingHistoryDAO();
            _context = context;
        }
        public async Task<dynamic> SaveTrainingHistory(TbTrainingHistory history, int userId, int Id)
        {
            try
            {
              
                    IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                    IDbConnection mycon = connection;
                    IDbCommand cmd = mycon.CreateCommand();
                    var emp = _trainingHistoryDAO.SaveTrainingHistory(cmd, history, userId, Id);
                    //_unitOfwork.TbEmployeeRepository.Insert(employee);
                    //_unitOfwork.Commit();

                    return history;
              
            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbTrainingHistory> GetTrainingHistory(string? Name = null,string? EmployeeCode=null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var history = _trainingHistoryDAO.GetTrainingHistory(cmd, Name, EmployeeCode);

                return history;
            }
            catch (Exception ex)
            {
                List<TbTrainingHistory> emps = new List<TbTrainingHistory>();
                return emps;
            }
        }
        public List<TbTrainingHistory> GetTrainingHistoryForAdmin( string? StateDivisionCode = null, string? TownshipCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var history = _trainingHistoryDAO.GetTrainingHistoryForAdmin(cmd, StateDivisionCode,TownshipCode);

                return history;
            }
            catch (Exception ex)
            {
                List<TbTrainingHistory> emps = new List<TbTrainingHistory>();
                return emps;
            }
        }
        public void DeleteTrainingHistory(int trainingHistoryPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _trainingHistoryDAO.DeleteHistory(cmd, trainingHistoryPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
