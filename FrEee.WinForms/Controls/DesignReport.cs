using System.Windows.Forms;
using System.Linq;
using FrEee.Game.Objects.Space;
using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System.Drawing;
using FrEee.Utility; using Resources = FrEee.Utility.Resources;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// Displays a report on a vehicle design.
	/// </summary>
	public partial class DesignReport : UserControl
	{
		public DesignReport()
		{
			InitializeComponent();
		}

		public DesignReport(IDesign design)
			: this()
		{
			Design = design;
		}

		private IDesign design;

		/// <summary>
		/// The design for which to display a report.
		/// </summary>
		public IDesign Design
		{
			get { return design; }
			set
			{
				design = value;
				Bind();
			}
		}

		public void Bind()
		{
			if (Design == null)
				Visible = false;
			else
			{
				Visible = true;
				picPortrait.Image = Design.Portrait;
				txtName.Text = Design.Name;
				txtVehicleType.Text = Design.VehicleTypeName;
				txtHull.Text = Design.Hull.Name + " (" + Design.Hull.Size.Kilotons() + ")";
				txtRole.Text = Design.Role;
				txtStrategy.Text = ""; // TODO - implement design combat strategies
				txtDate.Text = Design.TurnNumber.ToStardate();

				lstComponents.Items.Clear();
				lstComponents.Initialize(32, 32);
				foreach (var g in Design.Components.GroupBy(mct => mct))
					lstComponents.AddItemWithImage(g.First().ComponentTemplate.Group, g.Count() + "x " + g.First().ToString(), g, g.First().Icon);

				resCostMin.Amount = Design.Cost[Resource.Minerals];
				resCostOrg.Amount = Design.Cost[Resource.Organics];
				resCostRad.Amount = Design.Cost[Resource.Radioactives];
				txtSpeed.Text = Design.Speed.ToString() + " sectors/turn";
				txtSupplyStorage.Text = Design.GetAbilityValue("Supply Storage");
				txtSupplyUsage.Text = Design.SupplyUsagePerSector.ToString();
				if (Design.Speed == 0)
					txtRange.Text = "0 sectors";
				else if (Design.SupplyUsagePerSector == 0 || Design.HasAbility("Quantum Reactor"))
					txtRange.Text = "Unlimited";
				else
					txtRange.Text = (Design.GetAbilityValue("Supply Storage").ToInt() / Design.SupplyUsagePerSector) + " sectors";
				txtShieldsHitpoints.Text = Design.ShieldHitpoints + " shields (+" + Design.ShieldRegeneration + " regen)";
				txtArmorHitpoints.Text = Design.ArmorHitpoints + " armor";
				txtHullHitpoints.Text = Design.HullHitpoints + " hull";
				if (Design.Accuracy > 0)
					txtAccuracy.Text = Design.Accuracy.ToString("+0") + "%";
				else
					txtAccuracy.Text = Design.Accuracy.ToString("0") + "%";
				if (Design.Evasion > 0)
					txtEvasion.Text = Design.Evasion.ToString("+0") + "%";
				else
					txtEvasion.Text = Design.Evasion.ToString("0") + "%";
				txtCargo.Text = Design.CargoCapacity.Kilotons();
			}
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Design.Name);
		}
	}
}
