using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace SubDown.Codes
{
    class Release
    {
        public bool bluray = false; //same as brrip?
        public bool dvd = false;
        public bool hdtv = false;
        public bool xvid = false;
        public bool x264 = false;
        public bool dts = false;
        public bool ac3 = false;
        public bool r720 = false;
        public bool r1080 = false;
        public bool repack = false;

        public string title = "";
        public string group = "";

        public int episode = 0;
        public int season = 0;

        public string fileName = "";
        //public string md5hash = "";

        public Release(string fileName, bool filtraNome = false, string md5hash = "")
        {
            this.fileName = this.cleanFileName(fileName);
            this.bluray = this.fileName.Contains("bluray");
            if(!bluray)
                this.bluray = this.fileName.Contains("brrip");

            this.r720 = this.fileName.Contains("720");
            this.r1080 = this.fileName.Contains("1080");

            this.dvd = this.fileName.Contains("dvd");
            this.hdtv = this.fileName.Contains("hdtv");
            this.xvid = this.fileName.Contains("xvid");
            this.x264 = this.fileName.Contains("x264");
            this.dts = this.fileName.Contains("dts");
            this.ac3 = this.fileName.Contains("ac3");
            this.repack = this.fileName.Contains("repack");

            if (this.fileName.Contains("-"))
            {
                string[] split = this.fileName.Split('-');
                this.group = split[split.Length - 1];
            }

            string filtredName;
            if (filtraNome)
                filtredName = this.filterFileName(this.fileName);
            else if (this.group != "")
                filtredName = this.fileName.Replace(this.group, "");
            else
                filtredName = this.fileName;


            string pattern = @"s(\d?\d)e(\d?\d)";
            Regex reg = new Regex(pattern);
            Match m = reg.Match(filtredName);

            if (m.Groups.Count >= 3)
            {
                this.season = Convert.ToInt32(m.Groups[1].ToString());
                this.episode = Convert.ToInt32(m.Groups[2].ToString());

                filtredName = Regex.Replace(filtredName, pattern, "").Trim();
            }

            this.title = filtredName;
        }

        private string cleanFileName(string file)
        {
            file = file.Replace(";", " ");
            file = file.Replace(":", " ");
            file = file.Replace(".", " ");
            file = file.Replace("_", " ");
            file = file.Replace(" / ", "/");
            file = file.Replace(" /", "/");
            file = file.Replace("/  ", "/");
            return file.ToLower();
        }

        private string filterFileName(string file)
        {
            if (this.group != "")
            {
                file = file.Replace(this.group, "");
            }

            file = file.Replace("bluray", "");
            file = file.Replace("brip", "");
            file = file.Replace("720p", "");
            file = file.Replace("1080p", "");
            file = file.Replace("720", "");
            file = file.Replace("1080", "");
            file = file.Replace("dvdrip", "");
            file = file.Replace("dvd", "");
            file = file.Replace("hdtv", "");
            file = file.Replace("xvid", "");
            file = file.Replace("x264", "");
            file = file.Replace("dts", "");
            file = file.Replace("ac3", "");
            file = file.Replace("web-dl", "");
            file = file.Replace("aac2 0", "");
            file = file.Replace("dd5 1", "");
            file = file.Replace("-es", "");
            file = file.Replace("h 264", "");
            file = file.Replace("h264", "");
            file = file.Replace("proper", "");
            file = file.Replace("repack", "");
            file = file.Replace("(", "");
            file = file.Replace(")", "");
            file = file.Replace("[", "");
            file = file.Replace("]", "");
            file = file.Replace("-", "");
            file = file.Replace("  ", "");
            return file.Trim();
        }

        public string getEpisode()
        {
            if(this.season > 0 && this.episode >= 0)
                return "s"+String.Format("{0:00}", this.season)+"e"+String.Format("{0:00}", this.episode);

            return "";
        }

        public string getSeason()
        {
            if(this.season > 0)
                return "s"+String.Format("{0:00}", this.season);
            return "";
        }

        public string print()
        {
            return this.title + "|" + this.episode + "|" + this.season + "|" + this.group;
        }

        public bool Compare(Release other)
        {
            if (this.repack == other.repack && other.title == this.title && other.episode == this.episode && other.season == this.season)
            {
                if (this.CompareGroup(other.group))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CompareGroup(string otherGroups)
        {
            string[] groups = this.group.Split('/');
            string[] other = otherGroups.Split('/');

            foreach (string g in groups)
            {
                foreach (string o in other)
                {
                    if (g.Trim() == o.Trim() || (this.r720 && o == "720p"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
