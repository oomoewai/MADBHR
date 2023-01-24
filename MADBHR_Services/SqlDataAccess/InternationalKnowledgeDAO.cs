using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class InternationalKnowledgeDAO:GenericCommonExtensions<TbIntKnowledge>
    {
        public dynamic SaveIntKnowledge(IDbCommand cmd, TbIntKnowledge intKnowledge, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_IntKnowledge_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", intKnowledge.IntKnowledgePkid);
                cmd.AddParameter("@EmployeeCode", intKnowledge.EmployeeCode);
                cmd.AddParameter("@FromDate", intKnowledge.FromDate);
                cmd.AddParameter("@ToDate", intKnowledge.ToDate);
                cmd.AddParameter("@CountryName", intKnowledge.CountryName);
                cmd.AddParameter("@Description", intKnowledge.Description);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", intKnowledge.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return intKnowledge;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbIntKnowledge> GetIntKnowledge(IDbCommand cmd, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_IntKnowledge_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@FromDate", FromDate);
            cmd.AddParameter("@ToDate", ToDate);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbIntKnowledge> lstIntKnowledge = new List<TbIntKnowledge>();
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
                                TbIntKnowledge intKnowledge = new TbIntKnowledge
                                {
                                    IntKnowledgePkid = ResDs.Tables[0].Rows[i]["IntKnowledgePkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["IntKnowledgePkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    FromDateStr = ResDs.Tables[0].Rows[i]["FromDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FromDate"].ToString() : "",
                                    ToDateStr = ResDs.Tables[0].Rows[i]["ToDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ToDate"].ToString() : "",
                                    CountryName = ResDs.Tables[0].Rows[i]["CountryName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CountryName"].ToString() : "",
                                    Description = ResDs.Tables[0].Rows[i]["Description"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Description"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstIntKnowledge.Add(intKnowledge);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstIntKnowledge;

        }
        public void DeleteIntKnowledge(IDbCommand cmd, int intKnowledgePkid, int userId)
        {

            cmd.CommandText = "Sp_IntKnowledge_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", intKnowledgePkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbIntKnowledge aInfo, string prefix)
        { }
    }
}
