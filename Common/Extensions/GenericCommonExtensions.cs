using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Common.Extensions
{
    public abstract class GenericCommonExtensions<T>
    {
        #region [Public Method]
        public T GetByID(IDbCommand cmd, bool isSP, string aPrefix)
        {
            object result = 0;
            if (isSP) cmd.CommandType = CommandType.StoredProcedure;
            else cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 5000;

            IDataReader l_dbreader = null;
            T l_info = default(T);

            try
            {
                cmd.Connection.Open();
                l_dbreader = cmd.ExecuteReader();
                using (l_dbreader)
                {
                    while (l_dbreader.Read())
                    {

                        ReadRecord(ref l_dbreader, ref l_info, aPrefix);

                    }

                }

                return l_info;
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
        }

        public List<T> GetList(IDbCommand cmd, bool isSP, string aPrefix)
        {
            object result = 0;
            if (isSP) cmd.CommandType = CommandType.StoredProcedure;
            else cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 500;

            IDataReader l_dbreader = null;
            List<T> l_list = new List<T>();
            try
            {
                cmd.Connection.Open();
                l_dbreader = cmd.ExecuteReader();
                using (l_dbreader)
                {
                    while (l_dbreader.Read())
                    {
                        T l_info = default(T);
                        ReadRecord(ref l_dbreader, ref l_info, aPrefix);
                        l_list.Add(l_info);
                    }

                }
                return l_list;
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
        }
        #endregion

        #region [Abstract Method]
        protected abstract void ReadRecord(ref IDataReader aDataReader, ref T aInfo, String prefix);
        #endregion
    }
}
