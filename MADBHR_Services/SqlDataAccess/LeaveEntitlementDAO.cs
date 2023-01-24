using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
   public class LeaveEntitlementDAO:GenericCommonExtensions<TbLeaveEntitlement>
    {
        public dynamic SaveLeaveEntitlement(IDbCommand cmd, TbLeaveEntitlement leaveEntitlement, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_LeaveEntitlement_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", leaveEntitlement.LeaveEntitlementPkid);
                cmd.AddParameter("@EmployeeCode", leaveEntitlement.EmployeeCode);
                cmd.AddParameter("@ApprovedDate", leaveEntitlement.ApprovedDate);
                cmd.AddParameter("@ApprovedNo", leaveEntitlement.ApprovedNo);
                cmd.AddParameter("@StartDate", leaveEntitlement.StartDate);
                cmd.AddParameter("@EndDate", leaveEntitlement.EndDate);
                cmd.AddParameter("@Period", leaveEntitlement.Period);
                cmd.AddParameter("@LeaveTypeCode", leaveEntitlement.LeaveTypeCode);           
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", leaveEntitlement.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return leaveEntitlement;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbLeaveEntitlement> GetLeaveEntitlement(IDbCommand cmd, string? EmployeeCode = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_LeaveEntitlement_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@FromDate", FromDate);
            cmd.AddParameter("@ToDate", ToDate);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbLeaveEntitlement> lstLeaveentitlements = new List<TbLeaveEntitlement>();
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
                                TbLeaveEntitlement leaveEntitlement = new TbLeaveEntitlement
                                {
                                    LeaveEntitlementPkid = ResDs.Tables[0].Rows[i]["LeaveEntitlementPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["LeaveEntitlementPkid"]) : 0,
                                    EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                    ApproveDateStr = ResDs.Tables[0].Rows[i]["ApprovedDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ApprovedDate"].ToString() : "",
                                    ApprovedNo = ResDs.Tables[0].Rows[i]["ApprovedNo"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ApprovedNo"].ToString() : "",
                                    StartDateStr= ResDs.Tables[0].Rows[i]["StartDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StartDate"].ToString() : "",
                                    EndDateStr = ResDs.Tables[0].Rows[i]["EndDate"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EndDate"].ToString() : "",
                                    Period = ResDs.Tables[0].Rows[i]["Period"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Period"].ToString() : "",
                                    LeaveTypeCode = ResDs.Tables[0].Rows[i]["LeaveTypeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["LeaveTypeCode"].ToString() : "",
                                    LeaveType = ResDs.Tables[0].Rows[i]["LeaveType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["LeaveType"].ToString() : "",
                                    EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                    CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                };

                                lstLeaveentitlements.Add(leaveEntitlement);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstLeaveentitlements;

        }
        public void DeleteLeaveEntitlement(IDbCommand cmd, int leaveEntitlementPkid, int userId)
        {

            cmd.CommandText = "Sp_LeaveEntitlement_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@LeaveEntitlementPkid", leaveEntitlementPkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbLeaveEntitlement aInfo, string prefix)
        { }
    }
}
