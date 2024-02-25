using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;
using FrEee.UI.Blazor;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using BlazorProgressBar = FrEee.UI.Blazor.Views.ProgressBar;
using System.Collections.Generic;
using System.Linq;
using FrEee.UI.Blazor.Views;

namespace FrEee.UI.WinForms.Controls;

public partial class GameProgressBar : UserControl
{
	public GameProgressBar()
	{
		InitializeComponent();
		this.SizeChanged += GameProgressBar_SizeChanged;

		// set up view model
		vm.CenterText = CenterText;
		vm.OnClick = () => OnClick(new EventArgs());

		// set up Blazor
		var services = new ServiceCollection();
		services.AddSingleton(vm);
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "wwwroot\\index.html";
		blazorView.Services = services.BuildServiceProvider();
		blazorView.RootComponents.Add<BlazorProgressBar>("#app");
	}

	private ProgressBarViewModel vm = new ProgressBarViewModel();

	public Color BarColor
	{
		get => vm.BarColor;
		set => vm.BarColor = value;
	}

	// TODO: put this in the view model
	/// <summary>
	/// Color of the border for BorderStyle.FixedSingle mode.
	/// </summary>
	public Color BorderColor
	{
		get { return borderColor; }
		set
		{
			borderColor = value;
			Invalidate();
		}
	}

	public long IncrementalProgress
	{
		get => vm.Increment;
		set => vm.Increment = value;
	}

	public string LeftText
	{
		get => vm.LeftText;
		set => vm.LeftText = value;
	}

	public long Maximum
	{
		get => vm.Maximum;
		set => vm.Maximum = value;
	}

	public Progress Progress
	{
		get
		{
			return new Progress(Value, Maximum, IncrementalProgress);
		}
		set
		{
			Value = value.Value;
			Maximum = value.Maximum;
			IncrementalProgress = value.IncrementalProgressBeforeDelay;
		}
	}

	// TODO: put this in the view model
	public ProgressDisplayType ProgressDisplayType
	{
		get { return displayType; }
		set
		{
			displayType = value;
			Invalidate();
		}
	}

	public string RightText
	{
		get => vm.RightText;
		set => vm.RightText = value;
	}

	public long Value
	{
		get => vm.Value;
		set => vm.Value = value;
	}

	private Color borderColor;

	private ProgressDisplayType displayType = ProgressDisplayType.Percentage;

	private void GameProgressBar_SizeChanged(object sender, EventArgs e)
	{
		Invalidate();
	}

	// TODO: put this in the view model
	private string CenterText
	{
		get
		{
			switch (ProgressDisplayType)
			{
				case ProgressDisplayType.None:
					return "";
				case ProgressDisplayType.Percentage:
					return Math.Round(((double)Value / (double)Maximum * 100)) + "%";
				case ProgressDisplayType.Numeric:
					return Value.ToUnitString(true) + " / " + Maximum.ToUnitString(true);
				case ProgressDisplayType.Both:
					return Math.Round(((double)Value / (double)Maximum * 100)) + "% (" + Value.ToUnitString(true) + " / " + Maximum.ToUnitString(true) + ")";
				default:
					return "";
			}
		}
	}
}

[Flags]
public enum ProgressDisplayType
{
	None = 0,
	Percentage = 1,
	Numeric = 2,
	Both = 3,
}