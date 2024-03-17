using FrEee.Utility;
using FrEee.UI.WinForms.Interfaces;
using System.Windows.Forms;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using BlazorResourceQuantityDisplay = FrEee.UI.Blazor.Views.ResourceQuantityDisplay;
using FrEee.UI.Blazor.Views;

namespace FrEee.UI.WinForms.Controls;

public partial class ResourceQuantityDisplay : BlazorControl<BlazorResourceQuantityDisplay, ResourceQuantityDisplayViewModel>
{
	public ResourceQuantityDisplay()
	{
		InitializeComponent();
	}

	public ResourceQuantity ResourceQuantity
	{
		get => VM.Amounts;
		set => VM.Amounts = value;
	}
}