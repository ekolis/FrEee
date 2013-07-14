namespace FrEee.WinForms.Forms
{
	partial class StatusForm
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
			this.progressBar = new FrEee.WinForms.Controls.GameProgressBar();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.BackColor = System.Drawing.Color.Black;
			this.progressBar.BarColor = System.Drawing.Color.Blue;
			this.progressBar.BorderColor = System.Drawing.Color.Empty;
			this.progressBar.ForeColor = System.Drawing.Color.White;
			this.progressBar.IncrementalProgress = 0;
			this.progressBar.LeftText = null;
			this.progressBar.Location = new System.Drawing.Point(13, 13);
			this.progressBar.Margin = new System.Windows.Forms.Padding(0);
			this.progressBar.Maximum = 100;
			this.progressBar.Name = "progressBar";
			this.progressBar.Padding = new System.Windows.Forms.Padding(5);
			this.progressBar.RightText = null;
			this.progressBar.Size = new System.Drawing.Size(516, 28);
			this.progressBar.TabIndex = 0;
			this.progressBar.Value = 0;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// StatusForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(538, 50);
			this.ControlBox = false;
			this.Controls.Add(this.progressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StatusForm";
			this.Text = "Progress";
			this.Load += new System.EventHandler(this.ProgressForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.GameProgressBar progressBar;
		private System.Windows.Forms.Timer timer1;
	}
}