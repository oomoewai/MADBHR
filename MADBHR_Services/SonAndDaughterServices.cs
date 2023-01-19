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
    public class SonAndDaughterServices : ISonAndDaughterServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly SonAndDaughterDAO _sonAndDaughterDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public SonAndDaughterServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _sonAndDaughterDAO = new SonAndDaughterDAO();
            _context = context;
        }
        public async Task<dynamic> SaveSonAndDaughter(TbSonAndDaughter sonAndDaughter, int userId, int Id)
        {
            try
            {


                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _sonAndDaughterDAO.SaveSonAndDaughter(cmd, sonAndDaughter, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return sonAndDaughter;

            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public void DeleteSonAndDaughter(int pkId, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _sonAndDaughterDAO.DeleteSonAndDaughter(cmd, pkId, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
