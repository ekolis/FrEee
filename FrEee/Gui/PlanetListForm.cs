using FrEee.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.Gui
{
	public partial class PlanetListForm : Form
	{
		public PlanetListForm(Galaxy galaxy)
		{
			InitializeComponent();
			this.galaxy = galaxy;
		}

		private Galaxy galaxy;

		private void PlanetListForm_Load(object sender, EventArgs e)
		{
			if (galaxy == null)
				return;

			// show planet counts
			var systems = galaxy.CurrentEmpire.ExploredStarSystems;
			txtSystems.Text = systems.Count.ToString();
			var planets = systems.SelectMany(sys => sys.FindSpaceObjects<Planet>().SelectMany(g => g));
			txtPlanets.Text = planets.Count().ToString();
			// TODO - colonizable planets and various subcategories
			// TODO - colony ships

			// show galaxy view
			galaxyView.Galaxy = galaxy;

			// show planet data
			gridPlanets.DataSource = planets;
		}
	}
}
