using FrEee.Utility;
using FrEee.UI.WinForms.Interfaces;
using System.Windows.Forms;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using BlazorResourceQuantityDisplay = FrEee.UI.Blazor.Views.ResourceQuantityDisplay;
using FrEee.UI.Blazor.Views;

namespace FrEee.UI.WinForms.Controls;

public partial class ResourceQuantityDisplay : UserControl
{
	public ResourceQuantityDisplay()
	{
		InitializeComponent();

		// set up Blazor
		var services = new ServiceCollection();
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "index.html";
		blazorView.Services = services.BuildServiceProvider();
		var parameters = new Dictionary<string, object?> { ["VM"] = VM };
		blazorView.RootComponents.Add<BlazorResourceQuantityDisplay>("#app", parameters);
		blazorView.Padding = new(0);
		blazorView.Margin = new(0);
	}

	private ResourceQuantityDisplayViewModel VM { get; } = new();

	public ResourceQuantity ResourceQuantity
	{
		get => VM.Amounts;
		set => VM.Amounts = value;
	}
}