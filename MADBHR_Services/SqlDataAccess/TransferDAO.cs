using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class TransferDAO : GenericCommonExtensions<TbTransfer>
    {
        public dynamic SaveTransfer(IDbCommand cmd, TbTransfer transfer, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "SP_SaveTransfer";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

               
                cmd.AddParameter("@TransferPkid", transfer.TransferPkid);
                cmd.AddParameter("@EmployeeCode", transfer.EmployeeCode);
                cmd.AddParameter("@RankTypeCode", transfer.RankTypeCode);
                cmd.AddParameter("@FromTownshipCode", transfer.FromTownshipCode);
                cmd.AddParameter("@ToTownshipCode", transfer.ToTownshipCode);
                cmd.AddParameter("@TransferDate", transfer.TransferDate);
                cmd.AddParameter("@Remark", transfer.Remark);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return transfer;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbTransfer> GetTransferForAdmin(IDbCommand cmd, string? StateDivisionCode = null, string? TownshipCode = null)
        {

            cmd.CommandText = "SP_GetTransferForAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@DivisionCode", StateDivisionCode);
            cmd.AddParameter("@TownshipCode", TownshipCode);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbTransfer> lstTransfers = new List<TbTransfer>();
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
                                        TbTransfer transfer = new TbTransfer
                                        {
                                            TransferPkid = ResDs.Tables[0].Rows[i]["TransferPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TransferPkid"]) : 0,
                                            EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                            TransferDateStr = ResDs.Tables[0].Rows[i]["TransferDateStr"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TransferDateStr"].ToString() : "",
                                            FromTownship = ResDs.Tables[0].Rows[i]["FromTownship"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FromTownship"].ToString() : "",
                                            ToTownship = ResDs.Tables[0].Rows[i]["ToTownship"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ToTownship"].ToString() : "",
                                            EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                            RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                            StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                            Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                            IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                            CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                            CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                        };
                                        lstEmpCode.Add(transfer.EmployeeCode);
                                        lstTransfers.Add(transfer);
                                    }
                                }
                                else
                                {
                                    TbTransfer transfer = new TbTransfer
                                    {
                                        TransferPkid = ResDs.Tables[0].Rows[i]["TransferPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TransferPkid"]) : 0,
                                        EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                        TransferDateStr = ResDs.Tables[0].Rows[i]["TransferDateStr"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TransferDateStr"].ToString() : "",
                                        FromTownship = ResDs.Tables[0].Rows[i]["FromTownship"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FromTownship"].ToString() : "",
                                        ToTownship = ResDs.Tables[0].Rows[i]["ToTownship"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ToTownship"].ToString() : "",
                                        EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                        RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                        StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                        Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                        IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                        CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                        CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0
                                    };
                                    lstEmpCode.Add(transfer.EmployeeCode);
                                    lstTransfers.Add(transfer);
                                }
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstTransfers;
        }
        public List<TbTransfer> GetTransferForDetail(IDbCommand cmd, string EmployeeCode, string? FromTownshipCode = null, string? ToTownshipCode = null,int? TransferPkid=null)
        {

            cmd.CommandText = "SP_GetTransferForDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@EmployeeCode", EmployeeCode);
            cmd.AddParameter("@FromTownshipCode", FromTownshipCode);
            cmd.AddParameter("@ToTownshipCode", ToTownshipCode);
            cmd.AddParameter("@TransferPkid", TransferPkid);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbTransfer> lstTransfers = new List<TbTransfer>();
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

                                        TbTransfer transfer = new TbTransfer
                                        {
                                            TransferPkid = ResDs.Tables[0].Rows[i]["TransferPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["TransferPkid"]) : 0,
                                            EmployeeCode = ResDs.Tables[0].Rows[i]["EmployeeCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["EmployeeCode"].ToString() : "",
                                            TransferDateStr = ResDs.Tables[0].Rows[i]["TransferDateStr"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TransferDateStr"].ToString() : "",
                                            FromTownship = ResDs.Tables[0].Rows[i]["FromTownship"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FromTownship"].ToString() : "",
                                            ToTownship = ResDs.Tables[0].Rows[i]["ToTownship"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ToTownship"].ToString() : "",
                                            EmployeeName = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                            RankType = ResDs.Tables[0].Rows[i]["RankType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["RankType"].ToString() : "",
                                            StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                            Township = ResDs.Tables[0].Rows[i]["Township"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Township"].ToString() : "",
                                            IsDeleted = ResDs.Tables[0].Rows[i]["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(ResDs.Tables[0].Rows[i]["IsDeleted"]) : false,
                                            CreatedDate = ResDs.Tables[0].Rows[i]["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["CreatedDate"]) : DateTime.Now,
                                            TransferDate = ResDs.Tables[0].Rows[i]["TransferDate"] != DBNull.Value ? Convert.ToDateTime(ResDs.Tables[0].Rows[i]["TransferDate"]) : DateTime.Now,
                                            CreatedBy = ResDs.Tables[0].Rows[i]["CreatedBy"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["CreatedBy"]) : 0,
                                            Remark = ResDs.Tables[0].Rows[i]["Remark"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Remark"].ToString() : "",
                                            FromTownshipCode = ResDs.Tables[0].Rows[i]["FromTownshipCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["FromTownshipCode"].ToString() : "",
                                            ToTownshipCode = ResDs.Tables[0].Rows[i]["ToTownshipCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["ToTownshipCode"].ToString() : "",
                                        };                                       
                                        lstTransfers.Add(transfer);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstTransfers;
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbTransfer aInfo, string prefix)
        { }
    }
}
