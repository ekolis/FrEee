using FrEee.Game;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class GameSetupForm : Form
	{
		public GameSetupForm()
		{
			InitializeComponent();
			setup = new GameSetup();
			if (Mod.Current == null)
				Mod.Load(null);
			galaxyTemplateBindingSource.DataSource = Mod.Current.GalaxyTemplates;
			
			// initialize data
			ddlGalaxyType_SelectedIndexChanged(ddlGalaxyType, new EventArgs());
			spnWidth_ValueChanged(spnWidth, new EventArgs());
			spnHeight_ValueChanged(spnHeight, new EventArgs());
			spnStarSystems_ValueChanged(spnStarSystems, new EventArgs());
		}

		private GameSetup setup;

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			// TODO - don't add empires automatically, let the user specify them
			// TODO - use empire templates
			setup.Empires.Clear();
			setup.Empires.Add(new Empire { Name = "Jraenar Empire", ShipsetPath = "Jraenar", Color = Color.Red, EmperorTitle = "Master General", EmperorName = "Jar-Nolath" });
			setup.Empires.Add(new Empire { Name = "Eee Consortium", ShipsetPath = "Eee", Color = Color.Cyan });
			setup.Empires.Add(new Empire { Name = "Drushocka Empire", ShipsetPath = "Drushocka", Color = Color.Green });
			setup.Empires.Add(new Empire { Name = "Norak Ascendancy", ShipsetPath = "Norak", Color = Color.Blue });
			setup.Empires.Add(new Empire { Name = "Abbidon Enclave", ShipsetPath = "Abbidon", Color = Color.Orange });

			if (setup.Warnings.Any())
			{
				MessageBox.Show(setup.Warnings.First(), "FrEee");
				return;
			}

			progressBar.Visible = true;
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
					status.Message = "Setting up galaxy";
					Galaxy.Initialize(setup, status, 1d);
					if (Galaxy.Current.IsSinglePlayer)
					{
						var name = Galaxy.Current.Name;
						var turn = Galaxy.Current.TurnNumber;
						status.Message = "Loading game";
						Galaxy.Load(name + "_" + turn + "_1.gam");
					}
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
					progressBar.Value = 0;
					var sw = new StreamWriter("errorlog.txt");
					sw.WriteLine(status.Exception.GetType().Name + " occurred at " + DateTime.Now + ":");
					sw.WriteLine(status.Exception.ToString());
					sw.Close();
					t.Abort();
					break;
				}
				else
				{
					Text = "Game Setup - " + status.Message;
					progressBar.Value = (int)(progressBar.Maximum * status.Progress);
					Application.DoEvents();
				}
			}

			if (status.Exception == null)
			{
				if (Galaxy.Current.IsSinglePlayer)
				{
					var name = Galaxy.Current.Name;
					var turn = Galaxy.Current.TurnNumber;
					status.Message = "Loading game";
					Galaxy.Load(name + "_" + turn + "_1.gam");
					Hide();
					MainMenuForm.GetInstance().ShowChildForm(new GameForm(Galaxy.Current));
				}
				else
				{
					MessageBox.Show("The game \"" + Galaxy.Current.Name + "\" has been created. Please distribute the GAM files to appropriate players.");
					Close();
				}
			}
		}

		private void btnLoadSetup_Click(object sender, EventArgs e)
		{
			// TODO - load setup
			MessageBox.Show("Sorry, loading a game setup is not yet supported.");
		}

		private void btnSaveSetup_Click(object sender, EventArgs e)
		{
			// TODO - save setup
			MessageBox.Show("Sorry, saving a game setup is not yet supported.");
		}

		private void ddlGalaxyType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var gt = (GalaxyTemplate)ddlGalaxyType.SelectedItem;
			txtGalaxyTypeDescription.Text = gt.Description;
			setup.GalaxyTemplate = gt;
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
				tabs.Enabled = btnCancel.Enabled = btnStart.Enabled = btnLoadSetup.Enabled = btnSaveSetup.Enabled = !IsBusy;
				progressBar.Visible = IsBusy;
			}
		}

		private void spnWidth_ValueChanged(object sender, EventArgs e)
		{
			spnStarSystems.Maximum = spnWidth.Value * spnHeight.Value;
			setup.GalaxySize = new Size((int)spnWidth.Value, (int)spnHeight.Value);
		}

		private void spnHeight_ValueChanged(object sender, EventArgs e)
		{
			spnStarSystems.Maximum = spnWidth.Value * spnHeight.Value;
			setup.GalaxySize = new Size((int)spnWidth.Value, (int)spnHeight.Value);
		}

		private void spnStarSystems_ValueChanged(object sender, EventArgs e)
		{
			setup.StarSystemCount = (int)spnStarSystems.Value;
		}

		private void txtGalaxyName_TextChanged(object sender, EventArgs e)
		{
			setup.GameName = txtGalaxyName.Text;
		}
	}
}
