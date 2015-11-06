using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Interaction logic for StarSystemMapView.xaml
	/// </summary>
	public partial class StarSystemMapView
	{
		public StarSystemMapView()
		{
			InitializeComponent();
		}

		private void View_Loaded(object sender, RoutedEventArgs e)
		{
			if (StarSystem != null)
				StarSystem.PropertyChanged += StarSystem_PropertyChanged;
		}

		private void StarSystem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			InvalidateVisual();
		}

		public StarSystemMapViewModel StarSystem
		{
			get
			{
				return DataContext as StarSystemMapViewModel;
			}
			set
			{
				DataContext = value;
			}
		}

		private void canvas_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			// convert model to view model
			if (DataContext is StarSystem)
				DataContext = new StarSystemMapViewModel((StarSystem)DataContext);

			canvas.Children.Clear();

			if (StarSystem != null)
			{
				var w = canvas.ActualWidth / StarSystem.Diameter;
				var h = canvas.ActualHeight / StarSystem.Diameter;

				// TODO - place background

				foreach (var sector in StarSystem.Sectors)
				{
					// place sector view
					var sectorView = new SectorView { Sector = sector, Width = w, Height = h };
					canvas.Children.Add(sectorView);
					Canvas.SetLeft(sectorView, (sector.X + StarSystem.Radius) * w);
					Canvas.SetTop(sectorView, (sector.Y + StarSystem.Radius) * h);
					Canvas.SetZIndex(sectorView, 0);

					// place label
					if (sector.SpaceObjects.Any())
					{
						var label = new Label { Content = sector.LargestSpaceObject.Name, VerticalAlignment= VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Center, Height=12, FontSize=10};
						canvas.Children.Add(label);
						Canvas.SetLeft(label, (sector.X + StarSystem.Radius) * w - label.ActualWidth / 2);
						Canvas.SetTop(label, (sector.Y + StarSystem.Radius) * h);
						Canvas.SetZIndex(label, 1);
					}
				}
			}
		}
	}
}
