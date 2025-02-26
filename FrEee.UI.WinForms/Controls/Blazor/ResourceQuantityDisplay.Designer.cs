﻿using FrEee.UI.WinForms.Controls;
namespace FrEee.UI.WinForms.Controls.Blazor;

partial class ResourceQuantityDisplay
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
		blazorView.Size = new System.Drawing.Size(150, 150);
		blazorView.StartPath = "/";
		blazorView.TabIndex = 1;
		blazorView.Text = "blazorWebView1";
		// 
		// ResourceQuantityDisplay
		// 
		Controls.Add(blazorView);
		Name = "ResourceQuantityDisplay";
		ResumeLayout(false);
	}

	private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorView;
}
