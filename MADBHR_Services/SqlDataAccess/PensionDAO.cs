using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using MADBHR_Models.Employee;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class PensionDAO:GenericCommonExtensions<TbPension>
    {
        public dynamic SavePension(IDbCommand cmd, TbPension pension, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_Pension_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@pkid", Id);
                cmd.AddParameter("@EmployeeCode", pension.EmployeeCode);
                cmd.AddParameter("@OriginalEmployeeNo", pension.SerialNumber);
                cmd.AddParameter("@Name", pension.Name);
                cmd.AddParameter("@FatherName", pension.FatherName);
                cmd.AddParameter("@RankType", pension.RankType);
                cmd.AddParameter("@Department", pension.Department);
                cmd.AddParameter("@DateOfBirth", pension.DateOfBirth);
                cmd.AddParameter("@PensionReportNo", pension.PensionReportNo);
                cmd.AddParameter("@PensionDate", pension.PensionDate);
                cmd.AddParameter("@PensionType", pension.PensionTypeCode);
                cmd.AddParameter("@LatestSalary", pension.LatestSalary);
                cmd.AddParameter("@MonthlyPension", pension.MonthlyPension);
                cmd.AddParameter("@Saving", pension.Saving);
                cmd.AddParameter("@PensionStartDate", pension.PensionStartDate);
                cmd.AddParameter("@PensionBank", pension.PensionBank);
                cmd.AddParameter("@Remark", pension.Remark);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", pension.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return pension;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbPension> GetPension(IDbCommand cmd, string? StateDivisionCode = null, string? TownshipCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_Pension_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@DivisionCode", StateDivisionCode);
            cmd.AddParameter("@TownshipCode", TownshipCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbPension> lstPension = new List<TbPension>();
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
                                TbPension pension = new TbPension
                                {
                                    PensionPkid = ResDs.Tables[0].Rows[i]["PensionPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["PensionPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    RankTypeCode = ResDs.Tables[0].Rows[i]["RankTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankTypeCode"].ToString() : "",
                                    Name = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    PensionReportNo = ResDs.Tables[0].Rows[i]["PensionReportNo"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PensionReportNo"].ToString() : "",
                                    FatherName = ResDs.Tables[0].Rows[i]["FatherName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FatherName"].ToString() : "",
                                    RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                    Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                    DateOfBirthStr = ResDs.Tables[0].Rows[i]["DateOfBirth"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DateOfBirth"].ToString() : "",
                                    PensionDateStr = ResDs.Tables[0].Rows[i]["PensionDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PensionDate"].ToString() : "",
                                    PensionTypeStr = ResDs.Tables[0].Rows[i]["PensionType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PensionType"].ToString() : "",
                                    PensionTypeCode = ResDs.Tables[0].Rows[i]["PensionTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PensionTypeCode"].ToString() : "",
                                    LatestSalary = ResDs.Tables[0].Rows[i]["LatestSalary"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["LatestSalary"].ToString() : "",
                                    MonthlyPension = ResDs.Tables[0].Rows[i]["MonthlyPension"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["MonthlyPension"].ToString() : "",
                                    Saving = ResDs.Tables[0].Rows[i]["Saving"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Saving"].ToString() : "",
                                    PensionStartDateStr = ResDs.Tables[0].Rows[i]["PensionStartDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PensionStartDate"].ToString() : "",
                                    PensionBank = ResDs.Tables[0].Rows[i]["PensionBank"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["PensionBank"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                    Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                    Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstPension.Add(pension);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstPension;

        }
        public List<VMPensionEmpCount> GetPensionEmployeeCounts(IDbCommand cmd, string? StateDivisionCode = null)
        {
            cmd.CommandText = "SP_GetPensionCount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@StateDivisionCode", StateDivisionCode);
            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<VMPensionEmpCount> emps = new List<VMPensionEmpCount>();
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
                                VMPensionEmpCount employee = new VMPensionEmpCount();

                                employee.StateDivisionCode = ResDs.Tables[0].Rows[i]["StateDivisionCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivisionCode"].ToString() : "";
                                employee.StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "";
                                employee.TownshipCount = ResDs.Tables[0].Rows[i]["TownshipCount"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TownshipCount"]) : 0;
                                employee.EmployeeCount = ResDs.Tables[0].Rows[i]["EmployeeCount"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["EmployeeCount"]) : 0;


                                emps.Add(employee);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return emps;

        }
        public void DeletePension(IDbCommand cmd, int PensionPkid, int userId)
        {

            cmd.CommandText = "Sp_Pension_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", PensionPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbPension aInfo, string prefix)
        { }
    }
}
