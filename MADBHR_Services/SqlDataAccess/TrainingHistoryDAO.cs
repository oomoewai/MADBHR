using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class TrainingHistoryDAO:GenericCommonExtensions<TbTrainingHistory>
    {
        public dynamic SaveTrainingHistory(IDbCommand cmd, TbTrainingHistory history, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_TrainingHistory_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", history.TrainingHistoryPkid);
                cmd.AddParameter("@EmployeeCode", history.EmployeeCode);
                cmd.AddParameter("@StartDate", history.StartDate);
                cmd.AddParameter("@EndDate", history.EndDate);
                cmd.AddParameter("@TrainingTitle", history.TrainingTitle);
                cmd.AddParameter("@SchoolName", history.SchoolName);
                cmd.AddParameter("@Location", history.Location);
                cmd.AddParameter("@Remark", history.Remark);
                cmd.AddParameter("@IsDeleted",false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", history.UploadForTownship);
                cmd.AddParameter("@CertificatePic", history.CertificatePic);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return history;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbTrainingHistory> GetTrainingHistory(IDbCommand cmd, string? SchoolName = null, string? EmployeeCode=null,DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_TrainingHistory_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@SchoolName", SchoolName);
            cmd.AddParameter("@EmployeeCode", EmployeeCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbTrainingHistory> lstHistory = new List<TbTrainingHistory>();
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
                                TbTrainingHistory history = new TbTrainingHistory
                                {
                                    TrainingHistoryPkid = ResDs.Tables[0].Rows[i]["TrainingHistoryPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TrainingHistoryPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    SchoolName = ResDs.Tables[0].Rows[i]["SchoolName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SchoolName"].ToString() : "",
                                    TrainingTitle = ResDs.Tables[0].Rows[i]["TrainingTitle"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TrainingTitle"].ToString() : "",
                                    Location = ResDs.Tables[0].Rows[i]["Location"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Location"].ToString() : "",
                                    Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                    StartDateStr = ResDs.Tables[0].Rows[i]["StartDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StartDate"].ToString() : "",
                                    EndDateStr = ResDs.Tables[0].Rows[i]["EndDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EndDate"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                    Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                    Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                    RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstHistory.Add(history);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstHistory;

        }
        public List<TbTrainingHistory> GetTrainingHistoryForAdmin(IDbCommand cmd, string? StateDivisionCode = null, string? TownshipCode = null, string? SerialNumber = null, string Name = null)
        {

            cmd.CommandText = "Sp_GetTrainingHistoryForAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@DivisionCode", StateDivisionCode);
            cmd.AddParameter("@TownshipCode", TownshipCode);
            cmd.AddParameter("@SerialNumber", SerialNumber);
            cmd.AddParameter("@Name", Name);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbTrainingHistory> lstHistory = new List<TbTrainingHistory>();
            List<string> lstEmpCode = new List<string>();
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
                                if(lstEmpCode .Count >0)
                                {
                                    if(!lstEmpCode.Contains(ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString()))
                                    {
                                        TbTrainingHistory history = new TbTrainingHistory
                                        {
                                            TrainingHistoryPkid = ResDs.Tables[0].Rows[i]["TrainingHistoryPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TrainingHistoryPkid"]) : 0,
                                            EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                            SchoolName = ResDs.Tables[0].Rows[i]["SchoolName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SchoolName"].ToString() : "",
                                            TrainingTitle = ResDs.Tables[0].Rows[i]["TrainingTitle"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TrainingTitle"].ToString() : "",
                                            Location = ResDs.Tables[0].Rows[i]["Location"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Location"].ToString() : "",
                                            Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                            StartDateStr = ResDs.Tables[0].Rows[i]["StartDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StartDate"].ToString() : "",
                                            EndDateStr = ResDs.Tables[0].Rows[i]["EndDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EndDate"].ToString() : "",
                                            EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                            Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                            RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                            StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                            Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                            IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                            CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                            CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                        };
                                        lstEmpCode.Add(history.EmployeeCode);
                                        lstHistory.Add(history);
                                    }
                                }
                                else
                                {
                                    TbTrainingHistory history = new TbTrainingHistory
                                    {
                                        TrainingHistoryPkid = ResDs.Tables[0].Rows[i]["TrainingHistoryPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TrainingHistoryPkid"]) : 0,
                                        EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                        SchoolName = ResDs.Tables[0].Rows[i]["SchoolName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["SchoolName"].ToString() : "",
                                        TrainingTitle = ResDs.Tables[0].Rows[i]["TrainingTitle"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TrainingTitle"].ToString() : "",
                                        Location = ResDs.Tables[0].Rows[i]["Location"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Location"].ToString() : "",
                                        Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                        StartDateStr = ResDs.Tables[0].Rows[i]["StartDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StartDate"].ToString() : "",
                                        EndDateStr = ResDs.Tables[0].Rows[i]["EndDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EndDate"].ToString() : "",
                                        EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                        Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                        RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                        StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                        Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                        IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                        CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                        CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                    };
                                    lstEmpCode.Add(history.EmployeeCode);
                                    lstHistory.Add(history);
                                }
                                
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstHistory;

        }
        public void DeleteHistory(IDbCommand cmd, int trianingHistoryPkid, int userId)
        {

            cmd.CommandText = "Sp_TrainingHistory_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", trianingHistoryPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbTrainingHistory aInfo, string prefix)
        { }
    }
}
