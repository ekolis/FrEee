using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Interaction logic for ProgressView.xaml
	/// </summary>
	public partial class StatusView
	{
		public StatusView(Thread thread, Status status)
		{
			InitializeComponent();
			this.thread = thread;
			this.status = status;
			this.status.Changed += Status_Changed;
			bar.Minimum = 0d;
			bar.Maximum = 1d;
			AllowClose = false;
		}

		private void Status_Changed()
		{

			try
			{
				Dispatcher.Invoke(() =>
				{
					bar.Value = status.Progress;
					lbl.Content = status.Message;
					if (status.Progress >= 1d)
						ForceClose();
					InvalidateVisual();
				});
			}
			catch (TaskCanceledException ex)
			{
				Dispatcher.Invoke(() =>
				{
					ForceClose();
				});
			}
		}

		private Thread thread;
		private Status status;

		private void View_Loaded(object sender, RoutedEventArgs e)
		{
			thread.Start();
		}
	}
}
