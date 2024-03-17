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

namespace FrEee.UI.WinForms.Controls
{
	public partial class BlazorControl : Control
	{
		public BlazorControl()
		{
			// set up control
			SuspendLayout();
			BackColor = Color.Black;
			ForeColor = Color.White;
			Margin = new(0);
			Name = "BlazorControl";

			// set up Blazor
			try
			{
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
				blazorView.RootComponents.Add(new RootComponent("#app", BlazorComponentType, parameters));
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

		/// <summary>
		/// The type of Blazor component to display. Should be overridden.
		/// </summary>
		protected virtual Type BlazorComponentType { get; } = typeof(ComponentBase);

		/// <summary>
		/// The view model for this Blazor control. Should be overridden.
		/// </summary>
		protected virtual object VM { get; } = new();
	}
}
