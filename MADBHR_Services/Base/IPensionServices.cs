using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IPensionServices
    {
        Task<dynamic> SavePension(TbPension pension, int userId, int Id);
        List<TbPension> GetPension(string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        void DeletePension(int PensionPkid, int userId);
    }
}
