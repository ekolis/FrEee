using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Technology;
using FrEee.Utility;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// Displays a report on a vehicle design.
	/// </summary>
	public partial class DesignReport : UserControl, IBindable<IDesign>
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

		private IDesign design;

		public void Bind()
		{
			SuspendLayout();
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
				resMaintMin.Amount = Design.MaintenanceCost[Resource.Minerals];
				resMaintOrg.Amount = Design.MaintenanceCost[Resource.Organics];
				resMaintRad.Amount = Design.MaintenanceCost[Resource.Radioactives];
				txtSpeed.Text = Design.StrategicSpeed.ToString() + " sec/turn; " + Design.CombatSpeed.ToString("f2") + "km/s";
				var genPerTurn = Design.GetAbilityValue("Supply Generation Per Turn").ToInt();
				var genPerStar = Design.GetAbilityValue("Solar Supply Generation").ToInt();
				txtSupplyStorage.Text = "Cap. " + Design.SupplyStorage.ToUnitString(true) + ", " + genPerTurn.ToUnitString(true) + "/turn, " + genPerStar.ToUnitString(true) + "/star, -" + Design.SupplyUsagePerSector + "/sector";
				var burnPerTurn = Design.SupplyUsagePerSector * Design.StrategicSpeed;
				var range = GetRange(Design.SupplyStorage, Design.StrategicSpeed, Design.SupplyUsagePerSector, genPerTurn);
				var rangeWithOneStar = GetRange(Design.SupplyStorage, Design.StrategicSpeed, Design.SupplyUsagePerSector, genPerTurn + genPerStar);
				if (Design.StrategicSpeed == 0)
					txtRange.Text = "0 sectors";
				else if (range == null || Design.HasAbility("Quantum Reactor"))
					txtRange.Text = "Unlimited";
				else if (genPerStar == 0)
					txtRange.Text = range + " sectors";
				else if (rangeWithOneStar == null)
					txtRange.Text = range + " sectors (unlimited w/star)";
				else
					txtRange.Text = range + " sectors (" + rangeWithOneStar + " w/star)";
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
			ResumeLayout();
		}

		public void Bind(IDesign data)
		{
			Design = data;
			Bind();
		}

		private int? GetRange(int capacity, int speed, int usagePerSector, int generationPerTurn)
		{
			if (speed == 0)
				return 0;
			if (usagePerSector <= generationPerTurn / speed)
				return null; // infinity
			return capacity / (usagePerSector + (generationPerTurn / speed));
		}

		private void lstComponents_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var item = lstComponents.GetItemAt(e.X, e.Y);
				if (item != null)
				{
					var g = (IGrouping<MountedComponentTemplate, MountedComponentTemplate>)item.Tag;
					var mct = g.Key;
					Cursor = Cursors.WaitCursor;
					var form = new ComponentReport(mct).CreatePopupForm(mct.Name);
					form.ShowDialog();
					Cursor = Cursors.Default;
				}
			}
		}

		private void picPortrait_Click(object sender, System.EventArgs e)
		{
			picPortrait.ShowFullSize(Design.Name);
		}
	}
}
