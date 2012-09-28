using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace SubDown.Codes
{
    class Updater
    {
        public void Initialize()
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.executeUpdate);
            client.DownloadStringAsync(new Uri("http://subdown.oversubs.org/update.txt"));
        }

        public void executeUpdate(object o, DownloadStringCompletedEventArgs e)
        {
            int newVersion = Convert.ToInt32((string)o);
            int currentVersion = 0;

            if (currentVersion < newVersion)
            {
                //download the file and close the program and run updater.exe
            }
        }
    }
}
