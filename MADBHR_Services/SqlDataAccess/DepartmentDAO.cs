using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class DepartmentDAO:GenericCommonExtensions<TbDepartment>
    {
        public dynamic SaveDepartment(IDbCommand cmd, TbDepartment department, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_Department_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();
                cmd.AddParameter("@DepartmentPkid", department.DepartmentPkid);
                cmd.AddParameter("@Department", department.Department);
                cmd.AddParameter("@DepartmentCode", department.DepartmentCode);



                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return department;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbDepartment> GetDepartment(IDbCommand cmd, string? Department = null, DateTime? FromDate = null, DateTime? ToDate = null)
        {

            cmd.CommandText = "Sp_Department_Select";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@Department", Department);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbDepartment> lstDepartments = new List<TbDepartment>();
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
                                TbDepartment department = new TbDepartment
                                {
                                    DepartmentPkid = ResDs.Tables[0].Rows[i]["DepartmentPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["DepartmentPkid"]) : 0,
                                    Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                    DepartmentCode = ResDs.Tables[0].Rows[i]["DepartmentCode"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["DepartmentCode"].ToString() : "",
                                };

                                lstDepartments.Add(department);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstDepartments;

        }
        public void DeleteDepartment(IDbCommand cmd, string departmentCode)
        {

            cmd.CommandText = "Sp_Department_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@DepartmentCode", departmentCode);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbDepartment aInfo, string prefix)
        { }
    }
}
