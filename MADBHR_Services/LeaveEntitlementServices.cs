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
   public class LeaveEntitlementServices:ILeaveEntitlementServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly LeaveEntitlementDAO _leaveEntitlementDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public LeaveEntitlementServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _leaveEntitlementDAO = new LeaveEntitlementDAO();
            _context = context;
        }
        public async Task<dynamic> SaveLeaveEntitlement(TbLeaveEntitlement leaveEntitlement, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _leaveEntitlementDAO.SaveLeaveEntitlement(cmd, leaveEntitlement, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return leaveEntitlement;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbLeaveEntitlement> GetLeaveEntitlement(string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var leaveEntitlements = _leaveEntitlementDAO.GetLeaveEntitlement(cmd, EmployeeCode, FromDate, ToDate);

                return leaveEntitlements;
            }
            catch (Exception ex)
            {
                List<TbLeaveEntitlement> emps = new List<TbLeaveEntitlement>();
                return emps;
            }
        }
        public List<TbLeaveEntitlement> GetLeaveEntitlementForAdmin(string? StateDivisionCode = null, string? TownshipCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var leaveEntitlements = _leaveEntitlementDAO.GetLeaveEntitlementForAdmin(cmd, StateDivisionCode, TownshipCode);

                return leaveEntitlements;
            }
            catch (Exception ex)
            {
                List<TbLeaveEntitlement> emps = new List<TbLeaveEntitlement>();
                return emps;
            }
        }
        public void DeleteLeaveEntitlement (int leaveEntitlementPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _leaveEntitlementDAO.DeleteLeaveEntitlement(cmd, leaveEntitlementPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
