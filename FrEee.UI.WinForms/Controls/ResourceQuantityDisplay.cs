using FrEee.Utility;
using FrEee.UI.WinForms.Interfaces;
using System.Windows.Forms;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using BlazorResourceQuantityDisplay = FrEee.UI.Blazor.Views.ResourceQuantityDisplay;
using FrEee.UI.Blazor.Views;
using System;

namespace FrEee.UI.WinForms.Controls;

public partial class ResourceQuantityDisplay : BlazorControl
{
	public ResourceQuantityDisplay()
	{
		InitializeComponent();
	}

	protected override Type BlazorComponentType { get; } = typeof(BlazorResourceQuantityDisplay);

	protected override ResourceQuantityDisplayViewModel VM { get; } = new();

	#region viewmodel property wrappers for winforms
	public ResourceQuantity Amounts
	{
		get => VM.Amounts;
		set => VM.Amounts = value;
	}

	public ResourceQuantity Changes
	{
		get => VM.Changes;
		set => VM.Changes = value;
	}

	public IEnumerable<Resource> ResourcesToShow
	{
		get => VM.ResourcesToShow;
		set => VM.ResourcesToShow = value;
	}
	#endregion
}