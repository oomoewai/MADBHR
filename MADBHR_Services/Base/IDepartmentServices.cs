using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IDepartmentServices
    {
        Task<dynamic> SaveDepartment(TbDepartment department, int userId, int Id);
        List<TbDepartment> GetDepartment(string? Department = null, DateTime? FromDate = null, DateTime? ToDate = null);
        void DeleteDepartment(string departmentCode);
    }
}
