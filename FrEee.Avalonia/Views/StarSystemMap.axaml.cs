using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FrEee.Avalonia.ViewModels;
using FrEee.Game.Objects.Space;

namespace FrEee.Avalonia.Views
{
	public class StarSystemMap : UserControl
	{
		public StarSystemMap()
		{
			InitializeComponent();
		}

		public StarSystemMap(StarSystem starSystem)
			: this()
		{
			DataContext = new StarSystemViewModel(starSystem);
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
