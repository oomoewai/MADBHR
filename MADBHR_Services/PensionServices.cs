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
    public class PensionServices:IPensionServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly PensionDAO _pensionDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public PensionServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _pensionDAO = new PensionDAO();
            _context = context;
        }
        public async Task<dynamic> SavePension(TbPension pension, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _pensionDAO.SavePension(cmd, pension, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return emp;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbPension> GetPension( string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var pensions = _pensionDAO.GetPension(cmd, EmployeeCode);

                return pensions;
            }
            catch (Exception ex)
            {
                List<TbPension> emps = new List<TbPension>();
                return emps;
            }
        }
        public void DeletePension(int PensionPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _pensionDAO.DeletePension(cmd, PensionPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
