using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace ADO
{
    public class DownloadFiles
    {
        public static void dowanload(string url, string outPath, string fileName)
        {
            string outFile = string.Format("{0}\\{1}", outPath, fileName);

            //valido que exista el repositorio
            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            //valido que exista el archivo
            if (File.Exists(outFile))
                File.Delete(outFile);

            //Descargo el archivo
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent: Other");
                client.DownloadFile(url, outFile);
            }
        }
    }
}
