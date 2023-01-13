using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace MADBHR_Common.Extensions
{
    public class CommonHelperExtensions
    {

        public static Int32 getOneValue_Int32(IDbCommand cmd, bool isSP)
        {
            Int32 result;
            if (isSP) cmd.CommandType = CommandType.StoredProcedure;
            else cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 5000;

            try
            {
                cmd.Connection.Open();
                Object obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    result = Convert.ToInt32(obj);
                else
                    result = 0;
            }
            catch (Exception ex)
            {
                result = 0;
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return result;
        }

        public static string MD5Hash(dynamic input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
