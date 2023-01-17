using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using MADBHR_Models.Employee;
using MADBHR_Services.Base;
using MADBHR_Services.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services
{
    public class EmployeeServices : IEmployeeServices
    {
        public ConnectionStrings _connectionStrings;
        //public readonly DepartmentDAO _departmentDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public EmployeeServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings,MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            //_departmentDAO = new DepartmentDAO();
            _context = context;
        }
        public async Task<dynamic> SaveEmployee(TbEmployee employee)
        {
            try
            {
                var departmentInfo = _unitOfwork.TbEmployeeRepository.Get(x => x.EmployeeCode == employee.EmployeeCode).FirstOrDefault();
                if (departmentInfo == null)
                {
                   

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
