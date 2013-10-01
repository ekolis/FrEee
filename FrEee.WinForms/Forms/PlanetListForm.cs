using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.Utility; using FrEee.Utility.Serialization;
using System.IO;
using System.Collections.Generic;
using FrEee.WinForms.DataGridView;
using FrEee.WinForms.Properties;
using FrEee.WinForms.Controls;

namespace FrEee.WinForms.Forms
{
	public partial class PlanetListForm : Form
	{
		public PlanetListForm()
		{
			InitializeComponent();
            try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
			BindTabs();
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
			BindGrid(true);

			// show galaxy view background
			// TODO - galaxy view background image can depend on galaxy template?
			galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine("Pictures", "UI", "Map", "quadrant"));
		}

		private void BindTabs()
		{
			pnlConfigs.Controls.Clear();

			// add buttons for each existing config
			foreach (var cfg in ClientSettings.Instance.PlanetListConfigs)
			{
				var btn = new GameButton();
				btn.Text = cfg.Name;
				btn.Tag = cfg;
				btn.Click += btnConfig_Click;
				pnlConfigs.Controls.Add(btn);
			}

			// add button for a new config
			{
				var btn = new GameButton();
				btn.Text = "(New Config)";
				btn.Click += btnConfig_Click;
				pnlConfigs.Controls.Add(btn);
			}

			// highlight the current config
			foreach (var b in pnlConfigs.Controls.OfType<GameButton>())
				b.BackColor = b.Tag == ClientSettings.Instance.CurrentPlanetListConfig ? Color.Navy : Color.Black;
		}

		void btnConfig_Click(object sender, EventArgs e)
		{
			var btn = (GameButton)sender;
			if (btn.Tag == null)
			{
				// create a new config based on the default and activate it
				var cfg = ClientSettings.CreateDefaultPlanetListConfig();
				ClientSettings.Instance.PlanetListConfigs.Add(cfg);
				ClientSettings.Instance.CurrentPlanetListConfig = cfg;
				btn.Tag = cfg;
				BindTabs();
				BindGrid(true);
				txtConfigName.Text = cfg.Name;
			}
			else
			{
				// activate the selected config
				var cfg = (GridConfig)btn.Tag;
				ClientSettings.Instance.CurrentPlanetListConfig = cfg;
				BindGrid(true);
				txtConfigName.Text = cfg.Name;
			}
			foreach (var b in pnlConfigs.Controls.OfType<GameButton>())
				b.BackColor = b.Tag == ClientSettings.Instance.CurrentPlanetListConfig ? Color.Navy : Color.Black;
		}

		private void BindGrid(bool refreshColumns)
		{
			if (refreshColumns)
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
			}

			var sortedPlanets = planets.OrderBy(p => "");
			foreach (var col in ClientSettings.Instance.CurrentPlanetListConfig.Columns.OrderBy(c => c.SortPriority))
			{
				var gridCol = gridPlanets.Columns.Cast<DataGridViewColumn>().Single(c => c.DataPropertyName == col.PropertyName);
				if (col.Sort == Sort.Ascending)
				{
					sortedPlanets = sortedPlanets.ThenBy(p => p.GetPropertyValue(col.PropertyName));
					gridCol.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
				}
				else if (col.Sort == Sort.Descending)
				{
					sortedPlanets = sortedPlanets.ThenByDescending(p => p.GetPropertyValue(col.PropertyName));
					gridCol.HeaderCell.SortGlyphDirection = SortOrder.Descending;
				}
				else
				{
					gridCol.HeaderCell.SortGlyphDirection = SortOrder.None;
				}
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
			{
				var col = ClientSettings.Instance.CurrentPlanetListConfig.Columns.Single(c => c.PropertyName == prop.Name);

				// cycle sort options
				var minPriority = ClientSettings.Instance.CurrentPlanetListConfig.Columns.Min(c => c.SortPriority);
				if (col.Sort == Sort.None)
				{
					col.Sort = Sort.Ascending;
					col.SortPriority = minPriority - 1;
				}
				else if (col.Sort == Sort.Ascending)
				{
					col.Sort = Sort.Descending;
					col.SortPriority = minPriority - 1;
				}
				else if (col.Sort == Sort.Descending)
				{
					col.Sort = Sort.None;
					col.SortPriority = 0;
				}

				// normalize priorities so we don't get ridiculous negative numbers and cause an overflow when the player clicks 2 billion times
				minPriority = ClientSettings.Instance.CurrentPlanetListConfig.Columns.Min(c => c.SortPriority);
				foreach (var c in ClientSettings.Instance.CurrentPlanetListConfig.Columns.Where(c => c.Sort != Sort.None))
					c.SortPriority -= minPriority;

				BindGrid(false);
			}
		}

		private void btnDeleteConfig_Click(object sender, EventArgs e)
		{
			ClientSettings.Instance.PlanetListConfigs.Remove(ClientSettings.Instance.CurrentPlanetListConfig);
			ClientSettings.Instance.CurrentPlanetListConfig = ClientSettings.Instance.PlanetListConfigs.FirstOrDefault();
			// don't let the player have no configs
			if (ClientSettings.Instance.CurrentPlanetListConfig == null)
			{
				var cfg = ClientSettings.CreateDefaultPlanetListConfig();
				ClientSettings.Instance.CurrentPlanetListConfig = cfg;
				ClientSettings.Instance.PlanetListConfigs.Add(cfg);
			}
			BindTabs();
		}

		private void PlanetListForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// save client settings
			ClientSettings.Save();
		}

		private void txtConfigName_TextChanged(object sender, EventArgs e)
		{
			ClientSettings.Instance.CurrentPlanetListConfig.Name = txtConfigName.Text;
			BindTabs();
		}
	}
}
