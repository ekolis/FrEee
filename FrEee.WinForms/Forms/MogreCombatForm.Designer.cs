namespace FrEee.WinForms.Forms
{
    partial class MogreCombatForm
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
			this.txtLog = new System.Windows.Forms.TextBox();
			this.btnRestart = new FrEee.WinForms.Controls.GameButton();
			this.btnplay = new FrEee.WinForms.Controls.GameButton();
			this.btnFastForward = new FrEee.WinForms.Controls.GameButton();
			this.btnPreviousShip = new FrEee.WinForms.Controls.GameButton();
			this.btnFreeCam = new FrEee.WinForms.Controls.GameButton();
			this.btnNextShip = new FrEee.WinForms.Controls.GameButton();
			this.spaceVehicleReport = new FrEee.WinForms.Controls.SpaceVehicleReport();
			this.SuspendLayout();
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.txtLog.Location = new System.Drawing.Point(12, 92);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(255, 657);
			this.txtLog.TabIndex = 14;
			// 
			// btnRestart
			// 
			this.btnRestart.BackColor = System.Drawing.Color.Black;
			this.btnRestart.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnRestart.Location = new System.Drawing.Point(12, 12);
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(78, 34);
			this.btnRestart.TabIndex = 6;
			this.btnRestart.Text = "Back to Start";
			this.btnRestart.UseVisualStyleBackColor = false;
			// 
			// btnplay
			// 
			this.btnplay.BackColor = System.Drawing.Color.Black;
			this.btnplay.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnplay.Location = new System.Drawing.Point(96, 12);
			this.btnplay.Name = "btnplay";
			this.btnplay.Size = new System.Drawing.Size(78, 34);
			this.btnplay.TabIndex = 8;
			this.btnplay.Text = "Play";
			this.btnplay.UseVisualStyleBackColor = false;
			// 
			// btnFastForward
			// 
			this.btnFastForward.BackColor = System.Drawing.Color.Black;
			this.btnFastForward.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnFastForward.Location = new System.Drawing.Point(180, 12);
			this.btnFastForward.Name = "btnFastForward";
			this.btnFastForward.Size = new System.Drawing.Size(87, 34);
			this.btnFastForward.TabIndex = 9;
			this.btnFastForward.Text = "Fast Foward";
			this.btnFastForward.UseVisualStyleBackColor = false;
			// 
			// btnPreviousShip
			// 
			this.btnPreviousShip.BackColor = System.Drawing.Color.Black;
			this.btnPreviousShip.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPreviousShip.Location = new System.Drawing.Point(12, 52);
			this.btnPreviousShip.Name = "btnPreviousShip";
			this.btnPreviousShip.Size = new System.Drawing.Size(78, 34);
			this.btnPreviousShip.TabIndex = 10;
			this.btnPreviousShip.Text = "Previous Ship";
			this.btnPreviousShip.UseVisualStyleBackColor = false;
			this.btnPreviousShip.Click += new System.EventHandler(this.btnPreviousShip_Click);
			// 
			// btnFreeCam
			// 
			this.btnFreeCam.BackColor = System.Drawing.Color.Black;
			this.btnFreeCam.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnFreeCam.Location = new System.Drawing.Point(96, 52);
			this.btnFreeCam.Name = "btnFreeCam";
			this.btnFreeCam.Size = new System.Drawing.Size(78, 34);
			this.btnFreeCam.TabIndex = 11;
			this.btnFreeCam.Text = "Free Cam";
			this.btnFreeCam.UseVisualStyleBackColor = false;
			// 
			// btnNextShip
			// 
			this.btnNextShip.BackColor = System.Drawing.Color.Black;
			this.btnNextShip.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnNextShip.Location = new System.Drawing.Point(180, 52);
			this.btnNextShip.Name = "btnNextShip";
			this.btnNextShip.Size = new System.Drawing.Size(87, 34);
			this.btnNextShip.TabIndex = 12;
			this.btnNextShip.Text = "Next Ship";
			this.btnNextShip.UseVisualStyleBackColor = false;
			this.btnNextShip.Click += new System.EventHandler(this.btnNextShip_Click);
			// 
			// spaceVehicleReport
			// 
			this.spaceVehicleReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.spaceVehicleReport.BackColor = System.Drawing.Color.Black;
			this.spaceVehicleReport.ForeColor = System.Drawing.Color.White;
			this.spaceVehicleReport.Location = new System.Drawing.Point(842, 12);
			this.spaceVehicleReport.Name = "spaceVehicleReport";
			this.spaceVehicleReport.Size = new System.Drawing.Size(330, 737);
			this.spaceVehicleReport.TabIndex = 15;
			this.spaceVehicleReport.Vehicle = null;
			// 
			// MogreCombatForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.spaceVehicleReport);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.btnRestart);
			this.Controls.Add(this.btnplay);
			this.Controls.Add(this.btnFastForward);
			this.Controls.Add(this.btnPreviousShip);
			this.Controls.Add(this.btnFreeCam);
			this.Controls.Add(this.btnNextShip);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "MogreCombatForm";
			this.Text = "Space Combat";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private Controls.GameButton btnRestart;
		private Controls.GameButton btnplay;
		private Controls.GameButton btnFastForward;
		private Controls.GameButton btnPreviousShip;
		private Controls.GameButton btnFreeCam;
		private Controls.GameButton btnNextShip;
		private System.Windows.Forms.TextBox txtLog;
		private Controls.SpaceVehicleReport spaceVehicleReport;

	}
}