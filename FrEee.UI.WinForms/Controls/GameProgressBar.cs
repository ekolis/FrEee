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

namespace FrEee.UI.WinForms.Controls;

public partial class GameProgressBar : UserControl
{
	public GameProgressBar()
	{
		InitializeComponent();
		this.SizeChanged += GameProgressBar_SizeChanged;

		// set up Blazor
		var services = new ServiceCollection();
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "wwwroot\\index.html";
		blazorView.Services = services.BuildServiceProvider();
		InvalidateBlazor();
	}

	private void InvalidateBlazor()
	{
		if (blazorView.RootComponents.Any())
		{
			blazorView.RootComponents.Remove("#app");
		}
		blazorView.RootComponents.Add<BlazorProgressBar>("#app", new Dictionary<string, object?>
		{
			[nameof(BlazorProgressBar.Value)] = Value,
			[nameof(BlazorProgressBar.Increment)] = IncrementalProgress,
			[nameof(BlazorProgressBar.Maximum)] = Maximum,
			[nameof(BlazorProgressBar.BarColor)] = BarColor,
			[nameof(BlazorProgressBar.LeftText)] = LeftText,
			[nameof(BlazorProgressBar.CenterText)] = CenterText,
			[nameof(BlazorProgressBar.RightText)] = RightText,
			[nameof(BlazorProgressBar.OnClick)] = () => OnClick(new EventArgs())
		});
		Invalidate();
	}

	public Color BarColor
	{
		get { return barColor; }
		set
		{
			barColor = value;
			InvalidateBlazor();
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
			InvalidateBlazor();
		}
	}

	public long IncrementalProgress
	{
		get { return incrementalProgress; }
		set
		{
			incrementalProgress = value;
			InvalidateBlazor();
		}
	}

	public string LeftText { get { return leftText; } set { leftText = value; Invalidate(); } }

	public long Maximum
	{
		get { return maximum; }
		set
		{
			maximum = value;
			InvalidateBlazor();
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

	public ProgressDisplayType ProgressDisplayType
	{
		get { return displayType; }
		set
		{
			displayType = value;
			InvalidateBlazor();
		}
	}

	public string RightText { get { return rightText; } set { rightText = value; Invalidate(); } }

	public long Value
	{
		get { return value; }
		set
		{
			this.value = value;
			InvalidateBlazor();
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