using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FrEee.UI.WinForms.Utility.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

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
				var parameters = new Dictionary<string, object?> { ["VM"] = VM };
				var blazorView = new BlazorWebView
				{
					HostPage = "index.html",
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
				blazorView.InitializeServices(); // the code in this method can't go here directly because it crashes th eWinForms designer
				blazorView.RootComponents.Add(new RootComponent("#app", BlazorComponentType, parameters));
				Controls.Add(blazorView);
			}
			catch
			{
				// Blazor could not be loaded, display a dummy label
				VM = new();
				Controls.Add(new Label { Text = "Blazor " + BlazorComponentType.Name, Dock = DockStyle.Fill });
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

		private void InitializeComponent()
		{

		}
	}
}
