using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using SharpCompress.Reader;

namespace SubDown.Codes
{
    class Compressed
    {
        public static string[] List(string ext, string file)
        {
            string ret = "";

            Stream stream = File.OpenRead(file);
            var reader = ReaderFactory.Open(stream);
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    if (reader.Entry.FilePath != "")
                    {
                        ret += reader.Entry.FilePath + "\n";
                    }
                }
            }
            stream.Close();

            return ret.Split('\n');
        }

        public static void Extract(string file, string fileToExtract, string destFile)
        {
            Stream stream = File.OpenRead(file);
            var reader = ReaderFactory.Open(stream);
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    if (reader.Entry.FilePath == fileToExtract)
                    {
                        reader.WriteEntryToFile(destFile);
                    }
                }
            }
            stream.Close();
        }
    }
}
