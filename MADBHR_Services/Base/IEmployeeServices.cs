using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services.Base
{
    public interface IEmployeeServices
    {
        Task<dynamic> SaveEmployee(TbEmployee employee);
    }
}
