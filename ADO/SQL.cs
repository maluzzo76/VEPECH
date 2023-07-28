﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ADO
{
    public class SQL
    {
       
        #region <SQL>

        public static void SqlExecuteNonQuery(string query, string strConexion)
        {
            using (SqlCommand _sqlCommand = new SqlCommand(query, (new SqlConnection(strConexion))))
            {
                _sqlCommand.CommandTimeout = 6000;
                _sqlCommand.Connection.Open();
                _sqlCommand.ExecuteNonQuery();
            }
        }

        public static void SqlExecuteProcedureNonQuery(string procedureName, string strConexion)
        {
            SqlConnection _oconn = new SqlConnection(strConexion);

            try
            {
                using (SqlCommand _sqlCommand = new SqlCommand())
                {
                    _sqlCommand.Connection=_oconn;
                    _sqlCommand.CommandTimeout = 6000;
                    _sqlCommand.Connection.Open();
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = procedureName;
                    _sqlCommand.ExecuteNonQuery();
                    _oconn.Close();
                    _oconn.Dispose();
                }
            }
            catch (Exception ex)
            {
                _oconn.Close();
                _oconn.Dispose();   
                throw ex;
            }
        }

        public static DataSet SqlExecuteQueryDataSet(string query, string strConexion)
        {
            DataSet _ds = new DataSet();
            using (SqlCommand _sqlCommand = new SqlCommand(query, (new SqlConnection(strConexion))))
            {
                _sqlCommand.CommandTimeout = 6000;
                (new SqlDataAdapter(_sqlCommand)).Fill(_ds);
            }

            return _ds;
        }

        public static void SqlBulkCopy(string destinationTableName, DataTable tData, IList<SqlBulkCopyColumnMapping> mappings, string strConexion)
        {
            SqlConnection _sconn = new SqlConnection(strConexion);

            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_sconn))
                {
                    bulkCopy.DestinationTableName = destinationTableName;
                    bulkCopy.BulkCopyTimeout = 2000;
                    bulkCopy.BatchSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["BatchSize"]);
                    foreach (SqlBulkCopyColumnMapping _mapping in mappings)
                    {
                        bulkCopy.ColumnMappings.Add(_mapping);
                    }
                    _sconn.Open();
                    bulkCopy.WriteToServer(tData);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sconn.Close();
                _sconn.Dispose();
                Log.Write.WriteError(String.Format("Registros Importados: {0}",tData.Rows.Count));
            }
        }

        /// <summary>
        /// Elimina todos los registros de la tabla indicada
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="strConexion"></param>
        public static void SqlDeleteAllRows(string tableName, string strConexion)
        {
            string _query = string.Format("delete {0}",tableName );
            using (SqlCommand _sqlCommand = new SqlCommand(_query, (new SqlConnection(strConexion))))
            {
                _sqlCommand.CommandTimeout = 6000;
                _sqlCommand.Connection.Open();
                _sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Elimina todos los registros de la tabla indicada
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="strConexion"></param>
        public static void SqlDeleteRowsToWhere(string tableName,string where, string strConexion)
        {
            string _query = string.Format("delete {0} where {1}", tableName, where);
            using (SqlCommand _sqlCommand = new SqlCommand(_query, (new SqlConnection(strConexion))))
            {
                _sqlCommand.CommandTimeout = 6000;
                _sqlCommand.Connection.Open();
                _sqlCommand.ExecuteNonQuery();
            }
        }

        #endregion

    }
}
