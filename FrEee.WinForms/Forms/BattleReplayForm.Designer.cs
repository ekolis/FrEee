namespace FrEee.WinForms.Forms
{
	partial class BattleReplayForm
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
			this.btnClose = new FrEee.WinForms.Controls.GameButton();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.battleView1 = new FrEee.WinForms.Controls.BattleView();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.Color.Black;
			this.btnClose.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnClose.Location = new System.Drawing.Point(865, 727);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(140, 29);
			this.btnClose.TabIndex = 26;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// battleView1
			// 
			this.battleView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.battleView1.BackColor = System.Drawing.Color.Black;
			this.battleView1.Location = new System.Drawing.Point(12, 12);
			this.battleView1.Name = "battleView1";
			this.battleView1.Size = new System.Drawing.Size(993, 709);
			this.battleView1.TabIndex = 27;
			this.battleView1.Text = "battleView1";
			// 
			// BattleReplayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1017, 768);
			this.Controls.Add(this.battleView1);
			this.Controls.Add(this.btnClose);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "BattleReplayForm";
			this.ShowInTaskbar = false;
			this.Text = "Battle Replay";
			this.ResumeLayout(false);

		}

		#endregion
		private Controls.GameButton btnClose;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private Controls.BattleView battleView1;
	}
}