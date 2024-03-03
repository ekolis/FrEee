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
using System.Threading.Tasks;
using FrEee.UI.WinForms.Forms;

namespace FrEee.UI.WinForms.Controls;

public partial class GameProgressBar : UserControl
{
	public GameProgressBar()
	{
		InitializeComponent();
		this.SizeChanged += GameProgressBar_SizeChanged;

		// set up view model
		ViewModel.OnClick = () =>
		{
			// HACK: https://github.com/MicrosoftEdge/WebView2Feedback/issues/3028#issuecomment-1461207168
			Task.Delay(0).ContinueWith(_ => MainGameForm.Instance.Invoke(() =>
			{
				OnClick(new EventArgs());
			}));
		};

		// set up Blazor
		var services = new ServiceCollection();
		services.AddSingleton(ViewModel);
		services.AddWindowsFormsBlazorWebView();
		blazorView.HostPage = "wwwroot\\index.html";
		blazorView.Services = services.BuildServiceProvider();
		blazorView.RootComponents.Add<BlazorProgressBar>("#app");
	}

	public ProgressBarViewModel ViewModel { get; } = new();

	public Color BarColor
	{
		get => ViewModel.BarColor;
		set => ViewModel.BarColor = value;
	}

	public long IncrementalProgress
	{
		get => ViewModel.Increment;
		set => ViewModel.Increment = value;
	}

	public string LeftText
	{
		get => ViewModel.LeftText;
		set => ViewModel.LeftText = value;
	}

	public long Maximum
	{
		get => ViewModel.Maximum;
		set => ViewModel.Maximum = value;
	}

	public Progress Progress
	{
		get => ViewModel.Progress;
		set => ViewModel.Progress = value;
	}

	public ProgressDisplayType ProgressDisplayType
	{
		get => ViewModel.ProgressDisplayType;
		set => ViewModel.ProgressDisplayType = value;
	}

	public string RightText
	{
		get => ViewModel.RightText;
		set => ViewModel.RightText = value;
	}

	public long Value
	{
		get => ViewModel.Value;
		set => ViewModel.Value = value;
	}

	private void GameProgressBar_SizeChanged(object sender, EventArgs e)
	{
		Invalidate();
	}
}