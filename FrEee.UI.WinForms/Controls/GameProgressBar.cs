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

		// set up Blazor
		var services = new ServiceCollection();
		vm = new ProgressBarViewModel
		{
			Value = Value,
			Increment = IncrementalProgress,
			Maximum = Maximum,
			BarColor = BarColor,
			LeftText = LeftText,
			CenterText = CenterText,
			RightText = RightText,
			OnClick = () => OnClick(new EventArgs())
		};
		services.AddSingleton(vm);
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "wwwroot\\index.html";
		blazorView.Services = services.BuildServiceProvider();
		blazorView.RootComponents.Add<BlazorProgressBar>("#app");
	}

	private ProgressBarViewModel vm;

	// TODO: make these properties just wrap the view model, no need for private fields
	public Color BarColor
	{
		get { return barColor; }
		set
		{
			vm.BarColor = barColor = value;
		}
	}

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
		get { return incrementalProgress; }
		set
		{
			vm.Increment = incrementalProgress = value;
		}
	}

	public string LeftText { get { return leftText; } set { vm.LeftText = leftText = value; } }

	public long Maximum
	{
		get { return maximum; }
		set
		{
			vm.Maximum = maximum = value;
		}
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

	public string RightText { get { return rightText; } set { vm.RightText = rightText = value; } }

	public long Value
	{
		get { return value; }
		set
		{
			vm.Value = this.value = value;
		}
	}

	private Color barColor = Color.Blue;

	private Color borderColor;

	private ProgressDisplayType displayType = ProgressDisplayType.Percentage;

	private long incrementalProgress = 0;

	private string leftText, rightText;

	private long maximum = 100;

	private long value = 0;

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