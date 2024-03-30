using FrEee.UI.Blazor.Views;
using FrEee.UI.WinForms.Forms;
using FrEee.UI.WinForms.Utility.Extensions;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlazorPieChart = FrEee.UI.Blazor.Views.PieChart<int>;

namespace FrEee.UI.WinForms.Controls.Blazor;

public partial class PieChart : BlazorControl
{
	public PieChart()
	{
		InitializeComponent();

		// set up view model
		VM.OnClickEntry = (PieChartViewModel<int>.Entry entry) =>
		{
			// HACK: https://github.com/MicrosoftEdge/WebView2Feedback/issues/3028#issuecomment-1461207168
			Task.Delay(0).ContinueWith(_ => MainGameForm.Instance.Invoke(() =>
			{
				MessageBox.Show($"You clicked {entry.Text} which is equal to {entry.Value}.");
			}));
		};
	}

	protected override Type BlazorComponentType { get; } = typeof(BlazorPieChart);

	protected override PieChartViewModel<int> VM { get; } = new();

	#region viewmodel property wrappers for winforms
	public ISet<PieChartViewModel<int>.Entry> Entries
	{
		get => VM.Entries;
		set => VM.Entries = value;
	}
    #endregion
}
