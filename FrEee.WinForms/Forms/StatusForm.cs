using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class StatusForm : Form
	{
		public StatusForm(Thread thread, Status status)
		{
			InitializeComponent();
			this.thread = thread;
			this.status = status;
		}

		private Thread thread;
		private Status status;

		private void ProgressForm_Load(object sender, EventArgs e)
		{
			timer1.Start();
			thread.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (status.Exception != null)
			{
				Text = "Error";
				MessageBox.Show(status.Exception.Message + "\n\nPlease check errorlog.txt for more details.");
				Enabled = true;
				progressBar.Value = 0;
				status.Exception.Log();
				thread.Abort();
				timer1.Stop();
				Close();
			}
			else
			{
				Text = status.Message;
				progressBar.Value = (int)(progressBar.Maximum * status.Progress);
				Application.DoEvents();
				if (thread.ThreadState == ThreadState.Stopped)
					Close();
			}
		}
	}
}
