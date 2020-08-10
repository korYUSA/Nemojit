namespace Nemojit
{
    partial class Recorder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recorder));
            this.Record = new System.Windows.Forms.Button();
            this.RecordType = new System.Windows.Forms.ComboBox();
            this.Exit = new System.Windows.Forms.Button();
            this.TimeText = new System.Windows.Forms.Label();
            this.RecordFrame = new System.Windows.Forms.ComboBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.BackPanel = new System.Windows.Forms.Panel();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.BackPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Record
            // 
            this.Record.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.Record.Cursor = System.Windows.Forms.Cursors.Default;
            this.Record.FlatAppearance.BorderSize = 0;
            this.Record.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Record.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Record.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Record.Location = new System.Drawing.Point(0, 0);
            this.Record.Name = "Record";
            this.Record.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Record.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Record.Size = new System.Drawing.Size(48, 48);
            this.Record.TabIndex = 0;
            this.Record.Text = "◉";
            this.Record.UseVisualStyleBackColor = false;
            this.Record.Click += new System.EventHandler(this.Record_Click);
            this.Record.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseDown);
            this.Record.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseMove);
            this.Record.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseUp);
            // 
            // RecordType
            // 
            this.RecordType.Cursor = System.Windows.Forms.Cursors.Default;
            this.RecordType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RecordType.FormattingEnabled = true;
            this.RecordType.Items.AddRange(new object[] {
            "모니터",
            "열려있는 창",
            "영역 직접 지정"});
            this.RecordType.Location = new System.Drawing.Point(54, 15);
            this.RecordType.Name = "RecordType";
            this.RecordType.Size = new System.Drawing.Size(120, 20);
            this.RecordType.TabIndex = 1;
            this.RecordType.DropDown += new System.EventHandler(this.RecordType_DropDown);
            this.RecordType.SelectedIndexChanged += new System.EventHandler(this.RecordType_SelectedIndexChanged);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.Exit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Exit.Location = new System.Drawing.Point(248, 0);
            this.Exit.Name = "Exit";
            this.Exit.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.Exit.Size = new System.Drawing.Size(20, 48);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "×";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // TimeText
            // 
            this.TimeText.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TimeText.Location = new System.Drawing.Point(49, 0);
            this.TimeText.Name = "TimeText";
            this.TimeText.Size = new System.Drawing.Size(198, 48);
            this.TimeText.TabIndex = 3;
            this.TimeText.Text = "녹화 준비 중입니다...";
            this.TimeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TimeText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseDown);
            this.TimeText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseMove);
            this.TimeText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseUp);
            // 
            // RecordFrame
            // 
            this.RecordFrame.Cursor = System.Windows.Forms.Cursors.Default;
            this.RecordFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RecordFrame.FormattingEnabled = true;
            this.RecordFrame.Items.AddRange(new object[] {
            "24 fps",
            "30 fps",
            "60 fps"});
            this.RecordFrame.Location = new System.Drawing.Point(180, 15);
            this.RecordFrame.Name = "RecordFrame";
            this.RecordFrame.Size = new System.Drawing.Size(60, 20);
            this.RecordFrame.TabIndex = 4;
            // 
            // Timer
            // 
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // BackPanel
            // 
            this.BackPanel.BackColor = System.Drawing.SystemColors.Window;
            this.BackPanel.Controls.Add(this.RecordType);
            this.BackPanel.Controls.Add(this.Record);
            this.BackPanel.Controls.Add(this.Exit);
            this.BackPanel.Controls.Add(this.TimeText);
            this.BackPanel.Controls.Add(this.RecordFrame);
            this.BackPanel.Controls.Add(this.MinimizeBtn);
            this.BackPanel.Location = new System.Drawing.Point(1, 1);
            this.BackPanel.Name = "BackPanel";
            this.BackPanel.Size = new System.Drawing.Size(268, 48);
            this.BackPanel.TabIndex = 5;
            this.BackPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseDown);
            this.BackPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseMove);
            this.BackPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseUp);
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.MinimizeBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MinimizeBtn.FlatAppearance.BorderSize = 0;
            this.MinimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeBtn.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MinimizeBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MinimizeBtn.Location = new System.Drawing.Point(248, 0);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.MinimizeBtn.Size = new System.Drawing.Size(20, 48);
            this.MinimizeBtn.TabIndex = 5;
            this.MinimizeBtn.Text = "▼";
            this.MinimizeBtn.UseVisualStyleBackColor = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // Recorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.ClientSize = new System.Drawing.Size(270, 50);
            this.Controls.Add(this.BackPanel);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Recorder";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Recorder";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Recorder_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Recorder_MouseUp);
            this.BackPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox RecordType;
        private System.Windows.Forms.Timer Timer;
        public System.Windows.Forms.Button Record;
        public System.Windows.Forms.Label TimeText;
        public System.Windows.Forms.Button Exit;
        public System.Windows.Forms.ComboBox RecordFrame;
        private System.Windows.Forms.Panel BackPanel;
        public System.Windows.Forms.Button MinimizeBtn;
    }
}