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
using FrEee.Game.Enumerations;

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

		#region Button click handlers

		private void btnQuickStart_Click(object sender, EventArgs e)
		{
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
					if (Mod.Current == null)
					{
						status.Message = "Loading mod";
						Mod.Load(null, true, status, 0.5);
					}
					status.Message = "Setting up game";
					var setup = new GameSetup
					{
						GameName = "Quickstart",
						GalaxyTemplate = Mod.Current.GalaxyTemplates.PickRandom(),
						StarSystemCount = 5,
						GalaxySize = new System.Drawing.Size(40, 30),
						StarSystemGroups = 1,
						WarpPointPlacementStrategy = EdgeAlignedWarpPointPlacementStrategy.Instance,
						StandardMiningModel = new MiningModel
						{
							ValuePercentageBonus = 1,
						},
						RemoteMiningModel = new MiningModel
						{
							ValuePercentageBonus = 1,
						},
						MinPlanetValue = 0,
						MinSpawnedPlanetValue = 0,
						HomeworldValue = 120,
						MaxSpawnedPlanetValue = 150,
						MaxPlanetValue = 250,
						MinAsteroidValue = 0,
						MinSpawnedAsteroidValue = 50,
						MaxSpawnedAsteroidValue = 300,
						StartingTechnologyLevel = StartingTechnologyLevel.Low,
						TechnologyCost = TechnologyCost.Low,
						StartingResources = (int)20e3,
						ResourceStorage = (int)50e3,
						StartingResearch = (int)20e3,
						HomeworldsPerEmpire = 1,
						HomeworldSize = StellarSize.Large,
						EmpirePlacement = EmpirePlacement.Equidistant,
						MaxHomeworldDispersion = 1,
						ScoreDisplay = ScoreDisplay.OwnOnlyNoRankings,
						EmpirePoints = 2000,
						RandomAIs = 3,
						MinorEmpires = 5,
					};
					// TODO - let player pick his empire even with quickstart
					setup.EmpireTemplates.Add(new EmpireTemplate
					{
						Name = "Jraenar Imperium",
						LeaderName = "Master General Jar-Nolath",
						IsPlayerEmpire = true,
						PrimaryRace = new Race
						{
							Name = "Jraenar",
							Color = Color.Red,
							NativeAtmosphere = "Hydrogen",
							NativeSurface = "Rock"
						}
					});

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
			t.Name = "Game Setup";

			this.ShowChildForm(new StatusForm(t, status));

			if (status.Exception == null)
			{
				var game = new GameForm(Galaxy.Current);
				game.Show();
				game.FormClosed += (s, args) =>
				{
					game.Dispose();
					Show();
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
						Cursor = Cursors.WaitCursor;
						var status = new Status { Message = "Initializing" };
						var t = new Thread(new ThreadStart(() =>
							{
								status.Message = "Processing turn";
								Galaxy.Current.ProcessTurn();
								status.Progress = 0.5;
								Galaxy.SaveAll(status);
							}));
						this.ShowChildForm(new StatusForm(t, status));
						MessageBox.Show("Turn successfully processed. It is now turn " + Galaxy.Current.TurnNumber + " (stardate " + Galaxy.Current.Stardate + ").");
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
			var form = new ModPickerForm();
			this.ShowChildForm(form);
			if (form.DialogResult == DialogResult.OK)
				LoadMod(form.ModPath);

		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			if (Mod.Current == null)
				LoadMod(null);
			this.ShowChildForm(new GameSetupForm());
		}

		private void LoadMod(string modPath)
		{
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
					Mod.Load(modPath, true, status, 1d);
				}
				catch (Exception ex)
				{
					status.Exception = ex;
				}
			}));
			t.Name = "Mod Loading";

			this.ShowChildForm(new StatusForm(t, status));

			Text = "FrEee - " + Mod.Current.Info.Name;
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
