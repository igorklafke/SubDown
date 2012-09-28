using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using SubDown.Codes;
using System.Text.RegularExpressions;
using System.IO;

namespace SubDown.Downloaders
{
    class Result
    {
        public string name;
        public string id;
    }

    class LegendasTV
    {
        private static string session = "";
        private static int idiomaId = 1;

        public void ThreadlogIn()
        {
            logIn();
        }

        private bool logIn()
        {
            if (session != "")
            {
                return true;
            }

            lock (session)
            {
                string username = Properties.Settings.Default.Usuario;
                string password = Properties.Settings.Default.Senha;
                byte[] ret;

                WebClient client = new WebClient();
                client.Headers.Add("Cookie", "Auth=" + MD5.HashString(password) + "; Login=" + username + ";");

                try
                {
                    ret = client.DownloadData("http://legendas.tv/index.php");
                }
                catch
                {
                    return false;
                }

                if (!ASCIIEncoding.ASCII.GetString(ret).Contains("Editar perfil"))
                {
                    return false;
                }

                string cookies;
                try
                {
                    cookies = client.ResponseHeaders["Set-Cookie"];
                }
                catch
                {
                    return false;
                }

                string pattern = @"PHPSESSID=(.*?);";
                Regex reg = new Regex(pattern);
                Match m = reg.Match(cookies);

                if (m.Groups.Count == 2)
                {
                    session = m.Groups[1].ToString();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        
        public List<Result> SearchSubtitle(string searchString, int pagina = 1)
        {
            List<Result> result = new List<Result>();
            if(!logIn())
                return result;

            string username = Properties.Settings.Default.Usuario;
            string password = Properties.Settings.Default.Senha;

            WebClient client = new WebClient();
            client.Headers.Add("Cookie", "Auth=" + MD5.HashString(password) + "; Login=" + username + "; PHPSESSID=" + session + ";");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] response;

            try {
                response = client.UploadData("http://legendas.tv/index.php?opcao=buscarlegenda&pagina="+pagina.ToString(),
                                           "POST",
                                           Encoding.ASCII.GetBytes(
                                                                    "txtLegenda=" + Uri.EscapeUriString(searchString) + 
                                                                    "&selTipo=1&int_idioma=" + idiomaId.ToString()
                                                                  )
                                          );
            }
            catch
            {
                return result;
            }

            Regex nameRegex = new Regex(@"gpop\('(.*?)','(.*?)','(.*?)','(.*?)','(.*?)','(.*?)','(.*?)','(.*?)','(.*?)'\)");
            Regex idRegex = new Regex(@"abredown\('(.*?)'\)");
            MatchCollection nameMatches = nameRegex.Matches(Encoding.ASCII.GetString(response));
            MatchCollection idMatches = idRegex.Matches(Encoding.ASCII.GetString(response));

            for (int i = 0; i < nameMatches.Count; ++i)
            {
                Result r = new Result();
                r.id = idMatches[i].Groups[1].ToString();
                r.name = nameMatches[i].Groups[3].ToString();
                result.Add(r);
            }

            return result;
        }

        public string DownloadSubtitle(string id)
        {
            if (File.Exists("cache\\" + id + ".rar"))
            {
                return ".rar";
            }
            else if(File.Exists("cache\\" + id + ".zip"))
            {
                return ".zip";
            }
            
            if (!logIn())
                return "";
            
            string username = Properties.Settings.Default.Usuario;
            string password = Properties.Settings.Default.Senha;

            WebClient client = new WebClient();
            client.Headers.Add("Cookie", "Auth=" + MD5.HashString(password) + "; Login=" + username + "; PHPSESSID=" + session + ";");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] ret;
            try
            {
                ret = client.DownloadData("http://legendas.tv/info.php?d=" + id + "&c=1");
            }
            catch
            {
                return "";
            }

            string ext = "";

            if (client.ResponseHeaders["Content-Type"].Contains("rar"))
            {
                ext = ".rar";
            }
            else if (client.ResponseHeaders["Content-Type"].Contains("zip"))
            {
                ext = ".zip";
            }
            else
            {
                Console.WriteLine("Tipo de arquivo invalido: " + client.ResponseHeaders["Content-Type"]);
                return "";
            }

            FileStream file = new FileStream("cache\\" + id + ext, FileMode.CreateNew);
            file.Write(ret, 0, ret.Length);
            file.Close();
            
            return ext;
        }
    }
}
