using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MADBHR_Common.Extensions
{
    public static class DbCommandExtensionMethods
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        public static void AddOutputParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);
        }


        public static object GetValueFromOutputParameter(this IDbCommand command, string name)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;

            return parameter.Value;

        }

        public static bool IsColumnExists(this IDataReader dataReader, string columnName)
        {
            bool retVal = false;

            try
            {

                dataReader.GetSchemaTable().DefaultView.RowFilter = string.Format("ColumnName= '{0}'", columnName);
                if (dataReader.GetSchemaTable().DefaultView.Count > 0)
                {
                    retVal = true;
                }
            }

            catch (Exception ex)
            {

                throw;
            }

            return retVal;
        }
    }
}
