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
using System.Threading;
using FrEee.WinForms.Interfaces;
using FrEee.Game.Objects.Civilization.Diplomacy;

namespace FrEee.WinForms.Forms
{
	public partial class GameForm : Form
	{
		/// <summary>
		/// Creates a game form.
		/// </summary>
		/// <param name="hostView">Is this the host viewing a player's view? If so, we shouldn't attempt to process the turn after he clicks End Turn, even if the game is single player.</param>
		public GameForm(bool hostView)
		{
			InitializeComponent();
			this.hostView = hostView;
		}

		private bool hostView;

		private void GameForm_Load(object sender, EventArgs e)
		{
			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
			this.Enabled = false;

			// set up GUI images
			LoadButtonImage(btnMenu, "Menu");
			LoadButtonImage(btnDesigns, "Designs");
			LoadButtonImage(btnPlanets, "Planets");
			LoadButtonImage(btnEmpires, "Empires");
			LoadButtonImage(btnShips, "Ships");
			LoadButtonImage(btnQueues, "Queues");
			LoadButtonImage(btnLog, "Log");
			LoadButtonImage(btnEndTurn, "EndTurn");
			LoadButtonImage(btnMove, "Move");
			LoadButtonImage(btnPursue, "Pursue");
			LoadButtonImage(btnEvade, "Evade");
			LoadButtonImage(btnWarp, "Warp");
			LoadButtonImage(btnColonize, "Colonize");
			LoadButtonImage(btnSentry, "Sentry");
			LoadButtonImage(btnConstructionQueue, "ConstructionQueue");
			LoadButtonImage(btnTransferCargo, "TransferCargo");
			LoadButtonImage(btnClearOrders, "ClearOrders");
			LoadButtonImage(btnPrevIdle, "Previous");
			LoadButtonImage(btnNextIdle, "Next");

			// TODO - galaxy view background image can depend on galaxy template?
			galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine("Pictures", "UI", "Map", "quadrant"));

			// set up GUI bindings to galaxy
			SetUpGui();

			Enabled = true;

			// show empire log if there's anything new there
			if (Empire.Current.Log.Any(m => m.TurnNumber == Galaxy.Current.TurnNumber))
			{
				var form = new LogForm(this);
				form.StartPosition = FormStartPosition.CenterScreen;
				this.ShowChildForm(form);
			}

			// so the search box can lose focus...
			foreach (Control ctl in pnlLayout.Controls)
				AssignClickHandler(ctl);
		}

		private void AssignClickHandler(Control ctl)
		{
			ctl.Click += ctl_Click;
			if (ctl is ContainerControl)
			{
				foreach (Control c2 in ((ContainerControl)ctl).Controls)
					AssignClickHandler(c2);
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
				if (sector != null && sector.StarSystem.ExploredByEmpires.Contains(Empire.Current) && SelectedSpaceObject is IMobileSpaceObject)
				{
					// move ship to sector clicked
					var v = (IMobileSpaceObject)SelectedSpaceObject;
					var order = new MoveOrder<IMobileSpaceObject>(sector, !aggressiveMode);
					v.AddOrder(order);
					BindReport();
					var cmd = new AddOrderCommand<IMobileSpaceObject>(v, order);
					Empire.Current.Commands.Add(cmd);
					starSystemView.Invalidate(); // show move lines
					ChangeCommandMode(CommandMode.None, null);
				}
			}
			else if (commandMode == CommandMode.Pursue)
			{
				if (sector != null && sector.SpaceObjects.Any() && SelectedSpaceObject is IMobileSpaceObject)
				{
					ISpaceObject target = null;
					if (sector.SpaceObjects.Count() == 1)
						target = sector.SpaceObjects.Single();
					else
					{
						var form = new SpaceObjectPickerForm(sector.SpaceObjects);
						form.Text = "Pursue what?";
						this.ShowChildForm(form);
						if (form.DialogResult == DialogResult.OK)
							target = form.SelectedSpaceObject;
					}

					if (target != null)
					{
						// pursue
						IssueSpaceObjectOrder(new PursueOrder<IMobileSpaceObject>(target, !aggressiveMode));
						starSystemView.Invalidate(); // show move lines
						BindReport();
						starSystemView.Invalidate(); // show move lines
						ChangeCommandMode(CommandMode.None, null);
					}
				}
			}
			else if (commandMode == CommandMode.Evade)
			{
				if (sector != null && sector.SpaceObjects.Any() && SelectedSpaceObject is IMobileSpaceObject)
				{
					ISpaceObject target = null;
					if (sector.SpaceObjects.Count() == 1)
						target = sector.SpaceObjects.Single();
					else
					{
						var form = new SpaceObjectPickerForm(sector.SpaceObjects);
						form.Text = "Evade what?";
						this.ShowChildForm(form);
						if (form.DialogResult == DialogResult.OK)
							target = form.SelectedSpaceObject;
					}

					if (target != null)
					{
						// evade
						IssueSpaceObjectOrder(new EvadeOrder<IMobileSpaceObject>(target, !aggressiveMode));
						starSystemView.Invalidate(); // show move lines
						BindReport();
						starSystemView.Invalidate(); // show move lines
						ChangeCommandMode(CommandMode.None, null);
					}
				}
			}
			else if (commandMode == CommandMode.Warp)
			{
				if (sector != null && sector.SpaceObjects.OfType<WarpPoint>().Any() && SelectedSpaceObject is IMobileSpaceObject)
				{
					WarpPoint wp = null;
					if (sector.SpaceObjects.OfType<WarpPoint>().Count() == 1)
						wp = sector.SpaceObjects.OfType<WarpPoint>().Single();
					else
					{
						var form = new SpaceObjectPickerForm(sector.SpaceObjects.OfType<WarpPoint>());
						form.Text = "Use which warp point?";
						this.ShowChildForm(form);
						if (form.DialogResult == DialogResult.OK)
							wp = (WarpPoint)form.SelectedSpaceObject;
					}

					if (wp != null)
					{
						IssueSpaceObjectOrder(new PursueOrder<IMobileSpaceObject>(wp, !aggressiveMode));
						IssueSpaceObjectOrder(new WarpOrder<IMobileSpaceObject>(wp));
						BindReport();
						starSystemView.Invalidate(); // show move lines
						ChangeCommandMode(CommandMode.None, null);
					}
				}
			}
			else if (commandMode == CommandMode.Colonize)
			{
				if (SelectedSpaceObject is IMobileSpaceObject)
				{
					var v = (IMobileSpaceObject)SelectedSpaceObject;
					if (sector != null)
					{
						var suitablePlanets = sector.SpaceObjects.OfType<Planet>().Where(p => p.Colony == null && v.Abilities.Any(a => a.Rule.Matches("Colonize Planet - " + p.Surface)));
						if (Galaxy.Current.CanColonizeOnlyBreathable)
							suitablePlanets = suitablePlanets.Where(p => p.Atmosphere == Empire.Current.PrimaryRace.NativeAtmosphere);
						if (Galaxy.Current.CanColonizeOnlyHomeworldSurface)
							suitablePlanets = suitablePlanets.Where(p => p.Surface == Empire.Current.PrimaryRace.NativeSurface);
						if (suitablePlanets.Any())
						{
							Planet planet = null;
							if (suitablePlanets.Count() == 1)
								planet = suitablePlanets.Single();
							else
							{
								var form = new SpaceObjectPickerForm(suitablePlanets);
								form.Text = "Colonize which planet?";
								this.ShowChildForm(form);
								if (form.DialogResult == DialogResult.OK)
									planet = (Planet)form.SelectedSpaceObject;
							}

							if (planet != null)
							{
								if (!ModifierKeys.HasFlag(Keys.Shift))
								{
									// prefer population of breathers of target planet's atmosphere - don't load nonbreathers races if breathers are present
									bool foundBreathers = false;
									var planets = v.FinalSector().SpaceObjects.OfType<Planet>().Where(p => p.Owner == Empire.Current);
									foreach (var pHere in planets)
									{
										var delta = new CargoDelta();
										foreach (var kvp in pHere.AllPopulation)
										{
											if (kvp.Key.NativeAtmosphere == planet.Atmosphere)
											{
												delta.RacePopulation[kvp.Key] = null; // load all population of this race
												foundBreathers = true;
											}
										}
										if (foundBreathers)
										{
											var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
											IssueSpaceObjectOrder(loadPopOrder);
										}
									}
									if (!foundBreathers)
									{
										foreach (var pHere in planets)
										{
											var delta = new CargoDelta();
											delta.AnyPopulation = null;
											var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
											IssueSpaceObjectOrder(loadPopOrder);
										}
									}
								}
								IssueSpaceObjectOrder(new MoveOrder<IMobileSpaceObject>(sector, !aggressiveMode));
								IssueSpaceObjectOrder(new ColonizeOrder(planet));
								BindReport();
								ChangeCommandMode(CommandMode.None, null);
								starSystemView.Invalidate(); // refresh move lines
							}
						}
					}
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
				btnFleetTransfer.Visible = false;
				return;
			}

			btnFleetTransfer.Visible = sector.SpaceObjects.OfType<ISpaceVehicle>().Any(v => v.Owner == Empire.Current);

			if (sector.SpaceObjects.Any())
			{
				// add new report
				Control newReport = null;
				if (sector.SpaceObjects.Count() == 1)
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
			{
				SelectedSpaceObject = null;
				// TODO - show star system report
			}
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
			if (sobj is WarpPoint)
				return new WarpPointReport((WarpPoint)sobj);
			if (sobj is SpaceVehicle)
			{
				var r = new SpaceVehicleReport((SpaceVehicle)sobj);
				r.OrdersChanged += AutonomousSpaceVehicleReport_OrdersChanged;
				return r;
			};
			// TODO - fleet, unit group reports
			return null;
		}

		void AutonomousSpaceVehicleReport_OrdersChanged()
		{
			starSystemView.Invalidate(); // show move lines
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
			ShowConstructionQueueListForm();
		}

		private void btnEndTurn_Click(object sender, EventArgs e)
		{
			var todos = FindTodos();

			if (Galaxy.Current.IsSinglePlayer && !hostView)
			{
				var msg = !todos.Any() ? "Really end your turn now?" : "Really end your turn now? You have:\n\n" + string.Join("\n", todos.ToArray());
				if (MessageBox.Show(msg, "FrEee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					EndTurn();

					// show empire log if there's anything new there
					if (Empire.Current.Log.Any(m => m.TurnNumber == Galaxy.Current.TurnNumber))
						this.ShowChildForm(new LogForm(this));
				}
			}
			else
			{
				var msg = !todos.Any() ? "Save your commands before quitting?" : "Save your commands before quitting? You have:\n\n" + string.Join("\n", todos.ToArray());
				if (MessageBox.Show(msg, "FrEee", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					EndTurn();
					turnEnded = true;
					Close();
				}
			}
		}

		private IEnumerable<string> FindTodos()
		{
			var todos = new List<string>();

			var ships = Empire.Current.OwnedSpaceObjects.OfType<SpaceVehicle>().Where(v => v.Container == null && v.Speed > 0 && !v.Orders.Any()).Count();
			if (ships == 1)
				todos.Add("1 idle ship");
			else if (ships > 1)
				todos.Add(ships + " idle ships");

			var fleets = Empire.Current.OwnedSpaceObjects.OfType<Fleet>().Where(f => f.Container == null && f.Speed > 0 && !f.Orders.Any()).Count();
			if (fleets == 1)
				todos.Add("1 idle fleet");
			else if (fleets > 1)
				todos.Add(fleets + " idle fleets");

			var queues = Empire.Current.ConstructionQueues.Where(q => q.Eta < 1d).Count();
			if (queues == 1)
				todos.Add("1 idle construction queue");
			else if (ships > 1)
				todos.Add(queues + " idle construction queues");

			var idx = 0;
			var queueSpending = 0;
			var levels = new Dictionary<Technology, int>(Empire.Current.ResearchedTechnologies);
			Empire.Current.ComputeResearchProgress();
			foreach (var tech in Empire.Current.ResearchQueue)
			{
				levels[tech]++; // so we can research the same tech multiple times with the appropriate cost for each level
				queueSpending += tech.GetLevelCost(levels[tech]);
				idx++;
			}
			var totalRP = Empire.Current.NetIncome[Resource.Research] + Empire.Current.BonusResearch;
			var leftover = totalRP - queueSpending;
			var pctSpending = 0;
			foreach (var kvp in Empire.Current.ResearchSpending)
				pctSpending += leftover * kvp.Value;
			var totalSpending = queueSpending + pctSpending;
			var unallocatedPct = 1d - ((double)totalSpending / (double)totalRP);
			if (unallocatedPct > 0)
				todos.Add(unallocatedPct.ToString("0%") + " unallocated research");

			var messages = Empire.Current.IncomingMessages.OfType<ProposalMessage>().Count(m =>
				m.TurnNumber >= Galaxy.Current.TurnNumber - 1 && 
				!Empire.Current.Commands.OfType<SendMessageCommand>().Where(c => c.Message.InReplyTo == m).Any() &&
				!Empire.Current.Commands.OfType<DeleteMessageCommand>().Where(c => c.Message == m).Any());
			if (messages == 1)
				todos.Add("1 unresolved diplomatic proposal");
			else if (messages > 1)
				todos.Add(messages + " unresolved diplomatic proposals");

			return todos;
		}

		private void SetUpGui()
		{
			// set title
			ChangeCommandMode(CommandMode.None, null);

			// select nothing
			SelectedSpaceObject = null;

			// display empire flag
			picEmpireFlag.Image = Galaxy.Current.CurrentEmpire.Icon;

			// create homesystem tab
			foreach (var tab in ListTabs().ToArray())
				RemoveTab(tab);
			var highlyPopulated = Empire.Current.ExploredStarSystems.WithMax(sys => sys.FindSpaceObjects<Planet>(p => p.Owner == Empire.Current).Flatten().Sum(p => p.Colony.Population.Sum(kvp => kvp.Value)));
			if (highlyPopulated.Any())
				SelectTab(AddTab(highlyPopulated.First()));
			else
			{
				var withManyShips = Empire.Current.OwnedSpaceObjects.OfType<SpaceVehicle>().GroupBy(g => g.StarSystem).WithMax(g => g.Count());
				if (withManyShips.Any())
					SelectTab(AddTab(withManyShips.First().Key));
				else
					SelectTab(AddTab(Empire.Current.ExploredStarSystems.First()));
			}

			// set up resource display
			SetUpResourceDisplay();

			// show research progress
			BindResearch();

			// load space objects for search box
			if (!searchBox.IsDisposed)
				searchBox.ObjectsToSearch = Galaxy.Current.FindSpaceObjects<ISpaceObject>().Flatten().Flatten();

			// compute warp point connectivity
			galaxyView.ComputeWarpPointConnectivity();
		}

		private void SetUpResourceDisplay()
		{
			var income = Empire.Current.NetIncomeLessConstruction;
			resMin.Amount = Galaxy.Current.CurrentEmpire.StoredResources[Resource.Minerals];
			resMin.Change = income[Resource.Minerals];
			resOrg.Amount = Galaxy.Current.CurrentEmpire.StoredResources[Resource.Organics];
			resOrg.Change = income[Resource.Organics];
			resRad.Amount = Galaxy.Current.CurrentEmpire.StoredResources[Resource.Radioactives];
			resRad.Change = income[Resource.Radioactives];
			resRes.Amount = income[Resource.Research];
			if (Empire.Current.BonusResearch != 0)
				resRes.Change = Empire.Current.BonusResearch;
			resInt.Amount = income[Resource.Intelligence];
		}

		private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!turnEnded)
			{
				var todos = FindTodos();

				string msg;
				if (Galaxy.Current.IsSinglePlayer && !hostView)
					msg = !todos.Any() ? "Save your commands and process the turn before quitting?" : "Save your commands and process the turn before quitting? You have:\n\n" + string.Join("\n", todos.ToArray());
				else
					msg = !todos.Any() ? "Save your commands before quitting?" : "Save your commands before quitting? You have:\n\n" + string.Join("\n", todos.ToArray());
				
				switch (MessageBox.Show(msg, "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
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
			if (Galaxy.Current.IsSinglePlayer && !hostView)
			{
				Cursor = Cursors.WaitCursor;
				Enabled = false;
				var plrnum = Galaxy.Current.PlayerNumber;
				var status = new Status { Message = "Initializing" };
				var t = new Thread(new ThreadStart(() =>
				{
					status.Message = "Loading game";
					Galaxy.Load(Galaxy.Current.Name, Galaxy.Current.TurnNumber);
					status.Progress = 0.25;
					status.Message = "Processing turn";
					Galaxy.ProcessTurn(false, status, 0.5);
					status.Message = "Saving game";
					Galaxy.SaveAll(status, 0.75);
					status.Message = "Loading game";
					Galaxy.Load(Galaxy.Current.Name, Galaxy.Current.TurnNumber, plrnum);
					status.Progress = 1.00;
				}));
				this.ShowChildForm(new StatusForm(t, status));
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
				if (rpt != null)
					rpt.Dock = DockStyle.Fill;
				pnlDetailReport.Controls.Add(rpt);
				SelectedSpaceObject = sobj;
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
			btnTab.TabStop = false;
			btnTab.Width = 98;
			var btnX = new GameButton();
			btnX.TabStop = false;
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
			foreach (var tab in pnlTabs.Controls.OfType<FlowLayoutPanel>())
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
			btnTab_Click(tab.Controls[0], new EventArgs());
		}

		void btnTab_Click(object sender, EventArgs e)
		{
			var btnTab = (GameButton)sender;
			currentTab = (FlowLayoutPanel)btnTab.Parent;
			foreach (var tab2 in ListTabs())
			{
				// de-highlight tab
				tab2.Controls[0].BackColor = Color.Black;
			}
			// highlight tab
			currentTab.Controls[0].BackColor = Color.Navy;
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

				// for movement lines
				starSystemView.SelectedSpaceObject = value;

				if (value != null)
				{
					var sys = value.FindStarSystem();
					if (galaxyView.SelectedStarSystem != sys)
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
					if (rpt != null)
						rpt.Dock = DockStyle.Fill;
					pnlDetailReport.Controls.Add(rpt);
				}

				// show/hide command buttons
				if (value == null || value.Owner != Empire.Current)
				{
					// can't issue commands to objects we don't own
					foreach (GameButton btn in pnlSubCommands.Controls)
						btn.Visible = false;

					// fleet transfer button is special, it can be selected even with no space object selected
					btnFleetTransfer.Visible = starSystemView.SelectedSector != null && starSystemView.SelectedSector.SpaceObjects.OfType<ISpaceVehicle>().Any(v => v.Owner == Empire.Current);
				}
				else
				{
					// determine what commands are appropriate
					btnMove.Visible = value is IMobileSpaceObject;
					btnPursue.Visible = value is IMobileSpaceObject;
					btnEvade.Visible = value is IMobileSpaceObject;
					btnWarp.Visible = value is IMobileSpaceObject && ((IMobileSpaceObject)value).CanWarp;
					btnColonize.Visible = value is IMobileSpaceObject && ((IMobileSpaceObject)value).Abilities.Any(a => a.Rule.Name.StartsWith("Colonize Planet - "));
					btnSentry.Visible = value is IMobileSpaceObject;
					btnConstructionQueue.Visible = value != null && value.ConstructionQueue != null;
					btnTransferCargo.Visible = value != null && (value is ICargoContainer && ((ICargoContainer)value).CargoStorage > 0 || value.SupplyStorage > 0 || value.HasInfiniteSupplies);
					btnFleetTransfer.Visible = starSystemView.SelectedSector != null && starSystemView.SelectedSector.SpaceObjects.OfType<ISpaceVehicle>().Any(v => v.Owner == Empire.Current);
					btnClearOrders.Visible = value is IMobileSpaceObject || value is Planet;
				}
			}
		}

		private void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Shift)
			{
				if (e.KeyCode == Keys.D)
					this.ShowChildForm(new DesignListForm());
				else if (e.KeyCode == Keys.P || e.KeyCode == Keys.C) // planets and colonies screens are combined
					this.ShowChildForm(new PlanetListForm());
				else if (e.KeyCode == Keys.E)
					this.ShowChildForm(new EmpireListForm());
				else if (e.KeyCode == Keys.O)
					; // TODO - empire status screen
				else if (e.KeyCode == Keys.S)
					; // TODO - ships screen
				else if (e.KeyCode == Keys.Q)
					ShowConstructionQueueListForm();
				else if (e.KeyCode == Keys.L)
					this.ShowChildForm(new LogForm(this));
				else if (e.KeyCode == Keys.R)
					ShowResearchForm();
				else if (e.KeyCode == Keys.Tab)
					btnPrevIdle_Click(this, new EventArgs());
			}
			else
			{
				if (e.KeyCode == Keys.M && btnMove.Visible)
					ChangeCommandMode(CommandMode.Move, SelectedSpaceObject);
				else if (e.KeyCode == Keys.A && btnPursue.Visible)
					ChangeCommandMode(CommandMode.Pursue, SelectedSpaceObject);
				else if (e.KeyCode == Keys.E && btnEvade.Visible)
					ChangeCommandMode(CommandMode.Evade, SelectedSpaceObject);
				else if (e.KeyCode == Keys.W && btnWarp.Visible)
					ChangeCommandMode(CommandMode.Warp, SelectedSpaceObject);
				else if (e.KeyCode == Keys.C && btnColonize.Visible)
					ChangeCommandMode(CommandMode.Colonize, SelectedSpaceObject);
				else if (e.KeyCode == Keys.Y && btnSentry.Visible)
					IssueSpaceObjectOrder(new SentryOrder());
				else if (e.KeyCode == Keys.Q && btnConstructionQueue.Visible)
				{
					if (SelectedSpaceObject != null && SelectedSpaceObject.Owner == Empire.Current && SelectedSpaceObject.ConstructionQueue != null)
						ShowConstructionQueueForm(SelectedSpaceObject);
				}
				else if (e.KeyCode == Keys.T && btnTransferCargo.Visible)
					ShowCargoTransferForm();
				else if (e.KeyCode == Keys.F && btnFleetTransfer.Visible)
					ShowFleetTransferForm();
				else if (e.KeyCode == Keys.Tab)
					btnNextIdle_Click(this, new EventArgs());
			}

			if (e.KeyCode == Keys.F2)
				MessageBox.Show("Sorry, the game menu is not yet implemented."); // TODO - game menu
			else if (e.KeyCode == Keys.F3)
				this.ShowChildForm(new DesignListForm());
			else if (e.KeyCode == Keys.F4 || e.KeyCode == Keys.F5) // planets and colonies screens are combined
				this.ShowChildForm(new PlanetListForm());
			else if (e.KeyCode == Keys.F9)
				this.ShowChildForm(new EmpireListForm());
			else if (e.KeyCode == Keys.F11)
				MessageBox.Show("Sorry, the empire status screen is not yet implemented."); // TODO - empire status screen
			else if (e.KeyCode == Keys.F6)
				MessageBox.Show("Sorry, the ship list screen is not yet implemented."); // TODO - ships screen
			else if (e.KeyCode == Keys.F7)
				ShowConstructionQueueListForm();
			else if (e.KeyCode == Keys.F10)
				this.ShowChildForm(new LogForm(this));
			else if (e.KeyCode == Keys.F12)
				btnEndTurn_Click(btnEndTurn, new EventArgs());
			else if (e.KeyCode == Keys.F8)
				ShowResearchForm();
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
			/// <summary>
			/// Moves to and colonizes a planet.
			/// </summary>
			Colonize,
		}

		private void ChangeCommandMode(CommandMode mode, ISpaceObject sobj)
		{
			commandMode = mode;
			switch (mode)
			{
				case CommandMode.None:
					Text = "FrEee - " + Galaxy.Current.CurrentEmpire.Name + " - " + Galaxy.Current.CurrentEmpire.LeaderName + " - " + Galaxy.Current.Stardate;
					break;
				case CommandMode.Move:
					Text = "Click a sector for " + sobj + " to move to. (Ctrl-click to move aggressively)";
					break;
				case CommandMode.Pursue:
					Text = "Click a space object for " + sobj + " to pursue. (Ctrl-click to move aggressively)";
					break;
				case CommandMode.Evade:
					Text = "Click a space object for " + sobj + " to evade. (Ctrl-click to move aggressively)";
					break;
				case CommandMode.Warp:
					Text = "Click a warp point for " + sobj + " to traverse. (Ctrl-click to move aggressively)";
					break;
				case CommandMode.Colonize:
					Text = "Click a planet for " + sobj + " to colonize. (Ctrl-click to move aggressively; Shift-click to skip loading population)";
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
			if (SelectedSpaceObject != null)
				ChangeCommandMode(CommandMode.Colonize, SelectedSpaceObject);
		}

		private void btnSentry_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null)
				IssueSpaceObjectOrder(new SentryOrder());
		}

		private void btnConstructionQueue_Click(object sender, EventArgs e)
		{
			if (SelectedSpaceObject != null && SelectedSpaceObject.Owner == Empire.Current && SelectedSpaceObject.ConstructionQueue != null)
				ShowConstructionQueueForm(SelectedSpaceObject);
		}

		private void btnTransferCargo_Click(object sender, EventArgs e)
		{
			ShowCargoTransferForm();
		}

		private void ShowCargoTransferForm()
		{
			if (SelectedSpaceObject is ICargoTransferrer && SelectedSpaceObject.Owner == Empire.Current)
			{
				Sector lastSector;
				if (SelectedSpaceObject is IMobileSpaceObject)
				{
					var path = ((IMobileSpaceObject)SelectedSpaceObject).Path();
					if (path != null && path.Any())
						lastSector = path.Last();
					else
						lastSector = SelectedSpaceObject.Sector;
				}
				else
					lastSector = SelectedSpaceObject.FindSector();
				var form = new CargoTransferForm((ICargoTransferrer)SelectedSpaceObject, lastSector);
				this.ShowChildForm(form);
				BindReport();
			}
		}

		private void ShowFleetTransferForm()
		{
			if (starSystemView.SelectedSector != null && starSystemView.SelectedSector.SpaceObjects.OfType<ISpaceVehicle>().Any(v => v.Owner == Empire.Current))
			{
				var form = new FleetTransferForm(starSystemView.SelectedSector);
				this.ShowChildForm(form);
				BindReport();
				starSystemView.Invalidate();
			}
		}

		private void btnFleetTransfer_Click(object sender, EventArgs e)
		{
			ShowFleetTransferForm();
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
				if (SelectedSpaceObject is SpaceVehicle)
				{
					var v = (SpaceVehicle)SelectedSpaceObject;
					foreach (var order in v.Orders)
					{
						var addCmd = Empire.Current.Commands.OfType<AddOrderCommand<SpaceVehicle>>().SingleOrDefault(c => c.Order == order);
						if (addCmd == null)
						{
							// not a newly added order, so create a remove command to take it off the server
							var remCmd = new RemoveOrderCommand<SpaceVehicle>(v, order);
							Empire.Current.Commands.Add(remCmd);
						}
						else
						{
							// a newly added order, so just get rid of the add command
							Empire.Current.Commands.Remove(addCmd);
						}
					}
					v.Orders.Clear();
					BindReport();
				}
				else if (SelectedSpaceObject is Planet)
				{
					var p = (Planet)SelectedSpaceObject;
					foreach (var order in p.Orders)
					{
						var addCmd = Empire.Current.Commands.OfType<AddOrderCommand<Planet>>().SingleOrDefault(c => c.Order == order);
						if (addCmd == null)
						{
							// not a newly added order, so create a remove command to take it off the server
							var remCmd = new RemoveOrderCommand<Planet>(p, order);
							Empire.Current.Commands.Add(remCmd);
						}
						else
						{
							// a newly added order, so just get rid of the add command
							Empire.Current.Commands.Remove(addCmd);
						}
					}
					p.Orders.Clear();
					BindReport();
				}

				starSystemView.Invalidate(); // show move lines
			}
		}

		private void progResearch_Click(object sender, EventArgs e)
		{
			ShowResearchForm();
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
			var budget = Empire.Current.NetIncome[Resource.Research] + Empire.Current.BonusResearch;
			var forQueue = 100 - Empire.Current.ResearchSpending.Sum(kvp => kvp.Value);
			return t.Spending.Value * budget / 100 + (Empire.Current.ResearchQueue.FirstOrDefault() == t ? forQueue : 0);
		}

		public void ShowResearchForm()
		{
			this.ShowChildForm(new ResearchForm());
			BindResearch();
		}

		public void ShowVehicleDesignForm(VehicleDesignForm f = null)
		{
			if (f == null)
				f = new VehicleDesignForm();
			this.ShowChildForm(f);
		}

		public void ShowLogForm(LogForm f = null)
		{
			if (f == null)
				f = new LogForm(this);
			this.ShowChildForm(f);
		}

		private void searchBox_ObjectSelected(SearchBox sender, ISpaceObject sobj)
		{
			SelectedSpaceObject = sobj;
			searchBox.HideResults();
		}

		private void btnMenu_Click(object sender, EventArgs e)
		{
			// TODO - game menu
			MessageBox.Show("Sorry, the game menu is not yet implemented.");
		}

		private void btnEmpires_Click(object sender, EventArgs e)
		{
			this.ShowChildForm(new EmpireListForm());
		}

		private void btnShips_Click(object sender, EventArgs e)
		{
			// TODO - ships screen
			MessageBox.Show("Sorry, the ships screen is not yet implemented.");
		}

		private void btnPrevIdle_Click(object sender, EventArgs e)
		{
			var idle = Empire.Current.OwnedSpaceObjects.Where(sobj => sobj.IsIdle);
			if (!idle.Any())
				return;
			if (SelectedSpaceObject == null || SelectedSpaceObject == idle.First())
				SelectSpaceObject(idle.Last());
			else
				SelectSpaceObject(idle.ElementAt(idle.IndexOf(SelectedSpaceObject) - 1));
		}

		private void btnNextIdle_Click(object sender, EventArgs e)
		{
			var idle = Empire.Current.OwnedSpaceObjects.Where(sobj => sobj.IsIdle);
			if (!idle.Any())
				return;
			if (SelectedSpaceObject == null || SelectedSpaceObject == idle.Last())
				SelectSpaceObject(idle.First());
			else
				SelectSpaceObject(idle.ElementAt(idle.IndexOf(SelectedSpaceObject) + 1));
		}

		private void LoadButtonImage(Button btn, string picName)
		{
			var pic = Pictures.GetModImage(Path.Combine("Pictures", "UI", "Buttons", picName));
			if (pic != null)
			{
				btn.Text = "";
				btn.Image = pic;
			}
		}

		private void IssueSpaceObjectOrder<T>(IOrder<T> order) where T : IOrderable, ISpaceObject
		{
			if (SelectedSpaceObject == null)
				throw new Exception("No space object is selected to issue order \"" + order + "\" to.");
			if (!(SelectedSpaceObject is T))
				throw new Exception("Order \"" + order + "\" cannot be issued to objects of type " + SelectedSpaceObject.GetType() + ". It can only be issued to objects of type " + typeof(T) + ".");
			((T)SelectedSpaceObject).IssueOrder(order);
			BindReport();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// trap tab key
			if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
				GameForm_KeyDown(this, new KeyEventArgs(keyData));
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void BindReport()
		{
			foreach (var report in pnlDetailReport.Controls.Cast<Control>().OfType<IBindable>())
				report.Bind();
		}

		private void ShowConstructionQueueListForm()
		{
			this.ShowChildForm(new ConstructionQueueListForm());
			BindReport();
			SetUpResourceDisplay();
		}

		private void ShowConstructionQueueForm(ISpaceObject sobj)
		{
			this.ShowChildForm(new ConstructionQueueForm(SelectedSpaceObject.ConstructionQueue));
			BindReport();
			SetUpResourceDisplay();
		}
	}
}
