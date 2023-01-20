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
    public class PunishmentServices:IPunishmentServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly PunishmentDAO _punishmentDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public PunishmentServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _punishmentDAO = new PunishmentDAO();
            _context = context;
        }
        public async Task<dynamic> SavePunishment(TbPunishment punishment, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _punishmentDAO.SavePunishment(cmd, punishment, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return punishment;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbPunishment> GetPunishment(string? PunishmentTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var history = _punishmentDAO.GetPunishment(cmd, PunishmentTypeCode, EmployeeCode);

                return history;
            }
            catch (Exception ex)
            {
                List<TbPunishment> emps = new List<TbPunishment>();
                return emps;
            }
        }
        public void DeletePunishment(int punishmentPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _punishmentDAO.DeletePunishment(cmd, punishmentPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
