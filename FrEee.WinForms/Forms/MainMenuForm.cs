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
			Task.Factory.StartNewWithExceptionHandling(() =>
			{
				Mod.Load(null);
				var gsu = new GameSetup
				{
					GalaxyTemplate = Mod.Current.GalaxyTemplates.PickRandom(),
					StarSystemCount = 50,
					GalaxySize = new System.Drawing.Size(40, 30)
				};
				Galaxy.Initialize(gsu);
				var name = Galaxy.Current.Name;
				var turn = Galaxy.Current.TurnNumber;
				Galaxy.Load(name + "_" + turn + "_1.gam");
			})
				.ContinueWithWithExceptionHandling(t =>
				{
					var game = new GameForm(Galaxy.Current);
					game.Show();
					game.FormClosed += (s, args) =>
					{
						game.Dispose();
						Galaxy.Current.SaveCommands();
						Show();
						IsBusy = false;
					};
					Hide();
				}, scheduler);
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
					Galaxy.Current.SaveCommands();
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
