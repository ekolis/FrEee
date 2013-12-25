using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using System.IO;
using System.Collections.Generic;
using FrEee.WinForms.DataGridView;
using FrEee.WinForms.Properties;
using FrEee.WinForms.Controls;
using FrEee.WinForms.Objects;
using System.Reflection;

namespace FrEee.WinForms.Forms
{
	public partial class PlanetListForm : Form
	{
		public PlanetListForm()
		{
			InitializeComponent();
            try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
			//BindTabs();
		}

		private IEnumerable<Planet> planets;

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
			var colonizers = Galaxy.Current.FindSpaceObjects<SpaceVehicle>(v =>
				v.Owner == Empire.Current &&
				(
					v.Abilities().Any(a => a.Rule.Name.StartsWith("Colonize Planet - ")) 
				));
			txtShips.Text = colonizers.Count().ToString();
			txtAvailable.Text = colonizers.Where(v => v.Orders.Count == 0 && v.Speed > 0).Count().ToString();

			// show population and resources
			txtPopulation.Text = Empire.Current.ColonizedPlanets.Sum(p => p.Colony.Population.Sum(kvp => kvp.Value)).ToUnitString(true);
			var stored = Empire.Current.StoredResources;
			var income = Empire.Current.NetIncome;
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

		private void PlanetListForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// save client settings
			ClientSettings.Save();
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
	}
}
