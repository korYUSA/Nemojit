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
    public partial class Area : Form
    {
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        Recorder RecorderForm;
        public Area(Recorder _RecorderForm)
        {
            RecorderForm = _RecorderForm;
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            panel1.Left = 4;
            panel1.Width = this.Width - 8;
            panel1.Height = this.Height - 30;
            Title.Left = 4;
            Title.Width = this.Width - 8;
            Title.Height = 21;
            this.Left = (Screen.PrimaryScreen.Bounds.Width / 2 - 314);
            this.Top = (Screen.PrimaryScreen.Bounds.Height / 2 - 225);

            while (Title.Height < System.Windows.Forms.TextRenderer.MeasureText(Title.Text, new Font(Title.Font.FontFamily, Title.Font.Size, Title.Font.Style)).Height)
            {
                Title.Font = new Font(Title.Font.FontFamily, Title.Font.Size - 0.5f, Title.Font.Style);
            }

            StringBuilder RememberPos = new StringBuilder(255);
            GetPrivateProfileString("General", "RememberPos", "", RememberPos, 255, Application.StartupPath + "\\Options.ini");
            if (RememberPos.ToString() == "1")
            {
                StringBuilder savedX = new StringBuilder(255);
                StringBuilder savedY = new StringBuilder(255);
                StringBuilder savedW = new StringBuilder(255);
                StringBuilder savedH = new StringBuilder(255);
                GetPrivateProfileString("AreaSave", "AreaX", "", savedX, 255, Application.StartupPath + "\\Options.ini");
                GetPrivateProfileString("AreaSave", "AreaY", "", savedY, 255, Application.StartupPath + "\\Options.ini");
                GetPrivateProfileString("AreaSave", "AreaW", "", savedW, 255, Application.StartupPath + "\\Options.ini");
                GetPrivateProfileString("AreaSave", "AreaH", "", savedH, 255, Application.StartupPath + "\\Options.ini");
                this.Width = Convert.ToInt32(savedW.ToString());
                this.Height = Convert.ToInt32(savedH.ToString());
                this.Left = Convert.ToInt32(savedX.ToString());
                this.Top = Convert.ToInt32(savedY.ToString());
            }

            StringBuilder Theme = new StringBuilder(255);
            GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
            this.BackColor = ColorTranslator.FromHtml(Theme.ToString());
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 12; // you can rename this variable if you like

        Rectangle rTop { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle rLeft { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle rBottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle rRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }
        Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == (int)0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (rTop.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (rLeft.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (rRight.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (rBottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }

        Point MousePos;

        private void Area_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Application.OpenForms["SelectWindow"].Close();
            }
            catch { }
            MousePos = e.Location;
            return;
        }

        private void Area_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left = this.Left - MousePos.X + e.X;
                this.Top = this.Top - MousePos.Y + e.Y;
            }
            return;
        }

        private void Area_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.OpenForms["Recorder"].Close();
                return;
            }catch{ }
        }

        bool isMonitorSize = false;
        bool isWindowSize = false;

        private void Area_Resize(object sender, EventArgs e)
        {
            panel1.Width = this.Width - 8;
            panel1.Height = this.Height - 29;
            Title.Width = this.Width - 8;
            Title.Text = "네모짓 : 영역 지정 [" + (this.Width - 8) + "x" + (this.Height - 24) + "]";
            Area_Move(sender, e);
            /*
            if (RecorderForm.RecordType.SelectedIndex == 0)
            {
                Title.Text = "네모짓 : 영역 지정 [모니터 전체], 모니터 전체 녹화 시엔 영역 지정 틀의 테두리 위치까지 녹화 범위에 포함됩니다.";
                isMonitorSize = true;
            }
            if (RecorderForm.RecordType.SelectedIndex == 1)
            {
                Title.Text = "네모짓 : 영역 지정 [창 - " + (this.Width - 8) + "x" + (this.Height - 24) + "] 창 녹화 시엔 영역 지정 틀의 테두리 위치까지 녹화 범위에 포함됩니다.";
                isWindowSize = true;
            }
            */
            try
            {
                WritePrivateProfileString("AreaSave", "AreaX", Application.OpenForms["Area"].Left.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaY", Application.OpenForms["Area"].Top.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaW", Application.OpenForms["Area"].Width.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaH", Application.OpenForms["Area"].Height.ToString(), Application.StartupPath + "\\Options.ini");
            }
            catch { }
        }



        private void Area_Move(object sender, EventArgs e)
        {
            if (RecorderForm.RecordType.SelectedIndex == 0)
            {
                Title.Text = "네모짓 : 영역 지정 [모니터 전체], 모니터 전체 녹화 시엔 영역 지정 틀의 테두리 위치까지 녹화 범위에 포함됩니다.";
                isMonitorSize = true;
            }
            if (RecorderForm.RecordType.SelectedIndex == 1)
            {
                Title.Text = "네모짓 : 영역 지정 [창 - " + (this.Width - 8) + "x" + (this.Height - 24) + "] 창 녹화 시엔 영역 지정 틀의 테두리 위치까지 녹화 범위에 포함됩니다.";
                isWindowSize = true;
            }

            if (isMonitorSize == true)
            {
                if (RecorderForm.RecordType.SelectedIndex == 1)
                {
                    isMonitorSize = false;
                    return;
                }
                if ((Cursor.Position.Y <= this.Top + 20 && Cursor.Position.Y >= this.Top) && (Cursor.Position.X >= this.Left && Cursor.Position.X <= this.Right)
                   || (Cursor.Position.Y >= this.Top + 20 && Cursor.Position.Y <= this.Bottom && Cursor.Position.X >= this.Left && Cursor.Position.X <= this.Left + 4)
                   || (Cursor.Position.Y >= this.Top + 20 && Cursor.Position.Y <= this.Bottom && Cursor.Position.X >= this.Right - 4 && Cursor.Position.X <= this.Right)
                   || (Cursor.Position.Y >= this.Bottom - 4 && Cursor.Position.Y <= this.Bottom && Cursor.Position.X >= this.Left && Cursor.Position.X <= this.Right))
                {
                    Title.Text = "네모짓 : 영역 지정 [" + (this.Width - 8) + "x" + (this.Height - 24) + "]";
                    //StringBuilder savedX = new StringBuilder(255);
                    //StringBuilder savedY = new StringBuilder(255);
                    StringBuilder savedW = new StringBuilder(255);
                    StringBuilder savedH = new StringBuilder(255);
                    //GetPrivateProfileString("AreaSave", "AreaX", "", savedX, 255, Application.StartupPath + "\\Options.ini");
                    //GetPrivateProfileString("AreaSave", "AreaY", "", savedY, 255, Application.StartupPath + "\\Options.ini");
                    GetPrivateProfileString("AreaSave", "AreaW", "", savedW, 255, Application.StartupPath + "\\Options.ini");
                    GetPrivateProfileString("AreaSave", "AreaH", "", savedH, 255, Application.StartupPath + "\\Options.ini");
                    this.Width = Convert.ToInt32(savedW.ToString());
                    this.Height = Convert.ToInt32(savedH.ToString());
                    this.Left = Cursor.Position.X - this.Width / 2;
                    MousePos = new Point(this.Width / 2, 0);
                    RecorderForm.RecordType.SelectedIndex = 2;
                    isMonitorSize = false;
                }
                
            }
            else if (isWindowSize == true)
            {
                if (RecorderForm.RecordType.SelectedIndex == 0)
                {
                    isWindowSize = false;
                    return;
                }

                if ((Cursor.Position.Y <= this.Top + 20 && Cursor.Position.Y >= this.Top) && (Cursor.Position.X >= this.Left && Cursor.Position.X <= this.Right)
                    || (Cursor.Position.Y >= this.Top + 20 && Cursor.Position.Y <= this.Bottom && Cursor.Position.X >= this.Left && Cursor.Position.X <= this.Left + 4)
                    || (Cursor.Position.Y >= this.Top + 20 && Cursor.Position.Y <= this.Bottom && Cursor.Position.X >= this.Right - 4 && Cursor.Position.X <= this.Right)
                    || (Cursor.Position.Y >= this.Bottom - 4 && Cursor.Position.Y <= this.Bottom && Cursor.Position.X >= this.Left && Cursor.Position.X <= this.Right))
                {
                    Title.Text = "네모짓 : 영역 지정 [" + (this.Width - 8) + "x" + (this.Height - 24) + "]";
                    RecorderForm.RecordType.SelectedIndex = 2;
                    isWindowSize = false;
                }
            }

            try
            {
                WritePrivateProfileString("AreaSave", "AreaX", Application.OpenForms["Area"].Left.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaY", Application.OpenForms["Area"].Top.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaW", Application.OpenForms["Area"].Width.ToString(), Application.StartupPath + "\\Options.ini");
                WritePrivateProfileString("AreaSave", "AreaH", Application.OpenForms["Area"].Height.ToString(), Application.StartupPath + "\\Options.ini");
            }
            catch { }
        }

    }
}