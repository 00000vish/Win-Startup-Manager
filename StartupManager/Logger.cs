using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupManager
{
    static class Logger
    {
        public static void writeLog(string data)
        {
            if(Properties.Settings.Default.logging)
                System.IO.File.WriteAllText("log.txt", data);
        }

        internal static void openLogFile()
        {
            if (Properties.Settings.Default.logging)
            {
                if (System.IO.File.Exists("log.txt"))
                {
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);
                }
                else
                {
                    writeLog("Log file missing...");
                    MessageBox.Show("Log file missing... Making new one!");
                }
            }
               
        }
    }
}
