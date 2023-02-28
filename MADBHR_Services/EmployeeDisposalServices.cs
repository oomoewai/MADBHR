using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using MADBHR_Models.Employee;
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
   public class EmployeeDisposalServices:IEmployeeDisposalServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly EmployeeDisposalDAO _employeeDisposalDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public EmployeeDisposalServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _employeeDisposalDAO = new EmployeeDisposalDAO();
            _context = context;
        }
        public VMEmployeeInfo GetEmployeeInfo(string SerialNumber )
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var empInfo = _employeeDisposalDAO.GetEmpInfo(cmd, SerialNumber);

                return empInfo;
            }
            catch (Exception ex)
            {
                VMEmployeeInfo emps = new VMEmployeeInfo();
                return emps;
            }
        }
        public async Task<dynamic> SaveEmployeeDisposal(TbDisposal disposal, int userId, int Id,bool isTransfer)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _employeeDisposalDAO.SaveEmployeeDisposal(cmd, disposal, userId, Id,isTransfer);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return emp;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbDisposal> GetEmployeeDisposal(string? DisposalTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var disposals = _employeeDisposalDAO.GetDisposal(cmd, DisposalTypeCode, EmployeeCode);

                return disposals;
            }
            catch (Exception ex)
            {
                List<TbDisposal> emps = new List<TbDisposal>();
                return emps;
            }
        }
        public List<TbDisposal> GetEmployeeDisposalForAdmin(string? StateDivisionCode = null, string? TownshipCode = null,string ? Name = null, string? SerialNumber = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var disposals = _employeeDisposalDAO.GetDisposalForAdmin(cmd, StateDivisionCode, TownshipCode,Name,SerialNumber);

                return disposals;
            }
            catch (Exception ex)
            {
                List<TbDisposal> emps = new List<TbDisposal>();
                return emps;
            }
        }
        public void DeleteDisposal(string EmployeeCode, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _employeeDisposalDAO.DeleteDisposal(cmd, EmployeeCode, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
