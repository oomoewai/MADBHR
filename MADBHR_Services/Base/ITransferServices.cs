using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface ITransferServices
    {
        Task<dynamic> SaveTransfer(TbTransfer transfer, int userId, int Id);
        List<TbTransfer> GetTransferForAdmin(string? StateDivisionCode = null, string? TownshipCode = null);
        List<TbTransfer> GetTransfer(string EmployeeCode = null, string? FromTownshipCode = null, string? ToTownshipCode = null, int? TransferPkid = null);
    }
}
