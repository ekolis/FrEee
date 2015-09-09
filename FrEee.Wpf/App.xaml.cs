using System.IO;
using System.Reflection;
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
			new MainMenuView().ShowDialog(WindowStartupLocation.CenterScreen);
		}

		public static string RootDirectory
		{
			get
			{
				return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			}
		}

		public static Brush BrightBrightBrush
		{
			get
			{
				return Application.Current.Resources["GameBrightBrightBrush"] as Brush;
            }
		}
	}
}
