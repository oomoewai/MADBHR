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
    public class RelationshipServices:IRelationshipServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly RelationshipDAO _relationshipDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public RelationshipServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _relationshipDAO = new RelationshipDAO();
            _context = context;
        }
        public async Task<dynamic> SaveRelationShip(TbRelationship relationship, int userId, int Id)
        {
            try
            {


                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _relationshipDAO.SaveRelationShip(cmd, relationship, userId,Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return relationship;

            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public void DeleteRelationship(int RelationshipPkid, int userId)
        {
            try
            {
               
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _relationshipDAO.DeleteRelationship(cmd, RelationshipPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
