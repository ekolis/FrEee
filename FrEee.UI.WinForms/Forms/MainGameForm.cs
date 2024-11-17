using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Processes;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Controls;
using FrEee.UI.WinForms.Interfaces;
using FrEee.UI.WinForms.Objects;
using FrEee.UI.WinForms.Utility;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Diplomacy.Messages;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.UI.Blazor.Views.GalaxyMapModes;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Gameplay.Commands;
using FrEee.Gameplay.Commands.Orders;
using FrEee.Gameplay.Commands.Messages;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.UI.WinForms.Forms;

public partial class MainGameForm : GameForm
{
	/// <summary>
	/// Creates a game form.
	/// </summary>
	/// <param name="hostView">Is this the host viewing a player's view? If so, we shouldn't attempt to process the turn after he clicks End Turn, even if the game is single player.</param>
	public MainGameForm(bool hostView, bool quitOnClose)
	{
		if (Instance != null)
			throw new InvalidOperationException("Only one game form allowed.");
		InitializeComponent();
		this.hostView = hostView;
		QuitOnClose = quitOnClose;
		SetMouseDownHandler(this, GameForm_MouseDown);
		RemoveMouseDownHandler(searchBox, GameForm_MouseDown);
		foreach (var mode in GalaxyMapModeLibrary.All)
			ddlGalaxyViewMode.Items.Add(mode);
		ddlGalaxyViewMode.SelectedIndex = 0;
		Instance = this;
	}

	public static MainGameForm Instance { get; private set; }

	/// <summary>
	/// Has the turn log been shown yet?
	/// Will return true after the time the log would normally be shown
	/// even if there is no need to show the log.
	/// </summary>
	public bool HasLogBeenShown { get; private set; }

	/// <summary>
	/// Should
	/// </summary>
	/// <value>
	///   <c>true</c> if [quit on close]; otherwise, <c>false</c>.
	/// </value>
	public bool QuitOnClose { get; set; }

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

				var sector = value.Sector;
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
				// can't issue commands to objects we don't own, though we can issue fleet transfer commands
				foreach (GameButton btn in pnlSubCommands.Controls.Cast<Control>().ExceptSingle(btnFleetTransfer))
					btn.Visible = false;

				// fleet transfer and recycle buttons are special, they can be selected even with no space object selected
				btnFleetTransfer.Visible = IsFleetTransferOperationValid;
				btnRecycle.Visible = starSystemView.SelectedSector != null && starSystemView.SelectedSector.SpaceObjects.Any(sobj => sobj.Owner == Empire.Current && (sobj is Planet || sobj.HasAbility("Space Yard")));
			}
			else
			{
				// determine what commands are appropriate
				btnMove.Visible = value is IMobileSpaceObject;
				btnPursue.Visible = value is IMobileSpaceObject;
				btnEvade.Visible = value is IMobileSpaceObject;
				btnWarp.Visible = value is IMobileSpaceObject && ((IMobileSpaceObject)value).CanWarp;
				{
					btnColonize.Visible =
					   value is IMobileSpaceObject sobj && sobj.Abilities().Any(a => a.Rule.Name.StartsWith("Colonize Planet - "))
					   || value is Fleet f && f.LeafVehicles.Any(v => v.Abilities().Any(a => a.Rule.Name.StartsWith("Colonize Planet - ")));
				}
				btnSentry.Visible = value is IMobileSpaceObject;
				btnConstructionQueue.Visible = value is IConstructor c && c.ConstructionQueue != null;
				btnTransferCargo.Visible = value != null && (value is ICargoContainer && ((ICargoContainer)value).CargoStorage > 0 || value.SupplyStorage > 0 || value.HasInfiniteSupplies);
				btnRecycle.Visible = starSystemView.SelectedSector.SpaceObjects.Any(sobj => sobj.Owner == Empire.Current && (sobj is Planet || sobj.HasAbility("Space Yard")));
				btnActivate.Visible = value.ActivatableAbilities().Any();
				btnFleetTransfer.Visible = IsFleetTransferOperationValid;
				btnClearOrders.Visible = value is IMobileSpaceObject || value is Planet;
			}

			btnPrevIdle.Visible = true;
			btnNextIdle.Visible = true;
			btnCommands.Visible = true;
		}
	}

	/// <summary>
	/// Can the "fleet transfer" button be clicked now?
	/// </summary>
	private bool IsFleetTransferOperationValid
	{
		get
		{
			return starSystemView.SelectedSector != null &&
				starSystemView.SelectedSector.SpaceObjects.OfType<IMobileSpaceObject>().Any(
				v => v.Owner == Empire.Current && (v.CanBeInFleet || v is Fleet));
		}
	}

	/// <summary>
	/// Aggressive movement mode. Unless this is set, movement orders will avoid enemies.
	/// </summary>
	private bool aggressiveMode;

	private CommandMode commandMode;

	private int curIdleIndex = -1;

	private FlowLayoutPanel currentTab;

	private bool hostView;

	private ISpaceObject prevIdle = null, curIdle = null, nextIdle = null;

	private ISpaceObject selectedSpaceObject;

	private bool turnEnded;

	public void BindReport()
	{
		foreach (var report in pnlDetailReport.Controls.Cast<Control>().OfType<IBindable>())
			report.Bind();
	}

	public void SelectSector(Point p)
	{
		starSystemView.SelectedSector = starSystemView.StarSystem.GetSector(p);
	}

	// TODO - do we really need this? can't we do this in the SelectedSpaceObject setter?
	public void SelectSpaceObject(ISpaceObject sobj)
	{
		if (sobj != null)
		{
			SelectStarSystem(sobj.StarSystem);
			SelectSector(sobj.Sector.Coordinates);
			pnlDetailReport.Controls.Clear();
			var rpt = CreateSpaceObjectReport(sobj);
			if (rpt != null)
				rpt.Dock = DockStyle.Fill;
			pnlDetailReport.Controls.Add(rpt);
			SelectedSpaceObject = sobj;
		}
		else
		{
			pnlDetailReport.Controls.Clear();
			SelectedSpaceObject = null;
		}
	}

	public void SelectStarSystem(StarSystem sys)
	{
		var tab = FindTab(sys);
		if (tab == null)
			tab = AddTab(sys);
		SelectTab(tab);
	}

	public void ShowLogForm(LogForm f = null)
	{
		if (f == null)
			f = new LogForm(this, Empire.Current.Log);
		this.ShowChildForm(f);
	}

	public void ShowResearchForm()
	{
		this.ShowChildForm(new ResearchForm());
		Empire.Current.ComputeResearchProgress();
		BindResearch();
	}

	public void ShowVehicleDesignForm(VehicleDesignForm f = null)
	{
		if (f == null)
			f = new VehicleDesignForm();
		this.ShowChildForm(f);
	}

	protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
	{
		// trap tab key
		if (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift))
			GameForm_KeyDown(this, new KeyEventArgs(keyData));
		return base.ProcessCmdKey(ref msg, keyData);
	}

	private FlowLayoutPanel AddTab(StarSystem sys = null)
	{
		// check for existing tab and return that instead
		// no need to have multiple tabs for the same system!
		var oldTab = FindTab(sys);
		if (oldTab != null)
			return oldTab;

		// create new tab
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

	private void AssignClickHandler(Control ctl)
	{
		ctl.Click += ctl_Click;
		if (ctl is ContainerControl)
		{
			foreach (Control c2 in ((ContainerControl)ctl).Controls)
				AssignClickHandler(c2);
		}
	}

	private void BindResearch()
	{
		if (Empire.Current.ResearchSpending.Any() || Empire.Current.ResearchQueue.Any())
		{
			var techs = Empire.Current.ResearchSpending.Keys.Union(Empire.Current.ResearchQueue);
			var minEta = techs.Min(t => t.Progress.Eta);
			var tech = techs.Where(t => t.Progress.Eta == minEta).First();

			progResearch.Progress = tech.Progress;
			if (tech.CurrentLevel >= tech.MaximumLevel)
			{
				// HACK - why are random completed techs appearing for the next researched tech?!
				progResearch.Value = 0;
				progResearch.Maximum = 1;
				progResearch.LeftText = "No Research - Click to Begin";
				progResearch.RightText = "";
			}
			else
			{
				if (tech.MaximumLevel == 1)
					progResearch.LeftText = tech.Name;
				else
					progResearch.LeftText = tech.Name + " L" + (tech.CurrentLevel + 1);
				if (tech.Progress.Eta == null)
					progResearch.RightText = "Never";
				else
					progResearch.RightText = tech.Progress.Eta + " turn" + (tech.Progress.Eta == 1 ? "" : "s");
			}
		}
		else
		{
			progResearch.Value = 0;
			progResearch.Maximum = 1;
			progResearch.LeftText = "No Research - Click to Begin";
			progResearch.RightText = "";
		}
	}

	private void btnActivate_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject is IMobileSpaceObject)
		{
			this.ShowChildForm(new ActivateAbilityForm((IMobileSpaceObject)SelectedSpaceObject));
			BindReport();
		}
	}

	private void btnClearOrders_Click(object sender, EventArgs e)
	{
		ClearOrders();
	}

	private void btnColonize_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject != null)
			ChangeCommandMode(CommandMode.Colonize, SelectedSpaceObject);
	}

	private void btnConstructionQueue_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject is IConstructor c && c.Owner == Empire.Current && c.ConstructionQueue != null)
			ShowConstructionQueueForm(SelectedSpaceObject);
	}

	private void btnDesigns_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new DesignListForm());
	}

	private void btnEmpires_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new EmpireListForm());
	}

	private void btnEndTurn_Click(object sender, EventArgs e)
	{
		var todos = FindTodos();

		if (Game.Current.IsSinglePlayer && !hostView)
		{
			var msg = !todos.Any() ? "Process turn after saving your commands?" : "Process turn after saving your commands? You have:\n\n" + string.Join("\n", todos.ToArray());
			var result = MessageBox.Show(msg, "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			if (result == DialogResult.Yes)
			{
				SaveCommands(true);

				// show empire log if there's anything new there
				if (Empire.Current.Log.Any(m => m.TurnNumber == Game.Current.TurnNumber))
					this.ShowChildForm(new LogForm(this, Empire.Current.Log));
			}
			else if (result == DialogResult.No)
				SaveCommands(false);
		}
		else
		{
			var msg = !todos.Any() ? "Quit after saving your commands?" : "Quit after saving your commands? You have:\n\n" + string.Join("\n", todos.ToArray());
			var result = MessageBox.Show(msg, "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			if (result == DialogResult.Yes)
			{
				SaveCommands(true);
				turnEnded = true;
				Close();
			}
			else if (result == DialogResult.No)
				SaveCommands(false);
		}
	}

	private void btnEvade_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject != null)
			ChangeCommandMode(CommandMode.Evade, SelectedSpaceObject);
	}

	private void btnFleetTransfer_Click(object sender, EventArgs e)
	{
		ShowFleetTransferForm();
	}

	private void btnLog_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new LogForm(this, Empire.Current.Log));
	}

	private void btnMenu_Click(object sender, EventArgs e)
	{
		// TODO - proper game menu, not just the options screen
		this.ShowChildForm(new OptionsForm());
	}

	private void btnMove_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject != null)
			ChangeCommandMode(CommandMode.Move, SelectedSpaceObject);
	}

	private void btnNewTab_Click(object sender, EventArgs e)
	{
		SelectTab(AddTab(null));
	}

	private void btnNextIdle_Click(object sender, EventArgs e)
	{
		var idle = Empire.Current.OwnedSpaceObjects.Where(sobj => sobj.IsIdle).ToArray();
		if (!idle.Any())
		{
			prevIdle = curIdle = nextIdle = null;
			curIdleIndex = -1;
		}
		else if (curIdle == null)
		{
			curIdle = idle.First();
			prevIdle = idle.Previous(curIdle, true);
			nextIdle = idle.Next(curIdle, true);
			curIdleIndex = 0;
		}
		else if (!idle.Contains(curIdle))
		{
			// curIdle is no longer idle, find where it was using the index
			curIdle = idle.ElementAtOrDefault(curIdleIndex);
			prevIdle = idle.Previous(curIdle, true);
			nextIdle = idle.Next(curIdle, true);
		}
		else
		{
			prevIdle = curIdle;
			curIdle = idle.Next(curIdle, true);
			nextIdle = idle.Next(curIdle, true);
			curIdleIndex = idle.IndexOf(curIdle);
		}
		SelectSpaceObject(curIdle);
	}

	private void btnPlanets_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new PlanetListForm());
	}

	private void btnPrevIdle_Click(object sender, EventArgs e)
	{
		var idle = Empire.Current.OwnedSpaceObjects.Where(sobj => sobj.IsIdle);
		if (!idle.Any())
		{
			prevIdle = curIdle = nextIdle = null;
			curIdleIndex = -1;
		}
		else if (curIdle == null)
		{
			curIdle = idle.Last();
			prevIdle = idle.Previous(curIdle, true);
			nextIdle = idle.Next(curIdle, true);
			curIdleIndex = idle.Count() - 1;
		}
		else if (!idle.Contains(curIdle))
		{
			// curIdle is no longer idle, find where it was using the index
			curIdleIndex--;
			curIdle = idle.ElementAtOrDefault(curIdleIndex);
			prevIdle = idle.Previous(curIdle, true);
			nextIdle = idle.Next(curIdle, true);
		}
		else
		{
			nextIdle = curIdle;
			curIdle = idle.Previous(curIdle, true);
			prevIdle = idle.Previous(curIdle, true);
			curIdleIndex = idle.IndexOf(curIdle);
		}
		SelectSpaceObject(curIdle);
	}

	private void btnPursue_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject != null)
			ChangeCommandMode(CommandMode.Pursue, SelectedSpaceObject);
	}

	private void btnQueues_Click(object sender, EventArgs e)
	{
		ShowConstructionQueueListForm();
	}

	private void btnRecycle_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new RecycleForm(starSystemView.SelectedSector));
		BindReport();
	}

	private void btnSentry_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject != null)
			IssueSpaceObjectOrder(new SentryOrder());
	}

	private void btnShips_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new ShipListForm());
	}

	private void btnTab_Click(object sender, EventArgs e)
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

	private void btnTransferCargo_Click(object sender, EventArgs e)
	{
		ShowCargoTransferForm();
	}

	private void btnWarp_Click(object sender, EventArgs e)
	{
		if (SelectedSpaceObject != null)
			ChangeCommandMode(CommandMode.Warp, SelectedSpaceObject);
	}

	private void btnX_Click(object sender, EventArgs e)
	{
		var btnX = (GameButton)sender;
		var pnlTab = (FlowLayoutPanel)btnX.Parent;
		RemoveTab(pnlTab);
	}

	private void ChangeCommandMode(CommandMode mode, ISpaceObject sobj)
	{
		commandMode = mode;
		switch (mode)
		{
			case CommandMode.None:
				Text = "FrEee - " + Game.Current;
				break;

			case CommandMode.Move:
				Text = "Click a sector for " + sobj + " to move to. (Ctrl-click to move aggressively)";
				break;

			case CommandMode.Pursue:
				Text = "Click a space object for " + sobj + " to pursue. (Ctrl-click or pursue a hostile target to move aggressively)";
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
				foreach (var order in v.Orders.ToArray())
					v.RemoveOrderClientSide(order);
				BindReport();
			}
			else if (SelectedSpaceObject is Fleet)
			{
				var f = (Fleet)SelectedSpaceObject;
				foreach (var order in f.Orders.ToArray())
					f.RemoveOrderClientSide(order);
				f.Orders.Clear();
				BindReport();
			}
			else if (SelectedSpaceObject is Planet)
			{
				var p = (Planet)SelectedSpaceObject;
				foreach (var order in p.Orders.ToArray())
					p.RemoveOrderClientSide(order);
				p.Orders.Clear();
				BindReport();
			}

			starSystemView.Invalidate(); // show move lines
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
		if (sobj is WarpPoint)
			return new WarpPointReport((WarpPoint)sobj);
		if (sobj is SpaceVehicle)
		{
			var r = new SpaceVehicleReport((SpaceVehicle)sobj);
			r.OrdersChanged += VehicleFleetReport_OrdersChanged;
			return r;
		};
		if (sobj is Fleet)
		{
			var r = new FleetReport((Fleet)sobj);
			r.OrdersChanged += VehicleFleetReport_OrdersChanged;
			return r;
		}
		return null;
	}

	private void ctl_Click(object sender, EventArgs e)
	{
		((Control)sender).Focus();
	}

	private void ddlGalaxyViewMode_SelectedIndexChanged(object sender, EventArgs e)
	{
		// TODO: switch galaxy *map* mode
		galaxyView.Mode = (IGalaxyMapMode)ddlGalaxyViewMode.SelectedItem;
	}

	private MusicMood FindMusicMood()
	{
		var emps = Game.Current.Empires.ExceptSingle(Empire.Current).ExceptSingle(null);
		if (Empire.Current == null)
			return MusicMood.Peaceful; // we are the host
		else
		{
			var aboveUs = emps.Where(emp => emp.Score > Empire.Current.Score);
			var atOrBelowUs = emps.Where(emp => emp.Score <= Empire.Current.Score);
			if (aboveUs.Any())
			{
				// oh noes! someone has a higher score than us!
				if (aboveUs.Any(emp => emp.IsEnemyOf(Empire.Current, null)))
					return MusicMood.Tense; // and they hate us!
				else if (aboveUs.All(emp => emp.IsAllyOf(Empire.Current, null)))
					return MusicMood.Upbeat; // we have friends in high places
				else
					return MusicMood.Sad; // ominous...
			}
			else if (atOrBelowUs.Any())
			{
				// we are the king of the hill
				if (atOrBelowUs.Any(emp => emp.IsEnemyOf(Empire.Current, null)))
					return MusicMood.Upbeat; // we can kick their butts!
				else
					return MusicMood.Peaceful; // nothing to worry about
			}
			else
			{
				// we are alone... we are at one with the universe... ommmm...
				return MusicMood.Peaceful;
			}
		}
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

	private IEnumerable<string> FindTodos()
	{
		var todos = new List<string>();

		var ships = Empire.Current.OwnedSpaceObjects.OfType<SpaceVehicle>().Where(v => v.Container == null && v.StrategicSpeed > 0 && !v.Orders.Any()).Count();
		if (ships == 1)
			todos.Add("1 idle ship");
		else if (ships > 1)
			todos.Add(ships + " idle ships");

		var fleets = Empire.Current.OwnedSpaceObjects.OfType<Fleet>().Where(f => f.Container == null && f.StrategicSpeed > 0 && !f.Orders.Any()).Count();
		if (fleets == 1)
			todos.Add("1 idle fleet");
		else if (fleets > 1)
			todos.Add(fleets + " idle fleets");

		var queues = Empire.Current.ConstructionQueues.Where(q =>
		{
			bool idle = false;
			// queues are idle if they don't have a full turn of build orders queued
			if (q.Eta == null || q.Eta < 1d)
			{
				idle = true;
				// however they are not idle if they are not space yards and there's not enough facility/cargo space to build more facilities/units
				if (!q.IsSpaceYardQueue && q.FacilitySlotsFree == 0 && q.CargoStorageFreeInSector < Empire.Current.UnlockedItems.OfType<IHull>().Where(x => x.VehicleType != VehicleTypes.Ship && x.VehicleType != VehicleTypes.Base).Min(x => x.Size))
					idle = false;
			}
			return idle;
		}).Count();
		if (queues == 1)
			todos.Add("1 idle construction queue");
		else if (queues > 1)
			todos.Add(queues + " idle construction queues");

		var idx = 0;
		var queueSpending = 0;
		var levels = new Dictionary<Technology, int>(Empire.Current.ResearchedTechnologies);
		Empire.Current.ComputeResearchProgress();
		foreach (var tech in Empire.Current.ResearchQueue)
		{
			levels[tech]++; // so we can research the same tech multiple times with the appropriate cost for each level
			queueSpending += tech.GetLevelCost(levels[tech], Empire.Current);
			idx++;
		}
		var totalRP = Empire.Current.NetIncome[Resource.Research] + Empire.Current.BonusResearch;
		queueSpending = Math.Min(queueSpending, totalRP);
		var leftover = totalRP - queueSpending;
		var pctSpending = 0;
		foreach (var kvp in Empire.Current.ResearchSpending)
			pctSpending += leftover * kvp.Value;
		var totalSpending = queueSpending + pctSpending;
		var unallocatedPct = 1d - ((double)totalSpending / (double)totalRP);
		if (unallocatedPct > 0)
			todos.Add(unallocatedPct.ToString("0%") + " unallocated research");

		var messages = Empire.Current.IncomingMessages.OfType<ProposalMessage>().Count(m =>
			m.TurnNumber >= Game.Current.TurnNumber - 1 &&
			!Empire.Current.Commands.OfType<ISendMessageCommand>().Where(c => c.Message.InReplyTo == m).Any() &&
			!Empire.Current.Commands.OfType<IDeleteMessageCommand>().Where(c => c.Message == m).Any());
		if (messages == 1)
			todos.Add("1 unresolved diplomatic proposal");
		else if (messages > 1)
			todos.Add(messages + " unresolved diplomatic proposals");

		return todos;
	}

	private void galaxyView_StarSystemClicked(GalaxyView sender, StarSystem starSystem)
	{
		if (starSystem != null)
		{
			if (ModifierKeys.HasFlag(Keys.Control))
				btnNewTab_Click(this, new EventArgs());
			sender.SelectedStarSystem = starSystem;
		}
	}

	private void galaxyView_StarSystemSelected(GalaxyView sender, StarSystem starSystem)
	{
		if (starSystem != GetTabSystem(currentTab))
		{
			var selTab = SetTabSystem(currentTab, starSystem);
			if (selTab != currentTab)
				SelectTab(selTab);
		}
	}

	private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		Instance = null;
		if (QuitOnClose)
			Gui.CloseGame();
	}

	private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!turnEnded)
		{
			var todos = FindTodos();

			var msg = !todos.Any() ? "Save your commands before quitting?" : "Save your commands before quitting? You have:\n\n" + string.Join("\n", todos.ToArray());

			switch (MessageBox.Show(msg, "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
			{
				case DialogResult.Yes:
					SaveCommands(!Game.Current.IsSinglePlayer || hostView);
					break;

				case DialogResult.No:
					break; // nothing to do here
				case DialogResult.Cancel:
					e.Cancel = true; // don't quit!
					break;
			}
		}
	}

	private void GameForm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Shift && !e.Control && !e.Alt)
		{
			if (e.KeyCode == Keys.D)
				this.ShowChildForm(new DesignListForm());
			else if (e.KeyCode == Keys.P || e.KeyCode == Keys.C) // planets and colonies screens are combined
				this.ShowChildForm(new PlanetListForm());
			else if (e.KeyCode == Keys.E)
				this.ShowChildForm(new EmpireListForm());
			else if (e.KeyCode == Keys.O)
			{ } // TODO - empire status screen
			else if (e.KeyCode == Keys.S)
				this.ShowChildForm(new ShipListForm());
			else if (e.KeyCode == Keys.Q)
				ShowConstructionQueueListForm();
			else if (e.KeyCode == Keys.L)
				this.ShowChildForm(new LogForm(this, Empire.Current.Log));
			else if (e.KeyCode == Keys.R)
				ShowResearchForm();
			else if (e.KeyCode == Keys.Tab)
				btnPrevIdle_Click(this, new EventArgs());
			// set waypoint without redirecting
			else if (e.KeyCode == Keys.D0)
				SetWaypoint(0, false);
			else if (e.KeyCode == Keys.D1)
				SetWaypoint(1, false);
			else if (e.KeyCode == Keys.D2)
				SetWaypoint(2, false);
			else if (e.KeyCode == Keys.D3)
				SetWaypoint(3, false);
			else if (e.KeyCode == Keys.D4)
				SetWaypoint(4, false);
			else if (e.KeyCode == Keys.D5)
				SetWaypoint(5, false);
			else if (e.KeyCode == Keys.D6)
				SetWaypoint(6, false);
			else if (e.KeyCode == Keys.D7)
				SetWaypoint(7, false);
			else if (e.KeyCode == Keys.D8)
				SetWaypoint(8, false);
			else if (e.KeyCode == Keys.D9)
				SetWaypoint(9, false);
		}
		else if (e.Control && !e.Shift && !e.Alt)
		{
			if (e.KeyCode == Keys.R && btnRecycle.Visible)
				btnRecycle_Click(this, new EventArgs());
			else if (e.KeyCode == Keys.A && btnActivate.Visible)
				btnActivate_Click(this, new EventArgs());
			else if (e.KeyCode == Keys.D0)
				GoToWaypoint(0, true);
			else if (e.KeyCode == Keys.D1)
				GoToWaypoint(1, true);
			else if (e.KeyCode == Keys.D2)
				GoToWaypoint(2, true);
			else if (e.KeyCode == Keys.D3)
				GoToWaypoint(3, true);
			else if (e.KeyCode == Keys.D4)
				GoToWaypoint(4, true);
			else if (e.KeyCode == Keys.D5)
				GoToWaypoint(5, true);
			else if (e.KeyCode == Keys.D6)
				GoToWaypoint(6, true);
			else if (e.KeyCode == Keys.D7)
				GoToWaypoint(7, true);
			else if (e.KeyCode == Keys.D8)
				GoToWaypoint(8, true);
			else if (e.KeyCode == Keys.D9)
				GoToWaypoint(9, true);
		}
		else if (!e.Shift && !e.Control && !e.Alt)
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
				if (SelectedSpaceObject is IConstructor c && c.Owner == Empire.Current && c.ConstructionQueue != null)
					ShowConstructionQueueForm(SelectedSpaceObject);
			}
			else if (e.KeyCode == Keys.T && btnTransferCargo.Visible)
				ShowCargoTransferForm();
			else if (e.KeyCode == Keys.F && btnFleetTransfer.Visible)
				ShowFleetTransferForm();
			else if (e.KeyCode == Keys.Tab)
				btnNextIdle_Click(this, new EventArgs());
			else if (e.KeyCode == Keys.D0)
				GoToWaypoint(0, false);
			else if (e.KeyCode == Keys.D1)
				GoToWaypoint(1, false);
			else if (e.KeyCode == Keys.D2)
				GoToWaypoint(2, false);
			else if (e.KeyCode == Keys.D3)
				GoToWaypoint(3, false);
			else if (e.KeyCode == Keys.D4)
				GoToWaypoint(4, false);
			else if (e.KeyCode == Keys.D5)
				GoToWaypoint(5, false);
			else if (e.KeyCode == Keys.D6)
				GoToWaypoint(6, false);
			else if (e.KeyCode == Keys.D7)
				GoToWaypoint(7, false);
			else if (e.KeyCode == Keys.D8)
				GoToWaypoint(8, false);
			else if (e.KeyCode == Keys.D9)
				GoToWaypoint(9, false);
			else if (e.KeyCode == Keys.F2)
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
				this.ShowChildForm(new ShipListForm());
			else if (e.KeyCode == Keys.F7)
				ShowConstructionQueueListForm();
			else if (e.KeyCode == Keys.F10)
				this.ShowChildForm(new LogForm(this, Empire.Current.Log));
			else if (e.KeyCode == Keys.F12)
				btnEndTurn_Click(btnEndTurn, new EventArgs());
			else if (e.KeyCode == Keys.F8)
				ShowResearchForm();
			else if (e.KeyCode == Keys.Back && btnClearOrders.Visible)
				ClearOrders();
			else if (e.KeyCode == Keys.Escape)
			{
				if (commandMode == CommandMode.None && starSystemView.SelectedSector != null)
				{
					// deselect space object
					SelectedSpaceObject = null;

					// show star system report
					var newReport = new StarSystemReport(starSystemView.StarSystem);
					pnlDetailReport.Controls.Add(newReport);
					newReport.Left = newReport.Margin.Left;
					newReport.Width = pnlDetailReport.Width - newReport.Margin.Right - newReport.Margin.Left;
					newReport.Top = newReport.Margin.Top;
					newReport.Height = pnlDetailReport.Height - newReport.Margin.Bottom - newReport.Margin.Top;
					newReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
				}
				else
					ChangeCommandMode(CommandMode.None, null);
			}
		}
		else if (!e.Shift && !e.Control && e.Alt)
		{
			// set waypoint and redirect
			if (e.KeyCode == Keys.D0)
				SetWaypoint(0, true);
			else if (e.KeyCode == Keys.D1)
				SetWaypoint(1, true);
			else if (e.KeyCode == Keys.D2)
				SetWaypoint(2, true);
			else if (e.KeyCode == Keys.D3)
				SetWaypoint(3, true);
			else if (e.KeyCode == Keys.D4)
				SetWaypoint(4, true);
			else if (e.KeyCode == Keys.D5)
				SetWaypoint(5, true);
			else if (e.KeyCode == Keys.D6)
				SetWaypoint(6, true);
			else if (e.KeyCode == Keys.D7)
				SetWaypoint(7, true);
			else if (e.KeyCode == Keys.D8)
				SetWaypoint(8, true);
			else if (e.KeyCode == Keys.D9)
				SetWaypoint(9, true);
		}
		else if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
			aggressiveMode = true;
	}

	private void GameForm_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
			aggressiveMode = false;
	}

	private void GameForm_Load(object sender, EventArgs e)
	{
		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
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
		LoadButtonImage(btnRecycle, "Recycle");
		LoadButtonImage(btnTransferCargo, "TransferCargo");
		LoadButtonImage(btnFleetTransfer, "FleetTransfer");
		LoadButtonImage(btnClearOrders, "ClearOrders");
		LoadButtonImage(btnActivate, "Activate");
		LoadButtonImage(btnPrevIdle, "Previous");
		LoadButtonImage(btnNextIdle, "Next");
		LoadButtonImage(btnCommands, "Commands");

		// TODO - galaxy view background image can depend on galaxy template?
		galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "UI", "Map", "quadrant"));

		// set up GUI bindings to galaxy
		SetUpGui();

		Enabled = true;

		if (Empire.Current.IsWinner)
		{
			var form = new GameOverForm(true);
			form.StartPosition = FormStartPosition.CenterScreen;
			this.ShowChildForm(form);
		}
		if (Empire.Current.IsLoser)
		{
			var form = new GameOverForm(false);
			form.StartPosition = FormStartPosition.CenterScreen;
			this.ShowChildForm(form);
		}

		// show empire log if there's anything new there
		if (Empire.Current.Log.Any(m => m.TurnNumber == Game.Current.TurnNumber))
		{
			var form = new LogForm(this, Empire.Current.Log);
			form.StartPosition = FormStartPosition.CenterScreen;
			this.ShowChildForm(form);
		}

		HasLogBeenShown = true;

		// so the search box can lose focus...
		foreach (Control ctl in pnlLayout.Controls)
			AssignClickHandler(ctl);

		Empire.Current.PlayerInfo = ClientSettings.Instance.PlayerInfo;
	}

	private void GameForm_MouseDown(object sender, MouseEventArgs e)
	{
		searchBox.HideResults();
	}

	private StarSystem GetTabSystem(FlowLayoutPanel tab)
	{
		if (tab == null)
			return null;
		return (StarSystem)tab.Controls[0].Tag;
	}

	private int GetTotalSpending(Technology t)
	{
		var budget = Empire.Current.NetIncome[Resource.Research] + Empire.Current.BonusResearch;
		var forQueue = 100 - Empire.Current.ResearchSpending.Sum(kvp => kvp.Value);
		return (int)(t.Spending.Value * budget / 100 + (Empire.Current.ResearchQueue.FirstOrDefault() == t ? forQueue : 0));
	}

	private void GoToWaypoint(int waypointNumber, bool aggressive)
	{
		// find waypoint
		var wp = Empire.Current.NumberedWaypoints[waypointNumber];
		if (wp != null && SelectedSpaceObject != null && SelectedSpaceObject is IMobileSpaceObject)
		{
			// set an order to go there
			var sobj = SelectedSpaceObject as IMobileSpaceObject;
			sobj.IssueOrder(new WaypointOrder(wp, !aggressive));

			// refresh the map and orders
			starSystemView.Invalidate();
			BindReport();
		}
	}

	private void IssueSpaceObjectOrder(IOrder order)
	{
		if (SelectedSpaceObject == null)
			throw new Exception("No space object is selected to issue order \"" + order + "\" to.");
		if (!(SelectedSpaceObject is IOrderable))
			throw new Exception($"Selected space object {SelectedSpaceObject} is not orderable.");
		((IOrderable)SelectedSpaceObject).IssueOrder(order);
		BindReport();
	}

	private IEnumerable<FlowLayoutPanel> ListTabs()
	{
		return pnlTabs.Controls.OfType<FlowLayoutPanel>();
	}

	private void LoadButtonImage(Button btn, string picName)
	{
		var pic = Pictures.GetModImage(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "UI", "Buttons", picName));
		if (pic != null)
		{
			btn.Text = "";
			btn.Image = pic;
		}
	}

	private void progResearch_Click(object sender, EventArgs e)
	{
		ShowResearchForm();
	}

	/// <summary>
	/// Removes mouse down handler for a control and all its children recursively.
	/// </summary>
	/// <param name="ctl"></param>
	/// <param name="h"></param>
	private void RemoveMouseDownHandler(Control ctl, MouseEventHandler h)
	{
		ctl.MouseDown -= h;
		foreach (Control c in ctl.Controls)
			RemoveMouseDownHandler(c, h);
	}

	private void RemoveTab(FlowLayoutPanel tab)
	{
		// if currentTab is removed, select previous one. If it does not exist select next one. If does not exist or is the new tab button, create it and select it
		if (currentTab == tab)
		{
			var nextTabIndexToSelect = pnlTabs.Controls.IndexOf(tab) - 1;
			if (nextTabIndexToSelect < 0)
				nextTabIndexToSelect = 1;

			if (nextTabIndexToSelect > pnlTabs.Controls.Count || !(pnlTabs.Controls[nextTabIndexToSelect] is FlowLayoutPanel))
			{
				SelectTab(AddTab());
			}
			else
			{
				SelectTab(pnlTabs.Controls[nextTabIndexToSelect] as FlowLayoutPanel);
			}
		}
		pnlTabs.Controls.Remove(tab);
	}

	private void SaveCommands(bool endTurn)
	{
		Game.Current.SaveCommands();
		if (endTurn)
		{
			if (Game.Current.IsSinglePlayer && !hostView)
			{
				Cursor = Cursors.WaitCursor;
				Enabled = false;
				var plrnum = Game.Current.PlayerNumber;
				var status = new Status { Message = "Initializing" };
				var t = new Thread(new ThreadStart(() =>
				{
					status.Message = "Loading game";
					Game.Load(Game.Current.Name, Game.Current.TurnNumber);
					status.Progress = 0.25;
					status.Message = "Processing turn";
					var processor = DIRoot.TurnProcessor;
					processor.ProcessTurn(Game.Current, false, status, 0.5);
					status.Message = "Saving game";
					Game.SaveAll(status, 0.75);
					status.Message = "Loading game";
					Game.Load(Game.Current.Name, Game.Current.TurnNumber, plrnum);
					status.Progress = 1.00;
					// no need to reload designs from library, they're already loaded
				}));

				this.ShowChildForm(new StatusForm(t, status));
				SetUpGui();
				Enabled = true;
				Cursor = Cursors.Default;

				if (Empire.Current.IsWinner)
				{
					var form = new GameOverForm(true);
					form.StartPosition = FormStartPosition.CenterScreen;
					this.ShowChildForm(form);
				}
				if (Empire.Current.IsLoser)
				{
					var form = new GameOverForm(false);
					form.StartPosition = FormStartPosition.CenterScreen;
					this.ShowChildForm(form);
				}
			}
			else if (!hostView)
				MessageBox.Show("Please send " + Game.Current.CommandFileName + " to the game host.");
			else
				MessageBox.Show("Commands saved for " + Empire.Current + ".");
		}
	}

	private void searchBox_ObjectSelected(SearchBox sender, ISpaceObject sobj)
	{
		SelectedSpaceObject = sobj;
		searchBox.HideResults();
	}

	private void SelectTab(FlowLayoutPanel tab)
	{
		btnTab_Click(tab.Controls[0], new EventArgs());
	}

	/// <summary>
	/// Sets mouse down handler for a control and all its children recursively.
	/// </summary>
	/// <param name="ctl"></param>
	/// <param name="h"></param>
	private void SetMouseDownHandler(Control ctl, MouseEventHandler h)
	{
		ctl.MouseDown += h;
		foreach (Control c in ctl.Controls)
			SetMouseDownHandler(c, h);
	}

	private FlowLayoutPanel SetTabSystem(FlowLayoutPanel tab, StarSystem sys)
	{
		// check for existing tab and return that instead
		// no need to have multiple tabs for the same system!
		var oldTab = FindTab(sys);
		if (oldTab != null)
			return oldTab;

		var btnTab = (GameButton)tab.Controls[0];
		btnTab.Tag = sys;
		if (sys == null)
			btnTab.Text = "(No System)";
		else
			btnTab.Text = sys.Name;
		if (tab == currentTab)
			SelectTab(tab);

		return tab;
	}

	private void SetUpGui()
	{
		// set title
		ChangeCommandMode(CommandMode.None, null);

		// select nothing
		SelectedSpaceObject = null;

		// start music
		Music.Play(MusicMode.Strategic, FindMusicMood());

		// display empire flag
		picEmpireFlag.Image = Game.Current.CurrentEmpire.Icon;

		// create homesystem tab
		foreach (var tab in ListTabs().ToArray())
			RemoveTab(tab);
		var highlyPopulated = Empire.Current.ExploredStarSystems.WithMax(sys => sys.FindSpaceObjects<Planet>(p => p.Owner == Empire.Current).Sum(p => p.Colony.Population.Sum(kvp => kvp.Value)));
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
			searchBox.ObjectsToSearch = Galaxy.Current.FindSpaceObjects<ISpaceObject>();

		// compute warp point connectivity
		galaxyView.ComputeWarpPointConnectivity();
	}

	private void SetUpResourceDisplay()
	{
		rqdInventory.ResourcesToShow = [Resource.Minerals, Resource.Organics, Resource.Radioactives, Resource.Research, Resource.Intelligence];
		rqdInventory.Amounts = Empire.Current.StoredResources;
		rqdInventory.Changes = Empire.Current.NetIncomeLessConstruction;
	}

	private void SetWaypoint(int waypointNumber, bool redirect)
	{
		// find selected space object or sector and create a waypoint
		Waypoint waypoint = null;
		if (SelectedSpaceObject != null)
			waypoint = new SpaceObjectWaypoint(SelectedSpaceObject);
		else if (starSystemView.SelectedSector != null)
			waypoint = new SectorWaypoint(starSystemView.SelectedSector);

		// set the waypoint
		ICommand cmd;
		cmd = DIRoot.WaypointCommands.CreateWaypoint(waypoint);
		Empire.Current.Commands.Add(cmd);
		cmd.Execute();
		cmd = DIRoot.WaypointCommands.HotkeyWaypoint(waypoint, waypointNumber, redirect);
		Empire.Current.Commands.Add(cmd);
		cmd.Execute();

		// refresh the map and orders
		starSystemView.Invalidate();
		BindReport();
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
				lastSector = SelectedSpaceObject.Sector;
			var form = new CargoTransferForm((ICargoTransferrer)SelectedSpaceObject, lastSector);
			this.ShowChildForm(form);
			BindReport();
		}
	}

	private void ShowConstructionQueueForm(ISpaceObject sobj)
	{
		this.ShowChildForm(new ConstructionQueueForm((SelectedSpaceObject as IConstructor)?.ConstructionQueue));
		BindReport();
		SetUpResourceDisplay();
	}

	private void ShowConstructionQueueListForm()
	{
		this.ShowChildForm(new ConstructionQueueListForm());
		BindReport();
		SetUpResourceDisplay();
	}

	private void ShowFleetTransferForm()
	{
		if (IsFleetTransferOperationValid)
		{
			var form = new FleetTransferForm(starSystemView.SelectedSector);
			if (this.ShowChildForm(form) == DialogResult.OK)
				starSystemView.SelectedSector = starSystemView.SelectedSector; // reselect sector since we *probably* don't want to see what was selected before (might be a ship in a fleet now)
			starSystemView.Invalidate();
		}
	}

	private void SpaceObjectListReport_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			var lv = (ListView)sender;
			var item = lv.GetItemAt(e.X, e.Y);
			if (item != null)
				SelectedSpaceObject = (ISpaceObject)item.Tag;
		}
	}

	private void starSystemView_SectorClicked(StarSystemView sender, Sector sector)
	{
		if (commandMode == CommandMode.Move)
		{
			if (sector != null && sector.StarSystem.ExploredByEmpires.Contains(Empire.Current) && SelectedSpaceObject is IMobileSpaceObject)
			{
				// move ship to sector clicked
				var v = (IMobileSpaceObject)SelectedSpaceObject;
				var order = new MoveOrder(sector, !aggressiveMode);
				v.AddOrder(order);
				BindReport();
				var cmd = DIRoot.OrderCommands.AddOrder(v, order);
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
					IssueSpaceObjectOrder(new PursueOrder(target, !(aggressiveMode || target is ICombatant c && c.IsHostileTo(Empire.Current))));
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
					IssueSpaceObjectOrder(new EvadeOrder(target, !aggressiveMode));
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
					var form = new GenericPickerForm(sector.SpaceObjects.OfType<WarpPoint>());
					form.Text = "Use which warp point?";
					this.ShowChildForm(form);
					if (form.DialogResult == DialogResult.OK)
						wp = (WarpPoint)form.SelectedObject;
				}

				if (wp != null)
				{
					IssueSpaceObjectOrder(new PursueOrder(wp, !aggressiveMode));
					IssueSpaceObjectOrder(new WarpOrder(wp));
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
					var suitablePlanets = sector.SpaceObjects.OfType<Planet>().Where(p => p.Colony == null && v.Abilities().Any(a => a.Rule.Matches("Colonize Planet - " + p.Surface)));
					if (Game.Current.Setup.CanColonizeOnlyBreathable)
						suitablePlanets = suitablePlanets.Where(p => p.Atmosphere == Empire.Current.PrimaryRace.NativeAtmosphere);
					if (Game.Current.Setup.CanColonizeOnlyHomeworldSurface)
						suitablePlanets = suitablePlanets.Where(p => p.Surface == Empire.Current.PrimaryRace.NativeSurface);
					if (suitablePlanets.Any())
					{
						Planet planet = null;
						if (suitablePlanets.Count() == 1)
							planet = suitablePlanets.Single();
						else
						{
							var form = new GenericPickerForm(suitablePlanets);
							form.Text = "Colonize which planet?";
							this.ShowChildForm(form);
							if (form.DialogResult == DialogResult.OK)
								planet = (Planet)form.SelectedObject;
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
										delta.AllPopulation = true;
										var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
										IssueSpaceObjectOrder(loadPopOrder);
									}
								}
							}
							IssueSpaceObjectOrder(new MoveOrder(sector, !aggressiveMode));
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
					if (sobj is Planet p)
					{
						var img = sobj.Portrait.Resize(48);
						img = img.DrawPopulationBars(p);
						p.DrawStatusIcons(img);
						il.Images.Add(img);
					}
					else
						il.Images.Add(sobj.Portrait.Resize(48));
					item.ImageIndex = i;
					i++;
					lv.Items.Add(item);
				}
				lv.MouseClick += SpaceObjectListReport_MouseClick;
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
			// deselect space object
			SelectedSpaceObject = null;

			// show star system report
			var newReport = new StarSystemReport(starSystemView.StarSystem);
			pnlDetailReport.Controls.Add(newReport);
			newReport.Left = newReport.Margin.Left;
			newReport.Width = pnlDetailReport.Width - newReport.Margin.Right - newReport.Margin.Left;
			newReport.Top = newReport.Margin.Top;
			newReport.Height = pnlDetailReport.Height - newReport.Margin.Bottom - newReport.Margin.Top;
			newReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		}
	}

	private void btnCommands_Click(object sender, EventArgs e)
	{
		this.ShowChildForm(new CommandsForm());
	}

	private void VehicleFleetReport_OrdersChanged()
	{
		starSystemView.Invalidate(); // show move lines
	}

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
}
