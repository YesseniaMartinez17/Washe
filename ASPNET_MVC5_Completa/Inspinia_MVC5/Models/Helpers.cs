using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Inspinia_MVC5.Models
{
    public class Helpers
    {
        WashEEntities db = new WashEEntities();


        public string publicarArchivo(HttpPostedFileBase file, string path, string FileName)
        {
            string formateada = "";
            try
            {
                if (Directory.Exists(path))
                {

                }
                else
                {
                    Directory.CreateDirectory(path);
                }
                //Attribute[] ext = System.IO.File.GetAttributes("C:/Users/Axel Omar/Documents/Visual Studio 2017/Projects/Subirarchivos/Subirarchivos/Archivos/dir20200217_115120.jpg");
                string name = path + FileName;
                file.SaveAs(name);
                //string rformat = name.Replace(@"\","//"); usar el arroba antes de la pleca
                string rformat = name.Replace(@"\", "/");
                int pos = rformat.IndexOf("Content");
                formateada = rformat.Substring(pos);
                //rformatCut = "~/" + rformatCut;
                formateada = "/" + formateada;


                //}
                //else
                //{
                //    file.SaveAs(path + Path.GetFileName(file.FileName));
                //}
            }
            catch (Exception ex)
            {
                return "Error" + ex.Message;
            }
            return formateada;
        }

        public bool removerArchivo(string file)
        {
            System.IO.File.Delete(file);
            return true;
        }

        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-7)).DateTime;
            return dt;
        }
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }
}