using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Threading;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class StatusForm : GameForm
	{
		public StatusForm(Thread thread, Status status)
		{
			InitializeComponent();
			this.thread = thread;
			this.status = status;
		}

		// Set the WS_EX_COMPOSITED flag on this window, enabling double buffering
		// and making child rendering flicker-free
		// see https://social.msdn.microsoft.com/Forums/windows/en-US/aaed00ce-4bc9-424e-8c05-c30213171c2c/flickerfree-painting?forum=winforms
		// for details on this fix
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;
				return cp;
			}
		}

		private string lastMessage;

		private Status status;

		private Thread thread;

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
				timer1.Stop();
				MessageBox.Show(status.Exception.Message + "\n\nPlease check errorlog.txt for more details.");
				Console.Error.WriteLine(status.Exception.ToString());
				Enabled = true;
				progressBar.Value = 0;
				status.Exception.LogFatal();
				thread.Abort();
				Close();
			}
			else
			{
				Text = status.Message;
				if (status.Message != lastMessage)
				{
					Console.WriteLine(status.Message);
					lastMessage = status.Message;
				}
				progressBar.Value = (int)(progressBar.Maximum * status.Progress);
				Application.DoEvents();
				if (thread.ThreadState == ThreadState.Stopped)
					Close();
			}
		}
	}
}