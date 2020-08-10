namespace Nemojit
{
    partial class SelectWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectWindow));
            this.GetMousePos = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // GetMousePos
            // 
            this.GetMousePos.Tick += new System.EventHandler(this.GetMousePos_Tick);
            // 
            // SelectWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.ClientSize = new System.Drawing.Size(300, 50);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectWindow";
            this.Opacity = 0.3D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelectWindow";
            this.Click += new System.EventHandler(this.SelectWindow_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GetMousePos;
    }
}