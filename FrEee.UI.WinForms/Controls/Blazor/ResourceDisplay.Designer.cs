using FrEee.UI.WinForms.Controls;
namespace FrEee.UI.WinForms.Controls.Blazor;

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
		SuspendLayout();
		// 
		// blazorView
		// 
		blazorView.Dock = System.Windows.Forms.DockStyle.Fill;
		blazorView.Location = new System.Drawing.Point(0, 0);
		blazorView.Margin = new System.Windows.Forms.Padding(0);
		blazorView.Name = "blazorView";
		blazorView.Size = new System.Drawing.Size(150, 24);
		blazorView.StartPath = "/";
		blazorView.TabIndex = 0;
		blazorView.Text = "blazorWebView1";
		// 
		// ResourceDisplay
		// 
		BackColor = System.Drawing.Color.Black;
		Controls.Add(blazorView);
		ForeColor = System.Drawing.Color.White;
		Margin = new System.Windows.Forms.Padding(0);
		Name = "ResourceDisplay";
		Size = new System.Drawing.Size(150, 24);
		ResumeLayout(false);
	}

	#endregion

	private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorView;
}
