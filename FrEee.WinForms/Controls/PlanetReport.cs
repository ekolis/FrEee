using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.Utility;
using FrEee.WinForms.Utility.Extensions;
using FrEee.WinForms.Interfaces;

namespace FrEee.WinForms.Controls
{
	public partial class PlanetReport : UserControl, IBindable<Planet>
	{
		public PlanetReport()
		{
			InitializeComponent();
		}

		public PlanetReport(Planet planet)
			: this()
		{
			Planet = planet;
		}

		private Planet planet;

		/// <summary>
		/// The planet for which to display a report.
		/// </summary>
		public Planet Planet
		{
			get { return planet; }
			set
			{
				planet = value;
				Bind();
			}
		}

		public void Bind()
		{
			if (Planet == null)
				Visible = false;
			else
			{
				Visible = true;

				bool showColonyInfo = planet.Colony != null;
				pnlColony.Visible = showColonyInfo;
				if (showColonyInfo)
				{
					if (!gameTabControl1.TabPages.Contains(pageFacil))
						gameTabControl1.TabPages.Insert(1, pageFacil);
					if (!gameTabControl1.TabPages.Contains(pageCargo))
						gameTabControl1.TabPages.Insert(2, pageCargo);
					if (!gameTabControl1.TabPages.Contains(pageOrders))
						gameTabControl1.TabPages.Insert(3, pageOrders);
				}
				else
				{
					gameTabControl1.TabPages.Remove(pageFacil);
					gameTabControl1.TabPages.Remove(pageCargo);
					gameTabControl1.TabPages.Remove(pageOrders);
				}


				picOwnerFlag.Image = Planet.Owner == null ? null : Planet.Owner.Icon;
				picPortrait.Image = Planet.Portrait;

				txtName.Text = Planet.Name;
				txtSizeSurface.Text = Planet.Size + " " + Planet.Surface + " Planet";
				txtAtmosphere.Text = Planet.Atmosphere;
				txtConditions.Text = ""; // TODO - load conditions

				txtValueMinerals.Text = Planet.ResourceValue[Resource.Minerals].ToUnitString();
				txtValueOrganics.Text = Planet.ResourceValue[Resource.Organics].ToUnitString();
				txtValueRadioactives.Text = Planet.ResourceValue[Resource.Radioactives].ToUnitString();

				txtDescription.Text = Planet.Description;

				txtColonyType.Text = Planet.Owner == null ? "" : Planet.Owner.Name + " Colony"; // TODO - load colony type
				if (Planet.Owner == null)
					txtPopulation.Text = "0";
				else
				{
					var pop = Planet.Colony.Population.Sum(kvp => kvp.Value);
					if (Planet.PopulationChangePerTurn > 0)
						txtPopulation.Text = pop.ToUnitString(true) + " / " + Planet.MaxPopulation.ToUnitString(true) + " (+" + Planet.PopulationChangePerTurn.ToUnitString(true) + ")";
					else if (Planet.PopulationChangePerTurn < 0)
						txtPopulation.Text = pop.ToUnitString(true) + " / " + Planet.MaxPopulation.ToUnitString(true) + " (" + Planet.PopulationChangePerTurn.ToUnitString(true) + ")";
					else
						txtPopulation.Text = pop.ToUnitString(true) + " / " + Planet.MaxPopulation.ToUnitString(true) + " (stagnant)";
				}

				txtMood.Text = ""; // TODO - load mood

				// load income
				var income = Planet.Income;
				resIncomeMinerals.Amount = income[Resource.Minerals];
				resIncomeOrganics.Amount = income[Resource.Organics];
				resIncomeRadioactives.Amount = income[Resource.Radioactives];
				resResearch.Amount = income[Resource.Research];
				resIntel.Amount = income[Resource.Intelligence];

				// load construction data
				txtConstructionItem.Text = Planet.Colony == null ? "" : (Planet.Colony.ConstructionQueue.FirstItemName ?? "(None)");
				txtConstructionTime.Text = Planet.Colony == null ? "" : (Planet.Colony.ConstructionQueue.FirstItemEta == null ? "" : Planet.Colony.ConstructionQueue.FirstItemEta.ToString("f1") + (Planet.Colony.ConstructionQueue.Eta != Planet.Colony.ConstructionQueue.FirstItemEta ? " (" + Planet.Colony.ConstructionQueue.Eta.Value.ToString("f1") + ")" : ""));

				// load orders
				// TODO - let player adjust orders here
				lstOrdersDetail.Items.Clear();
				foreach (var order in Planet.Orders)
					lstOrdersDetail.Items.Add(order);

				// load facilities
				lstFacilitiesDetail.Items.Clear();
				if (Planet.Colony != null)
				{
					txtFacilitySlotsFree.Text = string.Format("{0} / {1} slots free", Planet.MaxFacilities - Planet.Colony.Facilities.Count, Planet.MaxFacilities);

					var il = new ImageList();
					il.ImageSize = new System.Drawing.Size(32, 32);
					lstFacilitiesDetail.LargeImageList = il;
					lstFacilitiesDetail.SmallImageList = il;
					int i = 0;
					foreach (var fg in Planet.Colony.Facilities.GroupBy(f => f.Template))
					{
						var item = new ListViewItem(fg.Count() + "x " + fg.Key.Name);
						item.ImageIndex = i;
						il.Images.Add((Image)fg.Key.Icon);
						lstFacilitiesDetail.Items.Add(item);
						i++;
					}
				}
				else
					txtFacilitySlotsFree.Text = "";

				// load cargo
				txtCargoSpaceFree.Text = string.Format("{0} / {1} free", (Planet.CargoStorage - (Planet.Cargo == null ? 0 : Planet.Cargo.Size)).Kilotons(), Planet.CargoStorage.Kilotons());
				lstCargoDetail.Initialize(32, 32);
				if (Planet.Cargo != null)
				{
					foreach (var ug in Planet.Cargo.Units.GroupBy(u => u.Design))
						lstCargoDetail.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
					foreach (var pop in Planet.Cargo.Population)
						lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
				}

				abilityTreeView.Abilities = Planet.UnstackedAbilities.StackToTree();
				if (Planet.Colony == null)
					abilityTreeView.IntrinsicAbilities = Planet.IntrinsicAbilities;
				else
					abilityTreeView.IntrinsicAbilities = Planet.IntrinsicAbilities.Concat(Planet.Colony.Abilities);

			}
		}

		public void Bind(Planet data)
		{
			Planet = data;
			Bind();
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Planet.Name);
		}
	}
}
