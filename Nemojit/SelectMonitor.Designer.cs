namespace Nemojit
{
    partial class SelectMonitor
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
            this.GetMousePos = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // GetMousePos
            // 
            this.GetMousePos.Tick += new System.EventHandler(this.GetMousePos_Tick);
            // 
            // SelectMonitor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(75)))), ((int)(((byte)(129)))));
            this.ClientSize = new System.Drawing.Size(253, 41);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectMonitor";
            this.Opacity = 0.3D;
            this.Text = "SelectMonitor";
            this.TopMost = true;
            this.Click += new System.EventHandler(this.SelectMonitor_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GetMousePos;
    }
}