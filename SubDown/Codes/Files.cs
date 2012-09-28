using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace SubDown.Codes
{
    class Files
    {
        static public List<string> getFilesFromFolder(string strFolder)
        {
            List<string> filesList = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(strFolder);
            if (folder.Exists)
            {
                string[] subFolders = Directory.GetDirectories(strFolder);
                foreach (string subFolder in subFolders)
                {
                    List<string> subFiles = getFilesFromFolder(subFolder);

                    foreach (string subFile in subFiles)
                    {
                        filesList.Add(subFile);
                    }
                }

                string[] files = Directory.GetFiles(strFolder);
                foreach (string file in files)
                {
                    if ((Path.GetExtension(file) == ".avi" || Path.GetExtension(file) == ".mp4" || Path.GetExtension(file) == ".mkv") && !File.Exists(Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".srt")))
                    {
                        if (!file.Contains("sample"))
                        {
                            filesList.Add(file);
                        }
                    }
                }
            }

            return filesList;
        }
    }
}
