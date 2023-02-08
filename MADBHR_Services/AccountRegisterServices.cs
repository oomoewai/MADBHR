using MADBHR_Data.Models;
using MADBHR_Data.Repository.Base;
using MADBHR_Services.Base;
using MADBHR_Services.Options;
using MADBHR_Services.SqlDataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MADBHR_Services
{
    public class AccountRegisterServices:IAccountRegisterServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly AccountRegisterDAO _accountRegisterDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public AccountRegisterServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _accountRegisterDAO = new AccountRegisterDAO();
            _context = context;
        }
        public async Task<dynamic> SaveAccount(TbUserLogin userLogin, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _accountRegisterDAO.SaveAccount(cmd, userLogin, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return userLogin;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
    }
}
