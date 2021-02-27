using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
			Icon = new WindowIcon("Pictures/FrEee.ico");
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
