using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class AwardDAO : GenericCommonExtensions<TbAward>
    {
        public dynamic SaveAward(IDbCommand cmd, TbAward award, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_Award_Insert";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", award.AwardPkid);
                cmd.AddParameter("@EmployeeCode", award.EmployeeCode);
                cmd.AddParameter("@AwardDate", award.AwardDate);
                cmd.AddParameter("@AwardYear", award.AwardYear);
                cmd.AddParameter("@AwardTitle", award.AwardTypeCode);
                cmd.AddParameter("@Reason", award.Reason);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", award.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return award;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbAward> GetAward(IDbCommand cmd, string? AwardTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_Award_SelectByEmployeeCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@AwardTypeCode", AwardTypeCode);
            cmd.AddParameter("@EmployeeCode", EmployeeCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbAward> lstAwards = new List<TbAward>();
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
                                TbAward award = new TbAward
                                {
                                    AwardPkid = ResDs.Tables[0].Rows[i]["AwardPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["AwardPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    AwardDateStr = ResDs.Tables[0].Rows[i]["AwardDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardDate"].ToString() : "",
                                    AwardYear = ResDs.Tables[0].Rows[i]["AwardYear"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardYear"].ToString() : "",
                                    AwardType = ResDs.Tables[0].Rows[i]["AwardType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardType"].ToString() : "",
                                    Reason = ResDs.Tables[0].Rows[i]["Reason"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Reason"].ToString() : "",
                                    AwardTypeCode = ResDs.Tables[0].Rows[i]["AwardTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardTypeCode"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                    Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstAwards.Add(award);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstAwards;

        }
        public List<TbAward> GetAwardForAdmin(IDbCommand cmd, string? StateDivisionCode = null, string? TownshipCode = null, string? Name = null, string? SerialNumber = null)
        {

            cmd.CommandText = "SP_GetAwardForAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@DivisionCode", StateDivisionCode);
            cmd.AddParameter("@TownshipCode", TownshipCode);
            cmd.AddParameter("@Name", Name);
            cmd.AddParameter("@SerialNumber", SerialNumber);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbAward> lstAwards = new List<TbAward>();
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
                                if (lstAwards.Count > 0)
                                {
                                    if (!lstEmpCode.Contains(ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString()))
                                    {
                                        TbAward award = new TbAward
                                        {
                                            AwardPkid = ResDs.Tables[0].Rows[i]["AwardPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["AwardPkid"]) : 0,
                                            EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                            AwardDateStr = ResDs.Tables[0].Rows[i]["AwardDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardDate"].ToString() : "",
                                            AwardYear = ResDs.Tables[0].Rows[i]["AwardYear"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardYear"].ToString() : "",
                                            AwardType = ResDs.Tables[0].Rows[i]["AwardType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardType"].ToString() : "",
                                            Reason = ResDs.Tables[0].Rows[i]["Reason"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Reason"].ToString() : "",
                                            AwardTypeCode = ResDs.Tables[0].Rows[i]["AwardTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardTypeCode"].ToString() : "",
                                            StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                            Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                            EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                            RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                            Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                            IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                            CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                            CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                        };
                                        lstEmpCode.Add(award.EmployeeCode);
                                        lstAwards.Add(award);
                                    }
                                }
                                else
                                {
                                    TbAward award = new TbAward
                                    {
                                        AwardPkid = ResDs.Tables[0].Rows[i]["AwardPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["AwardPkid"]) : 0,
                                        EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                        AwardDateStr = ResDs.Tables[0].Rows[i]["AwardDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardDate"].ToString() : "",
                                        AwardYear = ResDs.Tables[0].Rows[i]["AwardYear"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardYear"].ToString() : "",
                                        AwardType = ResDs.Tables[0].Rows[i]["AwardType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardType"].ToString() : "",
                                        Reason = ResDs.Tables[0].Rows[i]["Reason"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Reason"].ToString() : "",
                                        AwardTypeCode = ResDs.Tables[0].Rows[i]["AwardTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AwardTypeCode"].ToString() : "",
                                        StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                        Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                        EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                        RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                        Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                        IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                        CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                        CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                    };
                                    lstEmpCode.Add(award.EmployeeCode);
                                    lstAwards.Add(award);
                                }
                                
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstAwards;

        }
        public void DeleteAward(IDbCommand cmd, int awardPkid, int userId)
        {

            cmd.CommandText = "Sp_Award_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", awardPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbAward aInfo, string prefix)
        { }
    }
}
