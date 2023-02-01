using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using MADBHR_Models.Employee;
using MADBHR_Services.Base;
using MADBHR_Services.Options;
using MADBHR_Services.SqlDataAccess;
using Microsoft.AspNetCore.Http;
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
    public class EmployeeServices : IEmployeeServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly EmployeeDAO _employeeDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public EmployeeServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings,MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _employeeDAO = new EmployeeDAO();
            _context = context;
        }
        public async Task<dynamic> SaveEmployee(TbEmployee employee,int userId,int Id)
        {
            try
            {
                var departmentInfo = _unitOfwork.TbEmployeeRepository.Get(x => x.EmployeeCode == employee.EmployeeCode).FirstOrDefault();
                if (departmentInfo == null)
                {
                    IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                    IDbConnection mycon = connection;
                    IDbCommand cmd = mycon.CreateCommand();
                    var emp = _employeeDAO.SaveEmployee(cmd, employee, userId,Id);
                    //_unitOfwork.TbEmployeeRepository.Insert(employee);
                    //_unitOfwork.Commit();

                    return employee;
                }
                else
                {

                    return "Already Exists Department";
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public  List<TbEmployee> GetEmployee(string? Name = null, DateTime? FromDate = null, DateTime? ToDate = null,string? SerialNumber=null)
        {
            
            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var employees = _employeeDAO.GetEmployee(cmd,Name,FromDate,ToDate);
               
                return employees;
            }
            catch (Exception ex)
            {
                List<TbEmployee> emps = new List<TbEmployee>();
                return emps;
            }
        }
        public List<VMEmployeeCount> GetEmployeeCount(string? StateDivisionCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var employees = _employeeDAO.GetEmployeeCounts(cmd, StateDivisionCode);

                return employees;
            }
            catch (Exception ex)
            {
                List<VMEmployeeCount> emps = new List<VMEmployeeCount>();
                return emps;
            }
        }
        public List<TbEmployee> GetEmployeeForAdmin(string? StateDivisionCode = null,string? TownshipCode=null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var employees = _employeeDAO.GetEmployeeForAdmin(cmd, StateDivisionCode,TownshipCode);

                return employees;
            }
            catch (Exception ex)
            {
                List<TbEmployee> emps = new List<TbEmployee>();
                return emps;
            }
        }
        public void DeleteEmployee(int EmployeePkid,int userId)
        {
            try
            {
                var employeeCode = _context.TbEmployee.Where(x => x.EmployeePkid == EmployeePkid).Select(x => x.EmployeeCode).FirstOrDefault();
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
               _employeeDAO.DeleteEmployee(cmd,employeeCode,userId);
            }
            catch(Exception ex)
            {

            }
        }

    }
}
