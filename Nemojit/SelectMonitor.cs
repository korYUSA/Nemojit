using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemojit
{
    public partial class SelectMonitor : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        Recorder RecorderForm;
        public SelectMonitor(Recorder _RecorderForm)
        {
            InitializeComponent();
            RecorderForm = _RecorderForm;
            GetMousePos.Start();
        }

        private void GetMousePos_Tick(object sender, EventArgs e)
        {
            this.Location = Screen.FromPoint(Cursor.Position).Bounds.Location;
            this.Size = Screen.FromPoint(Cursor.Position).Bounds.Size;
        }

        private void SelectMonitor_Click(object sender, EventArgs e)
        {
            if (RecorderForm.prevSelect != 0)
            {
                WritePrivateProfileString("AreaSave", "AreaX", Application.OpenForms["Area"].Left.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaY", Application.OpenForms["Area"].Top.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaW", Application.OpenForms["Area"].Width.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaH", Application.OpenForms["Area"].Height.ToString(), Application.StartupPath + "\\Options.ini");
            }
            Application.OpenForms["Area"].Location = this.Location;
            Application.OpenForms["Area"].Size = this.Size;
            this.Close();
        }
    }
}
