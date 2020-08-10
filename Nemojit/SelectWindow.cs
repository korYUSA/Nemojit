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
    public partial class SelectWindow : Form
    {
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        public enum GetAncestorFlags
        {
            GetParent = 1,
            GetRoot = 2,
            GetRootOwner = 3
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("user32")]
        public static extern IntPtr WindowFromPoint(POINT pt);
        [DllImport("user32")]
        public static extern int GetWindowRect(int hwnd, ref RECT lpRect);
        [DllImport("user32.dll")]
        public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags gaFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("dwmapi.dll")]
        static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        public SelectWindow()
        {
            InitializeComponent();
            GetMousePos.Start();
        }

        private void GetMousePos_Tick(object sender, EventArgs e)
        {
            POINT pt;
            pt.x = Cursor.Position.X;
            pt.y = Cursor.Position.Y;
            IntPtr hwnd = WindowFromPoint(pt);
            RECT stRect = default(RECT);
            if (hwnd == this.Handle)
                return;

            IntPtr rootHandle = GetAncestor(hwnd, GetAncestorFlags.GetRoot);

            if (GetWindowTitle(rootHandle).ToString() == "")
                return;

            GetWindowRect((int)rootHandle, ref stRect);
            RECT r = new RECT();

            DwmGetWindowAttribute(rootHandle, 9, out r, Marshal.SizeOf(typeof(RECT)));

            this.Left = r.left;
            this.Top = r.top;
            this.Width = r.right - r.left;
            this.Height = r.bottom - r.top;
        }

        private void SelectWindow_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("AreaSave", "AreaX", Application.OpenForms["Area"].Left.ToString(), Application.StartupPath + "\\Options.ini");
            WritePrivateProfileString("AreaSave", "AreaY", Application.OpenForms["Area"].Top.ToString(), Application.StartupPath + "\\Options.ini");
            WritePrivateProfileString("AreaSave", "AreaW", Application.OpenForms["Area"].Width.ToString(), Application.StartupPath + "\\Options.ini");
            WritePrivateProfileString("AreaSave", "AreaH", Application.OpenForms["Area"].Height.ToString(), Application.StartupPath + "\\Options.ini");
            Application.OpenForms["Area"].Location = this.Location;
            Application.OpenForms["Area"].Size = this.Size;
            this.Close();
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd) + 1;
            var title = new StringBuilder(length);
            GetWindowText(hWnd, title, length);
            return title.ToString();
        }
    }
}
