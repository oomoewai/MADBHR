using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IEmployeeDisposalServices
    {
        VMEmployeeInfo GetEmployeeInfo(string SerialNumber);
        Task<dynamic> SaveEmployeeDisposal(TbDisposal disposal, int userId, int Id, bool isTransfer);
        List<TbDisposal> GetEmployeeDisposal(string? DisposalTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null);
        List<TbDisposal> GetEmployeeDisposalForAdmin(string? StateDivisionCode = null, string? TownshipCode = null);
        void DeleteDisposal(string EmployeeCode, int userId);
    }
}
