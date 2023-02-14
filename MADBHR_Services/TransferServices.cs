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
    public class TransferServices:ITransferServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly TransferDAO _transferDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public TransferServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _transferDAO = new TransferDAO();
            _context = context;
        }
        public async Task<dynamic> SaveTransfer(TbTransfer transfer, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _transferDAO.SaveTransfer(cmd, transfer, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return transfer;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbTransfer> GetTransferForAdmin(string? StateDivisionCode = null, string? TownshipCode = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var transfers = _transferDAO.GetTransferForAdmin(cmd, StateDivisionCode, TownshipCode);

                return transfers;
            }
            catch (Exception ex)
            {
                List<TbTransfer> emps = new List<TbTransfer>();
                return emps;
            }
        }
        public List<TbTransfer> GetTransfer( string EmployeeCode = null,string? FromTownshipCode=null,string? ToTownshipCode=null,int? TransferPkid=null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var transfers = _transferDAO.GetTransferForDetail(cmd, EmployeeCode,FromTownshipCode,ToTownshipCode,TransferPkid);

                return transfers;
            }
            catch (Exception ex)
            {
                List<TbTransfer> emps = new List<TbTransfer>();
                return emps;
            }
        }
    }
}
