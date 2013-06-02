using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System.Drawing;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// A report on a ship or base.
	/// </summary>
	public partial class AutonomousSpaceVehicleReport : UserControl
	{
		public AutonomousSpaceVehicleReport()
		{
			InitializeComponent();
		}

		public AutonomousSpaceVehicleReport(AutonomousSpaceVehicle vehicle)
		{
			InitializeComponent();
			Vehicle = vehicle;
		}

		private AutonomousSpaceVehicle vehicle;
		public AutonomousSpaceVehicle Vehicle { get { return vehicle; } set { vehicle = value; Invalidate(); } }

		private void AutonomousSpaceVehicleReport_Paint(object sender, PaintEventArgs e)
		{
			if (vehicle == null)
				Visible = false;
			else
			{
				// pictures
				picOwnerFlag.Image = vehicle.Owner.Flag;
				picPortrait.Image = vehicle.Portrait;

				// name and stuff
				txtName.Text = vehicle.Name;
				txtRole.Text = vehicle.Design.Role;
				txtClass.Text = vehicle.Design.Name;
				txtSize.Text = vehicle.Design.Hull.Name + " (" + vehicle.Design.Hull.Size.Kilotons() + ")";
				txtMovement.Text = vehicle.MovementRemaining + " / " + vehicle.Design.Speed;
				progMovement.Maximum = vehicle.Design.Speed;
				progMovement.Value = vehicle.MovementRemaining;

				// supplies and stuff
				progSupplies.Maximum = vehicle.Design.SupplyStorage;
				progSupplies.Value = vehicle.SupplyRemaining;
				// will we even use ammo/fuel?
				progAmmunition.Visible = false;
				progFuel.Visible = false;

				// damage
				// TODO - let ships take damage
				progShields.Maximum = vehicle.Design.ShieldHitpoints;
				progShields.Value = progShields.Maximum;
				progArmor.Maximum = vehicle.Design.ArmorHitpoints;
				progArmor.Value = progArmor.Maximum;
				progHull.Maximum = vehicle.Design.HullHitpoints;
				progHull.Value = progHull.Maximum;

				// orders and stuff
				txtOrder.Text = "None"; // TODO - orders for ships
				txtExperience.Text = "None"; // TODO - crew XP
				txtFleet.Text = "None"; // TODO - fleets
				
				// maintenance
				// TODO - compute maintenance modifiers and use mod files for basic rate
				resMaintMin.Amount = vehicle.Design.Cost["Minerals"] / 5;
				resMaintOrg.Amount = vehicle.Design.Cost["Organics"] / 5;
				resMaintRad.Amount = vehicle.Design.Cost["Radioactives"] / 5;

				// component summary
				// TODO - damaged components
				txtComponentsFunctional.Text = vehicle.Components.Count + " / " + vehicle.Components.Count + " functional";
				lstComponentsSummary.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(c => new { Template = c.Template, IsDamaged = false }))
					lstComponentsSummary.AddItemWithImage(null, g.Count().ToString(), g.First(), g.First().Template.Icon);

				// TODO - cargo summary
				txtCargoSpaceFree.Text = vehicle.Design.CargoStorage.Kilotons() + " / " + vehicle.Design.CargoStorage.Kilotons() + " free";
				lstCargoSummary.Initialize(32, 32);

				// TODO - orders detail
				lstOrdersDetail.Items.Clear();

				// component detail
				// TODO - damaged components
				txtComponentsFunctional.Text = vehicle.Components.Count + " / " + vehicle.Components.Count + " functional";
				lstComponentsSummary.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(c => c.Template))
					lstComponentsSummary.AddItemWithImage(null, g.Count() + "x " + g.First().Name, g.First(), g.First().Template.Icon);

				// TODO - cargo detail
				txtCargoSpaceFreeDetail.Text = vehicle.Design.CargoStorage.Kilotons() + " / " + vehicle.Design.CargoStorage.Kilotons() + " free";
				lstCargoDetail.Initialize(32, 32);

				// abilities
				treeAbilities.Nodes.Clear();
				foreach (var group in vehicle.Abilities.GroupBy(abil => abil.Name))
				{
					// TODO - deal with nonstacking abilities
					var branch = new TreeNode(group.Key + ": " + group.Sum(abil =>
					{
						double result = 0;
						double.TryParse(abil.Values.FirstOrDefault(), out result);
						return result;
					}));
					if (group.Any(abil => !vehicle.IntrinsicAbilities.Contains(abil)))
						branch.NodeFont = new Font(Font, FontStyle.Italic);
					treeAbilities.Nodes.Add(branch);
					foreach (var abil in group)
					{
						var twig = new TreeNode(abil.Description);
						if (vehicle.IntrinsicAbilities.Contains(abil))
							twig.NodeFont = new Font(Font, FontStyle.Italic);
						branch.Nodes.Add(twig);
					}
				}
			}
		}
	}
}
