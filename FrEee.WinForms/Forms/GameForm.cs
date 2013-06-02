using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Game;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Controls;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Game.Objects.Vehicles;

namespace FrEee.WinForms.Forms
{
	public partial class GameForm : Form
	{
		public GameForm(Galaxy galaxy)
		{
			InitializeComponent();

			this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);
			this.Enabled = false;

			// set up GUI images
			btnMenu.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Menu"));
			btnDesigns.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Designs"));
			btnPlanets.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Planets"));
			btnEmpires.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Empires"));
			btnShips.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Ships"));
			btnQueues.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Queues"));
			btnLog.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Log"));
			btnEndTurn.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "EndTurn"));

			// set up GUI bindings to galaxy
			SetUpGui();

			Enabled = true;
		}

		private void starSystemView_SectorClicked(StarSystemView sender, Sector sector)
		{
			// select the sector that was clicked
			starSystemView.SelectedSector = sector;
		}

		private void starSystemView_SectorSelected(StarSystemView sender, Sector sector)
		{
			// remove old report, if any
			pnlDetailReport.Controls.Clear();

			if (sector == null)
				return;

			if (sector.SpaceObjects.Count > 0)
			{
				// add new report
				Control newReport = null;
				if (sector.SpaceObjects.Count == 1)
				{
					// add new report
					newReport = CreateSpaceObjectReport(sector.SpaceObjects.Single());
				}
				else
				{
					// add list view
					var lv = new ListView();
					newReport = lv;
					lv.View = View.Tile;
					lv.BackColor = Color.Black;
					lv.ForeColor = Color.White;
					lv.BorderStyle = BorderStyle.None;
					var il = new ImageList();
					il.ColorDepth = ColorDepth.Depth32Bit;
					il.ImageSize = new Size(48, 48);
					lv.LargeImageList = il;
					lv.SmallImageList = il;
					int i = 0;
					foreach (var sobj in sector.SpaceObjects)
					{
						var item = new ListViewItem();
						item.Text = sobj.Name;
						item.Tag = sobj;
						if (sobj is Planet)
							il.Images.Add(sobj.Portrait.Resize(48).DrawPopulationBars((Planet)sobj));
						else
							il.Images.Add(sobj.Portrait.Resize(48));
						item.ImageIndex = i;
						i++;
						lv.Items.Add(item);
					}
					lv.MouseDoubleClick += SpaceObjectListReport_MouseDoubleClick;
				}

				if (newReport != null)
				{
					// align control
					pnlDetailReport.Controls.Add(newReport);
					newReport.Left = newReport.Margin.Left;
					newReport.Width = pnlDetailReport.Width - newReport.Margin.Right - newReport.Margin.Left;
					newReport.Top = newReport.Margin.Top;
					newReport.Height = pnlDetailReport.Height - newReport.Margin.Bottom - newReport.Margin.Top;
					newReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
				}
			}
		}

		void SpaceObjectListReport_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var lv = (ListView)sender;
			if (lv.SelectedItems.Count > 0)
			{
				// remove list view
				pnlDetailReport.Controls.Clear();

				// add new report
				var item = lv.GetItemAt(e.X, e.Y);
				if (item != null)
					pnlDetailReport.Controls.Add(CreateSpaceObjectReport((ISpaceObject)item.Tag));
			}
		}

		private Control CreateSpaceObjectReport(ISpaceObject sobj)
		{
			if (sobj is Star)
				return new StarReport((Star)sobj);
			if (sobj is Planet)
				return new PlanetReport((Planet)sobj);
			if (sobj is AsteroidField)
				return new AsteroidFieldReport((AsteroidField)sobj);
			if (sobj is Storm)
				return new StormReport((Storm)sobj);
			if (sobj is AutonomousSpaceVehicle)
				return new AutonomousSpaceVehicleReport((AutonomousSpaceVehicle)sobj);
			// TODO - warp point, fleet, unit group reports
			return null;
		}

		private void galaxyView_StarSystemClicked(GalaxyView sender, StarSystem starSystem)
		{
			if (starSystem != null)
				sender.SelectedStarSystem = starSystem;
		}

		private void galaxyView_StarSystemSelected(GalaxyView sender, StarSystem starSystem)
		{
			starSystemView.StarSystem = starSystem;
		}

		private void btnDesigns_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new DesignListForm());
		}

		private void btnPlanets_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new PlanetListForm());
		}

		private void btnQueues_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new ConstructionQueueListForm());
		}

		private void btnEndTurn_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Really end your turn now?", "FrEee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				EndTurn();
				if (!Galaxy.Current.IsSinglePlayer)
				{
					turnEnded = true;
					Close();
				}
			}
		}

		private void SetUpGui()
		{
			starSystemView.StarSystem = galaxyView.SelectedStarSystem = Galaxy.Current.CurrentEmpire.ExploredStarSystems.PickRandom();
			Text = "FrEee - " + Galaxy.Current.CurrentEmpire.Name + " - " + Galaxy.Current.CurrentEmpire.EmperorTitle + " " + Galaxy.Current.CurrentEmpire.EmperorName;
			picEmpireFlag.Image = Galaxy.Current.CurrentEmpire.Flag;
			txtGameDate.Text = Galaxy.Current.Stardate;

			// set up resource display
			resMin.Amount = Galaxy.Current.CurrentEmpire.StoredResources["Minerals"];
			resMin.Change = Galaxy.Current.CurrentEmpire.Income["Minerals"];
			resOrg.Amount = Galaxy.Current.CurrentEmpire.StoredResources["Organics"];
			resOrg.Change = Galaxy.Current.CurrentEmpire.Income["Organics"];
			resRad.Amount = Galaxy.Current.CurrentEmpire.StoredResources["Radioactives"];
			resRad.Change = Galaxy.Current.CurrentEmpire.Income["Radioactives"];
			resRes.Amount = Galaxy.Current.CurrentEmpire.Income["Research"];
			resInt.Amount = Galaxy.Current.CurrentEmpire.Income["Intelligence"];
		}

		private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!turnEnded)
			{
				switch (MessageBox.Show("Save your commands before quitting?", "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
				{
					case DialogResult.Yes:
						EndTurn();
						break;
					case DialogResult.No:
						break; // nothing to do here
					case DialogResult.Cancel:
						e.Cancel = true; // don't quit!
						break;
				}
			}
		}

		private bool turnEnded;

		private void EndTurn()
		{
			Galaxy.Current.SaveCommands();
			if (Galaxy.Current.IsSinglePlayer)
			{
				Enabled = false;
				var plrnum = Galaxy.Current.PlayerNumber;
				Galaxy.Load(Galaxy.Current.Name, Galaxy.Current.TurnNumber);
				Galaxy.Current.ProcessTurn();
				Galaxy.SaveAll();
				Galaxy.Load(Galaxy.Current.Name, Galaxy.Current.TurnNumber, plrnum);
				SetUpGui();
				Enabled = true;
			}
			else
			{
				MessageBox.Show("Please send " + Galaxy.Current.CommandFileName + " to the game host.");
			}
		}
	}
}
