using System.Windows;
using System.Windows.Media;
using FrEee.Wpf.Views;
using WpfViewShells;

namespace FrEee.Wpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			// Ed has his own funky way of handling windows.
			ShutdownMode = ShutdownMode.OnExplicitShutdown;

			var shell = new Shell(new MainMenuView())
			{
				// TODO Temporary, for my amusement. Remove.
				AllowsTransparency = true,
				Background = Brushes.Transparent,
				WindowStyle = WindowStyle.None,
				ResizeMode = ResizeMode.NoResize,
				BorderBrush = null,
				BorderThickness = new Thickness(0),
				WindowStartupLocation = WindowStartupLocation.CenterScreen
			};

			MainWindow = shell;
			shell.Show();
		}
	}
}
