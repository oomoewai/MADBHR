using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class JobHistoryDAO:GenericCommonExtensions<TbJobHistory>
    {
        public dynamic SaveJobHistory(IDbCommand cmd, TbJobHistory jobHistory, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_JobPosting_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", jobHistory.JobHistoryPkid);
                cmd.AddParameter("@EmployeeCode", jobHistory.EmployeeCode);
                cmd.AddParameter("@DepartmentName", jobHistory.DepartmentName);
                cmd.AddParameter("@RankType" ,jobHistory.RankTypeCode);
                cmd.AddParameter("@FromDate", jobHistory.FromDate);
                cmd.AddParameter("@ToDate", jobHistory.ToDate);
                cmd.AddParameter("@years", jobHistory.JobYear);
                cmd.AddParameter("@months", jobHistory.JobMonth);
                cmd.AddParameter("@days", jobHistory.JobDay);
                cmd.AddParameter("@Duration", jobHistory.Duration);
                cmd.AddParameter("@Remark", jobHistory.Remark);
                cmd.AddParameter("@IsCurrent", jobHistory.IsCurrent);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", jobHistory.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return jobHistory;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbJobHistory> GetJobHistory(IDbCommand cmd, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_JobPosting_SelectByEmployeeCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@FromDate", FromDate);
            cmd.AddParameter("@ToDate", ToDate);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbJobHistory> lstJobHisotories = new List<TbJobHistory>();
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
                                TbJobHistory jobHistory = new TbJobHistory
                                {
                                    JobHistoryPkid = ResDs.Tables[0].Rows[i]["JobHistoryPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["JobHistoryPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    FromDateStr = ResDs.Tables[0].Rows[i]["FromDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FromDate"].ToString() : "",
                                    ToDateStr = ResDs.Tables[0].Rows[i]["ToDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ToDate"].ToString() : "",
                                    Department_Name = ResDs.Tables[0].Rows[i]["Department_Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department_Name"].ToString() : "",
                                    RankTypeCode = ResDs.Tables[0].Rows[i]["RankType_Code"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType_Code"].ToString() : "",
                                    RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                    Duration = ResDs.Tables[0].Rows[i]["Duration"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["Duration"]) : 0,
                                    IsCurrent = ResDs.Tables[0].Rows[i]["IsCurrent"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsCurrent"]) :false,
                                    Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstJobHisotories.Add(jobHistory);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstJobHisotories;

        }
        public void DeleteJobHistory(IDbCommand cmd, int jobHistoryPkid, int userId)
        {

            cmd.CommandText = "Sp_JobPosting_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@JobPostingPkid", jobHistoryPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbJobHistory aInfo, string prefix)
        { }
    }
}
