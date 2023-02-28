using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
   public class PunishmentDAO : GenericCommonExtensions<TbPunishment>
    {
        public dynamic SavePunishment(IDbCommand cmd, TbPunishment award, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_Punishment_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", award.PunishmentPkid);
                cmd.AddParameter("@EmployeeCode", award.EmployeeCode);
                cmd.AddParameter("@OrderDate", award.OrderDate);
                cmd.AddParameter("@OrderNo", award.OrderNo);
                cmd.AddParameter("@CrimeYear", award.CrimeYear);
                cmd.AddParameter("@PunishmentTypeCode", award.PunishmentTypeCode);
                cmd.AddParameter("@Description", award.Description);
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
        public List<TbPunishment> GetPunishment(IDbCommand cmd, string? PunishmentTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_Punishment_SelectByEmployeeCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@PunishmentTypeCode", PunishmentTypeCode);
            cmd.AddParameter("@EmployeeCode", EmployeeCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbPunishment> lstPunishments = new List<TbPunishment>();
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
                                TbPunishment award = new TbPunishment
                                {
                                    PunishmentPkid = ResDs.Tables[0].Rows[i]["PunishmentPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["PunishmentPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    OrderDateStr = ResDs.Tables[0].Rows[i]["OrderDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OrderDate"].ToString() : "",
                                    OrderNo = ResDs.Tables[0].Rows[i]["OrderNo"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OrderNo"].ToString() : "",
                                    CrimeYear = ResDs.Tables[0].Rows[i]["CrimeYear"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CrimeYear"].ToString() : "",
                                    Description = ResDs.Tables[0].Rows[i]["Description"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Description"].ToString() : "",
                                    PunishmentTypeCode = ResDs.Tables[0].Rows[i]["PunishmentTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PunishmentTypeCode"].ToString() : "",
                                    PunishmentType = ResDs.Tables[0].Rows[i]["PunishmentType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PunishmentType"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                    RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstPunishments.Add(award);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstPunishments;

        }
        public List<TbPunishment> GetPunishmentForAdmin(IDbCommand cmd, string? StateDivisionCode = null, string? TownshipCode = null, string? Name = null, string? SerialNumber = null)
        {

            cmd.CommandText = "SP_GetPunishmentForAdmin";
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
            List<TbPunishment> lstPunishments = new List<TbPunishment>();
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
                                if (lstEmpCode.Count > 0)
                                {
                                    if (!lstEmpCode.Contains(ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString()))
                                    {
                                        TbPunishment award = new TbPunishment
                                        {
                                            PunishmentPkid = ResDs.Tables[0].Rows[i]["PunishmentPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["PunishmentPkid"]) : 0,
                                            EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                            OrderDateStr = ResDs.Tables[0].Rows[i]["OrderDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OrderDate"].ToString() : "",
                                            OrderNo = ResDs.Tables[0].Rows[i]["OrderNo"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OrderNo"].ToString() : "",
                                            CrimeYear = ResDs.Tables[0].Rows[i]["CrimeYear"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CrimeYear"].ToString() : "",
                                            Description = ResDs.Tables[0].Rows[i]["Description"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Description"].ToString() : "",
                                            PunishmentTypeCode = ResDs.Tables[0].Rows[i]["PunishmentTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PunishmentTypeCode"].ToString() : "",
                                            PunishmentType = ResDs.Tables[0].Rows[i]["PunishmentType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PunishmentType"].ToString() : "",
                                            StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                            Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                            EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                            Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                            RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                            IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                            CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                            CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                        };
                                        lstEmpCode.Add(award.EmployeeCode);
                                        lstPunishments.Add(award);
                                    }
                                    
                                }
                                else
                                {
                                    TbPunishment award = new TbPunishment
                                    {
                                        PunishmentPkid = ResDs.Tables[0].Rows[i]["PunishmentPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["PunishmentPkid"]) : 0,
                                        EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                        OrderDateStr = ResDs.Tables[0].Rows[i]["OrderDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OrderDate"].ToString() : "",
                                        OrderNo = ResDs.Tables[0].Rows[i]["OrderNo"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["OrderNo"].ToString() : "",
                                        CrimeYear = ResDs.Tables[0].Rows[i]["CrimeYear"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["CrimeYear"].ToString() : "",
                                        Description = ResDs.Tables[0].Rows[i]["Description"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Description"].ToString() : "",
                                        PunishmentTypeCode = ResDs.Tables[0].Rows[i]["PunishmentTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PunishmentTypeCode"].ToString() : "",
                                        PunishmentType = ResDs.Tables[0].Rows[i]["PunishmentType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PunishmentType"].ToString() : "",
                                        StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                        Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                        EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                        Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                        RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                        IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                        CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                        CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                    };
                                    lstEmpCode.Add(award.EmployeeCode);
                                    lstPunishments.Add(award);
                                }
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstPunishments;

        }
        public void DeletePunishment(IDbCommand cmd, int punishmentPkid, int userId)
        {

            cmd.CommandText = "Sp_Punishment_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", punishmentPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbPunishment aInfo, string prefix)
        { }
    }
}
