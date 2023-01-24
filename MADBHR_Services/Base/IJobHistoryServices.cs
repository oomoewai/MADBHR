using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
   public interface IJobHistoryServices
    {
        Task<dynamic> SaveJobHistory(TbJobHistory jobHistory, int userId, int Id);
        List<TbJobHistory> GetJobHistory(string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        void DeleteJobHistory(int jobHistoryPkid, int userId);
    }
}
