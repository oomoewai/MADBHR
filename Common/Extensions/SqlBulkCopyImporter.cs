//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Text;

//namespace Common.Extensions
//{
//    public class SqlBulkCopyImporter
//    {
//        protected const int BatchSize = 100000;

//        public SqlBulkCopyImporter()
//        {
//        }
//        public void LoadIntoSqlServer<T>(Func<SqlConnection, DataTable> dataTableFunc,
//            Func<SqlConnection, SqlBulkCopy> sqlBulkCreateFunc,
//            Action<SqlConnection> preImportQuery,
//            IEnumerator<T> collectionEnumerator,
//            Func<DataTable, T, DataRow> constructRowFunc, SqlConnection sqlConnection)
//        {

//            var createdCount = 0;

//            var dataTable = dataTableFunc(sqlConnection);

//            preImportQuery(sqlConnection);

//            var sqlBulkCopy = sqlBulkCreateFunc(sqlConnection);

//            // Loop through the CSV and load each set of 100,000 records into a DataTable
//            // Then send it to the staging Table

//            while (collectionEnumerator.MoveNext())
//            {
//                dataTable.Rows.Add(constructRowFunc(dataTable, collectionEnumerator.Current));

//                createdCount++;

//                if (createdCount % BatchSize == 0)
//                {
//                    InsertDataTable(sqlBulkCopy, sqlConnection, dataTable);
//                }
//            }

//            // Don't forget to send the last batch under 100,000
//            InsertDataTable(sqlBulkCopy, sqlConnection, dataTable);

//        }

//        protected void InsertDataTable(SqlBulkCopy sqlBulkCopy, SqlConnection sqlConnection, DataTable dataTable)
//        {
//            sqlBulkCopy.BulkCopyTimeout = 300; // wait for 5 mins

//            sqlBulkCopy.WriteToServer(dataTable);

//            dataTable.Rows.Clear();
//        }

//        protected void MergeTables(string spName, string connectionString)
//        {
//            using (var conn = new SqlConnection(connectionString))
//            {
//                conn.Open();

//                var cmd = new SqlCommand(spName, conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };

//                cmd.ExecuteNonQuery();
//            }
//        }
//    }
//}
