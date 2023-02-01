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
    public class DepartmentServices:IDepartmentServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly DepartmentDAO _departmentDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public DepartmentServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _departmentDAO = new DepartmentDAO();
            _context = context;
        }
        public async Task<dynamic> SaveDepartment(TbDepartment department, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _departmentDAO.SaveDepartment(cmd, department, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return department;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbDepartment> GetDepartment(string? Department = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var departments = _departmentDAO.GetDepartment(cmd,Department);

                return departments;
            }
            catch (Exception ex)
            {
                List<TbDepartment> emps = new List<TbDepartment>();
                return emps;
            }
        }
        public void DeleteDepartment(string departmentCode)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _departmentDAO.DeleteDepartment(cmd, departmentCode);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
