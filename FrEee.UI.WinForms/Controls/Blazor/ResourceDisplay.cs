using FrEee.Utility;
using System;
using System.ComponentModel;
using FrEee.UI.Blazor.Views;
using BlazorResourceDisplay = FrEee.UI.Blazor.Views.ResourceDisplay;

namespace FrEee.UI.WinForms.Controls.Blazor;

public partial class ResourceDisplay : BlazorControl
{
	public ResourceDisplay()
	{
		InitializeComponent();
	}

	protected override Type BlazorComponentType { get; } = typeof(BlazorResourceDisplay);

	protected override ResourceDisplayViewModel VM { get; } = new();

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
