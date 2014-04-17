namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    partial class UserControlBaseObj
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_FunctName = new System.Windows.Forms.Label();
            this.gameTableLayoutPanel1 = new FrEee.WinForms.Controls.GameTableLayoutPanel();
            this.gamePanel1 = new FrEee.WinForms.Controls.GamePanel();
            this.gameTableLayoutPanel1.SuspendLayout();
            this.gamePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_FunctName
            // 
            this.lbl_FunctName.AutoSize = true;
            this.gameTableLayoutPanel1.SetColumnSpan(this.lbl_FunctName, 2);
            this.lbl_FunctName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_FunctName.Location = new System.Drawing.Point(23, 0);
            this.lbl_FunctName.Name = "lbl_FunctName";
            this.lbl_FunctName.Size = new System.Drawing.Size(140, 13);
            this.lbl_FunctName.TabIndex = 0;
            this.lbl_FunctName.Text = "Name";
            this.lbl_FunctName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_FunctName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseDown);
            this.lbl_FunctName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseMove);
            this.lbl_FunctName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserControl_System_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.gameTableLayoutPanel1.ColumnCount = 4;
            this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.gameTableLayoutPanel1.Controls.Add(this.lbl_FunctName, 1, 0);
            this.gameTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.gameTableLayoutPanel1.Name = "tableLayoutPanel1";
            this.gameTableLayoutPanel1.RowCount = 1;
            this.gameTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gameTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gameTableLayoutPanel1.Size = new System.Drawing.Size(188, 61);
            this.gameTableLayoutPanel1.TabIndex = 1;
            // 
            // gamePanel1
            // 
            this.gamePanel1.BackColor = System.Drawing.Color.Black;
            this.gamePanel1.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.gamePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel1.Controls.Add(this.gameTableLayoutPanel1);
            this.gamePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel1.ForeColor = System.Drawing.Color.White;
            this.gamePanel1.Location = new System.Drawing.Point(0, 0);
            this.gamePanel1.Name = "gamePanel1";
            this.gamePanel1.Padding = new System.Windows.Forms.Padding(3);
            this.gamePanel1.Size = new System.Drawing.Size(196, 69);
            this.gamePanel1.TabIndex = 2;
            // 
            // UserControlBaseObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.gamePanel1);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Name = "UserControlBaseObj";
            this.Size = new System.Drawing.Size(196, 69);
            this.gameTableLayoutPanel1.ResumeLayout(false);
            this.gameTableLayoutPanel1.PerformLayout();
            this.gamePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_FunctName;
        private FrEee.WinForms.Controls.GameTableLayoutPanel gameTableLayoutPanel1;
        private Controls.GamePanel gamePanel1;
    }
}
