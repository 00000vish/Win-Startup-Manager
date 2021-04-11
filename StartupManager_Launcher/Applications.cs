using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupManager
{
    public static class Applications
    {
        private static ArrayList collection;
        private static string path;

        static Applications()
        {
            path = AppDomain.CurrentDomain.BaseDirectory;
            collection = new ArrayList();
            if (!File.Exists(path + "\\data.txt"))
            {
                using (var tw = new StreamWriter(path + "\\data.txt", true))
                {
                    tw.WriteLine("");
                }
            }
            else
            {
                try
                {
                    //var apiJson = new StreamReader(WebRequest.Create("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=006C1D814005AF1CAE4B670EE4B38979&steamid=" + steamId + "&l=english&json").GetResponse().GetResponseStream()).ReadToEnd();
                    var jsonList = JsonConvert.DeserializeObject<Data>(File.ReadAllText(path + "\\data.txt"));
                    //dynamic stuff = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(@"data.txt").ToString().Replace("\"" , "'"));
                    foreach (var item in jsonList.datas)
                    {
                        collection.Add(item);
                    }

                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Test" + e.ToString());
                }
            }
        }

        public static void add(App item)
        {
            collection.Add(item);
        }

        public static App getApp(int index)
        {
            return (App)collection[index];
        }

        public static ArrayList getApps()
        {
            return collection;
        }

        public static void writeToFile()
        {
            collection.TrimToSize();
            Data temp = new Data() { datas = new App[collection.Count] };
            for (int x = 0; x < temp.datas.Length; x++)
            {
                temp.datas[x] = (App)collection[x];
            }
            string json = JsonConvert.SerializeObject(temp);
            File.WriteAllText(path + "data.txt", json);
        }

        public static void removeApp(int index)
        {
            collection.RemoveAt(index);
        }
    }

    public class Data
    {
        public App[] datas { get; set; }
    }

    public class App
    {
        public bool enabled;
        public string name;
        public string path;
        public CommandLineArg cmd { get; set; }
        public bool usb { get; set; }
        public TimeObject startDelay { get; set; }
        public TimeObject closeDelay { get; set; }
        public bool hiddenStart { get; set; }
    }

    public class CommandLineArg
    {
        public bool enabled { get; set; }
        public string command { get; set; }
    }

    public class TimeObject
    {
        public bool enabled { get; set; }
        public int time;
        public string timeValString;
        public string timeTypeString;

        public void setTime(string type, int val)
        {
            System.Windows.Forms.MessageBox.Show(type);
            timeValString = val + "";
            timeTypeString = type + "";
            switch (type)
            {
                case "seconds": time = val * 1000; break;
                case "minutes": time = val * 60000; break;
                case "hours": time = val * 3600000; break;
                default: time = 0; break;
            }
        }

        public int getTime()
        {
            return time;
        }

        internal string getTimeString()
        {
            return timeValString + " " + timeTypeString;
        }
    }
}
