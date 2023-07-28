using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DS_Process
{
    public class Process
    {
        static string _strConexionMaster = System.Configuration.ConfigurationManager.ConnectionStrings["Master"].ConnectionString;
        static string _strConexionAmerica = System.Configuration.ConfigurationManager.ConnectionStrings["America"].ConnectionString;
        static string _strConexionWHS = System.Configuration.ConfigurationManager.ConnectionStrings["WHS"].ConnectionString;
        static IList<SqlBulkCopyColumnMapping> _mappingsColumns = new List<SqlBulkCopyColumnMapping>();

        public static void ImportarRubros()
        {
            Log.Write.WriteTextInConsole("Importando whs.Rubros");

            //Obtiene los datos de america
            string _QueryAmerica = "select codi, descrip from produc_agru1";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip", "Descripcion"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Rubro", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Rubro", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimRubros", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarClientes()
        {
            Log.Write.WriteTextInConsole("Importando whs.Clientes");

            //Obtiene los datos de america
            string _QueryAmerica = "select codi, nombre, razon_social from clientes";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("nombre", "Nombre"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("razon_social", "Razon_Social"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Clientes", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Clientes", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimClientes", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarDate(object yearFrom)
        {
            Log.Write.WriteTextInConsole("Importando whs.Date");

            //genera las fecha
            DataTable _dtFecha = new DataTable();
            _dtFecha.Columns.Add("codi");
            _dtFecha.Columns.Add("fecha", typeof(DateTime));

            DateTime _startDate = new DateTime(int.Parse(yearFrom.ToString()), 1, 1);
            TimeSpan ts = DateTime.Now.AddMonths(1) - _startDate;



            for (int _index = 1; _index < ts.Days; _index++)
            {
                DataRow _newRow = _dtFecha.NewRow();
                _startDate = _startDate.AddDays(1);
                _newRow[0] = string.Format("{0}{1}{2}", _startDate.Year, _startDate.Month.ToString().PadLeft(2, char.Parse("0")), _startDate.Day.ToString().PadLeft(2, char.Parse("0")));
                _newRow[1] = _startDate;
                _dtFecha.Rows.Add(_newRow);
            }


            try
            {
                _mappingsColumns.Clear();
                _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                _mappingsColumns.Add(new SqlBulkCopyColumnMapping("fecha", "Fecha"));

                //limpia la tabla
                string _where = string.Format("convert(int,substring(codi,0,5)) > {0}", yearFrom.ToString());
                ADO.SQL.SqlDeleteRowsToWhere("stg.Date", _where, _strConexionWHS);

                //Importa los datos de america a stg
                ADO.SQL.SqlBulkCopy("stg.Date", _dtFecha, _mappingsColumns, _strConexionWHS);

                //Actualiza whs
                ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimDate", _strConexionWHS);

            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarGruposEstacionales()
        {
            Log.Write.WriteTextInConsole("Importando whs.GruposEstacionales");

            //Obtiene los datos de america
            string _QueryAmerica = "select  codi,descrip from produc_agru6";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip", "Descripcion"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.GruposEstacionales", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.GruposEstacionales", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimGruposEstacionales", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarGruposNegocio()
        {
            Log.Write.WriteTextInConsole("Importando whs.GruposNegocios");

            //Obtiene los datos de america
            string _QueryAmerica = "select  codi,upper(descrip) descrip from produc_agru4";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip", "Descripcion"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.GruposNegocios", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.GruposNegocios", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimGruposNegocios", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarProductos()
        {
            Log.Write.WriteTextInConsole("Importando whs.Productos");

            //Obtiene los datos de america
            string _QueryAmerica = "select produc, descrip_corta from productos";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("produc", "Codigo"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip_corta", "Descripcion"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Productos", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Productos", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimProductos", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarProveedores()
        {
            Log.Write.WriteTextInConsole("Importando whs.Proveedores");

            //Obtiene los datos de america
            string _QueryAmerica = "select codi , nombre from  proveedores ";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Nombre", "Nombre"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Proveedores", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Proveedores", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimProveedores", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarSegmentos()
        {
            Log.Write.WriteTextInConsole("Importando whs.Segmentos");

            //Obtiene los datos de america
            string _QueryAmerica = "select codi , descrip,codi_orden3 from  produc_agru5";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip", "Nombre"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Codi_orden3", "Codi_orden3"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Segmentos", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Segmentos", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimSegmentos", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarSupervisores()
        {
            Log.Write.WriteTextInConsole("Importando whs.Supervisores");

            //Obtiene los datos de america
            string _QueryAmerica = "select super codi,upper(nombre) descrip from supervisores ";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip", "Nombre"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Supervisores", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Supervisores", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimSupervisores", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarVendedores()
        {
            Log.Write.WriteTextInConsole("Importando whs.Vendedores");

            //Obtiene los datos de america
            string _QueryAmerica = "select codi,upper(nombre) descrip from Vendedores";

            try
            {
                //Importa datos stg
                using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                {
                    _mappingsColumns.Clear();
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("codi", "Codi"));
                    _mappingsColumns.Add(new SqlBulkCopyColumnMapping("descrip", "Nombre"));

                    //limpia la tabla                    
                    ADO.SQL.SqlDeleteAllRows("stg.Vendedores", _strConexionWHS);

                    //Importa los datos de america a stg
                    ADO.SQL.SqlBulkCopy("stg.Vendedores", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    //Actualiza whs
                    ADO.SQL.SqlExecuteProcedureNonQuery("Sp_InsertUpdateDimVendedores", _strConexionWHS);
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarVentas(string yearFrom)
        {
            Log.Write.WriteTextInConsole("Importando whs.Ventas");


            try
            {
                Log.Write.WriteTextInConsole("Limpiando Historial");
                //limpia la tabla
                string _where = string.Format("tipoTransaccion  = 'VENTA'");
                ADO.SQL.SqlDeleteRowsToWhere("[stg].[FactTrasnaction]", _where, _strConexionWHS);

                for (int _year = int.Parse(yearFrom); _year <= DateTime.Now.Year; _year++)
                {
                    Log.Write.WriteTextInConsole(string.Format("Importando Ventas del año {0}", _year));
                    //Obtiene los datos de america
                    string _QueryAmerica = Querys.America.GetQueryImportarVentas(_year);

                    //Importa datos stg
                    using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                    {
                        _mappingsColumns.Clear();
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TipoTrasaccion", "TipoTransaccion"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Fecha_codi", "Date_Id"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("produc", "Producto_Codi"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Prov_codi", "Proveedor"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("cliente_codi", "Cliente"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("vende", "Vendedor"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("super", "Supervisor"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("segmento", "Segmanto"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Rubro", "Rubro"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("GruposEstacionales", "GruposEstacionales"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("GruposNegocio", "GruposNegocios"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TotalVentaKg", "Cantidad_KG"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TotalVentaBU", "Cantidad_BU"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TotalVentaLTS", "Cantidad_LTS"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("ImporteTotal", "Importe"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("factu_nume", "Numero_Factura"));

                        //Importa los datos de america a whs
                        ADO.SQL.SqlBulkCopy("[stg].[FactTrasnaction]", _ds.Tables[0], _mappingsColumns, _strConexionWHS);

                    }
                }


            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarCompras(string yearFrom)
        {
            Log.Write.WriteTextInConsole(string.Format("Importando whs.Compra desde {0}", yearFrom));

            try
            {

                //limpia la tabla
                Log.Write.WriteTextInConsole("Limpiando Historial");
                string _where = string.Format("tipoTransaccion  = 'COMPRA'");
                ADO.SQL.SqlDeleteRowsToWhere("[stg].[FactTrasnaction]", _where, _strConexionWHS);

                for (int _year = int.Parse(yearFrom); _year <= DateTime.Now.Year; _year++)
                {
                    Log.Write.WriteTextInConsole(string.Format("Importando Compras del año {0}", _year));

                    //Obtiene los datos de america
                    string _QueryAmerica = Querys.America.GetQueryImportarCompras(_year);

                    //Importa datos stg
                    using (DataSet _ds = ADO.SQL.SqlExecuteQueryDataSet(_QueryAmerica, _strConexionAmerica))
                    {
                        _mappingsColumns.Clear();
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TipoTrasaccion", "TipoTransaccion"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Fecha_codi", "Date_Id"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("produc", "Producto_Codi"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Prov_codi", "Proveedor"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("cliente_codi", "Cliente"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("vende", "Vendedor"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("super", "Supervisor"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("segmento", "Segmanto"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("Rubro", "Rubro"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("GruposEstacionales", "GruposEstacionales"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("GruposNegocio", "GruposNegocios"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TotalCompraKg", "Cantidad_KG"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TotalCompraBU", "Cantidad_BU"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("TotalCompraLTS", "Cantidad_LTS"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("ImporteTotal", "Importe"));
                        _mappingsColumns.Add(new SqlBulkCopyColumnMapping("factu_nume", "Numero_Factura"));



                        //Importa los datos de america a whs
                        ADO.SQL.SqlBulkCopy("[stg].[FactTrasnaction]", _ds.Tables[0], _mappingsColumns, _strConexionWHS);


                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void ImportarFactTransacciones()
        {
            Log.Write.WriteTextInConsole("Importando whs.Transacciones");

            //Obtiene los datos de america
            //string _QueryAmerica = Querys.America.GetQueryImportarVentas;

            try
            {
                //Actualiza whs
                ADO.SQL.SqlExecuteProcedureNonQuery("SP_InsertUpdateFactTransacciones", _strConexionWHS);
            }
            catch (Exception ex)
            {
                Log.Write.WriteException(ex);
            }
            finally
            {
                Log.Write.WriteTextInConsole("====================================");
            }
        }

        public static void RestoreDB(string bkFolder, string nameDB, string pathbkServer, string folderSQlData)
        {
            Console.WriteLine("Buscando archivo de backup");

            string _dbConnection = _strConexionMaster;
                     

            foreach (var _file in System.IO.Directory.GetFiles(bkFolder, "America_*.bak", System.IO.SearchOption.AllDirectories))
            {
                Console.WriteLine("Iniciando Restore");
                string _BkFile = _file;
                
                string _query = string.Format("RESTORE DATABASE [{1}] FROM  DISK = N'{0}' WITH  RESTRICTED_USER,  FILE = 1, MOVE N'ame_test_datData' TO N'{2}\\{1}.mdf',  MOVE N'ame_test_log' TO N'{2}\\{1}_1.ldf',  NOUNLOAD,  REPLACE,  STATS = 5", _BkFile, nameDB, folderSQlData);
                SqlCommand _sqlcomman = new SqlCommand(_query, new SqlConnection(_dbConnection));
                try
                {
                    _sqlcomman.CommandTimeout = 50000;
                    _sqlcomman.Connection.Open();
                    _sqlcomman.ExecuteNonQuery();
                    Console.WriteLine("Restore Finalizado con exito");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Log.Write.WriteError(e.Message);
                    Console.WriteLine("Error: " + e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                finally
                {
                    _sqlcomman.Connection.Close();
                    _sqlcomman.Dispose();
                }
            }

            Console.WriteLine("Activando data base");
            string _activarDb = string.Format("ALTER DATABASE [{0}] SET MULTI_USER;", nameDB);
            SqlCommand _sqlcomman2 = new SqlCommand(_activarDb, new SqlConnection(_dbConnection));
            try
            {
                _sqlcomman2.CommandTimeout = 50000;
                _sqlcomman2.Connection.Open();
                _sqlcomman2.ExecuteNonQuery();
                Console.WriteLine("Data base activa");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + e.Message);
                Log.Write.WriteError(e.Message);
                Console.ForegroundColor = ConsoleColor.White; 
            }
            finally
            {
                _sqlcomman2.Connection.Close();
                _sqlcomman2.Dispose();
            }
        }

    }
}
