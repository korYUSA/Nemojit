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
using System.Diagnostics;
using System.IO;
using System.Media;

namespace Nemojit
{
    public partial class Main : Form
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32")]
        public static extern IntPtr GetForegroundWindow();
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
        [DllImport("KERNEL32.DLL")]
        extern public static void Beep(int freq, int dur);

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

        public Main()
        {
            InitializeComponent();

            StringBuilder Theme = new StringBuilder(255);
            GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
            OpenFolder.FlatAppearance.BorderColor = ColorTranslator.FromHtml(Theme.ToString());
            RecordSelect.BackColor = ColorTranslator.FromHtml(Theme.ToString());
            Settings.FlatAppearance.BorderColor = ColorTranslator.FromHtml(Theme.ToString());
            Info.BackColor = ColorTranslator.FromHtml(Theme.ToString());

            this.NemojitTrayicon.Visible = true;
        }

        public void Hotkey_Reload(int first, int second, int third)
        {
            if (first == 1)
            {
                StringBuilder Key1Main = new StringBuilder(255);
                StringBuilder Key1Sub = new StringBuilder(255);
                GetPrivateProfileString("HotkeySave", "Key1Main", "", Key1Main, 255, Application.StartupPath + "\\Options.ini");
                GetPrivateProfileString("HotkeySave", "Key1Sub", "", Key1Sub, 255, Application.StartupPath + "\\Options.ini");
                RegisterHotKey(this.Handle, 0, Convert.ToInt32(Key1Sub.ToString()), Convert.ToInt32(Key1Main.ToString()));
            }
            if (second == 1)
            {
                StringBuilder Key2Main = new StringBuilder(255);
                StringBuilder Key2Sub = new StringBuilder(255);
                GetPrivateProfileString("HotkeySave", "Key2Main", "", Key2Main, 255, Application.StartupPath + "\\Options.ini");
                GetPrivateProfileString("HotkeySave", "Key2Sub", "", Key2Sub, 255, Application.StartupPath + "\\Options.ini");
                RegisterHotKey(this.Handle, 1, Convert.ToInt32(Key2Sub.ToString()), Convert.ToInt32(Key2Main.ToString()));
            }
            if (third == 1)
            {
                StringBuilder Key3Main = new StringBuilder(255);
                StringBuilder Key3Sub = new StringBuilder(255);
                GetPrivateProfileString("HotkeySave", "Key3Main", "", Key3Main, 255, Application.StartupPath + "\\Options.ini");
                GetPrivateProfileString("HotkeySave", "Key3Sub", "", Key3Sub, 255, Application.StartupPath + "\\Options.ini");
                RegisterHotKey(this.Handle, 2, Convert.ToInt32(Key3Sub.ToString()), Convert.ToInt32(Key3Main.ToString()));
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Hotkey_Reload(1, 1, 1);
        }

        protected override void WndProc(ref Message m) //윈도우프로시저 콜백함수
        {
            base.WndProc(ref m);

            if (m.Msg == (int)0x312)
            {
                if (m.WParam == (IntPtr)0x0)
                {
                    if (checkForm("Recorder") == false)
                        return;
                    if (checkForm("Options") == true)
                        return;
                    if (RecorderForm.isRecording == false)
                    {
                        UnregisterHotKey(this.Handle, 1);
                        UnregisterHotKey(this.Handle, 2);
                        RecorderForm.Record.Text = "*";
                        RecorderForm.Start();
                        RecorderForm.Record.Text = "▪";
                        RecorderForm.Exit.Visible = false;

                        RecorderForm.RecordType.Visible = false;
                        RecorderForm.RecordFrame.Visible = false;
                        RecorderForm.TimeText.Visible = true;
                    }
                    else
                    {
                        Hotkey_Reload(0, 1, 1);
                        RecorderForm.Stop();
                    }

                }
                if (m.WParam == (IntPtr)0x1)
                {
                    if (checkForm("Options") == true)
                        return;
                    if (isRecording == false)
                    {
                        UnregisterHotKey(this.Handle, 0);
                        UnregisterHotKey(this.Handle, 2);
                        Start("ActiveWindow");
                    }
                    else
                    {
                        Hotkey_Reload(1, 0, 1);
                        Stop();
                    }
                }
                if (m.WParam == (IntPtr)0x2)
                {
                    if (checkForm("Options") == true)
                        return;
                    if (isRecording == false)
                    {
                        UnregisterHotKey(this.Handle, 0);
                        UnregisterHotKey(this.Handle, 1);
                        Start("PrimaryScreen");
                    }
                    else
                    {
                        Hotkey_Reload(1, 1, 0);
                        Stop();
                    }
                }
            }
        }
        Recorder RecorderForm;
        private void RecordSelect_Click(object sender, EventArgs e)
        {
            //Record();

            if (checkForm("Recorder") == true)
                return;
            RecorderForm = new Recorder(this);
            RecorderForm.Show();
            Area AreaForm = new Area(RecorderForm);
            AreaForm.Show();
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            StringBuilder SavePath = new StringBuilder(255);
            GetPrivateProfileString("Save", "SavePath", "", SavePath, 255, Application.StartupPath + "\\Options.ini");
            Process.Start(SavePath + "\\");
        }

        private void Info_Click(object sender, EventArgs e)
        {
            info info = new info();
            info.Show();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Options options = new Options(this);
            options.Show();
            this.Hide();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            StringBuilder CloseTray = new StringBuilder(255);
            GetPrivateProfileString("General", "CloseTray", "", CloseTray, 255, Application.StartupPath + "\\Options.ini");
            if (CloseTray.ToString() == "1")
            {
                NemojitTrayicon.Visible = true;
                NemojitTrayicon.BalloonTipText = "네모짓이 여전히 실행 중이며, 단축키를 이용할 수 있습니다.";
                NemojitTrayicon.BalloonTipTitle = "네모짓이 실행 중입니다.";
                NemojitTrayicon.ShowBalloonTip(3);
                e.Cancel = true;
                this.Hide();
                return;
            }
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.ToUpper().StartsWith("ffmpeg-Nemojit"))
                {
                    process.Kill();
                }
            }
        }

        private void tray_exit_Click(object sender, EventArgs e)
        {
            this.FormClosing -= Main_FormClosing;
            Application.Exit();
        }

        private void tray_openFolder_Click(object sender, EventArgs e)
        {
            StringBuilder SavePath = new StringBuilder(255);
            GetPrivateProfileString("Save", "SavePath", "", SavePath, 255, Application.StartupPath + "\\Options.ini");
            Process.Start(SavePath + "\\");
        }

        private void tray_rec_Click(object sender, EventArgs e)
        {
            if (checkForm("Recorder") == true)
            {
                if (RecorderForm.isRecording == false)
                {
                    RecorderForm.isRecording = true;
                    RecorderForm.Record.Text = "*";
                    RecorderForm.Start();
                    RecorderForm.Record.Text = "▪";
                    RecorderForm.Exit.Visible = false;

                    RecorderForm.RecordType.Visible = false;
                    RecorderForm.RecordFrame.Visible = false;
                    RecorderForm.TimeText.Visible = true;
                }
                else
                {
                    RecorderForm.Stop();
                }
                return;
            }
            if (isRecording == true)
            {
                this.Stop();
                return;
            }
            if (checkForm("Options") == true)
                return;

            RecorderForm = new Recorder(this);
            RecorderForm.Show();
            Area AreaForm = new Area(RecorderForm);
            AreaForm.Show();
        }

        private void tray_mainShow_Click(object sender, EventArgs e)
        {
            if (checkForm("Recorder") == true)
                return;
            this.Show();
            this.Activate();
        }

        private void NemojitTrayicon_DoubleClick(object sender, EventArgs e)
        {
            if (checkForm("Recorder") == true)
                return;
            this.Show();
            this.Activate();
        }

        bool checkForm(string FormName)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == FormName)
                    return true;
            }
            return false;
        }

        public void NemojitTrayicon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (NemojitTrayicon.BalloonTipTitle == "네모짓이 실행 중입니다.")
                return;
            StringBuilder SavePath = new StringBuilder(255);
            GetPrivateProfileString("Save", "SavePath", "", SavePath, 255, Application.StartupPath + "\\Options.ini");
            Process.Start(SavePath + "\\");
        }

        ProcessStartInfo processInfo = new ProcessStartInfo();
        Process process = new Process();

        string FileName = "";
        bool isRecording = false;
        public void Start(string Type)
        {
            this.Hide();
            tray_rec.Text = "현재 녹화 완료";
            tray_exit.Enabled = false;
            tray_mainShow.Enabled = false;
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Windows Hardware Fail.wav");
                simpleSound.Play();
            }
            catch { };
            isRecording = true;

            StringBuilder CloseControler = new StringBuilder(255);
            GetPrivateProfileString("Rec", "CloseControler", "", CloseControler, 255, Application.StartupPath + "\\Options.ini");
            if (CloseControler.ToString() == "1")
                this.Visible = false;

            StringBuilder CloseArea = new StringBuilder(255);
            GetPrivateProfileString("Rec", "CloseArea", "", CloseArea, 255, Application.StartupPath + "\\Options.ini");
            if (CloseArea.ToString() == "1")
                Application.OpenForms["Area"].Opacity = 0;

            StringBuilder SavePath = new StringBuilder(255);
            GetPrivateProfileString("Save", "SavePath", "", SavePath, 255, Application.StartupPath + "\\Options.ini");
            string path = SavePath.ToString();

            StringBuilder CRF = new StringBuilder(255);
            GetPrivateProfileString("Rec", "CRF", "", CRF, 255, Application.StartupPath + "\\Options.ini");

            processInfo.FileName = @"cmd";
            processInfo.CreateNoWindow = true;
            processInfo.Verb = "runas";
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            process.StartInfo = processInfo;
            process.Start();

            int offsetX = 0;
            int offsetY = 0;
            int offsetW = 0;
            int offsetH = 0;
            if (Type == "ActiveWindow")
            {
                IntPtr hwnd = GetForegroundWindow();
                RECT stRect = default(RECT);
                IntPtr rootHandle = GetAncestor(hwnd, GetAncestorFlags.GetRoot);
                if (GetWindowTitle(rootHandle).ToString() == "")
                    return;
                GetWindowRect((int)rootHandle, ref stRect);
                RECT r = new RECT();
                DwmGetWindowAttribute(rootHandle, 9, out r, Marshal.SizeOf(typeof(RECT)));

                offsetX = r.left;
                offsetY = r.top;
                offsetW = r.right - r.left;
                offsetH = r.bottom - r.top;
            }
            else if (Type == "PrimaryScreen")
            {
                offsetX = Screen.PrimaryScreen.Bounds.Left;
                offsetY = Screen.PrimaryScreen.Bounds.Top;
                offsetW = Screen.PrimaryScreen.Bounds.Width;
                offsetH = Screen.PrimaryScreen.Bounds.Height;
            }
            StringBuilder Sound = new StringBuilder(255);
            GetPrivateProfileString("Rec", "Sound", "", Sound, 255, Application.StartupPath + "\\Options.ini");

            StringBuilder AudioDevice = new StringBuilder(255);
            GetPrivateProfileString("Rec", "AudioDevice", "", AudioDevice, 255, Application.StartupPath + "\\Options.ini");

            StringBuilder SaveFormat = new StringBuilder(255);
            GetPrivateProfileString("Save", "SaveFormat", "", SaveFormat, 255, Application.StartupPath + "\\Options.ini");
            if (SaveFormat.ToString() == "Date")
                FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".mp4";
            if (SaveFormat.ToString() == "Number")
            {
                int fCount = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
                for (int i = 0; i <= fCount; i++)
                {
                    int t = i + 1;
                    string FileChk = path + "\\" + t.ToString() + ".mp4";
                    bool FileEx = File.Exists(FileChk);
                    if (FileEx == false)
                    {
                        FileName = t.ToString() + ".mp4";
                        break;
                    }
                }
            }

            string audio = "";
            string audio2 = "";

            if (Sound.ToString() == "1")
            {
                audio = @"-y -rtbufsize 150M -f dshow -i audio=""virtual-audio-capturer""";
                if (Type == "PrimaryScreen")
                    audio = @"-y -rtbufsize 150M -f dshow -i audio=""virtual-audio-capturer"":video=""screen-capture-recorder""";
            }
            else if (Sound.ToString() == "2")
            {
                audio = @"-y -rtbufsize 150M -f dshow -i audio=""" + AudioDevice + @"""";
                if (Type == "PrimaryScreen")
                    audio = @"-y -rtbufsize 150M -f dshow -i audio=""" + AudioDevice + @""":video=""screen-capture-recorder""";
            }
            else if (Sound.ToString() == "3")
            {
                audio = @"-f dshow -i audio=""" + AudioDevice + @""" -f dshow -i audio=""virtual-audio-capturer""";
                audio2 = @"-filter_complex ""[0:a][1:a]amerge = inputs = 2[a]"" -map 2 -map ""[a]""";
                if (Type == "PrimaryScreen")
                    audio = @"-f dshow -i audio=""" + AudioDevice + @""" -f dshow -i audio=""virtual-audio-capturer"":video=""screen-capture-recorder""";
            }
            if (Type == "PrimaryScreen")
                process.StandardInput.Write(Application.StartupPath + @"\ffmpeg-Nemojit.exe -y -rtbufsize 150M " + audio + @" -f gdigrab -framerate " + 60 + " -probesize 10M -draw_mouse 1 -offset_x " + (offsetX) + " -offset_y " + (offsetY) + " -video_size " + (offsetW) + "x" + (offsetH) + @" -i desktop -c:v libx264 -r " + 60 + @" -preset ultrafast -tune zerolatency -crf " + CRF + @" -vf ""pad = ceil(iw / 2) * 2:ceil(ih / 2) * 2"" -pix_fmt yuv420p -y " + audio2 + @" """ + path + @"\" + FileName + @"""" + Environment.NewLine);
            else   
                process.StandardInput.Write(Application.StartupPath + @"\ffmpeg-Nemojit.exe -y -rtbufsize 150M " + audio + @" -f gdigrab -framerate " + 60 + " -probesize 10M -draw_mouse 1 -offset_x " + (offsetX) + " -offset_y " + (offsetY) + " -video_size " + (offsetW) + "x" + (offsetH) + @" -i desktop -c:v libx264 -r " + 60 + @" -preset ultrafast -tune zerolatency -crf " + CRF + @" -vf ""pad = ceil(iw / 2) * 2:ceil(ih / 2) * 2"" -pix_fmt yuv420p -y " + audio2 + @" """ + path + @"\" + FileName + @"""" + Environment.NewLine);
            
            return;
        }

        public void Stop()
        {
            Hotkey_Reload(1, 1, 1);
            tray_rec.Text = "녹화 창 열기";
            tray_exit.Enabled = true;
            tray_mainShow.Enabled = true;
            StringBuilder CloseTray = new StringBuilder(255);
            GetPrivateProfileString("General", "CloseTray", "", CloseTray, 255, Application.StartupPath + "\\Options.ini");
            if (CloseTray.ToString() == "0")
                this.Show();
            isRecording = false;
            process.StandardInput.Write("q" + Environment.NewLine);
            process.StandardInput.Close();
            process.WaitForExit(1);
            process.Close();

            StringBuilder Noti = new StringBuilder(255);
            GetPrivateProfileString("Rec", "Noti", "", Noti, 255, Application.StartupPath + "\\Options.ini");
            if (Noti.ToString() == "1")
            {
                StringBuilder SavePath = new StringBuilder(255);
                GetPrivateProfileString("Save", "SavePath", "", SavePath, 255, Application.StartupPath + "\\Options.ini");
                string path = SavePath.ToString();

                NemojitTrayicon.Visible = true;
                NemojitTrayicon.BalloonTipText = path + "\\" + FileName;
                NemojitTrayicon.BalloonTipTitle = "녹화가 완료되었습니다.";
                NemojitTrayicon.ShowBalloonTip(3);
            }
            else
            {
                try
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\Windows Hardware Fail.wav");
                    simpleSound.Play();
                }
                catch { };
            }
            return;
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
