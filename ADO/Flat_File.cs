using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ADO
{
    public class Flat_File
    {
        public static DataSet getCsvData(string fileName)
        {
            DataSet _ds = new DataSet();

            string _directory = (new System.IO.FileInfo(fileName)).DirectoryName;
            string _file = (new System.IO.FileInfo(fileName)).Name;

            String ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=\"Text;CHARSET=utf8\"", _directory);

            DataTable Table = new DataTable();

            using (OleDbConnection Connection = new OleDbConnection(ConnectionString))
            {
                Connection.Open();

                using (OleDbCommand Command = Connection.CreateCommand())
                {
                    Command.CommandText = string.Format("select * from [{0}]", _file);

                    using (OleDbDataAdapter Adapter = new OleDbDataAdapter(Command))
                    {
                        Adapter.Fill(_ds);
                    }
                }
            }

            return _ds;
        }

        public static void crearArhivoIni(string filename, string prostectPath)
        {
            string _file = string.Format("{0}\\schema.ini", prostectPath);
            string _fileName = (new FileInfo(filename)).Name;
            
            if (File.Exists(_file))
                File.Delete(_file);

            using (StreamWriter sw = File.CreateText(_file))
            {
                sw.WriteLine(string.Format("[{0}]", _fileName));
                sw.WriteLine("Format=Delimited(;)");
                sw.WriteLine("CharacterSet = 1252");
                sw.WriteLine("CurrencyDecimalSymbol =.");
                sw.WriteLine("DecimalSymbol =.");
            }
        }
    }
}
