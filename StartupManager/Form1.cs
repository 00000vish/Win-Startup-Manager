using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupManager
{
    public partial class Form1 : Form
    {
        
        private bool autoExit = false;

        public Form1()
        {
            InitializeComponent();
            initVaribales();
        }

        private void initVaribales()
        {
            versionToolStripMenuItem.Text = "Version " + Application.ProductVersion;
            autoExit = Properties.Settings.Default.autoExit;
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/isitavailable");
        }

        private void visitGithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/vishwenga/Startup-Manager");
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 add = new Form2();
            add.Show(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Applications.writeToFile();
            Properties.Settings.Default.autoExit = autoExit;
            Properties.Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.writeLog("Closing...!");
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.writeLog("updating...!");
            updateAppList();
        }

        public void updateAppList()
        {
            listView1.Items.Clear();
            foreach (App item in Applications.getApps())
            {
                ListViewItem temp = new ListViewItem(item.enabled.ToString());
                temp.SubItems.Add(item.name);
                temp.SubItems.Add(item.usb.ToString());
                temp.SubItems.Add(item.cmd.enabled.ToString() + " | \"" + item.cmd.command + "\"");
                listView1.Items.Add(temp);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                Form2 temp = new Form2();
                temp.Show(Applications.getApp(index), this);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.removeApp(listView1.SelectedItems[0].Index);
                updateAppList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 temp = new Form2();
            temp.Show(this);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 3000;
                toolTip1.Show(generateTooltipInfo(Applications.getApp(listView1.SelectedItems[0].Index)), listView1);
            }
        }

        private string generateTooltipInfo(App item)
        {
            return "Enabled: " + item.enabled + "\nName: " + item.name + "\nUSB: " + item.usb + "\nHidden: " + item.hiddenStart + "\nStart delay: " + item.startDelay.getTimeString() + "\nClose delay: " + item.closeDelay.getTimeString();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                Form2 temp = new Form2();
                temp.Show(Applications.getApp(index), this);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.removeApp(listView1.SelectedItems[0].Index);
                updateAppList();
            }
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).enabled = true;
                updateAppList();
            }

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).enabled = true;
                updateAppList();
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).enabled = false;
                updateAppList();
            }
        }

        private void disableToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).enabled = false;
                updateAppList();
            }
        }

        private void enabledToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).cmd.enabled = true;
                updateAppList();
            }
        }

        private void disabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).cmd.enabled = false;
                updateAppList();
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).cmd.enabled = true;
                updateAppList();
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.getApp(listView1.SelectedItems[0].Index).cmd.enabled = false;
                updateAppList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Applications.removeApp(listView1.SelectedItems[0].Index);
                updateAppList();
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 add = new Form2();
            add.Show(this);
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openInExplorer();
        }

        private void openInExplorer()
        {
            if (System.IO.File.Exists(Applications.getApp(listView1.SelectedItems[0].Index).path.ToString()))
            {
                System.Diagnostics.Process.Start(Applications.getApp(listView1.SelectedItems[0].Index).path.ToString());
            }
            else
            {
                Logger.writeLog("File does not exist!");
                MessageBox.Show("File does not exist!");
            }
        }

        private void openInExplorerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openInExplorer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //log
            Logger.writeLog("Closing...!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text.Equals("Enable"))
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    Applications.getApp(listView1.SelectedItems[0].Index).enabled = true ;
                    updateAppList();
                }
            }
            else
            {
                if (listView1.SelectedItems.Count > 0)
                {

                    Applications.getApp(listView1.SelectedItems[0].Index).enabled = false;
                    updateAppList();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (Applications.getApp(listView1.SelectedItems[0].Index).enabled)
                {
                    button4.Text = "Disable";
                }
                else
                {
                    button4.Text = "Enable";
                }
            }
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.logging = false;
            loggingToolStripMenuItem.Text = "Logging [Disabled]";
            button3.Visible = false;
        }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.logging = true;
            loggingToolStripMenuItem.Text = "Logging [Enabled]";
            button3.Visible = true;
        }
    }
}
