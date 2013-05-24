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

			var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
			var status = new Status
			{
				Progress = 0d,
				Message = "Initializing",
			};
			var task = Task.Factory.StartNewWithExceptionHandling(s =>
			{
				var st = (Status)s;
				st.Message = "Loading mod";
				Mod.Load(null, true, st, 0.5);
				st.Message = "Setting up game";
				var gsu = new GameSetup
				{
					GalaxyTemplate = Mod.Current.GalaxyTemplates.PickRandom(),
					StarSystemCount = 50,
					GalaxySize = new System.Drawing.Size(40, 30),
					IsSinglePlayer = true,
				};
				st.Message = "Setting up galaxy";
				Galaxy.Initialize(gsu, st, 1.0);
				var name = Galaxy.Current.Name;
				var turn = Galaxy.Current.TurnNumber;
				st.Message = "Loading game";
				Galaxy.Load(name + "_" + turn + "_1.gam");
			}, status)
				.ContinueWithWithExceptionHandling(t =>
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
				}, scheduler);
			while (!task.IsCompleted)
			{
				Text = "FrEee - " + status.Message;
				progressBar1.Value = (int)(progressBar1.Maximum * status.Progress);
				Application.DoEvents();
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
						Galaxy.Current.ProcessTurn();
						Galaxy.SaveAll();
						MessageBox.Show("Turn successfully processed. It is now stardate " + Galaxy.Current.Stardate + ".");
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
