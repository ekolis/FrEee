using System.Windows;
using FrEee.Wpf.Views;

namespace FrEee.Wpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			new MainMenuView().ShowDialog();
		}
	}
}
