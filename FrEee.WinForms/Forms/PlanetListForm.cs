using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;

namespace FrEee.WinForms.Forms
{
	public partial class PlanetListForm : Form
	{
		public PlanetListForm()
		{
			InitializeComponent();
            this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);
		}

		private void PlanetListForm_Load(object sender, EventArgs e)
		{
			if (Galaxy.Current == null)
				return;

			// show planet counts
			var systems = Galaxy.Current.CurrentEmpire.ExploredStarSystems;
			txtSystems.Text = systems.Count().ToString();
			// HACK - why are there null explored star systems?
			var planets = systems.Where(sys => sys != null).SelectMany(sys => sys.FindSpaceObjects<Planet>().SelectMany(g => g));
			txtPlanets.Text = planets.Count().ToString();
			// TODO - colonizable planets and various subcategories
			// TODO - colony ships

			// show galaxy view
			galaxyView.Galaxy = Galaxy.Current;

			// show planet data
			planetBindingSource.DataSource = planets.ToList();
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
	}
}
