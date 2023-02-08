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
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services
{
    public class AwardServices:IAwardServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly AwardDAO _awardDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public AwardServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _awardDAO = new AwardDAO();
            _context = context;
        }
        public async Task<dynamic> SaveAward(TbAward award, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _awardDAO.SaveAward(cmd, award, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return award;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbAward> GetAward(string? AwardTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var awards = _awardDAO.GetAward(cmd, AwardTypeCode, EmployeeCode);

                return awards;
            }
            catch (Exception ex)
            {
                List<TbAward> emps = new List<TbAward>();
                return emps;
            }
        }
        public List<TbAward> GetAwardForAdmin(string? StateDivisionCode = null, string? TownshipCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var awards = _awardDAO.GetAwardForAdmin(cmd, StateDivisionCode, TownshipCode);

                return awards;
            }
            catch (Exception ex)
            {
                List<TbAward> emps = new List<TbAward>();
                return emps;
            }
        }
        public void DeleteAward(int awardPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _awardDAO.DeleteAward(cmd, awardPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
