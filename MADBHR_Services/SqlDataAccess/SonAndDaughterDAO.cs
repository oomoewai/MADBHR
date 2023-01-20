using MADBHR_Common.Extensions;
using MADBHR_Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Services.SqlDataAccess
{
    public class SonAndDaughterDAO:GenericCommonExtensions<TbSonAndDaughter>
    {
        public dynamic SaveSonAndDaughter(IDbCommand cmd, TbSonAndDaughter sonAndDaughter, int userId,int Id)
        {
            try
            {
                cmd.CommandText = "Sp_SonAndDaughter_Save";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Connection.Open();

                cmd.AddParameter("@id", Id);
                cmd.AddParameter("@pkid", sonAndDaughter.SonAndDaughterPkid);
                cmd.AddParameter("@EmployeeCode", sonAndDaughter.EmployeeCode);
                cmd.AddParameter("@SonAndDaughterName", sonAndDaughter.SonAndDaughterName);
                cmd.AddParameter("@SonAndDaughterDOB", sonAndDaughter.SonAndDaughterDob);
                cmd.AddParameter("@SonAndDaughterOccupation", sonAndDaughter.SonAndDaughterOccupation);
                cmd.AddParameter("@SonAndDaughterAddress", sonAndDaughter.SonAndDaughterAddress);
                cmd.AddParameter("@IsDeleted", false);
                cmd.AddParameter("@CreatedBy", userId);
                cmd.AddParameter("@UploadForTownship", sonAndDaughter.UploadForTownship);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return sonAndDaughter;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public void DeleteSonAndDaughter(IDbCommand cmd, int pkid, int userId)
        {

            cmd.CommandText = "Sp_SonAndDaughter_Delete";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Connection.Open();
            cmd.AddParameter("@pkid", pkid);
            cmd.AddParameter("@CreatedBy", userId);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        protected override void ReadRecord(ref IDataReader aDataReader, ref TbSonAndDaughter aInfo, string prefix)
        { }
    }
}
