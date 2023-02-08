using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IPunishmentServices
    {
        Task<dynamic> SavePunishment(TbPunishment punishment, int userId, int Id);
        List<TbPunishment> GetPunishment(string? PunishmentTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        List<TbPunishment> GetPunishmentForAdmin(string? StateDivisionCode = null, string? TownshipCode = null);
        void DeletePunishment(int punishmentPkid, int userId);
    }
}
