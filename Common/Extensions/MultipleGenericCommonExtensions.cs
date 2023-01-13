using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Common.Extensions
{
    public abstract class MultipleGenericCommonExtensions<S, T, U, V>
    {
        #region [Public Method]
        public List<S> GetList(IDbCommand cmd, bool isSP, string aPrefix)
        {
            object result = 0;
            if (isSP) cmd.CommandType = CommandType.StoredProcedure;
            else cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 5000;

            IDataReader l_dbreader = null;
            List<S> l_list = new List<S>();

            try
            {
                cmd.Connection.Open();
                l_dbreader = cmd.ExecuteReader();
                using (l_dbreader)
                {
                    while (l_dbreader.Read())
                    {
                        S l_info = default(S);
                        ReadRecord(ref l_dbreader, ref l_info, aPrefix);
                        l_list.Add(l_info);
                    }
                    while (l_dbreader.NextResult())
                    {
                        while (l_dbreader.Read())
                        {
                            T s_info = default(T);
                            U u_info = default(U);

                            NextReadRecord(ref l_dbreader, l_list, ref s_info, ref u_info, aPrefix);
                        }
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

        public V GetAll(IDbCommand cmd, bool isSP, string aPrefix)
        {
            object result = 0;
            if (isSP) cmd.CommandType = CommandType.StoredProcedure;
            else cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 5000;

            IDataReader l_dbreader = null;
            S l_info = default(S);
            V v_info = default(V);

            try
            {
                cmd.Connection.Open();
                l_dbreader = cmd.ExecuteReader();
                using (l_dbreader)
                {

                    List<S> l_list1 = new List<S>();
                    List<T> l_list2 = new List<T>();
                    List<U> l_list3 = new List<U>();

                    while (l_dbreader.Read())
                    {

                        ReadRecord(ref l_dbreader, ref l_info, aPrefix);
                        l_list1.Add(l_info);
                    }

                    while (l_dbreader.NextResult())
                    {
                        while (l_dbreader.Read())
                        {
                            T s_info = default(T);
                            U u_info = default(U);

                            NextReadRecord(ref l_dbreader, l_list1, ref s_info, ref u_info, aPrefix);
                            if (s_info != null)
                                l_list2.Add(s_info);
                            else if (u_info != null)
                                l_list3.Add(u_info);
                        }
                    }

                    CombineResult(ref v_info, l_list1, l_list2, l_list3);

                }

                return v_info;
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
        protected abstract void ReadRecord(ref IDataReader aDataReader, ref S aInfo, String prefix);
        protected abstract void NextReadRecord(ref IDataReader aDataReader, List<S> list, ref T sInfo, ref U uInfo, String prefix);
        protected abstract void CombineResult(ref V tInof, List<S> list1, List<T> list2, List<U> list3);

        #endregion
    }

    public abstract class MultipleGenericCommonExtensionsModify<S, T, U, V, W>
    {
        #region [Public Method]

        protected S GetByID(IDbCommand cmd, bool isSP, String aPrefix)
        {
            IDataReader l_dbreader = null;
            if (isSP) cmd.CommandType = CommandType.StoredProcedure;
            else cmd.CommandType = CommandType.Text;
            S s_info = default(S);

            try
            {
                cmd.Connection.Open();
                l_dbreader = cmd.ExecuteReader();
                using (l_dbreader)
                {
                    List<T> l_list1 = new List<T>();
                    List<U> l_list2 = new List<U>();
                    List<V> l_list3 = new List<V>();
                    List<W> l_list4 = new List<W>();

                    while (l_dbreader.Read())
                    {
                        ReadRecord(ref l_dbreader, ref s_info, aPrefix);
                    }


                    while (l_dbreader.NextResult())
                    {
                        while (l_dbreader.Read())
                        {
                            T t_info = default(T);
                            U u_info = default(U);
                            V v_info = default(V);
                            W w_info = default(W);

                            NextReadRecordObj(ref l_dbreader, s_info, ref t_info, ref u_info, ref v_info, ref w_info, aPrefix);
                            if (t_info != null)
                                l_list1.Add(t_info);
                            else if (u_info != null)
                                l_list2.Add(u_info);
                            else if (v_info != null)
                                l_list3.Add(v_info);
                            else if (w_info != null)
                                l_list4.Add(w_info);

                        }
                    }

                    CombineResult(ref s_info, l_list1, l_list2, l_list3, l_list4);
                }
                cmd.Connection.Close();
                return s_info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [Abstract Method]
        protected abstract void ReadRecord(ref IDataReader aDataReader, ref S aInfo, String prefix);

        protected abstract void CombineResult(ref S sInfo, List<T> list1, List<U> list2, List<V> list3, List<W> list4);

        protected abstract void NextReadRecordObj(ref IDataReader aDataReader, S aInfo, ref T sInfo, ref U uInfo, ref V vInfo, ref W wInfo, String prefix);
        #endregion


    }
}
