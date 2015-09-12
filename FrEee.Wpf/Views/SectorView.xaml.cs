using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Interaction logic for SectorView.xaml
	/// </summary>
	public partial class SectorView
	{
		public SectorView()
		{
			InitializeComponent();
		}

		private void View_Loaded(object sender, RoutedEventArgs e)
		{
			if (Sector != null)
				Sector.PropertyChanged += Sector_PropertyChanged;
		}

		private void Sector_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			InvalidateVisual();
		}

		public SectorViewModel Sector
		{
			get
			{
				return DataContext as SectorViewModel;
			}
			set
			{
				DataContext = value;
			}
		}

		private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{

		}
	}
}
