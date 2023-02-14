using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class AccountRegisterDAO : GenericCommonExtensions<TbUserLogin>
    {
        public dynamic SaveAccount(IDbCommand cmd, TbUserLogin userLogin, int userId, int Id)
        {
            try
            {
                cmd.CommandText = "Sp_SaveAccount";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();


                cmd.AddParameter("@UserPkid", userLogin.UserPkid);
                cmd.AddParameter("@Name", userLogin.Name);
                cmd.AddParameter("@UsernameOrEmail", userLogin.UsernameOrEmail);
                cmd.AddParameter("@Password", userLogin.Password);
                cmd.AddParameter("@AccountType", userLogin.AccountType);
                cmd.AddParameter("@Department", userLogin.Department);
                cmd.AddParameter("@Office", userLogin.Office);
                cmd.AddParameter("@StateDivisionId", userLogin.StateDivisionId);
                cmd.AddParameter("@TownshipId", userLogin.TownshipId);
                cmd.AddParameter("@Status", userLogin.Status);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return userLogin;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public List<TbUserLogin> GetAccount(IDbCommand cmd, string? StateDivisionCode = null, string? TownshipCode = null,string? UsernameOrEmail=null)
        {

            cmd.CommandText = "SP_GetAccount";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@StateDivisionCode", StateDivisionCode);
            cmd.AddParameter("@TownshipCode", TownshipCode);
            cmd.AddParameter("@UsernameOrEmail", UsernameOrEmail);

            SqlDataAdapter ResAdapter = new SqlDataAdapter((SqlCommand)cmd);
            DataSet ResDs = new DataSet();
            ResAdapter.Fill(ResDs);
            List<TbUserLogin> lstAccounts = new List<TbUserLogin>();
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
                                TbUserLogin account = new TbUserLogin
                                {
                                    UserPkid = ResDs.Tables[0].Rows[i]["UserPkid"] != DBNull.Value ? Convert.ToInt32(ResDs.Tables[0].Rows[i]["UserPkid"]) : 0,
                                    Name = ResDs.Tables[0].Rows[i]["Name"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Name"].ToString() : "",
                                    UsernameOrEmail = ResDs.Tables[0].Rows[i]["UsernameOrEmail"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["UsernameOrEmail"].ToString() : "",
                                    Password = ResDs.Tables[0].Rows[i]["Password"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Password"].ToString() : "",
                                    AccountType = ResDs.Tables[0].Rows[i]["AccountType"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["AccountType"].ToString() : "",
                                    Office = ResDs.Tables[0].Rows[i]["Office"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Office"].ToString() : "",
                                    Department = ResDs.Tables[0].Rows[i]["Department"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Department"].ToString() : "",
                                    StateDivisionId = ResDs.Tables[0].Rows[i]["StateDivisionId"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivisionId"].ToString() : "",
                                    TownshipId = ResDs.Tables[0].Rows[i]["TownshipId"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TownshipId"].ToString() : "",
                                    StateDivision = ResDs.Tables[0].Rows[i]["StateDivision"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["StateDivision"].ToString() : "",
                                    Township = ResDs.Tables[0].Rows[i]["TownName"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["TownName"].ToString() : "",
                                    Status= ResDs.Tables[0].Rows[i]["Status"] != DBNull.Value ? ResDs.Tables[0].Rows[i]["Status"].ToString() : ""
                                };

                                lstAccounts.Add(account);
                            }
                        }
                    }
                }
            }
            cmd.Connection.Close();
            return lstAccounts;

        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbUserLogin aInfo, string prefix)
        { }
    }
}
