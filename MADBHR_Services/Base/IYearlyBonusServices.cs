using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IYearlyBonusServices
    {
        Task<dynamic> SaveYearlyBonus(TbYearlyBonus yearlyBonus, int userId, int Id);
        List<TbYearlyBonus> GetYearlyBonus(string? EmployeeCode = null, DateTime? ApprovedDate = null);
        void DeleteYearlyBonus(int yearlyBonusPkid, int userId);
    }
}
