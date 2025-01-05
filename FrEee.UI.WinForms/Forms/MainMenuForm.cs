using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Objects;
using FrEee.UI.WinForms.Utility;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using FrEee.Objects.GameState;
using FrEee.Processes.Setup;
using FrEee.Modding.Loaders;
using FrEee.Vehicles;

namespace FrEee.UI.WinForms.Forms;

public partial class MainMenuForm : GameForm
{
	private static MainMenuForm _instance;

	public static MainMenuForm GetInstance()
	{
		return _instance ?? (_instance = new MainMenuForm());
	}

	private MainMenuForm()
	{
		InitializeComponent();
		pictureBox1.Image = Properties.Resources.Splash;
		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
	}

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
#if RELEASE
			try
			{
#endif
			bool doOrDie = true;
			if (Mod.Current == null)
			{
				status.Message = "Loading mod";
				new ModLoader().Load(null, true, status, 0.5);
				if (Mod.Errors.Any())
				{
					Action a = delegate ()
					{
						doOrDie = this.ShowChildForm(new ModErrorsForm()) == System.Windows.Forms.DialogResult.OK;
					};
					this.Invoke(a);
				}
			}
			if (doOrDie)
			{
				status.Message = "Setting up game";
				var setup = GameSetup.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "GameSetups", "Quickstart.gsu"));
				warnings = setup.Warnings.ToArray();
				if (warnings.Any())
					MessageBox.Show(warnings.First(), "Game Setup Error");
				else
				{
					var dlg = new OpenFileDialog();
					dlg.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Empires");
					dlg.Filter = "Empires (*.emp)|*.emp";
					var result = dlg.ShowDialog();
					if (result == DialogResult.OK)
					{
						// replace existing first player with selected empire
						var et = EmpireTemplate.Load(dlg.FileName);
						setup.EmpireTemplates.RemoveAt(0);
						setup.EmpireTemplates.Insert(0, et);

						// set race trait points to however many were spent
						setup.EmpirePoints = et.PointsSpent;
					}

					status.Message = "Setting up galaxy";
					Game.Initialize(setup, null, status, 1.0);
					var name = Game.Current.Name;
					var turn = Game.Current.TurnNumber;
					status.Message = "Loading game";
					Game.Load(name + "_" + turn + "_0001.gam");
				}
			}
#if RELEASE
		}
                catch (Exception ex)
		{
			status.Exception = ex;
		}
#endif
		}));
		t.Name = "Game Setup";
		t.SetApartmentState(ApartmentState.STA);

		this.ShowChildForm(new StatusForm(t, status));

		if (status.Exception == null && !warnings.Any())
		{
			DIRoot.Designs.ImportDesignsFromLibrary();
			var game = new MainGameForm(false, true);
			this.ShowChildForm(game);
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
		Game.Load(filename);
		if (Game.Current.CurrentEmpire == null)
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
					Game.Current.LoadCommands();
			}

			// load library designs
			DIRoot.Designs.ImportDesignsFromLibrary();

			// display game view
			var form = new MainGameForm(false, true);
			Cursor = Cursors.Default;
			Hide();
			this.ShowChildForm(form);
			Show();
		}
	}

	private void btnQuit_Click(object sender, EventArgs e)
	{
		DIRoot.Gui.Exit();
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

	// TODO - put this in a utility class somewhere
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
#if RELEASE
			try
			{
#endif
			status.Message = "Loading mod";
			new ModLoader().Load(modPath, true, status, 1d);
			this.Invoke(new Action(delegate ()
				{
					if (Mod.Errors.Any())
						this.ShowChildForm(new ModErrorsForm());
				}));
#if RELEASE
		}
			catch (Exception ex)
			{
				status.Exception = ex;
			}
#endif
		}));
		t.Name = "Mod Loading";

		this.ShowChildForm(new StatusForm(t, status));

		Text = "FrEee - " + Mod.Current.Info.Name;
	}

	private void btnResume_Click(object sender, EventArgs e)
	{
		string savegameDir = Program.GetPath(FrEeeConstants.SaveGameDirectory);
		string noSavesMessage = "No games to resume; please create a new game.";

		if (Directory.Exists(savegameDir))
		{
			// Savegame folder exists, find recent saves
			var mostRecent = Directory.GetFiles(savegameDir)
				   .Select(filePath => new KeyValuePair<string, DateTime>(filePath, File.GetLastWriteTime(filePath)))
				   .OrderByDescending(kvp => kvp.Value)
				   .Where(kvp => Regex.Match(kvp.Key, @"_\d+_\d+.gam$").Success)
				   .ToList();
			if (mostRecent.Any())
				LoadGalaxyFromFile(mostRecent.First().Key, true);
			else
				MessageBox.Show(noSavesMessage);
		}
		else
		{
			// Savegame folder doesn't exist, create it
			Directory.CreateDirectory(savegameDir);
			MessageBox.Show(noSavesMessage);
		}
	}

	private void btnScenario_Click(object sender, EventArgs e)
	{
		MessageBox.Show("Sorry, playing a scenario is not yet supported.");
	}

	private void btnOptions_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new OptionsForm());
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

	private void MainMenuForm_VisibleChanged(object sender, EventArgs e)
	{
		if (Visible)
			Music.Play(MusicMode.Menu, Enum.GetValues(typeof(MusicMood)).Cast<MusicMood>().PickRandom());
	}

	private void MainMenuForm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Shift && e.KeyCode == Keys.Oem3) // tilde
			this.ShowChildForm(new DebugForm());
	}

	private void MainMenuForm_Load(object sender, EventArgs e)
	{
		try
		{
			ClientSettings.Load();
		}
		catch (Exception)
		{
			MessageBox.Show("Error loading client settings. Resetting to defaults.");
			ClientSettings.Initialize();
			ClientSettings.Save();
		}
		// set the default music volume according to the settings
		// volume values are 0-100, so scale appropriately to the 0-1 range
		Music.setVolume(ClientSettings.Instance.MasterVolume * ClientSettings.Instance.MusicVolume * 1.0e-4f);
	}

	private void MainMenuForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		ClientSettings.Save();
		DIRoot.Gui.Exit();
	}
}
