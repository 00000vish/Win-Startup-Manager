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
    public partial class Form2 : Form
    {
        App currApp;
        Form1 callBack;
        bool guiComplete = false;

        public Form2()
        {
            InitializeComponent();
        }

        public void Show(Form1 form1)
        {
            callBack = form1;
            currApp = new App();
            currApp.cmd = new CommandLineArg() { enabled = false, command = "" };
            currApp.startDelay = new TimeObject() { enabled = false };
            currApp.closeDelay = new TimeObject() { enabled = false };
            Show();
        }

        public void Show(App myApp, Form1 form1)
        {
            currApp = myApp;
            callBack = form1;
            updateGUI();
            button2.Text = "Save";
            Show();
        }

        private void updateGUI()
        {
            comboBox1.Text = currApp.startDelay.timeTypeString;
            comboBox2.Text = currApp.closeDelay.timeTypeString;
            checkBox5.Checked = currApp.enabled;
            OpenFileDialog OFD = new OpenFileDialog() { FileName = currApp.path };
            label1.Text = OFD.SafeFileName;
            OFD.Dispose();
            checkBox3.Checked = currApp.cmd.enabled;
            textBox2.Text = currApp.cmd.command;
            checkBox1.Checked = currApp.usb;
            checkBox2.Checked = currApp.startDelay.enabled;
            checkBox4.Checked = currApp.closeDelay.enabled;
            checkBox6.Checked = currApp.hiddenStart;
            textBox1.Text = currApp.startDelay.timeValString;
            textBox3.Text = currApp.closeDelay.timeValString;
            checkBox6.Checked = currApp.hiddenStart;
            Update();
            Refresh();
            guiComplete = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                currApp.path = OFD.FileName;
                label1.Text = OFD.SafeFileName;
                currApp.name = OFD.SafeFileName;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                currApp.cmd.enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                currApp.cmd.enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                currApp.usb = true;
            }
            else
            {
                currApp.usb = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                currApp.startDelay.enabled = true;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
            }
            else
            {
                currApp.startDelay.enabled = false;
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                currApp.closeDelay.enabled = true;
                textBox3.Enabled = true;
                comboBox2.Enabled = true;
            }
            else
            {
                currApp.closeDelay.enabled = false;
                textBox3.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!button2.Text.Equals("Save"))
            {
                Applications.add(currApp);
            }
            callBack.updateAppList();
            this.Close();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                currApp.enabled = true;
            }
            else
            {
                currApp.enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            currApp.cmd.command = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (guiComplete)
            {
                int timeOut;
                int.TryParse(textBox1.Text, out timeOut);
                currApp.startDelay.setTime(comboBox1.Text, timeOut);
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (guiComplete)
            {
                int timeOut;
                int.TryParse(textBox3.Text, out timeOut);
                currApp.closeDelay.setTime(comboBox2.Text, timeOut);
            }

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (guiComplete)
            {
                int timeOut;
                int.TryParse(textBox3.Text, out timeOut);
                currApp.startDelay.setTime(comboBox1.Text, timeOut);
            }

        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (guiComplete)
            {
                int timeOut;
                int.TryParse(textBox3.Text, out timeOut);
                currApp.closeDelay.setTime(comboBox2.Text, timeOut);
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                currApp.hiddenStart = true;
            }
            else
            {
                currApp.hiddenStart = false;
            }
        }
    }
}
