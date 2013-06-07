using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Orders;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System.Drawing;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;

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
				txtOrder.Text = vehicle.Orders.Any() ? vehicle.Orders.First().ToString() : "None";
				txtExperience.Text = "None"; // TODO - crew XP
				txtFleet.Text = "None"; // TODO - fleets
				
				// maintenance
				// TODO - compute maintenance modifiers and use mod files for basic rate
				resMaintMin.Amount = vehicle.Design.Cost["Minerals"] / 5;
				resMaintOrg.Amount = vehicle.Design.Cost["Organics"] / 5;
				resMaintRad.Amount = vehicle.Design.Cost["Radioactives"] / 5;

				// component summary
				txtComponentsFunctional.Text = vehicle.Components.Where(c => !c.IsDamaged).Count() + " / " + vehicle.Components.Count + " functional";
				lstComponentsSummary.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(c => c.Template))
				{
					var text = g.Any(c => c.IsDamaged) ? g.Where(c => !c.IsDamaged).Count() + " / " + g.Count() : g.Count().ToString();
					lstComponentsSummary.AddItemWithImage(null, text, g.First(), g.First().Template.Icon);
				}

				// TODO - cargo summary
				txtCargoSpaceFree.Text = vehicle.Design.CargoStorage.Kilotons() + " / " + vehicle.Design.CargoStorage.Kilotons() + " free";
				lstCargoSummary.Initialize(32, 32);

				// orders detail
				lstOrdersDetail.Items.Clear();
				foreach (var o in vehicle.Orders)
					lstOrdersDetail.Items.Add(o);

				// component detail
				txtComponentsFunctional.Text = vehicle.Components.Where(c => !c.IsDamaged).Count() + " / " + vehicle.Components.Count + " functional";
				lstComponentsSummary.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(c => c.Template))
				{
					lstComponentsSummary.AddItemWithImage(null, g.Where(c => !c.IsDamaged).Count() + "x " + g.First().Name, g.First(), g.First().Template.Icon);
					if (g.Where(c => c.IsDamaged).Any())
						lstComponentsSummary.AddItemWithImage(null, g.Where(c => c.IsDamaged).Count() + "x Damaged " + g.First().Name, g.First(), g.First().Template.Icon);
				}

				// TODO - cargo detail
				txtCargoSpaceFreeDetail.Text = vehicle.Design.CargoStorage.Kilotons() + " / " + vehicle.Design.CargoStorage.Kilotons() + " free";
				lstCargoDetail.Initialize(32, 32);

				// abilities
				treeAbilities.Nodes.Clear();
				foreach (var group in vehicle.Abilities.GroupBy(abil => abil.Name))
				{
					foreach (var stacked in group.Stack())
					{
						var branch = new TreeNode(stacked.ToString());
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

		private void btnOrderToTop_Click(object sender, System.EventArgs e)
		{
			var order = (IMobileSpaceObjectOrder<AutonomousSpaceVehicle>)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var cmd = new RearrangeOrdersCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(
					Empire.Current, vehicle, order, -vehicle.Orders.IndexOf(order));
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Invalidate();
			}
		}

		private void btnOrderToBottom_Click(object sender, System.EventArgs e)
		{
			var order = (IMobileSpaceObjectOrder<AutonomousSpaceVehicle>)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var cmd = new RearrangeOrdersCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(
					Empire.Current, vehicle, order, Vehicle.Orders.Count - vehicle.Orders.IndexOf(order) - 1);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Invalidate();
			}
		}

		private void btnOrderGoesUp_Click(object sender, System.EventArgs e)
		{
			var order = (IMobileSpaceObjectOrder<AutonomousSpaceVehicle>)lstOrdersDetail.SelectedItem;
			if (order != null && vehicle.Orders.IndexOf(order) > 0)
			{
				var cmd = new RearrangeOrdersCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(
					Empire.Current, vehicle, order, -1);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Invalidate();
			}
		}

		private void btnOrderGoesDown_Click(object sender, System.EventArgs e)
		{
			var order = (IMobileSpaceObjectOrder<AutonomousSpaceVehicle>)lstOrdersDetail.SelectedItem;
			if (order != null && vehicle.Orders.IndexOf(order) < vehicle.Orders.Count - 1)
			{
				var cmd = new RearrangeOrdersCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(
					Empire.Current, vehicle, order, 1);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Invalidate();
			}
		}

		private void btnClearOrders_Click(object sender, System.EventArgs e)
		{
			foreach (var order in vehicle.Orders)
			{
				var cmd = new RemoveOrderCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(
					Empire.Current, vehicle, order);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Invalidate();
			}
		}

		private void btnDeleteOrder_Click(object sender, System.EventArgs e)
		{
			var order = (IMobileSpaceObjectOrder<AutonomousSpaceVehicle>)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var cmd = new RemoveOrderCommand<AutonomousSpaceVehicle, IMobileSpaceObjectOrder<AutonomousSpaceVehicle>>(
					Empire.Current, vehicle, order);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Invalidate();
			}
		}
	}
}
