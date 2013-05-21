using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Game;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FrEee.WinForms.Forms
{
	public partial class GameForm : Form
	{
		public GameForm()
		{
			InitializeComponent();

			this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);
			this.Enabled = false;

			// set up GUI images
			btnMenu.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Menu"));
			btnDesigns.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Designs"));
			btnPlanets.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Planets"));
			btnEmpires.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Empires"));
			btnShips.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Ships"));
			btnQueues.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Queues"));
			btnLog.Image = Pictures.GetCachedImage(Path.Combine("Pictures", "UI", "Buttons", "Log"));

			// load the stock mod
			Mod.Load(null);

			var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
			Task.Factory.StartNewWithExceptionHandling(() =>
				{
					// create the game
					var galtemp = Mod.Current.GalaxyTemplates.PickRandom();
					var gsu = new GameSetup
					{
						GalaxyTemplate = galtemp,
						StarSystemCount = 10,
						GalaxySize = new System.Drawing.Size(40, 30)
					};
					gsu.Empires.Add(new Empire { Name = "Jraenar Empire", Color = Color.Red, EmperorTitle = "Master General", EmperorName = "Jar-Nolath" });
					gsu.Empires.Add(new Empire { Name = "Eee Consortium", Color = Color.Cyan });
					gsu.Empires.Add(new Empire { Name = "Drushocka Empire", Color = Color.Green });
					gsu.Empires.Add(new Empire { Name = "Norak Ascendancy", Color = Color.Blue });
					gsu.Empires.Add(new Empire { Name = "Abbidon Enclave", Color = Color.Orange });
					gsu.CreateGalaxy();

					// test saving the game
					var sw = new StreamWriter("save.gam");
					sw.Write(Galaxy.Current.SerializeGameState());
					sw.Close();

					// test loading the game
					var sr = new StreamReader("save.gam");
					Galaxy.Current = Galaxy.DeserializeGameState(sr);
					sr.Close();

					// test redacting fogged info
					Galaxy.Current.CurrentEmpire = Galaxy.Current.Empires[0];
					Galaxy.Current.Redact();

					// test saving the player's view
					sw = new StreamWriter("p1.gam");
					sw.Write(Galaxy.Current.SerializeGameState());
					sw.Close();
				})
				.ContinueWith(t =>
				{
					if (t.Exception != null)
						throw t.Exception;

					// set up resource display
					var pnlResources = new FlowLayoutPanel();
					pnlResources.FlowDirection = FlowDirection.LeftToRight;
					pnlResources.WrapContents = false;
					pnlResources.Controls.Add(new ResourceDisplay { ResourceColor = Color.Blue, Amount = Galaxy.Current.CurrentEmpire.StoredResources["Minerals"], Change = Galaxy.Current.CurrentEmpire.Income["Minerals"] });
					pnlResources.Controls.Add(new ResourceDisplay { ResourceColor = Color.Green, Amount = Galaxy.Current.CurrentEmpire.StoredResources["Organics"], Change = Galaxy.Current.CurrentEmpire.Income["Organics"] });
					pnlResources.Controls.Add(new ResourceDisplay { ResourceColor = Color.Red, Amount = Galaxy.Current.CurrentEmpire.StoredResources["Radioactives"], Change = Galaxy.Current.CurrentEmpire.Income["Radioactives"] });
					var pnlResIntel = new FlowLayoutPanel();
					pnlResIntel.FlowDirection = FlowDirection.LeftToRight;
					pnlResIntel.WrapContents = false;
					pnlResIntel.Controls.Add(new ResourceDisplay { ResourceColor = Color.Magenta, Amount = 50000 });
					pnlResIntel.Controls.Add(new ResourceDisplay { ResourceColor = Color.White, Amount = 10000 });
					pagResources.Content = new List<Control>();
					pagResources.Content.Add(pnlResources);
					pagResources.Content.Add(pnlResIntel);
					pagResources.CurrentPage = 0;

					// set up GUI
					galaxyView.Galaxy = Galaxy.Current;
					starSystemView.StarSystem = galaxyView.SelectedStarSystem = Galaxy.Current.CurrentEmpire.ExploredStarSystems.PickRandom();
					Text = "FrEee - " + Galaxy.Current.CurrentEmpire.Name + " - " + Galaxy.Current.CurrentEmpire.EmperorTitle + " " + Galaxy.Current.CurrentEmpire.EmperorName;
					picEmpireFlag.Image = Galaxy.Current.CurrentEmpire.Flag;

					Enabled = true;
				}, scheduler);
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

			if (sector == null)
				return;

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
			var form = new PlanetListForm();
			form.ShowDialog();
		}

		private void btnQueues_Click(object sender, EventArgs e)
		{
			var form = new ConstructionQueueListForm();
			form.ShowDialog();
		}
	}
}
