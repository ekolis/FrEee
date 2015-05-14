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
using FrEee.Utility;
using FrEee.WinForms.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Space;
using System;

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

		private SpaceVehicle vehicle;
		public SpaceVehicle Vehicle { get { return vehicle; } set { vehicle = value; Bind(); } }

		private void btnOrderToTop_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder<SpaceVehicle>)lstOrdersDetail.SelectedItem;
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

		private void btnOrderToBottom_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder<SpaceVehicle>)lstOrdersDetail.SelectedItem;
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

		private void btnOrderGoesUp_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder<SpaceVehicle>)lstOrdersDetail.SelectedItem;
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

		private void btnOrderGoesDown_Click(object sender, System.EventArgs e)
		{
			var order = (IOrder<SpaceVehicle>)lstOrdersDetail.SelectedItem;
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

		private void btnClearOrders_Click(object sender, System.EventArgs e)
		{
			foreach (var order in vehicle.Orders.ToArray())
			{
				var addCmd = Empire.Current.Commands.OfType<AddOrderCommand<SpaceVehicle>>().SingleOrDefault(c => c.Order == order);
				if (addCmd == null)
				{
					// not a newly added order, so create a remove command to take it off the server
					var remCmd = new RemoveOrderCommand<SpaceVehicle>(Vehicle, order);
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
			var order = (IOrder<SpaceVehicle>)lstOrdersDetail.SelectedItem;
			if (order != null)
			{
				var addCmd = Empire.Current.Commands.OfType<AddOrderCommand<SpaceVehicle>>().SingleOrDefault(c => c.Order == order);
				if (addCmd == null)
				{
					// not a newly added order, so create a remove command to take it off the server
					var remCmd = new RemoveOrderCommand<SpaceVehicle>(Vehicle, order);
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

		public delegate void OrdersChangedDelegate();

		public event OrdersChangedDelegate OrdersChanged;

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
				if (Vehicle.Timestamp == Galaxy.Current.Timestamp)
					txtAge.Text = "Current";
				else if (Galaxy.Current.Timestamp - Vehicle.Timestamp <= 1)
					txtAge.Text = "Last turn";
				else
					txtAge.Text = Math.Ceiling(Galaxy.Current.Timestamp - Vehicle.Timestamp) + " turns ago";

				// name and stuff
				txtName.Text = vehicle.Name;
				txtRole.Text = vehicle.Design.Role;
				txtClass.Text = vehicle.Design.Name;
				txtHullSize.Text = vehicle.Design.Hull.Name + " (" + vehicle.Design.Hull.Size.Kilotons() + ")";
				progMovement.Maximum = vehicle.Design.Speed;
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
				if (Vehicle.ConstructionQueue == null || Vehicle.ConstructionQueue.FirstItemEta == null)
				{
					txtConstructionItem.Text = "(None)";
					txtConstructionItem.BackColor = Color.Transparent;
					txtConstructionTime.Text = "";
					txtConstructionTime.BackColor = Color.Transparent;
				}
				else
				{
					txtConstructionItem.Text = Vehicle.ConstructionQueue.FirstItemName;
					txtConstructionItem.BackColor = Vehicle.ConstructionQueue.FirstItemEta <= 1d ? Color.DarkGreen : Color.Transparent;
					if (Vehicle.ConstructionQueue.Eta != Vehicle.ConstructionQueue.FirstItemEta)
						txtConstructionTime.Text = Vehicle.ConstructionQueue.FirstItemEta.ToString("f1") + " turns (" + Vehicle.ConstructionQueue.Eta.ToString("f1") + " turns for all)";
					else
						txtConstructionTime.Text = Vehicle.ConstructionQueue.FirstItemEta.ToString("f1") + " turns";
					txtConstructionTime.BackColor = Vehicle.ConstructionQueue.Eta <= 1d ? Color.DarkGreen : Color.Transparent;
				}

				// component summary
				txtComponentsFunctional.Text = vehicle.Components.Where(c => !c.IsDestroyed).Count() + " / " + vehicle.Components.Count + " functional";
				lstComponentsSummary.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(c => c.Template))
				{
					var text = g.Any(c => c.IsDestroyed) ? g.Where(c => !c.IsDestroyed).Count() + " / " + g.Count() : g.Count().ToString();
					lstComponentsSummary.AddItemWithImage(g.Key.ComponentTemplate.Group, text, g, g.First().Template.Icon);
				}

				// cargo summary
				txtCargoSpaceFree.Text = string.Format("{0} / {1} free", (Vehicle.CargoStorage - (Vehicle.Cargo == null ? 0 : Vehicle.Cargo.Size)).Kilotons(), Vehicle.CargoStorage.Kilotons());
				lstCargoSummary.Initialize(32, 32);
				foreach (var ug in Vehicle.Cargo.Units.GroupBy(u => u.Design))
					lstCargoSummary.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
				foreach (var pop in Vehicle.Cargo.Population)
					lstCargoSummary.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);

				// orders detail
				lstOrdersDetail.Items.Clear();
				foreach (var o in vehicle.Orders)
					lstOrdersDetail.Items.Add(o);

				// component detail
				txtComponentsFunctionalDetail.Text = vehicle.Components.Where(c => !c.IsDestroyed).Count() + " / " + vehicle.Components.Count + " functional";
				lstComponentsDetail.Initialize(32, 32);
				foreach (var g in vehicle.Components.GroupBy(c => new ComponentGroup(c.Template, c.Hitpoints)))
				{
					if (g.Count() > 1)
						lstComponentsDetail.AddItemWithImage(g.Key.Template.ComponentTemplate.Group, g.Count() + "x " + g.Key.Template.Name + " (" + g.Key.Hitpoints + " / " + g.Key.Template.Durability + " HP)", g.Key, g.First().Icon);
					else
						lstComponentsDetail.AddItemWithImage(g.Key.Template.ComponentTemplate.Group, g.Key.Template.Name + " (" + g.Key.Hitpoints + " / " + g.Key.Template.Durability + " HP)", g.Key, g.First().Icon);
				}

				// cargo detail
				txtCargoSpaceFreeDetail.Text = string.Format("{0} / {1} free", (Vehicle.CargoStorage - Vehicle.Cargo.Size).Kilotons(), Vehicle.CargoStorage.Kilotons());
				lstCargoDetail.Initialize(32, 32);
				foreach (var ug in Vehicle.Cargo.Units.GroupBy(u => u.Design))
					lstCargoDetail.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
				foreach (var pop in Vehicle.Cargo.Population)
					lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);

				// abilities
				abilityTreeView.Abilities = Vehicle.AbilityTree();
				abilityTreeView.IntrinsicAbilities = Vehicle.IntrinsicAbilities.Concat(Vehicle.Design.Hull.Abilities).Concat(Vehicle.Components.Where(c => !c.IsDestroyed).SelectMany(c => c.Abilities));
			}
			ResumeLayout();
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

		private class ComponentGroup
		{
			public ComponentGroup(MountedComponentTemplate mct, int hitpoints)
			{
				Template = mct;
				Hitpoints = hitpoints;
			}

			public MountedComponentTemplate Template
			{
				get;
				private set;
			}

			public int Hitpoints
			{
				get;
				private set;
			}

			public static bool operator ==(ComponentGroup g1, ComponentGroup g2)
			{
				return g1.Template == g2.Template && g1.Hitpoints == g2.Hitpoints;
			}

			public static bool operator !=(ComponentGroup g1, ComponentGroup g2)
			{
				return !(g1 == g2);
			}

			public override bool Equals(object obj)
			{
				if (obj is ComponentGroup)
				{
					var g = (ComponentGroup)obj;
					return this == g;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return HashCodeMasher.Mash(Template, Hitpoints);
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
		}

		private void picPortrait_MouseClick(object sender, MouseEventArgs e)
		{
			picPortrait.ShowFullSize(Vehicle.Name);
		}
	}
}
