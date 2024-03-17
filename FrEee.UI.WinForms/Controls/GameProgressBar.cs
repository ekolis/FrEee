using FrEee.Utility;
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

public partial class GameProgressBar : BlazorControl
{
	public GameProgressBar()
	{
		InitializeComponent();
		this.SizeChanged += GameProgressBar_SizeChanged;

		// set up view model
		VM.OnClick = () =>
		{
			// HACK: https://github.com/MicrosoftEdge/WebView2Feedback/issues/3028#issuecomment-1461207168
			Task.Delay(0).ContinueWith(_ => MainGameForm.Instance.Invoke(() =>
			{
				OnClick(new EventArgs());
			}));
		};
	}
	private void GameProgressBar_SizeChanged(object sender, EventArgs e)
	{
		Invalidate();
	}

	protected override Type BlazorComponentType { get; } = typeof(BlazorProgressBar);

	protected override ProgressBarViewModel VM { get; } = new();

	#region viewmodel property wrappers for winforms
	public Color BarColor
	{
		get => VM.BarColor;
		set => VM.BarColor = value;
	}

	public long IncrementalProgress
	{
		get => VM.Increment;
		set => VM.Increment = value;
	}

	public string LeftText
	{
		get => VM.LeftText;
		set => VM.LeftText = value;
	}

	public long Maximum
	{
		get => VM.Maximum;
		set => VM.Maximum = value;
	}

	public Progress Progress
	{
		get => VM.Progress;
		set => VM.Progress = value;
	}

	public ProgressDisplayType ProgressDisplayType
	{
		get => VM.ProgressDisplayType;
		set => VM.ProgressDisplayType = value;
	}

	public string RightText
	{
		get => VM.RightText;
		set => VM.RightText = value;
	}

	public long Value
	{
		get => VM.Value;
		set => VM.Value = value;
	}
	#endregion

}