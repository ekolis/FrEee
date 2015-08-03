using System.Windows;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Shell window which contains all FrEee views.
	/// </summary>
	public partial class Shell
	{
		public Shell(View view = null)
		{
			InitializeComponent();
			DataContext = this;
			View = view;
		}

		public View View
		{
			get { return (View)GetValue(ViewProperty); }
			set
			{
				SetValue(ViewProperty, value);
				Content = value;
			}
		}

		// Using a DependencyProperty as the backing store for View.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ViewProperty =
			DependencyProperty.Register("View", typeof(View), typeof(Shell), new PropertyMetadata(null));

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (View != null && !View.AllowClose)
				e.Cancel = true;
		}

		private void Window_Closed(object sender, System.EventArgs e)
		{
			if (View != null && View.CausesShutdown)
				App.Current.Shutdown();
		}
	}
}
