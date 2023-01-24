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
    public class EmployeeDisposalDAO : GenericCommonExtensions<TbDisposal>
    {
        public VMEmployeeInfo GetEmpInfo(IDbCommand cmd, string SerialNumber)
        {

            cmd.CommandText = "SP_GetEmployeeInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@SerialNumber", SerialNumber);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            VMEmployeeInfo empInfo=new VMEmployeeInfo ();
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

                                empInfo.Name = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "";
                                empInfo.DateOfBirth = ResDs.Tables[0].Rows[i]["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["DateOfBirth"]).ToString("dd/MM/yyyy") : "";
                                empInfo.EducationType = ResDs.Tables[0].Rows[i]["EducationType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EducationType"].ToString() : "";
                                empInfo.Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "";
                                empInfo.joindate = ResDs.Tables[0].Rows[i]["joindate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["joindate"]).ToString("dd/MM/yyyy") : "";
                                empInfo.FromDate = ResDs.Tables[0].Rows[i]["FromDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["FromDate"]).ToString("dd/MM/yyyy") : "";
                               

                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return empInfo;

        }
        public dynamic SaveEmployeeDisposal(IDbCommand cmd, TbDisposal disposal, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_Disposal_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@EmployeeCode", disposal.EmployeeCode);
                cmd.AddParameter("@DisposalDate", disposal.DisposalDate);
                cmd.AddParameter("@DisposalTypeCode", disposal.DisposalTypeCode);
                cmd.AddParameter("@remark", disposal.Remark);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", disposal.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return disposal;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbDisposal> GetDisposal(IDbCommand cmd, string? DisposalTypeCode = null, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_Disposal_SelectByEmployeeCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@DisposalTypeCode", DisposalTypeCode);
            cmd.AddParameter("@EmployeeCode", EmployeeCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbDisposal> lstDisposals = new List<TbDisposal>();
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
                                TbDisposal disposal = new TbDisposal
                                {
                                    DisposalPkid = ResDs.Tables[0].Rows[i]["DisposalPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["DisposalPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    DisposalDateStr = ResDs.Tables[0].Rows[i]["DisposalDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DisposalDate"].ToString() : "",
                                    DisposalType = ResDs.Tables[0].Rows[i]["DisposalType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DisposalType"].ToString() : "",
                                    DisposalTypeCode = ResDs.Tables[0].Rows[i]["DisposalTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DisposalTypeCode"].ToString() : "",
                                    Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstDisposals.Add(disposal);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstDisposals;

        }
        public void DeleteDisposal(IDbCommand cmd, string EmployeeCode, int userId)
        {

            cmd.CommandText = "Sp_Disposal_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbDisposal aInfo, string prefix)
        { }
    }
}
