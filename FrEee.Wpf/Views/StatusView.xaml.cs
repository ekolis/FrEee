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
			bar.Minimum = 0d;
			bar.Maximum = 1d;
			timer = new Timer(UpdateStatus, status, 0, 100);
			thread.Start();
		}

		private Timer timer;
		private Thread thread;
		private Status status;

		private void UpdateStatus(object o)
		{
			try
			{
				Dispatcher.Invoke(() =>
				{
					bar.Value = status.Progress;
					txt.Text = status.Message;
					if (thread.ThreadState == ThreadState.Stopped)
						Close();
				});
			}
			catch (TaskCanceledException ex)
			{
				timer.Dispose();
				Close();
			}
		}
	}
}
