using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface ITrainingHistoryServices
    {
        Task<dynamic> SaveTrainingHistory(TbTrainingHistory history, int userId, int Id);
        List<TbTrainingHistory> GetTrainingHistory(string? Name = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        void DeleteTrainingHistory(int traingHistoryPkid, int userId);
    }
}
