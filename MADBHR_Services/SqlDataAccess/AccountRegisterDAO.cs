using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
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

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return userLogin;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbUserLogin aInfo, string prefix)
        { }
    }
}
