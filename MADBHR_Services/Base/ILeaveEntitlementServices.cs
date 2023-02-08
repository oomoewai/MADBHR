using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface ILeaveEntitlementServices
    {
        void DeleteLeaveEntitlement(int leaveEntitlementPkid, int userId);
        Task<dynamic> SaveLeaveEntitlement(TbLeaveEntitlement leaveEntitlement, int userId, int Id);
        List<TbLeaveEntitlement> GetLeaveEntitlement(string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        List<TbLeaveEntitlement> GetLeaveEntitlementForAdmin(string? StateDivisionCode = null, string? TownshipCode = null);
    }
}
