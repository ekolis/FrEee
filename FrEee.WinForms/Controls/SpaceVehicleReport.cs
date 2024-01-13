using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Commands;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// A report on a space vehicle.
	/// </summary>
	public partial class SpaceVehicleReport : UserControl, IBindable<SpaceVehicle>
	{
		public SpaceVehicleReport()
		{
			InitializeComponent();
		}

		public SpaceVehicleReport(SpaceVehicle vehicle)
		{
			InitializeComponent();
			Vehicle = vehicle;
		}

		public SpaceVehicle Vehicle { get { return vehicle; } set { vehicle = value; Bind(); } }
		private SpaceVehicle vehicle;

		public void Bind(SpaceVehicle data)
		{
			Vehicle = data;
			Bind();
		}

		public void Bind()
		{
			SuspendLayout();
			if (vehicle == null)
				Visible = false;
			else
			{
				Visible = true;

				// pictures
				picOwnerFlag.Image = vehicle.Owner.Icon;
				picPortrait.Image = vehicle.Portrait;

				// timestamp
				txtAge.Text = Vehicle.Timestamp.GetMemoryAgeDescription();
				txtAge.BackColor = txtAge.Text == "Current" ? Color.Transparent : Color.FromArgb(64, 64, 0);

				// name and stuff
				txtName.Text = vehicle.Name;
				txtRole.Text = vehicle.Design.Role;
				txtClass.Text = vehicle.Design.Name;
				txtHullSize.Text = vehicle.Design.Hull.Name + " (" + vehicle.Design.Hull.Size.Kilotons() + ")";
				progMovement.Maximum = vehicle.Design.StrategicSpeed;
				progMovement.Value = vehicle.MovementRemaining;

				// supplies and stuff
				progSupplies.Maximum = vehicle.SupplyStorage;
				progSupplies.Value = vehicle.SupplyRemaining;
				// will we even use ammo/fuel?
				progAmmunition.Visible = false;
				progFuel.Visible = false;

				// damage
				progShields.Maximum = vehicle.Design.ShieldHitpoints;
				progShields.Value = vehicle.ShieldHitpoints;
				progArmor.Maximum = vehicle.Design.ArmorHitpoints;
				progArmor.Value = vehicle.ArmorHitpoints;
				progHull.Maximum = vehicle.Design.HullHitpoints;
				progHull.Value = vehicle.HullHitpoints;

				// orders and stuff
				txtOrder.Text = vehicle.Orders.Any() ? vehicle.Orders.First().ToString() : "None";
				txtExperience.Text = "None"; // TODO - crew XP
				txtFleet.Text = vehicle.Container == null ? "None" : vehicle.Container.Name;

				// income
				// TODO - research and intel income
				var remoteMining = vehicle.Owner.RemoteMiners.Where(kvp => kvp.Key.Item1 == vehicle).Sum(kvp => kvp.Value);
				var maintenance = vehicle.MaintenanceCost;
				var rawResources = vehicle.RawResourceIncome();
				var netIncome = remoteMining + rawResources - maintenance;
				resIncomeMin.Amount = netIncome[Resource.Minerals];
				resIncomeOrg.Amount = netIncome[Resource.Organics];
				resIncomeRad.Amount = netIncome[Resource.Radioactives];

				// construction data
				var c = Vehicle as IConstructor;
				if (c is null || c.ConstructionQueue == null || c.ConstructionQueue.FirstItemEta == null)
				{
					txtConstructionItem.Text = "(None)";
					txtConstructionItem.BackColor = Color.Transparent;
					txtConstructionTime.Text = "";
					txtConstructionTime.BackColor = Color.Transparent;
				}
				else
				{
					txtConstructionItem.Text = c.ConstructionQueue.FirstItemName;
					txtConstructionItem.BackColor = c.ConstructionQueue.FirstItemEta <= 1d ? Color.DarkGreen : Color.Transparent;
					if (c.ConstructionQueue.Eta != c.ConstructionQueue.FirstItemEta)
						txtConstructionTime.Text = c.ConstructionQueue.FirstItemEta.CeilingString(1) + " turns (" + c.ConstructionQueue.Eta.CeilingString(1) + " turns for all)";
					else
						txtConstructionTime.Text = c.ConstructionQueue.FirstItemEta.CeilingString(1) + " turns";
					txtConstructionTime.BackColor = c.ConstructionQueue.Eta <= 1d ? Color.DarkGreen : Color.Transparent;
				}

				// component summary
				txtComponentsFunctional.Text = vehicle.Components.Where(q => !q.IsDestroyed).Count() + " / " + vehicle.Components.Count + " functional";
				lstComponentsSummary.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(q => q.Template))
				{
					var text = g.Any(q => q.IsDestroyed) ? g.Where(q => !q.IsDestroyed).Count() + " / " + g.Count() : g.Count().ToString();
					lstComponentsSummary.AddItemWithImage(g.Key.ComponentTemplate.Group, text, g, g.First().Template.Icon);
				}

				// cargo summary
				lstCargoSummary.Initialize(32, 32);
				if (Vehicle is ICargoContainer cc)
				{
					txtCargoSpaceFree.Text = string.Format("{0} / {1} free", (Vehicle.CargoStorage - (cc.Cargo == null ? 0 : cc.Cargo.Size)).Kilotons(), Vehicle.CargoStorage.Kilotons());
					foreach (var ug in cc.Cargo.Units.GroupBy(u => u.Design))
						lstCargoSummary.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
					foreach (var pop in cc.Cargo.Population)
						lstCargoSummary.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
				}

				// orders detail
				chkOnHold.Checked = Vehicle.AreOrdersOnHold;
				chkRepeat.Checked = Vehicle.AreRepeatOrdersEnabled;
				if (!Vehicle.IsOurs)
				{
					chkOnHold.Visible = false;
					chkRepeat.Visible = false;
				}
				lstOrdersDetail.Items.Clear();
				foreach (var o in vehicle.Orders)
					lstOrdersDetail.Items.Add(o);

				// component detail
				txtComponentsFunctionalDetail.Text = vehicle.Components.Where(q => !q.IsDestroyed).Count() + " / " + vehicle.Components.Count + " functional";
				lstComponentsDetail.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(q => new ComponentGroup(q.Template, q.Hitpoints)))
				{
					var color = g.Key.Hitpoints == g.Key.Template.Durability ? Color.White : g.Key.Hitpoints == 0 ? Color.Red : Color.Yellow;
					if (g.Count() > 1)
						lstComponentsDetail.AddItemWithImage(g.Key.Template.ComponentTemplate.Group, g.Count() + "x " + g.Key.Template.Name + " (" + g.Key.Hitpoints + " / " + g.Key.Template.Durability + " HP)", g.Key, g.First().Icon, color);
					else
						lstComponentsDetail.AddItemWithImage(g.Key.Template.ComponentTemplate.Group, g.Key.Template.Name + " (" + g.Key.Hitpoints + " / " + g.Key.Template.Durability + " HP)", g.Key, g.First().Icon, color);
				}

				// cargo detail
				lstCargoDetail.Initialize(32, 32);
				if (Vehicle is ICargoContainer cc2)
				{
					txtCargoSpaceFreeDetail.Text = string.Format("{0} / {1} free", (Vehicle.CargoStorage - cc2.Cargo.Size).Kilotons(), Vehicle.CargoStorage.Kilotons());
					foreach (var ug in cc2.Cargo.Units.GroupBy(u => u.Design))
						lstCargoDetail.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
					foreach (var pop in cc2.Cargo.Population)
						lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
				}

				// abilities
				abilityTreeView.Abilities = Vehicle.AbilityTree();
				abilityTreeView.IntrinsicAbilities = Vehicle.IntrinsicAbilities.Concat(Vehicle.Design.Hull.Abilities).Concat(Vehicle.Components.Where(q => !q.IsDestroyed).SelectMany(q => q.Abilities));
			}
			ResumeLayout();
		}

		private void btnClearOrders_Click(object sender, System.EventArgs e)
		{
			foreach (var order in vehicle.Orders.ToArray())
			{
				var addCmd = Empire.Current.Commands.OfType<AddOrderCommand>().SingleOrDefault(c => c.Order == order);
				if (addCmd == null)
				{
					// not a newly added order, so create a remove command to take it off the server
					var remCmd = new RemoveOrderCommand(Vehicle, order);
					Empire.Current.Commands.Add(remCmd);
					remCmd.Execute(); // show change locally
				}
				else
				{
					// a newly added order, so just get rid of the add command
					Empire.Current.Commands.Remove(addCmd);
					addCmd.Execute(); // show change locally
				}

				Bind();

				if (OrdersChanged != null)
					OrdersChanged();
			}
		}

		private void btnDeleteOrder_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var addCmd = Empire.Current.Commands.OfType<AddOrderCommand>().SingleOrDefault(c => c.Order == order);
				if (addCmd == null)
				{
					// not a newly added order, so create a remove command to take it off the server
					var remCmd = new RemoveOrderCommand(Vehicle, order);
					Empire.Current.Commands.Add(remCmd);
					remCmd.Execute(); // show change locally
				}
				else
				{
					// a newly added order, so just get rid of the add command
					Empire.Current.Commands.Remove(addCmd);
					addCmd.Execute(); // show change locally
				}

				Bind();

				if (OrdersChanged != null)
					OrdersChanged();
			}
		}

		private void btnOrderGoesDown_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder)lstOrdersDetail.SelectedItem;
			if (order != null && vehicle.Orders.IndexOf(order) < vehicle.Orders.Count() - 1)
			{
				var cmd = new RearrangeOrdersCommand<SpaceVehicle>(
					vehicle, order, 1);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Bind();

				if (OrdersChanged != null)
					OrdersChanged();
			}
		}

		private void btnOrderGoesUp_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder)lstOrdersDetail.SelectedItem;
			if (order != null && vehicle.Orders.IndexOf(order) > 0)
			{
				var cmd = new RearrangeOrdersCommand<SpaceVehicle>(
					vehicle, order, -1);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Bind();

				if (OrdersChanged != null)
					OrdersChanged();
			}
			if (OrdersChanged != null)
				OrdersChanged();
		}

		private void btnOrderToBottom_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var cmd = new RearrangeOrdersCommand<SpaceVehicle>(
					vehicle, order, Vehicle.Orders.Count() - vehicle.Orders.IndexOf(order) - 1);
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Bind();

				if (OrdersChanged != null)
					OrdersChanged();
			}
		}

		private void btnOrderToTop_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var cmd = new RearrangeOrdersCommand<SpaceVehicle>(
					vehicle, order, -vehicle.Orders.IndexOf(order));
				Empire.Current.Commands.Add(cmd);
				cmd.Execute(); // show change locally
				Bind();

				if (OrdersChanged != null)
					OrdersChanged();
			}
		}

		private void lstComponentsDetail_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var item = lstComponentsDetail.GetItemAt(e.X, e.Y);
				if (item != null)
				{
					var g = (ComponentGroup)item.Tag;
					Cursor = Cursors.WaitCursor;
					var form = new ComponentReport(g.SampleComponent).CreatePopupForm(g.Template.Name);
					form.ShowDialog();
					Cursor = Cursors.Default;
				}
			}
		}

		private void lstComponentsSummary_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var item = lstComponentsSummary.GetItemAt(e.X, e.Y);
				if (item != null)
				{
					var g = (IGrouping<MountedComponentTemplate, Component>)item.Tag;
					var mct = g.Key;
					Cursor = Cursors.WaitCursor;
					var form = new ComponentReport(mct).CreatePopupForm(mct.Name);
					form.ShowDialog();
					Cursor = Cursors.Default;
				}
			}
		}

		private void picPortrait_MouseClick(object sender, MouseEventArgs e)
		{
			picPortrait.ShowFullSize(Vehicle.Name);
		}

		public event OrdersChangedDelegate OrdersChanged;

		public delegate void OrdersChangedDelegate();

		private class ComponentGroup
		{
			public ComponentGroup(MountedComponentTemplate mct, int hitpoints)
			{
				Template = mct;
				Hitpoints = hitpoints;
			}

			public int Hitpoints
			{
				get;
				private set;
			}

			public Component SampleComponent
			{
				get
				{
					var c = new Component(null, Template);
					c.Hitpoints = Hitpoints;
					return c;
				}
			}

			public MountedComponentTemplate Template
			{
				get;
				private set;
			}

			public static bool operator !=(ComponentGroup g1, ComponentGroup g2)
			{
				return !(g1 == g2);
			}

			public static bool operator ==(ComponentGroup g1, ComponentGroup g2)
			{
				if (g1.IsNull() && g2.IsNull())
					return true;
				if (g1.IsNull() || g2.IsNull())
					return false;
				return g1.Template == g2.Template && g1.Hitpoints == g2.Hitpoints;
			}

			public override bool Equals(object obj)
			{
				var x = obj as ComponentGroup;
				if (x == null)
					return false;
				return this == x;
			}

			public override int GetHashCode()
			{
				return HashCodeMasher.Mash(Template, Hitpoints);
			}
		}

		private void chkRepeat_CheckedChanged(object sender, EventArgs e)
		{
			var cmd = Empire.Current.Commands.OfType<ToggleRepeatOrdersCommand>().SingleOrDefault(x => x.Executor == Vehicle);
			if (cmd == null)
			{
				cmd = new ToggleRepeatOrdersCommand(Vehicle, chkRepeat.Checked);
				Empire.Current.Commands.Add(cmd);
			}
			else
				cmd.AreRepeatOrdersEnabled = chkRepeat.Checked;
			cmd.Execute();
		}

		private void chkOnHold_CheckedChanged(object sender, EventArgs e)
		{
			var cmd = Empire.Current.Commands.OfType<ToggleOrdersOnHoldCommand>().SingleOrDefault(x => x.Executor == Vehicle);
			if (cmd == null)
			{
				cmd = new ToggleOrdersOnHoldCommand(Vehicle, chkOnHold.Checked);
				Empire.Current.Commands.Add(cmd);
			}
			else
				cmd.AreOrdersOnHold = chkOnHold.Checked;
			cmd.Execute();
		}
	}
}