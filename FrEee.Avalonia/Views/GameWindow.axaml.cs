using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace FrEee.Avalonia.Views
{
	public class GameWindow : Window
	{
		public GameWindow()
		{
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
			// this needs to be set in code and not the designer becuse the icon gets copied in from the FrEee.Assets project by the build
			// maybe this could be solved by making FrEee.Assets a shared project?
			Icon = new WindowIcon("Pictures/FrEee.ico");
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

		private Control left, right, top, bottom, center;
	}
}
