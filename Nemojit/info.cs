using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemojit
{
    public partial class info : Form
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public info()
        {
            InitializeComponent();
            StringBuilder Theme = new StringBuilder(255);
            GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
            button1.BackColor = ColorTranslator.FromHtml(Theme.ToString());
            button2.BackColor = ColorTranslator.FromHtml(Theme.ToString());
            button3.BackColor = ColorTranslator.FromHtml(Theme.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("https://nemojit.github.io/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://nemojit.github.io/contact");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://nemojit.github.io/donate");
        }
    }
}
