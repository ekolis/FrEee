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
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Combat;

namespace FrEee.WinForms.Forms
{
	public partial class GameForm : Form
	{
		public GameForm(Galaxy galaxy)
		{
			InitializeComponent();
		}


		private void GameForm_Load(object sender, EventArgs e)
		{
			this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);
			this.Enabled = false;

			// set up GUI images
			// TODO - get GUI icons from mod too
			btnMenu.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Menu"));
			btnDesigns.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Designs"));
			btnPlanets.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Planets"));
			btnEmpires.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Empires"));
			btnShips.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Ships"));
			btnQueues.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Queues"));
			btnLog.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Log"));
			btnEndTurn.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "EndTurn"));
			btnMove.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Move"));
			btnPursue.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Pursue"));
			btnEvade.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Evade"));
			btnWarp.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Warp"));

			// set up GUI bindings to galaxy
			SetUpGui();

			Enabled = true;

			// show empire log if there's anything new there
			if (Empire.Current.Log.Any(m => m.TurnNumber == Galaxy.Current.TurnNumber))
				this.ShowChildForm(new LogForm(this));

			// so the search box can lose focus...
			foreach (Control ctl in tableLayoutPanel1.Controls)
			{
				ctl.Click += ctl_Click;
			}
		}

		void ctl_Click(object sender, EventArgs e)
		{
			((Control)sender).Focus();
		}

		private void starSystemView_SectorClicked(StarSystemView sender, Sector sector)
		{
			if (commandMode == CommandMode.Move)
			{
				if (sector != null)
				{
					// move ship to sector clicked
					if (SelectedSpaceObject is AutonomousSpaceVehicle)
					{
						var v = (AutonomousSpaceVehicle)SelectedSpaceObject;
						var order = new MoveOrder<AutonomousSpaceVehicle>(v, sector, !aggressiveMode);
						v.Orders.Add(order);
						var report = pnlDetailReport.Controls.OfType<AutonomousSpaceVehicleReport>().FirstOrDefault();
						if (report != null)
							report.Invalidate();
						var cmd = new AddOrderCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(Empire.Current, v, order);
						Empire.Current.Commands.Add(cmd);
					}
					else
					{
						// TODO - move orders for unit groups
					}
					ChangeCommandMode(CommandMode.None, null);
				}
			}
			else if (commandMode == CommandMode.Pursue)
			{
				// TODO - implement pursue orders
				MessageBox.Show("Sorry, pursue orders are not yet implemented.");
				ChangeCommandMode(CommandMode.None, null);
			}
			else if (commandMode == CommandMode.Evade)
			{
				// TODO - implement evade orders
				MessageBox.Show("Sorry, evade orders are not yet implemented.");
				ChangeCommandMode(CommandMode.None, null);
			}
			else if (commandMode == CommandMode.Warp)
			{
				if (sector != null && sector.SpaceObjects.OfType<WarpPoint>().Any())
				{
					// TODO - space object picker dialog
					var wp = sector.SpaceObjects.OfType<WarpPoint>().First();

					// warp
					if (SelectedSpaceObject is AutonomousSpaceVehicle)
					{
						var v = (AutonomousSpaceVehicle)SelectedSpaceObject;
						var order = new WarpOrder<AutonomousSpaceVehicle>(v, wp, !aggressiveMode);
						v.Orders.Add(order);
						var report = pnlDetailReport.Controls.OfType<AutonomousSpaceVehicleReport>().FirstOrDefault();
						if (report != null)
							report.Invalidate();
						var cmd = new AddOrderCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(Empire.Current, v, order);
						Empire.Current.Commands.Add(cmd);
					}
					else
					{
						// TODO - warp orders for unit groups
					}
					ChangeCommandMode(CommandMode.None, null);
				}
			}
			else if (commandMode == CommandMode.None)
			{
				// select the sector that was clicked
				starSystemView.SelectedSector = sector;
			}
		}

		private void starSystemView_SectorSelected(StarSystemView sender, Sector sector)
		{
			// remove old report, if any
			pnlDetailReport.Controls.Clear();

			if (sector == null)
			{
				SelectedSpaceObject = null;
				return;
			}

			if (sector.SpaceObjects.Count > 0)
			{
				// add new report
				Control newReport = null;
				if (sector.SpaceObjects.Count == 1)
				{
					SelectedSpaceObject = sector.SpaceObjects.Single();
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
					SelectedSpaceObject = null;
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
			else
				SelectedSpaceObject = null;
		}

		void SpaceObjectListReport_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var lv = (ListView)sender;
			var item = lv.GetItemAt(e.X, e.Y);
			if (item != null)
				SelectedSpaceObject = (ISpaceObject)item.Tag;
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
			if (starSystem != GetTabSystem(currentTab))
				SetTabSystem(currentTab, starSystem);
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
				// show empire log if there's anything new there
				if (Empire.Current.Log.Any(m => m.TurnNumber == Galaxy.Current.TurnNumber))
					this.ShowChildForm(new LogForm(this));
			}
		}

		private void SetUpGui()
		{
			// set title
			ChangeCommandMode(CommandMode.None, null);

			// select nothing
			SelectedSpaceObject = null;

			// display empire flag
			picEmpireFlag.Image = Galaxy.Current.CurrentEmpire.Flag;

			// create homesystem tab
			foreach (var tab in ListTabs().ToArray())
				RemoveTab(tab);
			SelectTab(AddTab(Galaxy.Current.CurrentEmpire.ExploredStarSystems.First()));

			// set up resource display
			resMin.Amount = Galaxy.Current.CurrentEmpire.StoredResources[Resource.Minerals];
			resMin.Change = Galaxy.Current.CurrentEmpire.Income[Resource.Minerals];
			resOrg.Amount = Galaxy.Current.CurrentEmpire.StoredResources[Resource.Organics];
			resOrg.Change = Galaxy.Current.CurrentEmpire.Income[Resource.Organics];
			resRad.Amount = Galaxy.Current.CurrentEmpire.StoredResources[Resource.Radioactives];
			resRad.Change = Galaxy.Current.CurrentEmpire.Income[Resource.Radioactives];
			resRes.Amount = Galaxy.Current.CurrentEmpire.Income[Resource.Research];
			resInt.Amount = Galaxy.Current.CurrentEmpire.Income[Resource.Intelligence];

			// show research progress
			BindResearch();

			// load space objects for search box
			searchBox.ObjectsToSearch = Galaxy.Current.FindSpaceObjects<ISpaceObject>().Flatten().Flatten();
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
				// TODO - use multithreading to prevent locking the GUI when processing turns
				Cursor = Cursors.WaitCursor;
				Enabled = false;
				var plrnum = Galaxy.Current.PlayerNumber;
				Galaxy.Load(Galaxy.Current.Name, Galaxy.Current.TurnNumber);
				Galaxy.Current.ProcessTurn();
				Galaxy.SaveAll();
				Galaxy.Load(Galaxy.Current.Name, Galaxy.Current.TurnNumber, plrnum);
				SetUpGui();
				Enabled = true;
				Cursor = Cursors.Default;
			}
			else
			{
				MessageBox.Show("Please send " + Galaxy.Current.CommandFileName + " to the game host.");
			}
		}

		public void SelectSpaceObject(ISpaceObject sobj)
		{
			var lookup = Galaxy.Current.FindSpaceObjects<ISpaceObject>(sobj2 => sobj2 == sobj);
			if (lookup.Any() && lookup.First().Any() && lookup.First().First().Any())
			{
				SelectStarSystem(lookup.First().Key.Item);
				SelectSector(lookup.First().First().First().Key);
				pnlDetailReport.Controls.Clear();
				var rpt = CreateSpaceObjectReport(sobj);
				if (rpt != null) // HACK - for warp points which still lack reports
					rpt.Dock = DockStyle.Fill;
				pnlDetailReport.Controls.Add(rpt);
			}
		}

		public void SelectStarSystem(StarSystem sys)
		{
			var tab = FindTab(sys);
			if (tab == null)
				tab = AddTab(sys);
			SelectTab(tab);
		}

		public void SelectSector(Point p)
		{
			starSystemView.SelectedSector = starSystemView.StarSystem.GetSector(p);
		}

		private void btnLog_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new LogForm(this));
		}

		#region Star System Tabs
		private FlowLayoutPanel currentTab;

		private FlowLayoutPanel AddTab(StarSystem sys = null)
		{
			var pnlTab = new FlowLayoutPanel();
			pnlTab.Padding = new Padding(0);
			pnlTab.Margin = new Padding(0);
			pnlTab.Height = 24;
			var btnTab = new GameButton();
			btnTab.Width = 98;
			var btnX = new GameButton();
			btnX.Width = 25;
			btnX.Text = "X";
			btnTab.Click += btnTab_Click;
			btnX.Click += btnX_Click;
			pnlTab.Controls.Add(btnTab);
			pnlTab.Controls.Add(btnX);
			SetTabSystem(pnlTab, sys);
			pnlTab.Controls.Remove(btnNewTab);
			pnlTabs.Controls.Add(pnlTab);
			pnlTabs.Controls.Add(btnNewTab);
			return pnlTab;
		}

		private void RemoveTab(FlowLayoutPanel tab)
		{
			if (currentTab == tab)
			{
				galaxyView.SelectedStarSystem = null;
				starSystemView.StarSystem = null;
				currentTab = null;
			}
			pnlTabs.Controls.Remove(tab);
		}

		private StarSystem GetTabSystem(FlowLayoutPanel tab)
		{
			if (tab == null)
				return null;
			return (StarSystem)tab.Controls[0].Tag;
		}

		private void SetTabSystem(FlowLayoutPanel tab, StarSystem sys)
		{
			var btnTab = (GameButton)tab.Controls[0];
			btnTab.Tag = sys;
			if (sys == null)
				btnTab.Text = "(No System)";
			else
				btnTab.Text = sys.Name;
			if (tab == currentTab)
				SelectTab(tab);
		}

		private FlowLayoutPanel FindTab(StarSystem sys)
		{
			foreach (FlowLayoutPanel tab in pnlTabs.Controls)
			{
				if (tab.Controls[0].Tag == sys)
					return tab;
			}
			return null;
		}

		private IEnumerable<FlowLayoutPanel> ListTabs()
		{
			return pnlTabs.Controls.OfType<FlowLayoutPanel>();
		}

		private void SelectTab(FlowLayoutPanel tab)
		{
			foreach (var tab2 in ListTabs())
			{
				// de-highlight tab
				tab2.Controls[0].BackColor = Color.Black;
			}
			// highlight tab
			tab.Controls[0].BackColor = Color.Navy;
			btnTab_Click(tab.Controls[0], new EventArgs());
		}

		void btnTab_Click(object sender, EventArgs e)
		{
			var btnTab = (GameButton)sender;
			currentTab = (FlowLayoutPanel)btnTab.Parent;
			var sys = (StarSystem)btnTab.Tag;
			if (galaxyView.SelectedStarSystem != sys)
				galaxyView.SelectedStarSystem = sys;
			if (starSystemView.StarSystem != sys)
				starSystemView.StarSystem = sys;
		}

		void btnX_Click(object sender, EventArgs e)
		{
			var btnX = (GameButton)sender;
			var pnlTab = (FlowLayoutPanel)btnX.Parent;
			RemoveTab(pnlTab);
		}

		private void btnNewTab_Click(object sender, EventArgs e)
		{
			SelectTab(AddTab(null));
		}
		#endregion

		private ISpaceObject selectedSpaceObject;
		public ISpaceObject SelectedSpaceObject
		{
			get { return selectedSpaceObject; }
			set
			{
				selectedSpaceObject = value;

				if (value != null)
				{
					var sys = value.FindStarSystem();
					if (galaxyView.SelectedStarSystem  != sys)
						galaxyView.SelectedStarSystem = sys;
					if (starSystemView.StarSystem != sys)
						starSystemView.StarSystem = galaxyView.SelectedStarSystem;

					var sector = value.FindSector();
					if (starSystemView.SelectedSector != sector)
						starSystemView.SelectedSector = sector;
				}

				// remove list view
				pnlDetailReport.Controls.Clear();

				// add new report
				if (value != null)
				{
					var rpt = CreateSpaceObjectReport(value);
					if (rpt != null) // HACK - for warp points which still lack reports
						rpt.Dock = DockStyle.Fill;
					pnlDetailReport.Controls.Add(rpt);
				}

				// show/hide command buttons
				if (value == null || value.Owner != Empire.Current)
				{
					// can't issue commands to objects we don't own
					foreach (GameButton btn in pnlSubCommands.Controls)
						btn.Visible = false;
				}
				else
				{
					// determine what commands are appropriate
					btnMove.Visible = value is IMobileSpaceObject;
					btnPursue.Visible = value is IMobileSpaceObject;
					btnEvade.Visible = value is IMobileSpaceObject;
					btnWarp.Visible = value is IMobileSpaceObject && ((IMobileSpaceObject)value).CanWarp;
					btnColonize.Visible = value is IMobileSpaceObject && ((IMobileSpaceObject)value).Abilities.Any(a => a.Name.StartsWith("Colonize Planet - "));
					btnConstructionQueue.Visible = value != null && value.ConstructionQueue != null;
					btnTransferCargo.Visible = value != null && (value.CargoStorage > 0 || value.SupplyStorage > 0 || value.HasInfiniteSupplies);
					btnFleetTransfer.Visible = value != null && value.CanBeInFleet;
					btnClearOrders.Visible = value is IMobileSpaceObject || value is Planet;
				}
			}
		}

		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.M && btnMove.Visible)
				ChangeCommandMode(CommandMode.Move, SelectedSpaceObject);
			if (e.KeyCode == Keys.P && btnPursue.Visible)
				ChangeCommandMode(CommandMode.Pursue, SelectedSpaceObject);
			if (e.KeyCode == Keys.V && btnEvade.Visible)
				ChangeCommandMode(CommandMode.Evade, SelectedSpaceObject);
			if (e.KeyCode == Keys.W && btnWarp.Visible)
				ChangeCommandMode(CommandMode.Warp, SelectedSpaceObject);
			else if (e.KeyCode == Keys.Back && btnClearOrders.Visible)
				ClearOrders();
			else if (e.KeyCode == Keys.Escape)
				ChangeCommandMode(CommandMode.None, null);
			else if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
				aggressiveMode = true;
		}

		private void GameForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
				aggressiveMode = false;
		}

		/// <summary>
		/// Aggressive movement mode. Unless this is set, movement orders will avoid enemies.
		/// </summary>
		private bool aggressiveMode;

		private CommandMode commandMode;

		private enum CommandMode
		{
			/// <summary>
			/// No command is being issued.
			/// </summary>
			None,
			/// <summary>
			/// Moves to a sector.
			/// </summary>
			Move,
			/// <summary>
			/// Pursues a target.
			/// </summary>
			Pursue,
			/// <summary>
			/// Evades a target.
			/// </summary>
			Evade,
			/// <summary>
			/// Moves to and traverses a warp point.
			/// </summary>
			Warp,
		}

		private void ChangeCommandMode(CommandMode mode, ISpaceObject sobj)
		{
			commandMode = mode;
			switch (mode)
			{
				case CommandMode.None:
					Text = "FrEee - " + Galaxy.Current.CurrentEmpire.Name + " - " + Galaxy.Current.CurrentEmpire.EmperorTitle + " " + Galaxy.Current.CurrentEmpire.EmperorName + " - " + Galaxy.Current.Stardate;
					break;
				case CommandMode.Move:
					Text = "Click a sector for " + sobj + " to move to. (Ctrl-click to move aggressively)";
					break;
				case CommandMode.Pursue:
					Text = "Click a space object for " + sobj + " to pursue.";
					break;
				case CommandMode.Evade:
					Text = "Click a space object for " + sobj + " to evade.";
					break;
				case CommandMode.Warp:
					Text = "Click a warp point for " + sobj + " to traverse. (Ctrl-click to move aggressively)";
					break;
			}
		}

		private void btnMove_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null)
				ChangeCommandMode(CommandMode.Move, SelectedSpaceObject);
		}

		private void btnPursue_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null)
				ChangeCommandMode(CommandMode.Pursue, SelectedSpaceObject);
		}

		private void btnEvade_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null)
				ChangeCommandMode(CommandMode.Evade, SelectedSpaceObject);
		}

		private void btnWarp_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null)
				ChangeCommandMode(CommandMode.Warp, SelectedSpaceObject);
		}

		private void btnColonize_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, colonize orders are not yet implemented.");
		}

		private void btnConstructionQueue_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null && SelectedSpaceObject.Owner == Empire.Current && SelectedSpaceObject.ConstructionQueue != null)
			{
				var form = new ConstructionQueueForm(SelectedSpaceObject.ConstructionQueue);
				this.ShowChildForm(form);
			}
		}

		private void btnTransferCargo_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, transfer cargo orders are not yet implemented.");
		}

		private void btnFleetTransfer_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Sorry, fleet transfer orders are not yet implemented.");
		}

		private void btnClearOrders_Click(object sender, EventArgs e)
		{
			ClearOrders();
		}

		/// <summary>
		/// Clears the selected space object's orders.
		/// </summary>
		private void ClearOrders()
		{
			if (SelectedSpaceObject.Owner == Empire.Current)
			{
				if (SelectedSpaceObject is AutonomousSpaceVehicle)
				{
					var v = (AutonomousSpaceVehicle)SelectedSpaceObject;
					v.Orders.Clear();
					var report = pnlDetailReport.Controls.OfType<AutonomousSpaceVehicleReport>().FirstOrDefault();
					if (report != null)
						report.Invalidate();
				}
				else if (SelectedSpaceObject is Planet)
				{
					var p = (Planet)SelectedSpaceObject;
					p.Orders.Clear();
					var report = pnlDetailReport.Controls.OfType<PlanetReport>().FirstOrDefault();
					if (report != null)
						report.Invalidate();
				}
			}
		}

		private void progResearch_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new ResearchForm());
			BindResearch();
		}

		private void BindResearch()
		{
			if (Empire.Current.ResearchSpending.Any() || Empire.Current.ResearchQueue.Any())
			{
				var techs = Empire.Current.ResearchSpending.Keys.Union(Empire.Current.ResearchQueue);
				var minEta = techs.Min(t => t.Progress.Eta);
				var tech = techs.Where(t => t.Progress.Eta == minEta).First();

				progResearch.Progress = tech.Progress;
				progResearch.LeftText = tech.Name + " L" + (tech.CurrentLevel + 1);
				if (tech.Progress.Eta == null)
					progResearch.RightText = "Never";
				else
					progResearch.RightText = tech.Progress.Eta + " turns";
			}
			else
			{
				progResearch.Value = 0;
				progResearch.Maximum = 1;
				progResearch.LeftText = "No Research - Click to Begin";
				progResearch.RightText = "";
			}
		}

		private int GetTotalSpending(Technology t)
		{
			var budget = Empire.Current.Income[Resource.Research];
			var forQueue = 100 - Empire.Current.ResearchSpending.Sum(kvp => kvp.Value);
			return t.Spending.Value * budget / 100 + (Empire.Current.ResearchQueue.FirstOrDefault() == t ? forQueue : 0);
		}

		public void ShowResearchForm(ResearchForm f)
		{
			this.ShowChildForm(f);
			BindResearch();
		}

		public void ShowVehicleDesignForm(VehicleDesignForm f)
		{
			this.ShowChildForm(f);
		}

		public void ShowLogForm(LogForm f)
		{
			this.ShowChildForm(f);
		}

		private void searchBox_ObjectSelected(SearchBox sender, ISpaceObject sobj)
		{
			SelectedSpaceObject = sobj;
			searchBox.HideResults();
		}
	}
}
