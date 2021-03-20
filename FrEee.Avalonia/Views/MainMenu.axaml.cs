using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FrEee.Avalonia.Views
{
	public class MainMenu : UserControl
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
