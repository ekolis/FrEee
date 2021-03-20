using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using FrEee.Avalonia.ViewModels;
using FrEee.Avalonia.Views;

namespace FrEee.Avalonia
{
	public class App : Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				var win = new GameWindow
				{
					DataContext = new MainMenuWindowViewModel(),
				};
				win.SetContent(null, new Label
				{
					Content = "Welcome to FrEee!",
					VerticalAlignment = VerticalAlignment.Center,
					HorizontalAlignment = HorizontalAlignment.Center,
				});
				desktop.MainWindow = win;
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}
