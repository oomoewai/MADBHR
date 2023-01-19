using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
   public interface ISonAndDaughterServices
    {
        Task<dynamic> SaveSonAndDaughter(TbSonAndDaughter sonAndDaughter, int userId, int Id);
        void DeleteSonAndDaughter(int pkId, int userId);
    }
}
