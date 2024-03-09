namespace FrEee.UI.WinForms.Controls;

partial class ResourceDisplay
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
		blazorView = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
		this.SuspendLayout();
		// 
		// blazorView
		// 
		blazorView.Dock = System.Windows.Forms.DockStyle.Fill;
		blazorView.Location = new System.Drawing.Point(0, 0);
		blazorView.Margin = new System.Windows.Forms.Padding(0);
		blazorView.Name = "blazorView";
		blazorView.Size = new System.Drawing.Size(429, 37);
		blazorView.StartPath = "/";
		blazorView.TabIndex = 0;
		blazorView.Text = "blazorWebView1";
		// 
		// ResourceDisplay
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		this.ForeColor = System.Drawing.Color.White;
		this.Margin = new System.Windows.Forms.Padding(0);
		this.Name = "ResourceDisplay";
		this.Size = new System.Drawing.Size(128, 20);
		this.ResumeLayout();

	}

	#endregion

	private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorView;
}
