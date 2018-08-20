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
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.minimap = new FrEee.WinForms.Controls.BattleView();
			this.battleView = new FrEee.WinForms.Controls.BattleView();
			this.btnClose = new FrEee.WinForms.Controls.GameButton();
			this.btnBack = new FrEee.WinForms.Controls.GameButton();
			this.btnPause = new FrEee.WinForms.Controls.GameButton();
			this.btnForward = new FrEee.WinForms.Controls.GameButton();
			this.SuspendLayout();
			// 
			// minimap
			// 
			this.minimap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.minimap.AutoZoom = true;
			this.minimap.BackColor = System.Drawing.Color.Black;
			this.minimap.Battle = null;
			this.minimap.FocusedLocation = null;
			this.minimap.IsPaused = false;
			this.minimap.Location = new System.Drawing.Point(749, 12);
			this.minimap.Name = "minimap";
			this.minimap.Round = 0;
			this.minimap.Size = new System.Drawing.Size(256, 256);
			this.minimap.TabIndex = 28;
			this.minimap.Text = "minimap";
			this.minimap.UseSquares = true;
			this.minimap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.minimap_MouseDown);
			// 
			// battleView
			// 
			this.battleView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.battleView.AutoZoom = false;
			this.battleView.BackColor = System.Drawing.Color.Black;
			this.battleView.Battle = null;
			this.battleView.FocusedLocation = null;
			this.battleView.IsPaused = false;
			this.battleView.Location = new System.Drawing.Point(12, 12);
			this.battleView.Name = "battleView";
			this.battleView.Round = 0;
			this.battleView.Size = new System.Drawing.Size(731, 709);
			this.battleView.TabIndex = 27;
			this.battleView.Text = "battleView";
			this.battleView.UseSquares = false;
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
			// btnBack
			// 
			this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBack.BackColor = System.Drawing.Color.Black;
			this.btnBack.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnBack.Location = new System.Drawing.Point(749, 274);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(75, 23);
			this.btnBack.TabIndex = 29;
			this.btnBack.Text = "|<";
			this.btnBack.UseVisualStyleBackColor = false;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnPause
			// 
			this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPause.BackColor = System.Drawing.Color.Black;
			this.btnPause.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnPause.Location = new System.Drawing.Point(830, 274);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(75, 23);
			this.btnPause.TabIndex = 30;
			this.btnPause.Text = "||";
			this.btnPause.UseVisualStyleBackColor = false;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnForward
			// 
			this.btnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnForward.BackColor = System.Drawing.Color.Black;
			this.btnForward.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.btnForward.Location = new System.Drawing.Point(911, 274);
			this.btnForward.Name = "btnForward";
			this.btnForward.Size = new System.Drawing.Size(75, 23);
			this.btnForward.TabIndex = 31;
			this.btnForward.Text = ">|";
			this.btnForward.UseVisualStyleBackColor = false;
			this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
			// 
			// BattleReplayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1017, 768);
			this.Controls.Add(this.battleView);
			this.Controls.Add(this.btnForward);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.minimap);
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
		private Controls.BattleView battleView;
		private Controls.BattleView minimap;
		private Controls.GameButton btnBack;
		private Controls.GameButton btnPause;
		private Controls.GameButton btnForward;
	}
}