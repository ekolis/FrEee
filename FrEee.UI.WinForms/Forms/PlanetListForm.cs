using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.DataGridView;
using FrEee.UI.WinForms.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Ecs;

namespace FrEee.UI.WinForms.Forms;

public partial class PlanetListForm : GameForm
{
	public PlanetListForm()
	{
		InitializeComponent();
		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
		grid.AppendMenuItems = contextMenu.Items.Cast<ToolStripMenuItem>().ToArray();
		contextMenu.Items.Clear();
		//BindTabs();
	}

	private IEnumerable<SpaceVehicle> colonizers, availableColonizers;

	private IEnumerable<Planet> planets;

	private void colonizeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		var p = (Planet)grid.SelectedItem;
		if (p == null)
		{
			MessageBox.Show("No planet is selected to colonize.");
			return;
		}
		var goodColonizers = availableColonizers.Where(c => c.HasAbility("Colonize Planet - " + p.Surface));
		if (!goodColonizers.Any())
		{
			MessageBox.Show("We have no colonizers capable of landing on a " + p.Surface + " planet.");
			return;
		}
		var paths = goodColonizers.Select(c => new { Colonizer = c, Path = Pathfinder.Pathfind(c, c.Sector, p.Sector, true, true, c.DijkstraMap) });
		var goodPaths = paths.Where(path => path.Path.Last() == p.Sector);
		if (!goodPaths.Any())
		{
			MessageBox.Show("We have no colonizers capable of reaching " + p + ".");
		}
		var shortest = paths.WithMin(path => path.Path.Count()).First();
		var colonizer = (MajorSpaceVehicle)shortest.Colonizer; // HACK - what if units want to colonize?

		// load population
		// prefer population of breathers of target planet's atmosphere - don't load nonbreathers races if breathers are present
		bool foundBreathers = false;
		var planets = colonizer.FinalSector().SpaceObjects.OfType<Planet>().Where(pl => pl.Owner == Empire.Current);
		foreach (var pHere in planets)
		{
			var delta = new CargoDelta();
			foreach (var kvp in pHere.AllPopulation)
			{
				if (kvp.Key.NativeAtmosphere == p.Atmosphere)
				{
					delta.RacePopulation[kvp.Key] = null; // load all population of this race
					foundBreathers = true;
				}
			}
			if (foundBreathers)
			{
				var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
				colonizer.IssueOrder(loadPopOrder);
			}
		}
		if (!foundBreathers)
		{
			foreach (var pHere in planets)
			{
				var delta = new CargoDelta();
				delta.AllPopulation = true;
				var loadPopOrder = new TransferCargoOrder(true, delta, pHere);
				colonizer.IssueOrder(loadPopOrder);
			}
		}

		// move and colonize
		colonizer.IssueOrder(new MoveOrder(p.Sector, true));
		colonizer.IssueOrder(new ColonizeOrder(p));
	}

	private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			var sobj = (ISpaceObject)grid.SelectedItem;
			if (sobj != null)
			{
				MainGameForm.Instance.SelectSpaceObject(sobj);
				Close();
			}
		}
	}

	private void grid_RowEnter(object sender, DataGridViewCellEventArgs e)
	{
		var planet = (Planet)grid.SelectedItem;
		foreach (var sys in Galaxy.Current.CurrentEmpire.ExploredStarSystems)
		{
			if (sys.FindSpaceObjects<Planet>().Any(p => p == planet))
			{
				galaxyView.SelectedStarSystem = sys;
				break;
			}
		}
	}

	private void grid_RowLeave(object sender, DataGridViewCellEventArgs e)
	{
		galaxyView.SelectedStarSystem = null;
	}

	private void PlanetListForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		// save client settings
		ClientSettings.Save();
	}

	private void PlanetListForm_Load(object sender, EventArgs e)
	{
		if (Galaxy.Current == null)
			return;

		// show planet counts
		var systems = Empire.Current.ExploredStarSystems;
		txtSystems.Text = systems.Count().ToString();
		txtSystemsWithColonies.Text = systems.Where(s => s.FindSpaceObjects<Planet>(p => p.Owner == Empire.Current).Any()).Count().ToString();
		// HACK - why are there null explored star systems?
		planets = systems.Where(sys => sys != null).SelectMany(sys => sys.FindSpaceObjects<Planet>());
		txtPlanets.Text = planets.Count().ToString();
		txtUs.Text = Empire.Current.ColonizedPlanets.Count().ToString();
		var colonizable = planets.Where(p => p.Owner != Empire.Current && Empire.Current.CanColonize(p));
		txtColonizable.Text = colonizable.Count().ToString();
		txtEnemies.Text = colonizable.Count(p => p.Owner != null && p.Owner.IsEnemyOf(Empire.Current, p.StarSystem)).ToString();
		txtAllies.Text = colonizable.Count(p => p.Owner != null && p.Owner.IsAllyOf(Empire.Current, p.StarSystem)).ToString();
		txtNonAligned.Text = colonizable.Count(p => p.Owner != null && p.Owner.IsNeutralTo(Empire.Current, p.StarSystem)).ToString();
		var uncolonized = colonizable.Where(p => p.Owner == null);
		txtUncolonized.Text = uncolonized.Count().ToString();
		txtBreathableUs.Text = uncolonized.Where(p => p.Atmosphere == Empire.Current.PrimaryRace.NativeAtmosphere).Count().ToString();
		var otherAtmospheres = Empire.Current.ColonizedPlanets.SelectMany(p => p.Colony.Population).Select(kvp => kvp.Key.NativeAtmosphere).Distinct().Where(a => a != Empire.Current.PrimaryRace.NativeAtmosphere).Union(Empire.Current.OwnedSpaceObjects.OfType<ICargoContainer>().SelectMany(sobj => sobj.Cargo.Population.Keys.Select(r => r.NativeAtmosphere)).Distinct());
		txtBreathableOther.Text = uncolonized.Where(p => otherAtmospheres.Contains(p.Atmosphere)).Count().ToString();

		// show colony ship counts
		colonizers = Galaxy.Current.FindSpaceObjects<SpaceVehicle>(v =>
			v.Owner == Empire.Current &&
			(
				v.Abilities().Any(a => a.Rule.Name.StartsWith("Colonize Planet - "))
			));
		txtShips.Text = colonizers.Count().ToString();
		availableColonizers = colonizers.Where(v => v.Orders.Count == 0 && v.StrategicSpeed > 0);
		txtAvailable.Text = availableColonizers.Count().ToString();

		// show population and resources
		txtPopulation.Text = Empire.Current.ColonizedPlanets.Sum(p => p.Colony.Population.Sum(kvp => kvp.Value)).ToUnitString(true);
		var stored = Empire.Current.StoredResources;
		var income = Empire.Current.NetIncomeLessConstruction;
		var storage = Empire.Current.ResourceStorage;
		resMin.Amount = stored[Resource.Minerals];
		resMin.Change = income[Resource.Minerals];
		resOrg.Amount = stored[Resource.Organics];
		resOrg.Change = income[Resource.Organics];
		resRad.Amount = stored[Resource.Radioactives];
		resRad.Change = income[Resource.Radioactives];
		resRes.Amount = income[Resource.Research];
		resInt.Amount = income[Resource.Intelligence];
		resStorageMin.Amount = storage[Resource.Minerals];
		resStorageOrg.Amount = storage[Resource.Organics];
		resStorageRad.Amount = storage[Resource.Radioactives];

		// show planet data
		grid.Data = planets.ToArray();
		grid.CreateDefaultGridConfig = ClientSettings.CreateDefaultPlanetListConfig;
		grid.LoadCurrentGridConfig = () => ClientSettings.Instance.CurrentPlanetListConfig;
		grid.LoadGridConfigs = () => ClientSettings.Instance.PlanetListConfigs;
		grid.ResetGridConfigs = () => new List<GridConfig> { ClientSettings.CreateDefaultPlanetListConfig(), ClientSettings.CreateDefaultColonyPlanetListConfig() };
		grid.Initialize();

		// show galaxy view background
		// TODO - galaxy view background image can depend on galaxy template?
		galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "UI", "Map", "quadrant"));
	}
}