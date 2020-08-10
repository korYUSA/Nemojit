namespace Nemojit
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.RecordSelect = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.OpenFolder = new System.Windows.Forms.Button();
            this.Info = new System.Windows.Forms.Button();
            this.NemojitTrayicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tray_mainShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tray_rec = new System.Windows.Forms.ToolStripMenuItem();
            this.tray_openFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tray_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordSelect
            // 
            this.RecordSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.RecordSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.RecordSelect.FlatAppearance.BorderSize = 0;
            this.RecordSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RecordSelect.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RecordSelect.ForeColor = System.Drawing.Color.White;
            this.RecordSelect.Location = new System.Drawing.Point(145, 43);
            this.RecordSelect.Name = "RecordSelect";
            this.RecordSelect.Size = new System.Drawing.Size(100, 100);
            this.RecordSelect.TabIndex = 1;
            this.RecordSelect.Text = "녹화하기";
            this.RecordSelect.UseVisualStyleBackColor = false;
            this.RecordSelect.Click += new System.EventHandler(this.RecordSelect_Click);
            // 
            // Settings
            // 
            this.Settings.BackColor = System.Drawing.Color.White;
            this.Settings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.Settings.FlatAppearance.BorderSize = 2;
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Settings.Location = new System.Drawing.Point(251, 95);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(100, 48);
            this.Settings.TabIndex = 4;
            this.Settings.Text = "설정";
            this.Settings.UseVisualStyleBackColor = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // OpenFolder
            // 
            this.OpenFolder.BackColor = System.Drawing.Color.White;
            this.OpenFolder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.OpenFolder.FlatAppearance.BorderSize = 2;
            this.OpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenFolder.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.OpenFolder.ForeColor = System.Drawing.Color.Black;
            this.OpenFolder.Location = new System.Drawing.Point(39, 43);
            this.OpenFolder.Name = "OpenFolder";
            this.OpenFolder.Size = new System.Drawing.Size(100, 100);
            this.OpenFolder.TabIndex = 5;
            this.OpenFolder.Text = "폴더 열기";
            this.OpenFolder.UseVisualStyleBackColor = false;
            this.OpenFolder.Click += new System.EventHandler(this.OpenFolder_Click);
            // 
            // Info
            // 
            this.Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.Info.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.Info.FlatAppearance.BorderSize = 0;
            this.Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Info.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Info.ForeColor = System.Drawing.Color.White;
            this.Info.Location = new System.Drawing.Point(251, 43);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(100, 48);
            this.Info.TabIndex = 6;
            this.Info.Text = "정보";
            this.Info.UseVisualStyleBackColor = false;
            this.Info.Click += new System.EventHandler(this.Info_Click);
            // 
            // NemojitTrayicon
            // 
            this.NemojitTrayicon.BalloonTipText = "네모짓이 여전히 실행 중이며, 단축키를 이용할 수 있습니다.";
            this.NemojitTrayicon.BalloonTipTitle = "네모짓이 실행 중입니다.";
            this.NemojitTrayicon.ContextMenuStrip = this.ContextMenu;
            this.NemojitTrayicon.Icon = ((System.Drawing.Icon)(resources.GetObject("NemojitTrayicon.Icon")));
            this.NemojitTrayicon.Text = "네모짓";
            this.NemojitTrayicon.Visible = true;
            this.NemojitTrayicon.BalloonTipClicked += new System.EventHandler(this.NemojitTrayicon_BalloonTipClicked);
            this.NemojitTrayicon.DoubleClick += new System.EventHandler(this.NemojitTrayicon_DoubleClick);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tray_mainShow,
            this.tray_rec,
            this.tray_openFolder,
            this.tray_exit});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.ShowImageMargin = false;
            this.ContextMenu.Size = new System.Drawing.Size(170, 92);
            // 
            // tray_mainShow
            // 
            this.tray_mainShow.Name = "tray_mainShow";
            this.tray_mainShow.Size = new System.Drawing.Size(169, 22);
            this.tray_mainShow.Text = "네모짓 메인 화면 열기";
            this.tray_mainShow.Click += new System.EventHandler(this.tray_mainShow_Click);
            // 
            // tray_rec
            // 
            this.tray_rec.Name = "tray_rec";
            this.tray_rec.Size = new System.Drawing.Size(169, 22);
            this.tray_rec.Text = "녹화 / 중지";
            this.tray_rec.Click += new System.EventHandler(this.tray_rec_Click);
            // 
            // tray_openFolder
            // 
            this.tray_openFolder.Name = "tray_openFolder";
            this.tray_openFolder.Size = new System.Drawing.Size(169, 22);
            this.tray_openFolder.Text = "저장 폴더 열기";
            this.tray_openFolder.Click += new System.EventHandler(this.tray_openFolder_Click);
            // 
            // tray_exit
            // 
            this.tray_exit.Name = "tray_exit";
            this.tray_exit.Size = new System.Drawing.Size(169, 22);
            this.tray_exit.Text = "종료";
            this.tray_exit.Click += new System.EventHandler(this.tray_exit_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 186);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.OpenFolder);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.RecordSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "네모짓";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button RecordSelect;
        public System.Windows.Forms.Button Settings;
        public System.Windows.Forms.Button OpenFolder;
        public System.Windows.Forms.Button Info;
        private System.Windows.Forms.ToolStripMenuItem tray_mainShow;
        private System.Windows.Forms.ToolStripMenuItem tray_rec;
        private System.Windows.Forms.ToolStripMenuItem tray_openFolder;
        private System.Windows.Forms.ToolStripMenuItem tray_exit;
        public System.Windows.Forms.NotifyIcon NemojitTrayicon;
        public System.Windows.Forms.ContextMenuStrip ContextMenu;
    }
}

