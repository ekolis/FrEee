using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Gui.Controls;
using FrEee.Game;

namespace FrEee
{
	public partial class GameForm : Form
	{
		public GameForm()
		{
			InitializeComponent();

			// set up resource display
			var pnlResources = new FlowLayoutPanel();
			pnlResources.FlowDirection = FlowDirection.LeftToRight;
			pnlResources.WrapContents = false;
			pnlResources.Controls.Add(new ResourceDisplay { ResourceColor = Color.Blue, Amount = 500000, Change = -25000});
			pnlResources.Controls.Add(new ResourceDisplay { ResourceColor = Color.Green, Amount = 250000, Change = +10000});
			pnlResources.Controls.Add(new ResourceDisplay { ResourceColor = Color.Red, Amount = 0, Change = -5000});
			var pnlResIntel = new FlowLayoutPanel();
			pnlResIntel.FlowDirection = FlowDirection.LeftToRight;
			pnlResIntel.WrapContents = false;
			pnlResIntel.Controls.Add(new ResourceDisplay { ResourceColor = Color.Magenta, Amount = 50000 });
			pnlResIntel.Controls.Add(new ResourceDisplay { ResourceColor = Color.White, Amount = 10000 });
			pagResources.Content = new List<Control>();
			pagResources.Content.Add(pnlResources);
			pagResources.Content.Add(pnlResIntel);
			pagResources.CurrentPage = 0;

			// set up system view
			var starsys = new StarSystem(8);
			starsys.GetSector(0, 0).SpaceObjects.Add(new Star());
			starsys.GetSector(6, 3).SpaceObjects.Add(new Planet());
			starsys.GetSector(1, 5).SpaceObjects.Add(new Planet());
			starsys.GetSector(1, 5).SpaceObjects.Add(new Planet());
			starsys.GetSector(0, 8).SpaceObjects.Add(new WarpPoint());
			starSystemView.StarSystem = starsys;
		}

		private void starSystemView_SectorClicked(StarSystemView sender, Sector sector)
		{
			// select the sector that was clicked
			starSystemView.SelectedSector = sector;
		}
	}
}
