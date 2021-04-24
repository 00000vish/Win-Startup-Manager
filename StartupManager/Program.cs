using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            setupRegKey();
            Application.Run(new Form1());
        }

        private static void setupRegKey()
        {
            Microsoft.Win32.RegistryKey regKey = default(Microsoft.Win32.RegistryKey);
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (regKey.GetValue("Startup Manager") == null)
            {
                try
                {
                    string KeyName = "Startup Manager";
                    string KeyValue = AppDomain.CurrentDomain.BaseDirectory + "StartupManager_Launcher.exe";
                    regKey.SetValue(KeyName, KeyValue, Microsoft.Win32.RegistryValueKind.String);
                }
                catch (Exception) { }
            }
            else
            {
                //regKey.DeleteValue("Startup Manager");
            }
            regKey.Close();
        }
    }
}
