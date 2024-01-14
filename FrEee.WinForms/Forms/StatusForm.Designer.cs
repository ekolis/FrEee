namespace FrEee.WinForms.Forms;

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
		this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
		this.progressBar.ForeColor = System.Drawing.Color.White;
		this.progressBar.IncrementalProgress = 0;
		this.progressBar.LeftText = null;
		this.progressBar.Location = new System.Drawing.Point(0, 0);
		this.progressBar.Margin = new System.Windows.Forms.Padding(10);
		this.progressBar.Maximum = 100;
		this.progressBar.Name = "progressBar";
		this.progressBar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
		this.progressBar.RightText = null;
		this.progressBar.Size = new System.Drawing.Size(717, 62);
		this.progressBar.TabIndex = 0;
		this.progressBar.Value = 0;
		// 
		// timer1
		// 
		this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
		// 
		// StatusForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.BackColor = System.Drawing.Color.Black;
		this.ClientSize = new System.Drawing.Size(717, 62);
		this.ControlBox = false;
		this.Controls.Add(this.progressBar);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(600, 100);
		this.Name = "StatusForm";
		this.ShowInTaskbar = false;
		this.Text = "Progress";
		this.Load += new System.EventHandler(this.ProgressForm_Load);
		this.ResumeLayout(false);

	}

	#endregion

	private Controls.GameProgressBar progressBar;
	private System.Windows.Forms.Timer timer1;
}