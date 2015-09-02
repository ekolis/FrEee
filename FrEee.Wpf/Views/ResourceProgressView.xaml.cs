using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using FrEee.Utility;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Displays resource progress.
	/// </summary>
	public partial class ResourceProgressView
	{
		public ResourceProgressView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The orientation of this resource view.
		/// </summary>
		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ResourceProgressView), new PropertyMetadata(Orientation.Horizontal));

		public ResourceProgressViewModel ResourceProgress
		{
			get
			{
				return (ResourceProgressViewModel)DataContext;
			}
			set
			{
				DataContext = value;
			}
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// disable selection
			var lv = sender as ListView;
			if (lv.SelectedItems.Count > 0)
				lv.SelectedItems.Clear();
		}
	}
}
