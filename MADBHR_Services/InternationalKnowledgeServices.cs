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
    public class InternationalKnowledgeServices : IInternationalKnowledgeServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly InternationalKnowledgeDAO _internationalKnowledgeDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public InternationalKnowledgeServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _internationalKnowledgeDAO = new InternationalKnowledgeDAO();
            _context = context;
        }
        public async Task<dynamic> SaveIntKnowledge(TbIntKnowledge intKnowledge, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _internationalKnowledgeDAO.SaveIntKnowledge(cmd, intKnowledge, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return intKnowledge;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbIntKnowledge> GetIntKnowledge( string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var knowledges = _internationalKnowledgeDAO.GetIntKnowledge(cmd, EmployeeCode,FromDate,ToDate);

                return knowledges;
            }
            catch (Exception ex)
            {
                List<TbIntKnowledge> emps = new List<TbIntKnowledge>();
                return emps;
            }
        }
        public List<TbIntKnowledge> GetIntKnowledgeForAdmin(string? StateDivisionCode = null, string? TownshipCode = null, string? Name = null, string? SerialNumber = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var knowledges = _internationalKnowledgeDAO.GetIntKnowledgeForAdmn(cmd, StateDivisionCode, TownshipCode,Name,SerialNumber);

                return knowledges;
            }
            catch (Exception ex)
            {
                List<TbIntKnowledge> emps = new List<TbIntKnowledge>();
                return emps;
            }
        }
        public void DeleteIntKnowledge(int intKnowledgePkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _internationalKnowledgeDAO.DeleteIntKnowledge(cmd, intKnowledgePkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
