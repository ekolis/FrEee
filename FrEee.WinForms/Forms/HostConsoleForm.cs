using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	/// <summary>
	/// Form which allows the game host to perform various operations on a game,
	/// such as processing a turn or setting players to AI control.
	/// </summary>
	public partial class HostConsoleForm : GameForm
	{
		public HostConsoleForm()
		{
			InitializeComponent();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }

			Bind();

			// cache galaxy since we might be loading a player view
			CacheGalaxy();
		}

		private string serializedGalaxy;

		private void Bind()
		{
			empireStatusBindingSource.DataSource = Galaxy.Current.Empires.Select(e => new EmpireStatus(e));
			Text = "Host Console - " + Galaxy.Current.Name + " turn " + Galaxy.Current.TurnNumber;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, the scenario editor is not yet implemented.");
		}

		private void btnPatchMod_Click(object sender, EventArgs e)
		{
			var pickerForm = new ModPickerForm();
			var result = this.ShowChildForm(pickerForm);
			if (result == DialogResult.OK)
			{
				var status = new Status();
				var t = new Thread(new ThreadStart(() =>
				{
					bool doOrDie = true;
					this.Invoke(new Action(() => { Cursor = Cursors.WaitCursor; }));
					var mod = Mod.Load(pickerForm.ModPath, false, status, 0.5);
					if (Mod.Errors.Any())
						doOrDie = this.ShowChildForm(new ModErrorsForm()) == DialogResult.OK;
					if (doOrDie)
					{
						status.Message = "Backing up GAM file";
						File.Copy(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", Galaxy.Current.GameFileName), Path.Combine("Savegame", Galaxy.Current.GameFileName + ".bak"), true);
						// TODO - back up player GAM files too
						Mod.Current.Dispose();
						Mod.Current = mod;
						Mod.Patch();
						Galaxy.SaveAll(status);
					}
					else
					{
						status.Message = "Aborting";
						status.Progress = 1d;
					}
					this.Invoke(new Action(() => { Cursor = Cursors.Default; }));
				}));
				this.ShowChildForm(new StatusForm(t, status));
			}
		}

		private void btnPlayerView_Click(object sender, EventArgs e)
		{
			if (gridEmpires.SelectedRows.Count == 1)
			{
				var status = (EmpireStatus)gridEmpires.SelectedRows[0].DataBoundItem;
				var emp = status.Empire;
				if (emp.IsDefeated)
					MessageBox.Show(emp + " is defeated. There is nothing to view.");
				else
				{
					if (MessageBox.Show("Really show the player view for " + emp + "?\nThis is intended primarily for AI debugging, but it can also be used to cheat in multiplayer!", "Confirm Player View", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						if (emp.IsPlayerEmpire)
						{
							// load GAM file if possible
							var savefile = Galaxy.Current.GetGameSavePath(emp);
							try
							{
								Galaxy.Load(savefile);
							}
							catch (IOException)
							{
								MessageBox.Show("Could not load " + savefile + ". Attempting to recreate player view.");
								Galaxy.Current.CurrentEmpire = emp;
								Galaxy.Current.Redact();
							}
						}
						else
						{
							// AI empires have no GAM files, so create their views in memory
							Galaxy.Current.CurrentEmpire = emp;
							Galaxy.Current.Redact();
						}
					}
					Design.ImportFromLibrary();
					var form = new MainGameForm(true, false);
					Hide();
					this.ShowChildForm(form);
					Show();
					ReloadGalaxy();
					Bind(); // in case the host saved the commands
				}
			}
			else
				MessageBox.Show("Please select an empire in order to show the player view.");
		}

		private void btnProcess_Click(object sender, EventArgs e)
		{
			var empStatuses = gridEmpires.Rows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem).Cast<EmpireStatus>();
			if (empStatuses.Any(s => s.PlrUploadStatus == PlrUploadStatus.NotUploaded))
			{
				if (MessageBox.Show("Not all players have uploaded PLR files. Process the turn anyway?", "Confirm Processing", MessageBoxButtons.YesNo) == DialogResult.No)
					return;
			}
			var status = new Status { Message = "Initializing" };
			var t = new Thread(new ThreadStart(() =>
			{
				status.Message = "Processing turn";
				Galaxy.ProcessTurn(false, status, 0.5);
				Galaxy.SaveAll(status, 1.0);
			}));
			this.ShowChildForm(new StatusForm(t, status));
			MessageBox.Show("Turn successfully processed. It is now turn " + Galaxy.Current.TurnNumber + " (stardate " + Galaxy.Current.Stardate + ").");
			Cursor = Cursors.Default;
			CacheGalaxy();
			Bind();
		}

		private void btnToggleAI_Click(object sender, EventArgs e)
		{
			if (gridEmpires.SelectedRows.Count == 1)
			{
				var status = (EmpireStatus)gridEmpires.SelectedRows[0].DataBoundItem;
				var emp = status.Empire;
				emp.IsPlayerEmpire = !emp.IsPlayerEmpire;
				var saveStatus = new Status { Message = "Initializing" };
				var t = new Thread(new ThreadStart(() =>
				{
					saveStatus.Message = "Saving galaxy";
					Galaxy.SaveAll(saveStatus, 1.0);
				}));
				this.ShowChildForm(new StatusForm(t, saveStatus));
				CacheGalaxy();
				Bind();
			}
			else
				MessageBox.Show("Please select an empire in order to toggle AI status.");
		}

		private void CacheGalaxy()
		{
			serializedGalaxy = Serializer.SerializeToString(Galaxy.Current);
		}

		private void ReloadGalaxy()
		{
			Galaxy.LoadFromString(serializedGalaxy);
		}
	}
}