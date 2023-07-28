using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace ADO
{
    public class AccessDB
    {

        public static DataTable getData()
        {
            DataTable dt = new DataTable();
            string cadena = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\maria\Documents\Fuentes\DISAM\Documentos\CronosXXI.mdb";
            string cadena_com = "select * from personas";
            OleDbConnection con = new OleDbConnection(cadena);
            OleDbCommand com = new OleDbCommand(cadena_com, con);

            con.Open();

            (new OleDbDataAdapter(com)).Fill(dt);

            
            return dt;
        }

    }
}
