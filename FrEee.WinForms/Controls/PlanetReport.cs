using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class PlanetReport : UserControl
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
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
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


				picOwnerFlag.Image = Planet.Owner == null ? null : Planet.Owner.Flag;
				picPortrait.Image = Planet.Portrait;

				txtName.Text = Planet.Name;
				txtSizeSurface.Text = Planet.Size + " " + Planet.Surface + " Planet";
				txtAtmosphere.Text = Planet.Atmosphere;
				txtConditions.Text = ""; // TODO - load conditions

				txtValueMinerals.Text = Planet.ResourceValue["Minerals"].ToUnitString();
				txtValueOrganics.Text = Planet.ResourceValue["Organics"].ToUnitString();
				txtValueRadioactives.Text = Planet.ResourceValue["Radioactives"].ToUnitString();

				txtDescription.Text = Planet.Description;

				txtColonyType.Text = Planet.Owner == null ? "" : Planet.Owner.Name + " Colony"; // TODO - load colony type
				txtPopulation.Text = ""; // TODO - load population
				txtReproduction.Text = ""; // TODO - load reproduction
				txtMood.Text = ""; // TODO - load mood

				// load income
				var income = Planet.Income;
				resIncomeMinerals.Amount = income["Minerals"];
				resIncomeOrganics.Amount = income["Organics"];
				resIncomeRadioactives.Amount = income["Radioactives"];
				resResearch.Amount = income["Research"];
				resIntel.Amount = income["Intelligence"];

				// TODO - load construction data
				txtConstructionItem.Text = "";
				txtConstructionTime.Text = "";

				lstOrdersDetail.Items.Clear();
				// TODO - load orders

				// TODO - load facility slots free
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

				// TODO - load cargo
				txtCargoSpaceFree.Text = string.Format("{0} / {0} free", Planet.MaxCargo.Kilotons());
				lstCargoDetail.Items.Clear();

				abilityTreeView.Abilities = Planet.Abilities.StackToTree();
				if (Planet.Colony == null)
					abilityTreeView.IntrinsicAbilities = Planet.IntrinsicAbilities;
				else
					abilityTreeView.IntrinsicAbilities = Planet.IntrinsicAbilities.Concat(Planet.Colony.Abilities);

			}

			base.OnPaint(e);
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Planet.Name);
		}
	}
}
