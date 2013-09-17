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

namespace FrEee.WinForms.Forms
{
	public partial class PlanetListForm : Form
	{
		public PlanetListForm()
		{
			InitializeComponent();
            try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private IEnumerable<Planet> planets;

		private void PlanetListForm_Load(object sender, EventArgs e)
		{
			if (Galaxy.Current == null)
				return;

			// show planet counts
			var systems = Empire.Current.ExploredStarSystems;
			txtSystems.Text = systems.Count().ToString();
			txtSystemsWithColonies.Text = systems.Where(s => s.FindSpaceObjects<Planet>(p => p.Owner == Empire.Current).Flatten().Any()).Count().ToString();
			// HACK - why are there null explored star systems?
			planets = systems.Where(sys => sys != null).SelectMany(sys => sys.FindSpaceObjects<Planet>().SelectMany(g => g));
			txtPlanets.Text = planets.Count().ToString();
			txtUs.Text = Empire.Current.ColonizedPlanets.Count().ToString();
			var colonizable = planets.Where(p => p.Owner != Empire.Current && Empire.Current.CanColonize(p));
			txtColonizable.Text = colonizable.Count().ToString();
			txtEnemies.Text = "0"; // TODO - war state
			txtAllies.Text = "0"; // TODO - alliances
			txtNonAligned.Text = colonizable.Where(p => p.Owner != null).Count().ToString();
			var uncolonized = colonizable.Where(p => p.Owner == null);
			txtUncolonized.Text = uncolonized.Count().ToString();
			txtBreathableUs.Text = uncolonized.Where(p => p.Atmosphere == Empire.Current.PrimaryRace.NativeAtmosphere).Count().ToString();
			var otherAtmospheres = Empire.Current.ColonizedPlanets.SelectMany(p => p.Colony.Population).Select(kvp => kvp.Key.NativeAtmosphere).Distinct().Where(a => a != Empire.Current.PrimaryRace.NativeAtmosphere).Union(Empire.Current.OwnedSpaceObjects.OfType<ICargoContainer>().SelectMany(sobj => sobj.Cargo.Population.Keys.Select(r => r.NativeAtmosphere)).Distinct());
			txtBreathableOther.Text = uncolonized.Where(p => otherAtmospheres.Contains(p.Atmosphere)).Count().ToString();

			// show colony ship counts
			var colonizers = Galaxy.Current.FindSpaceObjects<AutonomousSpaceVehicle>(v =>
				v.Owner == Empire.Current &&
				(
					v.Abilities.Any(a => a.Name.StartsWith("Colonize Planet - ")) 
				)).Flatten().Flatten();
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
			BindGrid(planets);

			// show galaxy view background
			// TODO - galaxy view background image can depend on galaxy template?
			galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine("Pictures", "UI", "Map", "quadrant"));
		}

		private void BindGrid(IEnumerable<Planet> planets)
		{
			gridPlanets.Columns.Clear();
			foreach (var col in ClientSettings.Instance.CurrentPlanetListConfig.Columns)
			{
				var gridcol = (DataGridViewColumn)Activator.CreateInstance(col.ColumnType);
				gridcol.DataPropertyName = col.PropertyName;
				gridcol.HeaderText = col.HeaderText;
				gridcol.DefaultCellStyle.ForeColor = col.ForeColor;
				gridPlanets.Columns.Add(gridcol);
			}
			var sortedPlanets = planets.OrderBy(p => "");
			foreach (var col in ClientSettings.Instance.CurrentPlanetListConfig.Columns.OrderBy(c => c.SortPriority))
			{
				if (col.Sort == Sort.Ascending)
					sortedPlanets = sortedPlanets.ThenBy(p => p.GetPropertyValue(col.PropertyName));
				else if (col.Sort == Sort.Descending)
					sortedPlanets = sortedPlanets.ThenByDescending(p => p.GetPropertyValue(col.PropertyName));
			}

			planetBindingSource.DataSource = sortedPlanets;
		}

		private void gridPlanets_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			// ignore silly errors
			e.ThrowException = false;
		}

		private void gridPlanets_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			var planet = (Planet)gridPlanets.Rows[e.RowIndex].DataBoundItem;
			foreach (var sys in Galaxy.Current.CurrentEmpire.ExploredStarSystems)
			{
				if (sys.FindSpaceObjects<Planet>().SelectMany(g => g).Any(p => p == planet))
				{
					galaxyView.SelectedStarSystem = sys;
					break;
				}
			}
		}

		private void gridPlanets_RowLeave(object sender, DataGridViewCellEventArgs e)
		{
			galaxyView.SelectedStarSystem = null;
		}

		private void gridPlanets_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var prop = typeof(Planet).GetProperty(gridPlanets.Columns[e.ColumnIndex].DataPropertyName);
			if (typeof(IComparable).IsAssignableFrom(prop.PropertyType))
				planetBindingSource.DataSource = planets.OrderBy(p => prop.GetValue(p, new object[0]));
		}

		private void btnSaveConfig_Click(object sender, EventArgs e)
		{

		}

		private void btnDeleteConfig_Click(object sender, EventArgs e)
		{
		
		}
	}
}
