using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IAccountRegisterServices
    {
        Task<dynamic> SaveAccount(TbUserLogin userLogin, int userId, int Id);
    }
}
