using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupManager
{
    static class Logger
    {
        static string path = "";
        static void writeLog(string path)
        {
            System.IO.File.AppendAllLines("test.txt", new string[] {path});
        }
    }
}
