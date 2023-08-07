using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace CMD_TestServices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SettingConsole();

         
            
            Console.Title = "Alusoft - Data Import";
            DateTime _dtStart = DateTime.Now;

            
          

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Log.Write.WriteTextInConsole("====================================");
            Log.Write.WriteTextInConsole("======= ALUSOFT DATA IMPORT ========");
            Log.Write.WriteTextInConsole("====================================");

            Console.ForegroundColor = ConsoleColor.White;

            Log.Write.WriteTextInConsole("====================================");
            Log.Write.WriteTextInConsole("======== EXTRACT ZIP BACKUP ========");
            Log.Write.WriteTextInConsole("====================================");

            DescomprimirZip(GetAppConfigValue("PathFileBK"), GetAppConfigValue("PathFileBKout"), GetAppConfigValue("PathFileBKProcess"));

            Log.Write.WriteTextInConsole("====================================");
            Log.Write.WriteTextInConsole("======== RESTORE DB AMERICA ========");
            Log.Write.WriteTextInConsole("====================================");

            DS_Process.Process.RestoreDB(GetAppConfigValue("PathFileBKout"), GetAppConfigValue("BkDbName"), GetAppConfigValue("PathFileBKServer"), GetAppConfigValue("PathSQLData"));
                        
            
            Log.Write.WriteTextInConsole("====================================");
            Log.Write.WriteTextInConsole("===== IMPORT DATA FROM AMERICA =====");
            Log.Write.WriteTextInConsole("====================================");

            //importar comprea venta
            ImportCompraVenta();

            //importar Usuarios
            ImportarUsuarios();

            DateTime _dtEnd = DateTime.Now;
            TimeSpan ts = _dtEnd - _dtStart;

            Console.ForegroundColor = ConsoleColor.Green;
            Log.Write.WriteTextInConsole("====================================");
            Log.Write.WriteTextInConsole("::Finalizado");
            Log.Write.WriteTextInConsole(string.Format("::Duracion en minutos del proceso: {0}", decimal.Round((decimal)ts.TotalMinutes, 2)));
            Log.Write.WriteTextInConsole("====================================");
            Log.Write.WriteSoloFile(string.Format("::Duracion en minutos del proceso: {0}", decimal.Round((decimal)ts.TotalMinutes, 2)));
            Log.Write.WriteSoloFile("");
            Console.ForegroundColor = ConsoleColor.White;
           // Console.ReadLine();
        }

        static void ImportCompraVenta()
        {

            DS_Process.Process.ImportarRubros();
            DS_Process.Process.ImportarClientes();
            DS_Process.Process.ImportarDate(GetAppConfigValue("YearFrom"));
            DS_Process.Process.ImportarGruposEstacionales();
            DS_Process.Process.ImportarGruposNegocio();
            DS_Process.Process.ImportarProductos();
            DS_Process.Process.ImportarProveedores();
            DS_Process.Process.ImportarSegmentos();
            DS_Process.Process.ImportarSupervisores();
            DS_Process.Process.ImportarVendedores();
            DS_Process.Process.ImportarVentas(GetAppConfigValue("YearFrom"));
            DS_Process.Process.ImportarCompras(GetAppConfigValue("YearFrom"));
            DS_Process.Process.ImportarFactTransacciones();
        }

        static void ImportarUsuarios()
        {
            
        }

        static void DescomprimirZip(string FolderZip, string folderOut, string folderProcess)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Log.Write.WriteTextInConsole("Extrayendo Backup Diario");
                Log.Write.WriteSoloFile("Extrayendo Backup Diario");
                //limpia folder out
                if (Directory.Exists(folderOut))
                    Directory.Delete(folderOut,true);

                if (!Directory.Exists(folderOut))
                    Directory.CreateDirectory(folderOut);

                foreach (string _file in Directory.GetFiles(FolderZip))
                {
                    DateTime _dateBk = DateTime.Now.AddDays(-1);
                    FileInfo _fi = new FileInfo(_file);
                    if (_fi.CreationTime.Date == _dateBk.Date )
                    {
                        Log.Write.WriteTextInConsole(_file);

                        if (!Directory.Exists(folderOut))
                            Directory.CreateDirectory(folderOut);

                        ZipFile.ExtractToDirectory(_file, folderOut);
                        Log.Write.WriteTextInConsole(string.Format("Backup descomprimido exitosamente."));
                        Log.Write.WriteSoloFile("Backup descomprimido exitosamente.");
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(Exception ex)
            {
                Log.Write.WriteError(ex.Message);
                throw;
            }

        }

        static void SettingConsole()
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;    
        }

        static string GetAppConfigValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}
