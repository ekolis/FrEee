namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    partial class UCLinkObj
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
            this.gameTableLayoutPanel1 = new FrEee.WinForms.Controls.GameTableLayoutPanel();
            this.gamePictureBox1 = new FrEee.WinForms.Controls.GamePictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gameTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gameTableLayoutPanel1
            // 
            this.gameTableLayoutPanel1.ColumnCount = 2;
            this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gameTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gameTableLayoutPanel1.Controls.Add(this.gamePictureBox1, 0, 0);
            this.gameTableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.gameTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.gameTableLayoutPanel1.Name = "gameTableLayoutPanel1";
            this.gameTableLayoutPanel1.RowCount = 1;
            this.gameTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.gameTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.gameTableLayoutPanel1.Size = new System.Drawing.Size(80, 18);
            this.gameTableLayoutPanel1.TabIndex = 0;
            // 
            // gamePictureBox1
            // 
            this.gamePictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePictureBox1.Image = global::FrEee.WinForms.Properties.Resources.check_ethernet_clear;
            this.gamePictureBox1.Location = new System.Drawing.Point(0, 0);
            this.gamePictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.gamePictureBox1.Name = "gamePictureBox1";
            this.gamePictureBox1.Size = new System.Drawing.Size(20, 18);
            this.gamePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gamePictureBox1.TabIndex = 0;
            this.gamePictureBox1.TabStop = false;
            this.gamePictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.checkBox1_DragDrop);
            this.gamePictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.checkBox1_DragEnter);
            this.gamePictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkBox1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(23, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UCLinkObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.gameTableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Name = "UCLinkObj";
            this.Size = new System.Drawing.Size(80, 18);
            this.gameTableLayoutPanel1.ResumeLayout(false);
            this.gameTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gamePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GameTableLayoutPanel gameTableLayoutPanel1;
        private Controls.GamePictureBox gamePictureBox1;
        private System.Windows.Forms.Label label1;

    }
}
