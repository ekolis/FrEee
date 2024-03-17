using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using static IronPython.Modules._ast;

namespace FrEee.UI.WinForms.Controls
{
	public partial class BlazorControl<TBlazorComponent, TViewModel> : Control
		where TBlazorComponent : ComponentBase
		where TViewModel: class, new()
	{
		public BlazorControl(TViewModel? viewModel = null)
		{
			if (viewModel is null) viewModel = new TViewModel();

			// set up control
			SuspendLayout();
			BackColor = Color.Black;
			ForeColor = Color.White;
			Margin = new(0);
			Name = "GameProgressBar";

			// set up Blazor
			try
			{
				VM = viewModel;
				var services = new ServiceCollection();
				services.AddWindowsFormsBlazorWebView();
				var parameters = new Dictionary<string, object?> { ["VM"] = VM };
				var blazorView = new BlazorWebView
				{
					HostPage = "index.html",
					Services = services.BuildServiceProvider(),
					Padding = new(0),
					Margin = new(0),
					Dock = DockStyle.Fill,
					Location = new(0, 0),
					Name = "blazorView",
					StartPath = "/",
					TabIndex = 0,
					Text = "blazorWebView1",
					AutoScroll = false
				};
				blazorView.RootComponents.Add<TBlazorComponent>("#app", parameters);
				Controls.Add(blazorView);
			}
			catch
			{
				// Blazor could not be loaded, display a dummy label
				VM = new();
				Controls.Add(new Label { Text = "Blazor Control"});
			}

			ResumeLayout(false);
		}

		public TViewModel VM { get; }
	}
}
