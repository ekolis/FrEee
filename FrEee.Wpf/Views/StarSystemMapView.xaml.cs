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

		private void grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			// convert model to view model
			if (DataContext is StarSystem)
				DataContext = new StarSystemMapViewModel((StarSystem)DataContext);

			if (StarSystem == null)
			{
				grid.Columns = 0;
				grid.Rows = 0;
			}
			else
			{
				grid.Columns = StarSystem.Diameter;
				grid.Rows = StarSystem.Diameter;
				foreach (var sector in StarSystem.Sectors.OrderBy(s => s.Y).ThenBy(s => s.X))
					grid.Children.Add(new SectorView { Sector = sector });
			}
		}
	}
}
