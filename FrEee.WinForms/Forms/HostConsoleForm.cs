using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Processes;
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
			empireStatusBindingSource.DataSource = The.Game.Empires.Select(e => new EmpireStatus(e));
			Text = "Host Console - " + The.Game.Name + " turn " + The.Game.TurnNumber;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, the scenario editor is not yet implemented.");
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
							var savefile = The.Game.GetGameSavePath(emp);
							try
							{
								Game.Load(savefile);
							}
							catch (IOException)
							{
								MessageBox.Show("Could not load " + savefile + ". Attempting to recreate player view.");
								The.Game.CurrentEmpire = emp;
								The.Game.Redact();
							}
						}
						else
						{
							// AI empires have no GAM files, so create their views in memory
							The.Game.CurrentEmpire = emp;
							The.Game.Redact();
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
			ProcessTurn(); 
			MessageBox.Show("Turn successfully processed. It is now turn " + The.Game.TurnNumber + " (stardate " + The.Game.Stardate + ").");
			Cursor = Cursors.Default;
			CacheGalaxy();
			Bind();
		}


		void ProcessTurn()
		{
			var status = new Status { Message = "Initializing" };
			var t = new Thread(new ThreadStart(() =>
			{
				status.Message = "Processing turn";
				var processor = new TurnProcessor(The.Game);
				processor.ProcessTurn(false, status, 0.5);
				The.Game.SaveAll(status, 1.0);
			}));
			this.ShowChildForm(new StatusForm(t, status));
		}

		private void btnToggleAI_Click(object sender, EventArgs e)
		{
			if (gridEmpires.SelectedRows.Count == 1)
			{
				var status = (EmpireStatus)gridEmpires.SelectedRows[0].DataBoundItem;
				var emp = status.Empire;
				emp.IsPlayerEmpire = !emp.IsPlayerEmpire;
				//ensure that the AI can actually do stuff by turning on all the ministers, or turn them off if the player is taking over. 
				if (emp.IsPlayerEmpire)
					emp.EnabledMinisters = new SafeDictionary<string, System.Collections.Generic.ICollection<string>>();
				else
					emp.EnabledMinisters = emp.AI?.MinisterNames ?? new SafeDictionary<string, System.Collections.Generic.ICollection<string>>(); 
				var saveStatus = new Status { Message = "Initializing" };
				var t = new Thread(new ThreadStart(() =>
				{
					saveStatus.Message = "Saving galaxy";
					The.Game.SaveAll(saveStatus, 1.0);
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
			serializedGalaxy = Serializer.SerializeToString(The.Game);
		}

		private void ReloadGalaxy()
		{
			The.Game = Game.LoadFromString(serializedGalaxy);
		}

		private void autoProcess_CheckedChanged(object sender, EventArgs e)
		{
			autoProcessTimer.Enabled = autoProcess.Checked; 
		}

		private void autoProcessTimer_Tick(object sender, EventArgs e)
		{
			var empStatuses = gridEmpires.Rows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem).Cast<EmpireStatus>();
			if (empStatuses.Any(s => s.PlrUploadStatus == PlrUploadStatus.NotUploaded) 
				|| empStatuses.All(s => s.PlrUploadStatus == PlrUploadStatus.Defeated))
			{
				return;
			}
			autoProcessTimer.Stop(); 
			ProcessTurn();
			Cursor = Cursors.Default;
			CacheGalaxy();
			Bind();
			autoProcessTimer.Start(); 
		}
	}
}
