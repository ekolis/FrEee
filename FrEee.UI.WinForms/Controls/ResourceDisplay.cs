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

namespace FrEee.UI.WinForms.Controls;

public partial class ResourceDisplay : UserControl
{
	public ResourceDisplay()
	{
		InitializeComponent();

		// set up Blazor
		var services = new ServiceCollection();
		services.AddSingleton(ViewModel);
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "wwwroot\\index.html";
		blazorView.Services = services.BuildServiceProvider();
		blazorView.RootComponents.Add<BlazorResourceDisplay>("#app");
		blazorView.Padding = new(0);
		blazorView.Margin = new(0);
	}

	public ResourceDisplayViewModel ViewModel { get; } = new();

	public int Amount
	{
		get => ViewModel.Amount;
		set => ViewModel.Amount = value;
	}

	public int? Change
	{
		get => ViewModel.Change;
		set => ViewModel.Change = value;
	}

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Resource? Resource
	{
		get => ViewModel.Resource;
		set => ViewModel.Resource = value;
	}

	public Color ResourceColor => ViewModel.Color;

	public Image? ResourceIcon => ViewModel.Icon;

	public string? ResourceName
	{
		get => ViewModel.Name;
		set => ViewModel.Name = value;
	}
}