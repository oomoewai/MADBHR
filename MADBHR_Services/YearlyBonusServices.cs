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
    public class YearlyBonusServices:IYearlyBonusServices
    {
        public ConnectionStrings _connectionStrings;
        public readonly YearlyBonusDAO _yearlyBonusDAO;
        public IUnitOfWork _unitOfwork;
        public readonly MADBAdminSolutionContext _context;
        public YearlyBonusServices(IUnitOfWork unitOfWork, IOptions<ConnectionStrings> connectionStrings, MADBAdminSolutionContext context)
        {
            _unitOfwork = unitOfWork;
            _connectionStrings = connectionStrings.Value;
            _yearlyBonusDAO = new YearlyBonusDAO();
            _context = context;
        }
        public async Task<dynamic> SaveYearlyBonus(TbYearlyBonus yearlyBonus, int userId, int Id)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection mycon = connection;
                IDbCommand cmd = mycon.CreateCommand();
                var emp = _yearlyBonusDAO.SaveYearlyBonus(cmd, yearlyBonus, userId, Id);
                //_unitOfwork.TbEmployeeRepository.Insert(employee);
                //_unitOfwork.Commit();

                return yearlyBonus;

            }
            catch (Exception ex)
            {
                return ex;
            }

        }
        public List<TbYearlyBonus> GetYearlyBonus( string? EmployeeCode = null, DateTime? ApprovedDate = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var yearlyBonus = _yearlyBonusDAO.GetYearlyBonus(cmd, EmployeeCode,ApprovedDate);

                return yearlyBonus;
            }
            catch (Exception ex)
            {
                List<TbYearlyBonus> emps = new List<TbYearlyBonus>();
                return emps;
            }
        }
        public List<TbYearlyBonus> GetYearlyBonusForAdmin(string? StateDivisionCode = null, string? TownshipCode = null, string? SerialNumber = null, string Name = null)
        {

            try
            {
                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                var yearlyBonus = _yearlyBonusDAO.GetYearlyBonusForAdmin(cmd, StateDivisionCode, TownshipCode,SerialNumber,Name);

                return yearlyBonus;
            }
            catch (Exception ex)
            {
                List<TbYearlyBonus> emps = new List<TbYearlyBonus>();
                return emps;
            }
        }
        public void DeleteYearlyBonus(int yearlyBonusPkid, int userId)
        {
            try
            {

                IDbConnection connection = new SqlConnection(_connectionStrings.DefaultConnection);
                IDbConnection myCon = connection;
                IDbCommand cmd = myCon.CreateCommand();
                _yearlyBonusDAO.DeleteYearlyBonus(cmd, yearlyBonusPkid, userId);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
