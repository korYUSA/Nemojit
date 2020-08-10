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
using DirectShowLib;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Nemojit
{
    public partial class Options : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        Main main;
        public Options(Main _main)
        {
            InitializeComponent();
            main = _main;
            button_setRec.KeyDown -= button_setRec_KeyDown;
            button_activeRec.KeyDown -= button_activeRec_KeyDown;
            button_mainRec.KeyDown -= button_mainRec_KeyDown;

            button7.Top = this.Height - 79;
            button6.Top = this.Height - 119;

            /*일반*/
            StringBuilder CloseTray = new StringBuilder(255);
            StringBuilder RememberPos = new StringBuilder(255);
            StringBuilder Theme = new StringBuilder(255);
            GetPrivateProfileString("General", "CloseTray", "", CloseTray, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("General", "RememberPos", "", RememberPos, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
            if (CloseTray.ToString() == "1")
                checkBox1.Checked = true;
            if (RememberPos.ToString() == "1")
                checkBox2.Checked = true;
            ChangeColor(ColorTranslator.FromHtml(Theme.ToString()));

            /*녹화*/
            StringBuilder CloseControler = new StringBuilder(255);
            StringBuilder CloseArea = new StringBuilder(255);
            StringBuilder Noti = new StringBuilder(255);
            StringBuilder Sound = new StringBuilder(255);
            StringBuilder CRF = new StringBuilder(255);
            GetPrivateProfileString("Rec", "CloseControler", "", CloseControler, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("Rec", "CloseArea", "", CloseArea, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("Rec", "Noti", "", Noti, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("Rec", "Sound", "", Sound, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("Rec", "CRF", "", CRF, 255, Application.StartupPath + "\\Options.ini");
            if (CloseControler.ToString() == "1")
                checkBox3.Checked = true;
            if (CloseArea.ToString() == "1")
                checkBox4.Checked = true;
            if (Noti.ToString() == "1")
                checkBox5.Checked = true;
            if (Sound.ToString() == "1")
                radioButton1.Checked = true;
            else if (Sound.ToString() == "2")
                radioButton2.Checked = true;
            else if (Sound.ToString() == "3")
                radioButton3.Checked = true;
            else if (Sound.ToString() == "4")
                radioButton4.Checked = true;
            trackBar1.Value = Convert.ToInt32(CRF.ToString());
            label4.Text = CRF.ToString();

            /*저장*/
            StringBuilder SaveFormat = new StringBuilder(255);
            GetPrivateProfileString("Save", "SaveFormat", "", SaveFormat, 255, Application.StartupPath + "\\Options.ini");
            if (SaveFormat.ToString() == "Number")
                radioButton5.Checked = true;
            else if (SaveFormat.ToString() == "Date")
                radioButton6.Checked = true;

            /*사용성*/
            StringBuilder Key1Main = new StringBuilder(255);
            StringBuilder Key1Sub = new StringBuilder(255);
            StringBuilder Key2Main = new StringBuilder(255);
            StringBuilder Key2Sub = new StringBuilder(255);
            StringBuilder Key3Main = new StringBuilder(255);
            StringBuilder Key3Sub = new StringBuilder(255);
            GetPrivateProfileString("HotkeySave", "Key1Main", "", Key1Main, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("HotkeySave", "Key1Sub", "", Key1Sub, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("HotkeySave", "Key2Main", "", Key2Main, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("HotkeySave", "Key2Sub", "", Key2Sub, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("HotkeySave", "Key3Main", "", Key3Main, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("HotkeySave", "Key3Sub", "", Key3Sub, 255, Application.StartupPath + "\\Options.ini");

            KeysConverter kc = new KeysConverter();

            if (Key1Sub.ToString() == "0") button_setRec.Text = kc.ConvertToString(Convert.ToInt32(Key1Main.ToString())).ToString();
            else if (Key1Sub.ToString() == "1") button_setRec.Text = "Alt + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));
            else if (Key1Sub.ToString() == "2") button_setRec.Text = "Control + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));
            else if (Key1Sub.ToString() == "3") button_setRec.Text = "Control, Alt + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));
            else if (Key1Sub.ToString() == "4") button_setRec.Text = "Shift + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));
            else if (Key1Sub.ToString() == "5") button_setRec.Text = "Shift, Alt + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));
            else if (Key1Sub.ToString() == "6") button_setRec.Text = "Shift, Control + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));
            else if (Key1Sub.ToString() == "7") button_setRec.Text = "Shift, Control, Alt + " + kc.ConvertToString(Convert.ToInt32(Key1Main.ToString()));

            if (Key2Sub.ToString() == "0") button_activeRec.Text = kc.ConvertToString(Convert.ToInt32(Key2Main.ToString())).ToString();
            else if (Key2Sub.ToString() == "1") button_activeRec.Text = "Alt + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));
            else if (Key2Sub.ToString() == "2") button_activeRec.Text = "Control + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));
            else if (Key2Sub.ToString() == "3") button_activeRec.Text = "Control, Alt + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));
            else if (Key2Sub.ToString() == "4") button_activeRec.Text = "Shift + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));
            else if (Key2Sub.ToString() == "5") button_activeRec.Text = "Shift, Alt + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));
            else if (Key2Sub.ToString() == "6") button_activeRec.Text = "Shift, Control + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));
            else if (Key2Sub.ToString() == "7") button_activeRec.Text = "Shift, Control, Alt + " + kc.ConvertToString(Convert.ToInt32(Key2Main.ToString()));

            if (Key3Sub.ToString() == "0") button_mainRec.Text = kc.ConvertToString(Convert.ToInt32(Key3Main.ToString())).ToString();
            else if (Key3Sub.ToString() == "1") button_mainRec.Text = "Alt + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
            else if (Key3Sub.ToString() == "2") button_mainRec.Text = "Control + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
            else if (Key3Sub.ToString() == "3") button_mainRec.Text = "Control, Alt + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
            else if (Key3Sub.ToString() == "4") button_mainRec.Text = "Shift + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
            else if (Key3Sub.ToString() == "5") button_mainRec.Text = "Shift, Alt + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
            else if (Key3Sub.ToString() == "6") button_mainRec.Text = "Shift, Control + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
            else if (Key3Sub.ToString() == "7") button_mainRec.Text = "Shift, Control, Alt + " + kc.ConvertToString(Convert.ToInt32(Key3Main.ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 4;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 5;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OptionTab.SelectedIndex = 6;
        }

        bool EditMode = false;
        private void button_setRec_Click(object sender, EventArgs e)
        {
            //if (EditMode == false)
            {
                button_setRec.KeyDown += button_setRec_KeyDown;
                button_setRec.BackColor = Color.White;
                button_setRec.ForeColor = Color.Black;
                button_setRec.FlatAppearance.BorderSize = 1;
                EditMode = true;
            }
        }

        private void button_setRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (EditMode == true)
            {
                button_setRec.Text = e.Modifiers + " +";
                if (e.Modifiers.ToString() == "None")
                    button_setRec.Text = "";
                if (e.KeyCode.ToString() != "ControlKey" && e.KeyCode.ToString() != "Menu" && e.KeyCode.ToString() != "ShiftKey")
                {
                    string keycode = e.KeyCode.ToString();
                    if (e.KeyCode.ToString() == "D0" || e.KeyCode.ToString() == "D1" || e.KeyCode.ToString() == "D2" || e.KeyCode.ToString() == "D3" || e.KeyCode.ToString() == "D4" || e.KeyCode.ToString() == "D5" || e.KeyCode.ToString() == "D6" || e.KeyCode.ToString() == "D7" || e.KeyCode.ToString() == "D8" || e.KeyCode.ToString() == "D9")
                        keycode = e.KeyCode.ToString().Substring(1, 1);
                    button_setRec.Text = button_setRec.Text + " " + keycode;
                    EditMode = false;
                    button_setRec.KeyDown -= button_setRec_KeyDown;
                    StringBuilder Theme = new StringBuilder(255);
                    GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
                    button_setRec.BackColor = ColorTranslator.FromHtml(Theme.ToString());
                    button_setRec.ForeColor = Color.White;
                    button_setRec.FlatAppearance.BorderSize = 0;


                    if (button_activeRec.Text == button_setRec.Text || button_mainRec.Text == button_setRec.Text)
                    {
                        button_setRec.Text = "단축키가 중복됩니다.";
                        return;
                    }

                    //MessageBox.Show(e.KeyValue.ToString());
                    int modKey = 0;
                    if (e.Modifiers.ToString().Contains("Alt"))
                        modKey += 1;
                    if (e.Modifiers.ToString().Contains("Control"))
                        modKey += 2;
                    if (e.Modifiers.ToString().Contains("Shift"))
                        modKey += 4;
                    WritePrivateProfileString("HotkeySave", "Key1Main", e.KeyValue.ToString(), Application.StartupPath + "\\Options.ini");
                    WritePrivateProfileString("HotkeySave", "Key1Sub", modKey.ToString(), Application.StartupPath + "\\Options.ini");
                }
            }
        }

        private void button_activeRec_Click(object sender, EventArgs e)
        {
            //if (EditMode == false)
            {
                button_activeRec.KeyDown += button_activeRec_KeyDown;
                button_activeRec.BackColor = Color.White;
                button_activeRec.ForeColor = Color.Black;
                button_activeRec.FlatAppearance.BorderSize = 1;
                EditMode = true;
            }
           // else
            //    EditMode = false;
        }

        private void button_mainRec_Click(object sender, EventArgs e)
        {
            //if (EditMode == false)
            {
                button_mainRec.KeyDown += button_mainRec_KeyDown;
                button_mainRec.BackColor = Color.White;
                button_mainRec.ForeColor = Color.Black;
                button_mainRec.FlatAppearance.BorderSize = 1;
                EditMode = true;
            }
        }

        private void button_activeRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (EditMode == true)
            {
                button_activeRec.Text = e.Modifiers + " +";
                if (e.Modifiers.ToString() == "None")
                    button_activeRec.Text = "";
                if (e.KeyCode.ToString() != "ControlKey" && e.KeyCode.ToString() != "Menu" && e.KeyCode.ToString() != "ShiftKey")
                {
                    string keycode = e.KeyCode.ToString();
                    if (e.KeyCode.ToString() == "D0" || e.KeyCode.ToString() == "D1" || e.KeyCode.ToString() == "D2" || e.KeyCode.ToString() == "D3" || e.KeyCode.ToString() == "D4" || e.KeyCode.ToString() == "D5" || e.KeyCode.ToString() == "D6" || e.KeyCode.ToString() == "D7" || e.KeyCode.ToString() == "D8" || e.KeyCode.ToString() == "D9")
                        keycode = e.KeyCode.ToString().Substring(1, 1);
                    button_activeRec.Text = button_activeRec.Text + " " + keycode;
                    EditMode = false;
                    button_activeRec.KeyDown -= button_setRec_KeyDown;
                    StringBuilder Theme = new StringBuilder(255);
                    GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
                    button_activeRec.BackColor = ColorTranslator.FromHtml(Theme.ToString());
                    button_activeRec.ForeColor = Color.White;
                    button_activeRec.FlatAppearance.BorderSize = 0;

                    if (button_activeRec.Text == button_setRec.Text || button_mainRec.Text == button_activeRec.Text)
                    {
                        button_activeRec.Text = "단축키가 중복됩니다.";
                        return;
                    }

                    int modKey = 0;
                    if (e.Modifiers.ToString().Contains("Alt"))
                        modKey += 1;
                    if (e.Modifiers.ToString().Contains("Control"))
                        modKey += 2;
                    if (e.Modifiers.ToString().Contains("Shift"))
                        modKey += 4;
                    WritePrivateProfileString("HotkeySave", "Key2Main", e.KeyValue.ToString(), Application.StartupPath + "\\Options.ini");
                    WritePrivateProfileString("HotkeySave", "Key2Sub", modKey.ToString(), Application.StartupPath + "\\Options.ini");
                }
            }
        }

        private void button_mainRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (EditMode == true)
            {
                button_mainRec.Text = e.Modifiers + " +";
                if (e.Modifiers.ToString() == "None")
                    button_mainRec.Text = "";
                if (e.KeyCode.ToString() != "ControlKey" && e.KeyCode.ToString() != "Menu" && e.KeyCode.ToString() != "ShiftKey")
                {
                    string keycode = e.KeyCode.ToString();
                    if (e.KeyCode.ToString() == "D0" || e.KeyCode.ToString() == "D1" || e.KeyCode.ToString() == "D2" || e.KeyCode.ToString() == "D3" || e.KeyCode.ToString() == "D4" || e.KeyCode.ToString() == "D5" || e.KeyCode.ToString() == "D6" || e.KeyCode.ToString() == "D7" || e.KeyCode.ToString() == "D8" || e.KeyCode.ToString() == "D9")
                        keycode = e.KeyCode.ToString().Substring(1, 1);
                    button_mainRec.Text = button_mainRec.Text + " " + keycode;
                    EditMode = false;
                    button_mainRec.KeyDown -= button_setRec_KeyDown;
                    StringBuilder Theme = new StringBuilder(255);
                    GetPrivateProfileString("General", "Theme", "", Theme, 255, Application.StartupPath + "\\Options.ini");
                    button_mainRec.BackColor = ColorTranslator.FromHtml(Theme.ToString());
                    button_mainRec.ForeColor = Color.White;
                    button_mainRec.FlatAppearance.BorderSize = 0;

                    if (button_mainRec.Text == button_setRec.Text || button_mainRec.Text == button_activeRec.Text)
                    {
                        button_mainRec.Text = "단축키가 중복됩니다.";
                        return;
                    }

                    int modKey = 0;
                    if (e.Modifiers.ToString().Contains("Alt"))
                        modKey += 1;
                    if (e.Modifiers.ToString().Contains("Control"))
                        modKey += 2;
                    if (e.Modifiers.ToString().Contains("Shift"))
                        modKey += 4;
                    WritePrivateProfileString("HotkeySave", "Key3Main", e.KeyValue.ToString(), Application.StartupPath + "\\Options.ini");
                    WritePrivateProfileString("HotkeySave", "Key3Sub", modKey.ToString(), Application.StartupPath + "\\Options.ini");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                WritePrivateProfileString("General", "CloseTray", "1", Application.StartupPath + "\\Options.ini");
            else
                WritePrivateProfileString("General", "CloseTray", "0", Application.StartupPath + "\\Options.ini");
        }

        private void TabButton1_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("AreaSave", "AreaW", "629", Application.StartupPath + "\\Options.ini");
            WritePrivateProfileString("AreaSave", "AreaH", "450", Application.StartupPath + "\\Options.ini");
            WritePrivateProfileString("AreaSave", "AreaX", (Screen.PrimaryScreen.Bounds.Width / 2 - 314).ToString(), Application.StartupPath + "\\Options.ini");
            WritePrivateProfileString("AreaSave", "AreaY", (Screen.PrimaryScreen.Bounds.Height / 2 - 225).ToString(), Application.StartupPath + "\\Options.ini");
            TabButton1.Text = "재설정 완료";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                WritePrivateProfileString("General", "RememberPos", "1", Application.StartupPath + "\\Options.ini");
            else
                WritePrivateProfileString("General", "RememberPos", "0", Application.StartupPath + "\\Options.ini");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                WritePrivateProfileString("Rec", "CloseControler", "1", Application.StartupPath + "\\Options.ini");
            else
                WritePrivateProfileString("Rec", "CloseControler", "0", Application.StartupPath + "\\Options.ini");
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
                WritePrivateProfileString("Rec", "CloseArea", "1", Application.StartupPath + "\\Options.ini");
            else
                WritePrivateProfileString("Rec", "CloseArea", "0", Application.StartupPath + "\\Options.ini");
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
                WritePrivateProfileString("Rec", "Noti", "1", Application.StartupPath + "\\Options.ini");
            else
                WritePrivateProfileString("Rec", "Noti", "0", Application.StartupPath + "\\Options.ini");
        }

        private void RecSoundRadio_Chk(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            RadioButton btn = sender as RadioButton;
            if (btn.Checked == false)
                return;
            WritePrivateProfileString("Rec", "Sound", btn.Name.Substring(11,1), Application.StartupPath + "\\Options.ini");
            if (btn.Name.Substring(11, 1) == "2" || btn.Name.Substring(11, 1) == "3")
            {
                comboBox1.Enabled = true;
                ComboboxItem item = new ComboboxItem();

                DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice);
                for (int i = 0; i < capDevices.Length;i++)
                    comboBox1.Items.Add(capDevices[i].Name);
                StringBuilder AudioDevice = new StringBuilder(255);
                GetPrivateProfileString("Rec", "AudioDevice", "", AudioDevice, 255, Application.StartupPath + "\\Options.ini");
                comboBox1.SelectedItem = AudioDevice.ToString();
            }
            else
            {
                comboBox1.Items.Clear();
                comboBox1.Enabled = false;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar1.Value.ToString();
            WritePrivateProfileString("Rec", "CRF", trackBar1.Value.ToString(), Application.StartupPath + "\\Options.ini");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            WritePrivateProfileString("Rec", "AudioDevice", comboBox1.Text, Application.StartupPath + "\\Options.ini");
        }

        private void comboBox1_EnabledChanged(object sender, EventArgs e)
        {
            StringBuilder AudioDevice = new StringBuilder(255);
            GetPrivateProfileString("Rec", "AudioDevice", "", AudioDevice, 255, Application.StartupPath + "\\Options.ini");
            comboBox1.SelectedItem = AudioDevice.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            Process process = new Process();
            processInfo.FileName = @"cmd";
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            processInfo.Verb = "runas";
            process.StartInfo = processInfo;
            process.Start();
            process.StandardInput.Write(@"cd " + Application.StartupPath + Environment.NewLine);
            process.StandardInput.Write(@"regsvr32 virtual-audio-capturer-x64.dll"+ Environment.NewLine);
            process.Close();
        }

        private void button_blue_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("General", "Theme", "#0F4B81", Application.StartupPath + "\\Options.ini");
            ChangeColor(ColorTranslator.FromHtml("#FF0F4B81"));
        }

        private void button_orange_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("General", "Theme", "#FF8000", Application.StartupPath + "\\Options.ini");
            ChangeColor(ColorTranslator.FromHtml("#FF8000"));
        }

        private void button_black_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("General", "Theme", "#333333", Application.StartupPath + "\\Options.ini");
            ChangeColor(ColorTranslator.FromHtml("#333333"));
        }

        private void button_red_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("General", "Theme", "#A52A2A", Application.StartupPath + "\\Options.ini");
            ChangeColor(ColorTranslator.FromHtml("#A52A2A"));
        }

        private void button_green_Click(object sender, EventArgs e)
        {
            WritePrivateProfileString("General", "Theme", "#00C896", Application.StartupPath + "\\Options.ini");
            ChangeColor(ColorTranslator.FromHtml("#00C896"));
        }

        private void ChangeColor(Color color)
        {
            button1.BackColor = color;
            button2.BackColor = color;
            button3.BackColor = color;
            button4.BackColor = color;
            button5.BackColor = color;
            button6.BackColor = color;
            button7.BackColor = color;
            button8.BackColor = color;
            TabButton1.BackColor = color;
            button_save.BackColor = color;
            button_setRec.BackColor = color;
            button_mainRec.BackColor = color;
            button_activeRec.BackColor = color;
            main.OpenFolder.FlatAppearance.BorderColor = color;
            main.RecordSelect.BackColor = color;
            main.Settings.FlatAppearance.BorderColor = color;
            main.Info.BackColor = color;
            button9.BackColor = color;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
                WritePrivateProfileString("Save", "SaveFormat", "Number", Application.StartupPath + "\\Options.ini");
            else
                WritePrivateProfileString("Save", "SaveFormat", "Date", Application.StartupPath + "\\Options.ini");

            PathExample.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true; if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                WritePrivateProfileString("Save", "SavePath", dialog.FileName, Application.StartupPath + "\\Options.ini");
                PathExample.Text = "";
            }
        }

        private void PathExample_TextChanged(object sender, EventArgs e)
        {
            StringBuilder SavePath = new StringBuilder(255);
            StringBuilder SaveFormat = new StringBuilder(255);
            GetPrivateProfileString("Save", "SavePath", "", SavePath, 255, Application.StartupPath + "\\Options.ini");
            GetPrivateProfileString("Save", "SaveFormat", "", SaveFormat, 255, Application.StartupPath + "\\Options.ini");
            string NameEx = "";
            if (SaveFormat.ToString() == "Number")
                NameEx = "1.mp4";
            else if (SaveFormat.ToString() == "Date")
                NameEx = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".mp4";
            PathExample.Text = SavePath + "\\" + NameEx;
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms["Main"].Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Restart();
            }
            catch { }
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
