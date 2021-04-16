using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupManager
{
    static class Logger
    {
        public static void writeLog(string data)
        {
            if(Properties.Settings.Default.logging)
                System.IO.File.WriteAllText("log.txt", data);
        }
    }
}
