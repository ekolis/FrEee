using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
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
			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
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

			string[] warnings = new string[0];
			Thread t = new Thread(new ThreadStart(() =>
			{
				try
				{
					if (Mod.Current == null)
					{
						status.Message = "Loading mod";
						Mod.Load(null, true, status, 0.5);
						if (Mod.Errors.Any())
						{
							Action a = delegate()
							{
								var doOrDie = this.ShowChildForm(new ModErrorsForm());
								if (doOrDie == DialogResult.Cancel)
									Close();
							};
							this.Invoke(a);
						}
					}
					status.Message = "Setting up game";
					var setup = GameSetup.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "GameSetups", "Quickstart.gsu"));
					warnings = setup.Warnings.ToArray();
					if (warnings.Any())
						MessageBox.Show(warnings.First(), "Game Setup Error");
					else
					{
						// TODO - let player pick his empire even with quickstart, replacing the player 1 empire?

						status.Message = "Setting up galaxy";
						Galaxy.Initialize(setup, status, 1.0);
						var name = Galaxy.Current.Name;
						var turn = Galaxy.Current.TurnNumber;
						status.Message = "Loading game";
						Galaxy.Load(name + "_" + turn + "_0001.gam");
					}
				}
				catch (Exception ex)
				{
					status.Exception = ex;
				}
			}));
			t.Name = "Game Setup";

			this.ShowChildForm(new StatusForm(t, status));

			if (status.Exception == null && !warnings.Any())
			{
				var game = new GameForm(false);
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
			// TODO - let player resume turn from previously saved PLR
			dlg.Filter = "Savegames (*.gam)|*.gam";
			dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Savegame");
			var result = dlg.ShowDialog();
			if (result == DialogResult.OK)
				LoadGalaxyFromFile(dlg.FileName);
		}

		private void LoadGalaxyFromFile(string filename, bool? loadPlr = null)
		{
			Cursor = Cursors.WaitCursor;
			var plrfile = Path.GetFileNameWithoutExtension(filename) + ".plr";
			Galaxy.Load(filename);
			if (Galaxy.Current.CurrentEmpire == null)
			{
				// host view, load host console
				Cursor = Cursors.WaitCursor;
				var form = new HostConsoleForm();
				Cursor = Cursors.Default;
				Hide();
				this.ShowChildForm(form);
				Show();
			}
			else
			{
				// player view, load up the game
				if (File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", plrfile)))
				{
					if (loadPlr == null)
						loadPlr = MessageBox.Show("Player commands file exists for this turn. Resume turn from where you left off?", "Resume Turn", MessageBoxButtons.YesNo) == DialogResult.Yes;
					if (loadPlr.Value)
						Galaxy.Current.LoadCommands();
				}
				var form = new GameForm(false);
				Cursor = Cursors.Default;
				Hide();
				form.ShowDialog();
				Show();
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
					if (Mod.Errors.Any())
					{
						var doOrDie = this.ShowChildForm(new ModErrorsForm());
						if (doOrDie == DialogResult.Cancel)
							Close();
					}
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
			var mostRecent = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame"))
				.Select(filePath => new KeyValuePair<string, DateTime>(filePath, File.GetLastWriteTime(filePath)))
				.OrderByDescending(kvp => kvp.Value)
				.Where(kvp => Regex.Match(kvp.Key, @"_\d+_\d+.gam$").Success)
				.ToList();
			if (mostRecent.Any())
				LoadGalaxyFromFile(mostRecent.First().Key, true);
			else
				MessageBox.Show("No games to resume; please create a new game.");
		}

		private void btnScenario_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, playing a scenario is not yet supported.");
		}

		private void btnCredits_Click(object sender, EventArgs e)
		{
			string credits;
			try
			{
				credits = File.ReadAllText("licenses/credits.txt");
			}
			catch
			{
				credits = "Could not open licenses/credits.txt.";
			}
			MessageBox.Show(credits, "FrEee v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
		}

		#endregion
	}
}
