using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemojit
{
    public partial class Recorder : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        ProcessStartInfo processInfo = new ProcessStartInfo();
        Process process = new Process();

        Main main;

        Stopwatch SW = new Stopwatch();

        public Recorder(Main _main)
        {
            InitializeComponent();
            main = _main;
            this.Left = Screen.PrimaryScreen.Bounds.Width - 320;
            this.Left -= 320 * (DeviceDpi / 100);
            this.Top = Screen.PrimaryScreen.Bounds.Top + 50;
            Application.OpenForms["Main"].Hide();
            TimeText.Visible = false;
            RecordType.SelectedIndex = 2;
            RecordFrame.SelectedIndex = 1;
            UnregisterHotKey(main.Handle, 1);
            UnregisterHotKey(main.Handle, 2);

            StringBuilder Theme = new StringBuilder(255);
            GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
            Record.BackColor = ColorTranslator.FromHtml(Theme.ToString());
            this.BackColor = ColorTranslator.FromHtml(Theme.ToString());
            Exit.BackColor = ColorTranslator.FromHtml(Theme.ToString());
            MinimizeBtn.BackColor = ColorTranslator.FromHtml(Theme.ToString());

        }


        Point MousePos;

        private void Recorder_MouseDown(object sender, MouseEventArgs e)
        {
            MousePos = e.Location;
            return;
        }

        private void Recorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MousePos.X == 0 && MousePos.Y == 0)
                {
                    mouse_event(0x0002, 0, 0, 0, 0);
                    return;
                }
                this.Left = this.Left - MousePos.X + e.X;
                this.Top = this.Top - MousePos.Y + e.Y;
            }
            return;
        }

        private void Recorder_MouseUp(object sender, MouseEventArgs e)
        {
            MousePos.X = 0;
            MousePos.Y = 0;
            return;
        }

        private void Recorder_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["Main"].Show();
            Application.OpenForms["Main"].Activate();
            try
            {
                Application.OpenForms["Area"].Close();
                return;
            }
            catch { }
            return;
        }

        public bool isRecording = false;

        private void Record_Click(object sender, EventArgs e)
        {
            if (isRecording == false)
            {
                Record.Text = "*";
                Start();
                Record.Text = "▪";
                Exit.Visible = false;

                RecordType.Visible = false;
                RecordFrame.Visible = false;
                TimeText.Visible = true;
            }
            else if (isRecording == true)
            {
                Stop();
            }
        }


        string FileName = "";
        public void Start()
        {
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
            processInfo.UseShellExecute = false;
            processInfo.Verb = "runas";
            processInfo.RedirectStandardInput = true;
            process.StartInfo = processInfo;
            process.Start();

            int offsetX = Application.OpenForms["Area"].Left + 4;
            int offsetY = Application.OpenForms["Area"].Top + 20;
            int offsetW = Application.OpenForms["Area"].Width - 8;
            int offsetH = Application.OpenForms["Area"].Height - 24;

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
            }
            else if (Sound.ToString() == "2")
            {
                audio = @"-y -rtbufsize 150M -f dshow -i audio=""" + AudioDevice + @"""";
            }
            else if (Sound.ToString() == "3")
            {
                audio = @"-f dshow -i audio=""" + AudioDevice + @""" -f dshow -i audio=""virtual-audio-capturer""";
                audio2 = @"-filter_complex ""[0:a][1:a]amerge = inputs = 2[a]"" -map 2 -map ""[a]""";
            }

            if (RecordType.SelectedIndex != 2)
            {
                Application.OpenForms["Area"].Hide();
                process.StandardInput.Write(Application.StartupPath + @"\ffmpeg-Nemojit.exe -y -rtbufsize 150M " + audio + @" -f gdigrab -framerate " + RecordFrame.SelectedItem.ToString().Substring(0, 2) + " -probesize 10M -draw_mouse 1 -offset_x " + (offsetX - 4) + " -offset_y " + (offsetY - 20) + " -video_size " + (offsetW + 8) + "x" + (offsetH + 24) + @" -i desktop -c:v libx264 -r " + RecordFrame.SelectedItem.ToString().Substring(0, 2) + @" -preset ultrafast -tune zerolatency -crf " + CRF + @" -vf ""pad = ceil(iw / 2) * 2:ceil(ih / 2) * 2"" -pix_fmt yuv420p -y " + audio2 + @" """ + path + @"\" + FileName + @"""" + Environment.NewLine);
                
            }
            else
            {
                process.StandardInput.Write(Application.StartupPath + @"\ffmpeg-Nemojit.exe -y -rtbufsize 150M " + audio + @" -f gdigrab -framerate " + RecordFrame.SelectedItem.ToString().Substring(0, 2) + " -probesize 10M -draw_mouse 1 -offset_x " + (offsetX) + " -offset_y " + (offsetY) + " -video_size " + (offsetW) + "x" + (offsetH) + @" -i desktop -c:v libx264 -r " + RecordFrame.SelectedItem.ToString().Substring(0, 2) + @" -preset ultrafast -tune zerolatency -crf " + CRF + @" -vf ""pad = ceil(iw / 2) * 2:ceil(ih / 2) * 2"" -pix_fmt yuv420p -y " + audio2 + @" """ + path + @"\" + FileName + @"""" + Environment.NewLine);
            }
            Timer.Start();
            SW.Start();
            return;
        }

        public void Stop()
        {
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

                main.NemojitTrayicon.Visible = true;
                main.NemojitTrayicon.BalloonTipText = path + "\\" + FileName;
                main.NemojitTrayicon.BalloonTipTitle = "녹화가 완료되었습니다.";
                main.NemojitTrayicon.ShowBalloonTip(3);
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
            main.Hotkey_Reload(0, 1, 1);
            this.Close();
            return;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void RecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RecordType.SelectedIndex == 0)
            {
                SelectMonitor SelectMonitorForm = new SelectMonitor(this);
                SelectMonitorForm.Show();
            }
            else if (RecordType.SelectedIndex == 1)
            {
                SelectWindow SelectWindowForm = new SelectWindow();
                SelectWindowForm.Show();
            }
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = SW.Elapsed;
            TimeText.Text = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }

        public int prevSelect = 0;
        private void RecordType_DropDown(object sender, EventArgs e)
        {
            prevSelect = RecordType.SelectedIndex;
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
