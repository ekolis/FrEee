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
using FrEee.WinForms.Utility.Extensions;
using System.Threading;
using System.Reflection;
using FrEee.Game.Setup;
using FrEee.Game.Setup.WarpPointPlacementStrategies;

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
				tblButtonPanel.Enabled = !IsBusy;
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
			Thread t = new Thread(new ThreadStart(() =>
			{
				try
				{
					status.Message = "Loading mod";
					Mod.Load(null, true, status, 0.5);
					status.Message = "Setting up game";
					var setup = new GameSetup
					{
						GameName = "Quickstart",
						GalaxyTemplate = Mod.Current.GalaxyTemplates.PickRandom(),
						StarSystemCount = 5,
						GalaxySize = new System.Drawing.Size(40, 30),
						IsSinglePlayer = true,
						StarSystemGroups = 1,
						WarpPointPlacementStrategy = EdgeAlignedWarpPointPlacementStrategy.Instance,
					};
					// TODO - pick random empires from a list
					// TODO - use empire templates
					setup.Empires.Add(new Empire { Name = "Jraenar Empire", ShipsetPath = "Jraenar", Color = Color.Red, EmperorTitle = "Master General", EmperorName = "Jar-Nolath" });
					setup.Empires.Add(new Empire { Name = "Eee Consortium", ShipsetPath = "Eee", Color = Color.Cyan });
					setup.Empires.Add(new Empire { Name = "Drushocka Empire", ShipsetPath = "Drushocka", Color = Color.Green });
					setup.Empires.Add(new Empire { Name = "Norak Ascendancy", ShipsetPath = "Norak", Color = Color.Blue });
					setup.Empires.Add(new Empire { Name = "Abbidon Enclave", ShipsetPath = "Abbidon", Color = Color.Orange });
					status.Message = "Setting up galaxy";
					Galaxy.Initialize(setup, status, 1.0);
					var name = Galaxy.Current.Name;
					var turn = Galaxy.Current.TurnNumber;
					status.Message = "Loading game";
					Galaxy.Load(name + "_" + turn + "_1.gam");
				}
				catch (Exception ex)
				{
					status.Exception = ex;
				}
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

		private void btnMods_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, loading custom mods is not yet supported. But you can edit the stock files, if you really want to...");
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			if (Mod.Current == null)
			{
				progressBar1.Visible = true;
				var status = new Status
				{
					Progress = 0d,
					Message = "Initializing",
					Exception = null,
				};
				Thread t = new Thread(new ThreadStart(() =>
				{
					try
					{
						status.Message = "Loading mod";
						Mod.Load(null, true, status, 1d);
					}
					catch (Exception ex)
					{
						status.Exception = ex;
					}
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
			}
			Text = "FrEee";
			progressBar1.Visible = false;
			this.ShowChildForm(new GameSetupForm());
		}

		private void btnResume_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, resuming your latest game is not yet supported. But you can load a game of your choosing using Load.");
		}

		private void btnScenario_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, playing a scenario is not yet supported.");
		}

		private void btnCredits_Click(object sender, EventArgs e)
		{
			MessageBox.Show(
@"Project Lead:
	James Phillips (Combat Wombat)

Programming:
	Ed Kolis (ekolis)
	Kevin Seitz (guttsy)

Art:
	James Phillips (Combat Wombat)

Special Thanks:
	Aaron Hall - For creating Space Empires!
	Nick Dumas (Suicide Junkie) - For suggesting the title!
	All the denizens of #spaceempires and spaceempires.net

FrEee is licensed under a Creative Commons Attribution License.", "FrEee v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
		}

		#endregion
	}
}
