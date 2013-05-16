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
using FrEee.Modding;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace FrEee.Gui
{
	public partial class GameForm : Form
	{
		private Galaxy galaxy;

		public GameForm()
		{
			InitializeComponent();

			// set up GUI images
			btnMenu.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Menu"));
			btnDesigns.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Designs"));
			btnPlanets.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Planets"));
			btnEmpires.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Empires"));
			btnShips.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Ships"));
			btnQueues.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Queues"));
			btnLog.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Log"));

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
			
			// load the stock mod
			Mod.Load("Stock");

			// create the game
			var galtemp = Mod.Current.GalaxyTemplates.PickRandom();
			var gsu = new GameSetup();
			gsu.GalaxyTemplate = galtemp;
			gsu.StarSystemCount = 10;
			gsu.GalaxySize = new System.Drawing.Size(40, 30);
			gsu.Empires.Add(new Empire { Name = "Jraenar Empire", Color = Color.Red, EmperorTitle = "Master General", EmperorName = "Jar-Nolath" });
			gsu.Empires.Add(new Empire { Name = "Eee Consortium", Color = Color.Cyan });
			gsu.Empires.Add(new Empire { Name = "Drushocka Empire", Color = Color.Green });
			gsu.Empires.Add(new Empire { Name = "Norak Ascendancy", Color = Color.Blue });
			gsu.Empires.Add(new Empire { Name = "Abbidon Enclave", Color = Color.Orange });
			galaxy = gsu.CreateGalaxy();

			// test saving the game
			var sw = new StreamWriter("save.gam");
			var js = new JsonSerializer();
			js.TypeNameHandling = TypeNameHandling.All;
			js.Formatting = Formatting.Indented;
			//js.Converters.Add(new Serialization.GalaxyMapConverter());
			var cr = new DefaultContractResolver();
			cr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
			js.ContractResolver = cr;
			js.PreserveReferencesHandling = PreserveReferencesHandling.All;
			js.Serialize(sw, galaxy);
			sw.Close();

			// test loading the game
			var sr = new StreamReader("save.gam");
			galaxy = js.Deserialize<Galaxy>(new JsonTextReader(sr));
			sr.Close();

			// test redacting fogged info
			galaxy.CurrentEmpire = galaxy.Empires[0];
			galaxy.Redact();

			// test saving the player's view
			sw = new StreamWriter("p1.gam");
			js.Serialize(sw, galaxy);
			sw.Close();

			// set up GUI
			galaxyView.Galaxy = galaxy;
			starSystemView.StarSystem = galaxyView.SelectedStarSystem = galaxy.ExploredStarSystems.PickRandom();
			Text = "FrEee - " + galaxy.CurrentEmpire.Name + " - " + galaxy.CurrentEmpire.EmperorTitle + " " + galaxy.CurrentEmpire.EmperorName;
			picEmpireFlag.Image = galaxy.CurrentEmpire.Flag;
		}

		private void starSystemView_SectorClicked(StarSystemView sender, Sector sector)
		{
			// select the sector that was clicked
			starSystemView.SelectedSector = sector;
		}

		private void starSystemView_SectorSelected(StarSystemView sender, Sector sector)
		{
			// remove old report, if any
			pnlDetailReport.Controls.Clear();

			if (sector.SpaceObjects.Count > 0)
			{
				// add new report
				Control newReport = null;
				if (sector.SpaceObjects.Count == 1)
				{
					// add new report
					newReport = CreateSpaceObjectReport(sector.SpaceObjects.Single());
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
					lv.LargeImageList = il;
					lv.SmallImageList = il;
					int i = 0;
					foreach (var sobj in sector.SpaceObjects)
					{
						var item = new ListViewItem();
						item.Text = sobj.Name;
						item.Tag = sobj;
						il.Images.Add(sobj.Icon);
						item.ImageIndex = i;
						i++;
						lv.Items.Add(item);
					}
					lv.MouseDoubleClick += SpaceObjectListReport_MouseDoubleClick;
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
		}

		void SpaceObjectListReport_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var lv = (ListView)sender;
			if (lv.SelectedItems.Count > 0)
			{
				// remove list view
				pnlDetailReport.Controls.Clear();

				// add new report
				var item = lv.GetItemAt(e.X, e.Y);
				if (item != null)
					pnlDetailReport.Controls.Add(CreateSpaceObjectReport((ISpaceObject)item.Tag));
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
			// TODO - warp point, ship, fleet, unit group reports
			return null;
		}

		private void galaxyView_StarSystemClicked(GalaxyView sender, StarSystem starSystem)
		{
			if (starSystem != null)
				sender.SelectedStarSystem = starSystem;
		}

		private void galaxyView_StarSystemSelected(GalaxyView sender, StarSystem starSystem)
		{
			starSystemView.StarSystem = starSystem;
		}

		private void btnPlanets_Click(object sender, EventArgs e)
		{
			var form = new PlanetListForm(galaxy);
			form.ShowDialog();
		}
	}
}
