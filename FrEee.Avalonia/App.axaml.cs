using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
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
				var win = new GameWindow();
				var layer = new ViewLayer()
				{
					Left = new Image
					{
						Source = new Bitmap("Pictures/Splash.jpg")
					},
					Right = new MainMenu
					{
						DataContext = new MainMenuViewModel()
					},
				};
				ViewLayer.Push(layer);
				desktop.MainWindow = win;
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}
