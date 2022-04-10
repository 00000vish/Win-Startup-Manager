using StartupManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupManager_Launcher
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
            startApplications();
        }


        static void startApplications()
        {
            ArrayList collection = Applications.getApps();
            foreach(App item in collection)
            {
                if (item.enabled)
                {
                    processApp(item);
                }
            }
        }

        static void processApp(App item)
        {
            Thread t = new Thread(() =>
            {                
                Process proc = new Process();
                proc.StartInfo.FileName = item.path;
                if (item.hiddenStart)
                    proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                if (item.startDelay.enabled)                                   
                    Thread.Sleep(item.startDelay.getTime());
                if (item.cmd.enabled)
                    proc.StartInfo.Arguments = item.cmd.command;
                while (item.usb)
                {
                    Thread.Sleep(10000);
                }                   
                proc.Start();
                if (item.closeDelay.enabled)
                {                   
                    Thread.Sleep(item.closeDelay.getTime());
                    try
                    {
                        proc.Kill();
                    }
                    catch (Exception)
                    {

                    }                   
                }
            });
            t.Start();
           
        }
    }
}
