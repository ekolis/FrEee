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
	public partial class ShipListForm : Form
	{
		public ShipListForm()
		{
			InitializeComponent();
            try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private IEnumerable<IMobileSpaceObject> sobjs;

		private void ShipListForm_Load(object sender, EventArgs e)
		{
			if (Galaxy.Current == null)
				return;

			// show ship/unit/fleet counts
			sobjs = Galaxy.Current.FindSpaceObjects<IMobileSpaceObject>().Where(o => !(o is Planet) && (!(o is IUnit && ((IUnit)o).Container == null)));
			var ours = sobjs.Where(o => o.Owner == Empire.Current);
			var ourShips = ours.OfType<SpaceVehicle>();
			txtShips.Text = ourShips.Count().ToString();
			txtShipsOutsideFleets.Text = ourShips.Where(s => s.Container == null).Count().ToString();
			var ourFleets = ours.OfType<Fleet>();
			txtFleets.Text = ourFleets.Count().ToString();
			txtFleetsOutsideFleets.Text = ourFleets.Where(f => f.Container == null).Count().ToString();
			var alienShips = sobjs.OfType<SpaceVehicle>();
			txtAlienShips.Text = alienShips.Count().ToString();
			txtAllyShips.Text = alienShips.Where(s => s.Owner.IsAllyOf(Empire.Current, null)).Count().ToString();
			txtEnemyShips.Text = alienShips.Where(s => s.Owner.IsEnemyOf(Empire.Current, null)).Count().ToString();
			txtNonAlignedShips.Text = alienShips.Where(s => s.Owner.IsNeutralTo(Empire.Current, null)).Count().ToString();
			
			resMaintenanaceMin.Amount = ourShips.Sum(s => s.MaintenanceCost[Resource.Minerals]);
			resMaintenanceOrg.Amount = ourShips.Sum(s => s.MaintenanceCost[Resource.Organics]);
			resMaintenanceRad.Amount = ourShips.Sum(s => s.MaintenanceCost[Resource.Radioactives]);
			
			// show ship/fleet/unit data
			grid.Data = sobjs.Where(o => o.Container == null).ToArray();
			grid.CreateDefaultGridConfig = ClientSettings.CreateDefaultShipListConfig;
			grid.LoadCurrentGridConfig = () => ClientSettings.Instance.CurrentShipListConfig;
			grid.LoadGridConfigs = () => ClientSettings.Instance.ShipListConfigs;
			grid.ResetGridConfigs = () => new List<GridConfig> { ClientSettings.CreateDefaultShipListConfig(), ClientSettings.CreateDefaultAlienShipListConfig() };
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
			var sobj = (IMobileSpaceObject)grid.SelectedItem;
			if (sobj != null)
				galaxyView.SelectedStarSystem = sobj.StarSystem;
			else
				galaxyView.SelectedStarSystem = null;
		}

		private void grid_RowLeave(object sender, DataGridViewCellEventArgs e)
		{
			galaxyView.SelectedStarSystem = null;
		}

		private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var sobj = (ISpaceObject)grid.SelectedItem;
				if (sobj != null)
				{
					GameForm.Instance.SelectSpaceObject(sobj);
					Close();
				}
			}
		}
	}
}
