using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace FrEee.Avalonia.Views
{
	public class GameWindow : Window
	{
		public static GameWindow Instance { get; private set; }

		public GameWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			// this needs to be set in code and not the designer becuse the icon gets copied in from the FrEee.Assets project by the build
			// maybe this could be solved by making FrEee.Assets a shared project?
			Icon = new WindowIcon("Pictures/FrEee.ico");

			if (Instance is not null)
				throw new Exception("GameWindow.Instance is already set.");
			Instance = this;
		}

		public GameWindow(Control content)
			: this()
		{
			SetContent(null, content);
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		/// <summary>
		/// Sets content in a dock pane.
		/// </summary>
		/// <param name="dock">The docking direction, or null for the center.</param>
		/// <param name="control">The control to display.</param>
		public void SetContent(Dock? dock, Control control)
		{
			switch (dock)
			{
				case Dock.Left:
					left = control;
					break;
				case Dock.Right:
					right = control;
					break;
				case Dock.Top:
					top = control;
					break;
				case Dock.Bottom:
					bottom = control;
					break;
				case null:
					center = control;
					break;
			}

			RefreshDockedControls();
		}

		private void RefreshDockedControls()
		{
			var dockPanel = this.FindControl<DockPanel>("dockPanel");
			dockPanel.Children.Clear();
			if (left is not null)
			{
				dockPanel.Children.Add(left);
				left.SetValue(DockPanel.DockProperty, Dock.Left);
			}
			if (right is not null)
			{
				dockPanel.Children.Add(right);
				right.SetValue(DockPanel.DockProperty, Dock.Right);
			}
			if (top is not null)
			{
				dockPanel.Children.Add(top);
				top.SetValue(DockPanel.DockProperty, Dock.Top);
			}
			if (bottom is not null)
			{
				dockPanel.Children.Add(bottom);
				bottom.SetValue(DockPanel.DockProperty, Dock.Bottom);
			}
			if (center is not null)
			{
				dockPanel.Children.Add(center);
			}
		}

		private Control? left, right, top, bottom, center;

		public void Load(ViewLayer layer)
		{
			if (left is null && layer.LeftLoader is not null)
				left = layer.LeftLoader();
			else
				left = layer.Left;
			if (right is null && layer.RightLoader is not null)
				right = layer.RightLoader();
			else
				right = layer.Right;
			if (top is null && layer.TopLoader is not null)
				top = layer.TopLoader();
			else
				top = layer.Top;
			if (bottom is null && layer.BottomLoader is not null)
				bottom = layer.BottomLoader();
			else
				bottom = layer.Bottom;
			if (center is null && layer.CenterLoader is not null)
				center = layer.CenterLoader();
			else
				center = layer.Center;
			RefreshDockedControls();
		}
	}
}
