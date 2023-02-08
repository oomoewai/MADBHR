using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IPensionServices
    {
        Task<dynamic> SavePension(TbPension pension, int userId, int Id);
        List<TbPension> GetPension(string? StateDivisionCode = null, string? TownshipCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        List<VMPensionEmpCount> GetPensionEmployeeCount(string? StateDivisionCode = null);
        void DeletePension(int PensionPkid, int userId);
    }
}
