using System.Windows.Threading;
using FrEee.Game;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.Utility;
using System.Threading;

namespace FrEee.WinForms.Forms
{
	public partial class MainMenuForm : Form
	{
		private static MainMenuForm _instance;
		public static MainMenuForm GetInstance()
		{
			return _instance ?? (_instance = new MainMenuForm());
		}

		private MainMenuForm()
		{
			InitializeComponent();
			pictureBox1.Image = Image.FromFile(Properties.Resources.FrEeeSplash);
			Icon = new Icon(Properties.Resources.FrEeeIcon);
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				tblButtonPanel.Visible = !IsBusy;
				progressBar1.Visible = IsBusy;
			}
		}

		#region Button click handlers

		private void btnQuickStart_Click(object sender, EventArgs e)
		{
			IsBusy = true;

			//var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
			var status = new Status
			{
				Progress = 0d,
				Message = "Initializing",
				Exception = null,
			};
			//var task = Task.Factory.StartNewWithExceptionHandling(s =>
			//{
			Thread t = new Thread(new ThreadStart(() =>
			{
				//try
				{
					status.Message = "Loading mod";
					Mod.Load(null, true, status, 0.5);
					status.Message = "Setting up game";
					var gsu = new GameSetup
					{
						GalaxyTemplate = Mod.Current.GalaxyTemplates.PickRandom(),
						StarSystemCount = 50,
						GalaxySize = new System.Drawing.Size(40, 30),
						IsSinglePlayer = true,
					};
					status.Message = "Setting up galaxy";
					Galaxy.Initialize(gsu, status, 1.0);
					var name = Galaxy.Current.Name;
					var turn = Galaxy.Current.TurnNumber;
					status.Message = "Loading game";
					Galaxy.Load(name + "_" + turn + "_1.gam");
				}
				//catch (Exception ex)
				//{
					//status.Exception = ex;
				//}
			}));
			t.Start();
			while (t.IsAlive)
			{
				if (status.Exception != null)
				{
					Text = "FrEee - Error";
					MessageBox.Show(status.Exception.Message + "\n\nPlease check errorlog.txt for more details.");
					Enabled = true;
					IsBusy = false;
					progressBar1.Value = 0;
					var sw = new StreamWriter("errorlog.txt");
					sw.WriteLine(status.Exception.GetType().Name + " occurred at " + DateTime.Now + ":");
					sw.WriteLine(status.Exception.ToString());
					sw.Close();
					t.Abort();
					break;
				}
				else
				{
					Text = "FrEee - " + status.Message;
					progressBar1.Value = (int)(progressBar1.Maximum * status.Progress);
					Application.DoEvents();
				}
			}

			if (status.Exception == null)
			{
				var game = new GameForm(Galaxy.Current);
				game.Show();
				game.FormClosed += (s, args) =>
				{
					game.Dispose();
					Show();
					IsBusy = false;
				};
				Hide();
			}
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "Savegames (*.gam)|*.gam";
			dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Savegame");
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				Galaxy.Load(dlg.FileName);
				if (Galaxy.Current.CurrentEmpire == null)
				{
					// host view, prompt for turn processing
					if (MessageBox.Show("Process the turn for " + Galaxy.Current.Name + " stardate " + Galaxy.Current.Stardate + "?", "FrEee", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						// TODO - use multithreading to avoid locking the GUI when processing turns
						Cursor = Cursors.WaitCursor;
						Galaxy.Current.ProcessTurn();
						Galaxy.SaveAll();
						MessageBox.Show("Turn successfully processed. It is now stardate " + Galaxy.Current.Stardate + ".");
						Cursor = Cursors.Default;
					}
				}
				else
				{
					// player view, load up the game
					var form = new GameForm(Galaxy.Current);
					Hide();
					form.ShowDialog();
					Show();
				}
			}
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#endregion
	}
}
