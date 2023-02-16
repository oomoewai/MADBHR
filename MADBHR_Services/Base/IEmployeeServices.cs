using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IEmployeeServices
    {
        Task<dynamic> SaveEmployee(TbEmployee employee,int userId, int Id);
        List<TbEmployee> GetEmployee(string? Name = null, DateTime? FromDate = null, DateTime? ToDate = null,string? SerialNumber=null);
        List<VMEmployeeCount> GetEmployeeCount(string? StateDivisionCode = null);
        List<TbEmployee> GetEmployeeForAdmin(string? StateDivisionCode = null, string? TownshipCode = null,string? status=null);
        void DeleteEmployee(int EmployeePkid, int userId);
    }
}
