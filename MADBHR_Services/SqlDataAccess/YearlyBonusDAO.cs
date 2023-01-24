using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class YearlyBonusDAO:GenericCommonExtensions<TbYearlyBonus>
    {
        public dynamic SaveYearlyBonus(IDbCommand cmd, TbYearlyBonus yearlyBonus, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_YearlyBonus_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", yearlyBonus.YearlyBonusPkid);
                cmd.AddParameter("@EmployeeCode", yearlyBonus.EmployeeCode);
                cmd.AddParameter("@ApprovedDate", yearlyBonus.ApprovedDate);
                cmd.AddParameter("@ApprovedNo", yearlyBonus.ApprovedNo);
                cmd.AddParameter("@YearlyBonusCount", yearlyBonus.YearlyBonusCount);
                cmd.AddParameter("@YearlyBonusSalary", yearlyBonus.YearlyBonusSalary);
                cmd.AddParameter("@YearlyBonusDate", yearlyBonus.YearlyBonusDate);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", yearlyBonus.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return yearlyBonus;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbYearlyBonus> GetYearlyBonus(IDbCommand cmd,  string? EmployeeCode = null, DateTime? ApprovedDate = null)
        {

            cmd.CommandText = "Sp_YearlyBonus_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@ApprovedDate", ApprovedDate);
            cmd.AddParameter("@EmployeeCode", EmployeeCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbYearlyBonus> lstYearlyBonus = new List<TbYearlyBonus>();
            if (ResDs != null)
            {
                if (ResDs.Tables.Count > 0)
                {
                    if (ResDs.Tables[0] != null)
                    {
                        if (ResDs.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ResDs.Tables[0].Rows.Count; i++)
                            {
                                TbYearlyBonus yearlyBonus = new TbYearlyBonus
                                {
                                    YearlyBonusPkid = ResDs.Tables[0].Rows[i]["YearlyBonusPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["YearlyBonusPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    ApproveDateStr = ResDs.Tables[0].Rows[i]["ApprovedDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ApprovedDate"].ToString() : "",
                                    ApprovedNo = ResDs.Tables[0].Rows[i]["ApprovedNo"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ApprovedNo"].ToString() : "",
                                    YearlyBonusCount = ResDs.Tables[0].Rows[i]["YearlyBonusCount"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["YearlyBonusCount"].ToString() : "",
                                    YearlyBonusSalary = ResDs.Tables[0].Rows[i]["YearlyBonusSalary"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["YearlyBonusSalary"].ToString() : "",
                                    YearlyBonusDateStr = ResDs.Tables[0].Rows[i]["YearlyBonusDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["YearlyBonusDate"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstYearlyBonus.Add(yearlyBonus);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstYearlyBonus;

        }
        public void DeleteYearlyBonus(IDbCommand cmd, int awardPkid, int userId)
        {

            cmd.CommandText = "Sp_YearlyBonus_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", awardPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbYearlyBonus aInfo, string prefix)
        { }
    }
}
