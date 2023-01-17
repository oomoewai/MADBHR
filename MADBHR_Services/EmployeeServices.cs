﻿using MADBHR_Data.Models;
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
        public async Task<dynamic> SaveEmployee(TbEmployee employee)
        {
            try
            {
                var departmentInfo = _unitOfwork.TbEmployeeRepository.Get(x => x.EmployeeCode == employee.EmployeeCode).FirstOrDefault();
                if (departmentInfo == null)
                {
                    IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                    IDbConnection mycon = connection;
                    IDbCommand cmd = mycon.CreateCommand();
                    var emp = _employeeDAO.SaveEmployee(cmd, employee, 1);
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
    }
}
