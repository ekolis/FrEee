using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FrEee.UI.Blazor.Views;
using Microsoft.Extensions.DependencyInjection;
using BlazorResourceDisplay = FrEee.UI.Blazor.Views.ResourceDisplay;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using System.Collections.Generic;

namespace FrEee.UI.WinForms.Controls;

public partial class ResourceDisplay : UserControl
{
	public ResourceDisplay()
	{
		InitializeComponent();

		// set up Blazor
		var services = new ServiceCollection();
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "index.html";
		blazorView.Services = services.BuildServiceProvider();
		var parameters = new Dictionary<string, object?> { ["VM"] = VM };
		blazorView.RootComponents.Add<BlazorResourceDisplay>("#app", parameters);
		blazorView.Padding = new(0);
		blazorView.Margin = new(0);
	}

	private ResourceDisplayViewModel VM { get; } = new();

	#region viewmodel property wrappers for winforms

	public int Amount
	{
		get => VM.Amount;
		set => VM.Amount = value;
	}

	public int? Change
	{
		get => VM.Change;
		set => VM.Change = value;
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Resource? Resource
	{
		get => VM.Resource;
		set => VM.Resource = value;
	}

	public string? ResourceName
	{
		get => VM.Name;
		set => VM.Name = value;
	}
	#endregion
}