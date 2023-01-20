using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IAwardServices
    {
       Task<dynamic> SaveAward(TbAward award, int userId, int Id);
        List<TbAward> GetAward(string? AwardTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        void DeleteAward(int awardPkid, int userId);
    }
}
