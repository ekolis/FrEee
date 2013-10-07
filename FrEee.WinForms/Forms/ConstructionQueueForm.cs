using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Controls;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class ConstructionQueueForm : Form
	{
		public ConstructionQueueForm(ConstructionQueue queue)
		{
			InitializeComponent();

			ConstructionQueue = queue;

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
		}

		private void ConstructionQueueForm_Load(object sender, EventArgs e)
		{
			Text = ConstructionQueue.SpaceObject.Name + " Construction Queue";
			resMineralsRate.Amount = ConstructionQueue.Rate[Resource.Minerals];
			resOrganicsRate.Amount = ConstructionQueue.Rate[Resource.Organics];
			resRadioactivesRate.Amount = ConstructionQueue.Rate[Resource.Radioactives];

			// bind constructable lists
			chkOnlyLatest_CheckedChanged(this, new EventArgs());

			// show existing queued items
			BindQueueListView();

			// setup command list
			newCommands = new List<ICommand>();

			oldQueue = new List<IConstructionOrder>();
			foreach (var order in ConstructionQueue.Orders)
				oldQueue.Add(order);
		}

		public ConstructionQueue ConstructionQueue { get; private set; }

		private void lstFacilities_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var facil = (FacilityTemplate)e.Item.Tag;
			txtName.Text = facil.Name;
			resCostMin.Amount = facil.Cost[Resource.Minerals];
			resCostOrg.Amount = facil.Cost[Resource.Organics];
			resCostRad.Amount = facil.Cost[Resource.Radioactives];
		}

		private void lstFacilities_MouseLeave(object sender, EventArgs e)
		{
			ClearDetails();
		}

		private void BindQueueListView()
		{
			lstQueue.Items.Clear();
			var il = new ImageList();
			il.ImageSize = new Size(32, 32);
			lstQueue.SmallImageList = il;
			lstQueue.LargeImageList = il;
			int i = 0;
			var prevCost = new ResourceQuantity();
			foreach (var order in ConstructionQueue.Orders)
			{
				var item = new ListViewItem(order.Template.Name);
				item.Tag = order;
				item.UseItemStyleForSubItems = false;
				var duration = Math.Ceiling(order.Template.Cost.Keys.Max(res => (double)order.Cost[res] / (double)ConstructionQueue.Rate[res]));
				var remainingCost = order.Cost - (order.Item == null ? new ResourceQuantity() : order.Item.ConstructionProgress);
				var minprogress = order.Item == null ? 0d : (double)order.Item.ConstructionProgress[Resource.Minerals] / (double)order.Item.Cost[Resource.Minerals];
				var orgprogress = order.Item == null ? 0d : (double)order.Item.ConstructionProgress[Resource.Organics] / (double)order.Item.Cost[Resource.Organics];
				var radprogress = order.Item == null ? 0d : (double)order.Item.ConstructionProgress[Resource.Radioactives] / (double)order.Item.Cost[Resource.Radioactives];
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(minprogress) ? "-" : (int)Math.Round(minprogress * 100) + "%", Resource.Minerals.Color, lstQueue.BackColor, lstQueue.Font));
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(orgprogress) ? "-" : (int)Math.Round(orgprogress * 100) + "%", Resource.Organics.Color, lstQueue.BackColor, lstQueue.Font));
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(radprogress) ? "-" : (int)Math.Round(radprogress * 100) + "%", Resource.Radioactives.Color, lstQueue.BackColor, lstQueue.Font));
				var eta = remainingCost.Keys.Max(res => (double)(remainingCost[res] + prevCost[res]) / (double)ConstructionQueue.Rate[res]);
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.ToString("f1"), lstQueue.ForeColor, eta <= 1.0 ? Color.DarkGreen : lstQueue.BackColor, lstQueue.Font));
				item.ImageIndex = i;
				il.Images.Add(order.Template.Icon);
				lstQueue.Items.Add(item);
				i++;
				prevCost += remainingCost;
			}
			txtCargoStorageFree.Text = string.Format("Cargo Storage Free: {0} ({1} total in sector)", ConstructionQueue.CargoStorageFree.Kilotons(), ConstructionQueue.CargoStorageFreeInSector.Kilotons());
			txtFacilitySlotsFree.Text = string.Format("Facility Slots Free: {0}", ConstructionQueue.FacilitySlotsFree);
		}

		/// <summary>
		/// Commands that need to be sent to the game host if the user doesn't cancel
		/// </summary>
		private IList<ICommand> newCommands;

		/// <summary>
		/// Old queue, to be restored in case the user cancels
		/// </summary>
		private IList<IConstructionOrder> oldQueue;

		private void ConstructionQueueForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (newCommands.Any())
			{
				switch (MessageBox.Show("Save changes?", "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
				{
					case DialogResult.Yes:
						SaveCommands();
						break;
					case DialogResult.No:
						CancelChanges();
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}

			// TODO - remember "only latest" between invocations of this form
		}

		private void SaveCommands()
		{
			foreach (var cmd in newCommands)
				Galaxy.Current.CurrentEmpire.Commands.Add(cmd);
		}

		private void CancelChanges()
		{
			ConstructionQueue.Orders.Clear();
			foreach (var order in oldQueue)
				ConstructionQueue.Orders.Add(order);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveCommands();
			newCommands.Clear();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			CancelChanges();
			newCommands.Clear();
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void chkOnlyLatest_CheckedChanged(object sender, EventArgs e)
		{
			if (chkOnlyLatest.Checked)
			{
				BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatest(f => f.Family));
				BindShipListView(Empire.Current.KnownDesigns.Where(d => d.Owner == Empire.Current && d.HasBeenUnlockedBy(Empire.Current) && !d.IsObsolete));
				BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatest(f => f.Family));
			}
			else
			{
				BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()));
				BindShipListView(Empire.Current.KnownDesigns.Where(d => d.Owner == Empire.Current && d.HasBeenUnlockedBy(Empire.Current)));
				BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()));
			}
		}

		private void BindFacilityListView(IEnumerable<FacilityTemplate> templates)
		{
			int i = 0;
			var ilFacil = new ImageList();
			ilFacil.ImageSize = new Size(32, 32);
			lstFacilities.LargeImageList = ilFacil;
			lstFacilities.SmallImageList = ilFacil;
			lstFacilities.Items.Clear();
			foreach (var facil in templates)
			{
				if (ConstructionQueue.CanConstruct(facil))
				{
					var group = lstFacilities.Groups.Cast<ListViewGroup>().SingleOrDefault(g => g.Header == facil.Group);
					if (group == null)
					{
						group = new ListViewGroup(facil.Group);
						lstFacilities.Groups.Add(group);
					}
					var item = new ListViewItem(facil.Name, i, group);
					item.ImageIndex = i;
					item.Tag = facil;
					var eta = facil.Cost.Keys.Max(res => (double)(facil.Cost[res]) / (double)ConstructionQueue.Rate[res]);
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.ToString("f1")));
					ilFacil.Images.Add(facil.Icon);
					lstFacilities.Items.Add(item);
					i++;
				}
			}
		}

		private void BindUpgradeListView(IEnumerable<FacilityTemplate> templates)
		{
			int i = 0;
			var ilFacil = new ImageList();
			ilFacil.ImageSize = new Size(32, 32);
			lstUpgrades.LargeImageList = ilFacil;
			lstUpgrades.SmallImageList = ilFacil;
			lstUpgrades.Items.Clear();
			if (ConstructionQueue.Colony != null)
			{
				foreach (var g in ConstructionQueue.Colony.Facilities.GroupBy(f => f.Template))
				{
					var oldf = g.Key;

					// facilites which are not yet upgraded
					var count = g.Count() - ConstructionQueue.Orders.OfType<UpgradeFacilityOrder>().Where(o => o.OldTemplate == oldf).Count();

					if (count > 0)
					{
						foreach (var newf in templates)
						{
							if (ConstructionQueue.CanConstruct(newf))
							{
								var group = lstFacilities.Groups.Cast<ListViewGroup>().SingleOrDefault(g2 => g2.Header == newf.Group);
								if (newf.Family == oldf.Family && newf != oldf)
								{
									if (group == null)
									{
										group = new ListViewGroup(newf.Group);
										lstUpgrades.Groups.Add(group);
									}
									string name;
									if (count == 1)
										name = oldf.Name;
									else
										name = count + "x " + oldf.Name;
									var item = new ListViewItem(name + " to " + newf.Name, i, group);
									item.ImageIndex = i;
									item.Tag = new FacilityUpgrade(oldf, newf);
									var cost = newf.Cost * Mod.Current.Settings.UpgradeFacilityPercentCost / 100;
									var eta = cost.Keys.Max(res => (double)(cost[res]) / (double)ConstructionQueue.Rate[res]);
									item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.ToString("f1")));
									ilFacil.Images.Add(newf.Icon);
									lstUpgrades.Items.Add(item);
									i++;
								}
							}
						}
					}
				}
			}
		}

		private class FacilityUpgrade
		{
			public FacilityUpgrade(FacilityTemplate old, FacilityTemplate nu)
			{
				Old = old;
				New = nu;
			}
			public FacilityTemplate Old { get; set; }
			public FacilityTemplate New { get; set; }
		}

		private void BindShipListView(IEnumerable<IDesign> designs)
		{
			lstShips.Initialize(32, 32);
			foreach (var design in designs.Where(d => ConstructionQueue.CanConstruct(d)))
			{
				var eta = design.Cost.Keys.Max(res => (double)(design.Cost[res]) / (double)ConstructionQueue.Rate[res]);
				lstShips.AddItemWithImage(design.Role, design.Name, design, design.Icon, eta.ToString("f1"));
			}
		}

		private void lstShips_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var d = (IDesign)e.Item.Tag;
			txtName.Text = d.Name;
			resCostMin.Amount = d.Cost[Resource.Minerals];
			resCostOrg.Amount = d.Cost[Resource.Organics];
			resCostRad.Amount = d.Cost[Resource.Radioactives];
		}

		private void lstShips_MouseLeave(object sender, EventArgs e)
		{
			ClearDetails();
		}

		private void ClearDetails()
		{
			txtName.Text = "(No Item)";
			resCostMin.Amount = 0;
			resCostOrg.Amount = 0;
			resCostRad.Amount = 0;
		}

		private void lstShips_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstShips.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				var design = (IDesign)item.Tag;
				if (e.Button == MouseButtons.Left)
				{
					// add to queue
					var order = design.CreateConstructionOrder(ConstructionQueue);
					ConstructionQueue.Orders.Add(order);
					var cmd = new AddOrderCommand<ConstructionQueue>
					(
						Galaxy.Current.CurrentEmpire,
						ConstructionQueue,
						order
					);
					newCommands.Add(cmd);
					BindQueueListView();
				}
				else if (e.Button == MouseButtons.Right)
				{
					// display detailed report
					var report = new DesignReport(design);
					this.ShowChildForm(report.CreatePopupForm(design.Name));
				}
			}
		}

		private void lstQueue_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// TODO - display detailed report on construction item
			}
		}

		private void lstFacilities_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstFacilities.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					var template = (FacilityTemplate)item.Tag;
					var order = new ConstructionOrder<Facility, FacilityTemplate> { Template = template };
					ConstructionQueue.Orders.Add(order);
					var cmd = new AddOrderCommand<ConstructionQueue>
					(
						Galaxy.Current.CurrentEmpire,
						ConstructionQueue,
						order
					);
					newCommands.Add(cmd);
					BindQueueListView();
				}
				else if (e.Button == MouseButtons.Right)
				{
					// TODO - display detailed report on facility template
				}
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			if (SelectedOrderIndex > 0)
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(Empire.Current, ConstructionQueue, SelectedOrder, -1);
				Empire.Current.Commands.Add(cmd);
				newCommands.Add(cmd);
				cmd.Execute();
				BindQueueListView();
			}
		}

		private void btnTop_Click(object sender, EventArgs e)
		{
			if (SelectedOrderIndex > 0)
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(Empire.Current, ConstructionQueue, SelectedOrder, -SelectedOrderIndex);
				Empire.Current.Commands.Add(cmd);
				newCommands.Add(cmd);
				cmd.Execute();
				BindQueueListView();
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (SelectedOrderIndex >= 0 && SelectedOrderIndex < ConstructionQueue.Orders.Count - 1)
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(Empire.Current, ConstructionQueue, SelectedOrder, 1);
				Empire.Current.Commands.Add(cmd);
				newCommands.Add(cmd);
				cmd.Execute();
				BindQueueListView();
			}
		}

		private void btnBottom_Click(object sender, EventArgs e)
		{
			if (SelectedOrderIndex >= 0 && SelectedOrderIndex < ConstructionQueue.Orders.Count - 1)
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(Empire.Current, ConstructionQueue, SelectedOrder, ConstructionQueue.Orders.Count - SelectedOrderIndex - 1);
				Empire.Current.Commands.Add(cmd);
				newCommands.Add(cmd);
				cmd.Execute();
				BindQueueListView();
			}
		}

		public IConstructionOrder SelectedOrder
		{
			get
			{
				if (lstQueue.SelectedItems.Count == 1)
					return (IConstructionOrder)lstQueue.SelectedItems[0].Tag;
				return null;
			}
		}

		public int SelectedOrderIndex
		{
			get
			{
				if (lstQueue.SelectedIndices.Count == 1)
					return lstQueue.SelectedIndices[0];
				return -1;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (SelectedOrder != null)
				RemoveOrder(SelectedOrder);
		}

		private void lstQueue_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstQueue.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					// delete order
					var order = (IConstructionOrder)item.Tag;
					RemoveOrder(order);
				}
			}
		}

		private void RemoveOrder(IConstructionOrder order, bool rebindGui = true)
		{
			var cmds = Empire.Current.Commands.OfType<AddOrderCommand<ConstructionQueue>>().Where(o => o.Order == order).ToArray();
			if (cmds.Any())
			{
				// remove add-order command since the order is new this turn
				foreach (var cmd in cmds)
					Empire.Current.Commands.Remove(cmd);
			}
			else
			{
				// add remove-order command
				var cmd = new RemoveOrderCommand<ConstructionQueue>(Empire.Current, ConstructionQueue, order);
				Empire.Current.Commands.Add(cmd);
				newCommands.Add(cmd);
			}
			ConstructionQueue.Orders.Remove(order);
			if (rebindGui)
				BindQueueListView();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			foreach (var order in ConstructionQueue.Orders.ToArray())
				RemoveOrder(order);
			BindQueueListView();
		}

		private void lstUpgrades_MouseLeave(object sender, EventArgs e)
		{
			ClearDetails();
		}

		private void lstUpgrades_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstFacilities.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					var upgrade = (FacilityUpgrade)item.Tag;
					var order = new UpgradeFacilityOrder(upgrade.Old, upgrade.New);
					ConstructionQueue.Orders.Add(order);
					var cmd = new AddOrderCommand<ConstructionQueue>
					(
						Galaxy.Current.CurrentEmpire,
						ConstructionQueue,
						order
					);
					newCommands.Add(cmd);
					BindQueueListView();
					IEnumerable<FacilityTemplate> templates;
					if (chkOnlyLatest.Checked)
						templates = Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatest(f => f.Family);
					else
						templates = Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any());
					BindUpgradeListView(templates);
				}
				else if (e.Button == MouseButtons.Right)
				{
					// TODO - display detailed report on new facility template
				}
			}
		}

		private void lstUpgrades_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{

		}
	}
}
