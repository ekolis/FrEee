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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class ConstructionQueueForm : GameForm
	{
		public ConstructionQueueForm(ConstructionQueue queue)
		{
			InitializeComponent();

			ConstructionQueue = queue;

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }


			// setup command list
			newCommands = new List<ICommand>();
			removedCommands = new List<ICommand>();

			wasOnHold = chkOnHold.Checked = queue.AreOrdersOnHold;
			wasRepeat = chkRepeat.Checked = queue.AreRepeatOrdersEnabled;
			lblSpaceportWarning.Visible = queue.IsColonyQueue && !queue.Colony.Container.StarSystem.HasAbility("Spaceport") && queue.Colony.MerchantsRatio < 1;
		}

		public ConstructionQueue ConstructionQueue { get; private set; }

		private bool wasRepeat, wasOnHold;

		/// <summary>
		/// The queued orders that are selected by the player.
		/// </summary>
		public IEnumerable<IConstructionOrder> SelectedOrders
		{
			get
			{
				foreach (ListViewItem item in lstQueue.SelectedItems)
				{
					if (item.Tag is IConstructionOrder)
						yield return (IConstructionOrder)item.Tag;
					else if (item.Tag is IEnumerable<IConstructionOrder>)
					{
						foreach (var o in (IEnumerable<IConstructionOrder>)item.Tag)
							yield return o;
					}
				}
			}
		}

		/// <summary>
		/// Commands that need to be sent to the game host if the user doesn't cancel
		/// </summary>
		private IList<ICommand> newCommands;

		/// <summary>
		/// Old queue, to be restored in case the user cancels
		/// </summary>
		private IList<IConstructionOrder> oldQueue;

		private IList<ICommand> removedCommands;

		/// <summary>
		/// Finds the number of orders directly above this order that have the same template as the item directly above.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public int FindSameItemsCountAbove(IConstructionOrder o)
		{
			var pos = ConstructionQueue.Orders.IndexOf(o);
			int num = 0;
			IConstructionTemplate t = null;
			for (var i = pos - 1; i >= 0; i--)
			{
				if (t == null)
					t = ConstructionQueue.Orders[i].Template;
				if (ConstructionQueue.Orders[i].Template == t)
					num++;
				else
					break;
			}
			return num;
		}

		/// <summary>
		/// Finds the number of orders directly below this order that have the same template as the item directly above.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public int FindSameItemsCountBelow(IConstructionOrder o)
		{
			var pos = ConstructionQueue.Orders.IndexOf(o);
			int num = 0;
			IConstructionTemplate t = null;
			for (var i = pos + 1; i < ConstructionQueue.Orders.Count; i++)
			{
				if (t == null)
					t = ConstructionQueue.Orders[i].Template;
				if (ConstructionQueue.Orders[i].Template == t)
					num++;
				else
					break;
			}
			return num;
		}

		private void BindFacilityListView(IEnumerable<FacilityTemplate> templates)
		{
			int i = 0;
			var ilFacil = new ImageList();
			ilFacil.ImageSize = new Size(32, 32);
			lstFacilities.LargeImageList = ilFacil;
			lstFacilities.SmallImageList = ilFacil;
			lstFacilities.Items.Clear();
			foreach (var facil in templates.OrderBy(f => f.Group).ThenBy(f => f.Name)) // TODO - roman numeral sorting
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
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.CeilingString(1)));
					ilFacil.Images.Add(facil.Icon);
					lstFacilities.Items.Add(item);
					if (facil.IsObsolete)
						item.ForeColor = Color.Gray;
					else if (facil.IsObsolescent)
						item.ForeColor = Color.Yellow;
					i++;
				}
			}
		}

		private void BindQueueListView(IEnumerable<IConstructionOrder> selected = null)
		{
			lstQueue.Items.Clear();
			var il = new ImageList();
			il.ImageSize = new Size(32, 32);
			lstQueue.SmallImageList = il;
			lstQueue.LargeImageList = il;
			int i = 0;
			var prevCost = new ResourceQuantity();
			IConstructionTemplate lastTemplate = null;
			var firstEta = 0d;
			int count = 0;
			double totalMin = 0d, totalOrg = 0d, totalRad = 0d;
			var orders = new List<IConstructionOrder>();
			var rate = ConstructionQueue.Rate;
			ListViewItem item;
			foreach (var order in ConstructionQueue.Orders)
			{
				var cost = order.Cost;
				var duration = Math.Ceiling(cost.Keys.MaxOrDefault(res => (double)cost[res] / (double)rate[res]));
				var remainingCost = order.Cost - (order.Item == null ? new ResourceQuantity() : order.Item.ConstructionProgress);
				var minprogress = order.Item == null ? 0d : (double)order.Item.ConstructionProgress[Resource.Minerals] / (double)order.Cost[Resource.Minerals];
				var orgprogress = order.Item == null ? 0d : (double)order.Item.ConstructionProgress[Resource.Organics] / (double)order.Cost[Resource.Organics];
				var radprogress = order.Item == null ? 0d : (double)order.Item.ConstructionProgress[Resource.Radioactives] / (double)order.Cost[Resource.Radioactives];
				var eta = remainingCost.Keys.MaxOrDefault(res => (double)(remainingCost[res] + prevCost[res]) / (double)rate[res]);
				if (!chkExpanded.Checked && order.Template == lastTemplate)
				{
					// building same as previous item, and using condensed view
					count++;
					orders.Add(order);
					totalMin += minprogress;
					totalOrg += orgprogress;
					totalRad += radprogress;
					if (lstQueue.Items.Count > 0)
						lstQueue.Items.RemoveAt(lstQueue.Items.Count - 1);
					item = new ListViewItem(count + "x " + order.Name);
					item.Tag = orders;
					item.UseItemStyleForSubItems = false;
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(totalMin) ? "-" :
						totalMin >= 1 ?
							totalMin.ToString("f1") + "x" :
							(int)Math.Round(totalMin * 100) + "%",
						Resource.Minerals.Color, lstQueue.BackColor, lstQueue.Font));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(totalOrg) ? "-" :
						totalOrg >= 1 ?
							totalOrg.ToString("f1") + "x" :
							(int)Math.Round(totalOrg * 100) + "%",
						Resource.Organics.Color, lstQueue.BackColor, lstQueue.Font));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(totalRad) ? "-" :
						totalRad >= 1 ?
							totalRad.ToString("f1") + "x" :
							(int)Math.Round(totalRad * 100) + "%",
						Resource.Radioactives.Color, lstQueue.BackColor, lstQueue.Font));
					var firstFinishesThisTurn = firstEta <= 1.0;
					var idle = eta < 1.0;
					var allFinishThisTurn = eta <= 1.0;
					Color fore = lstQueue.ForeColor, back = lstQueue.BackColor;
					if (i == ConstructionQueue.Orders.Count - 1)
					{
						if (idle)
							back = Color.DarkOrange;
						else if (firstFinishesThisTurn)
							fore = Color.LightGreen;
					}
					else
						fore = firstFinishesThisTurn ? (allFinishThisTurn ? Color.Cyan : Color.LightGreen) : lstQueue.ForeColor;
					if (back != lstQueue.BackColor)
						fore = lstQueue.BackColor;
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, firstEta.CeilingString(1) + "(" + eta.CeilingString(1) + ")", fore, back, lstQueue.Font));
					item.ImageIndex = i;
					il.Images.Add(order.Template.Icon);
					lstQueue.Items.Add(item);
					i++;
				}
				else
				{
					// building something different, or using expanded view
					count = 1;
					orders = new List<IConstructionOrder>();
					orders.Add(order);
					totalMin = minprogress;
					totalOrg = orgprogress;
					totalRad = radprogress;
					item = new ListViewItem(order.Name);
					item.Tag = orders;
					item.UseItemStyleForSubItems = false;
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(minprogress) ? "-" : (int)Math.Round(minprogress * 100) + "%", Resource.Minerals.Color, lstQueue.BackColor, lstQueue.Font));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(orgprogress) ? "-" : (int)Math.Round(orgprogress * 100) + "%", Resource.Organics.Color, lstQueue.BackColor, lstQueue.Font));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(radprogress) ? "-" : (int)Math.Round(radprogress * 100) + "%", Resource.Radioactives.Color, lstQueue.BackColor, lstQueue.Font));
					Color fore = lstQueue.ForeColor, back = lstQueue.BackColor;
					var idle = eta < 1.0;
					var finishesThisTurn = eta <= 1.0;
					if (i == ConstructionQueue.Orders.Count - 1)
						back = idle ? Color.DarkOrange : lstQueue.BackColor;
					else
						fore = finishesThisTurn ? Color.Cyan : lstQueue.ForeColor;
					if (back != lstQueue.BackColor)
						fore = lstQueue.BackColor;
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.CeilingString(1) + "(" + eta.CeilingString(1) + ")", fore, back, lstQueue.Font));
					item.ImageIndex = i;
					il.Images.Add(order.Template.Icon);
					lstQueue.Items.Add(item);
					i++;
					firstEta = eta;
				}
				if (selected != null && selected.Intersect(orders).Any())
					item.Selected = true;
				prevCost += remainingCost;
				lastTemplate = order.Template;
			}
			txtCargoStorageFree.Text = string.Format("Cargo Storage Free: {0} ({1} total in sector)", ConstructionQueue.CargoStorageFree.Kilotons(), ConstructionQueue.CargoStorageFreeInSector.Kilotons());
			txtFacilitySlotsFree.Text = string.Format("Facility Slots Free: {0}", ConstructionQueue.FacilitySlotsFree);
		}

		private void BindShipListView(IEnumerable<IDesign> designs)
		{
			lstShips.Initialize(32, 32);
			foreach (var design in designs.Where(d => ConstructionQueue.CanConstruct(d)).OrderBy(d => d.Role).ThenBy(d => d.Name).ThenBy(d => d.Iteration))
			{
				var eta = design.Cost.Keys.Max(res => (double)(design.Cost[res]) / (double)ConstructionQueue.Rate[res]);
				var x = lstShips.AddItemWithImage(design.Role, design.Name, design, design.Icon, null, eta.CeilingString(1));
				if (design.IsObsolete)
					x.ForeColor = Color.Gray;
				else if (design.IsObsolescent)
					x.ForeColor = Color.Yellow;
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
				foreach (var g in ConstructionQueue.Colony.Facilities.GroupBy(f => f.Template).OrderBy(g => g.Key.Group).ThenBy(g => g.Key.Name)) // TODO - roman numeral sorting
				{
					var oldf = g.Key;

					// facilites which are not yet upgraded
					var count = g.Count() - ConstructionQueue.Orders.OfType<UpgradeFacilityOrder>().Where(o => o.Upgrade.Old == oldf).Count();

					if (count > 0)
					{
						foreach (var newf in templates)
						{
							if (ConstructionQueue.CanConstruct(newf))
							{
								var group = lstFacilities.Groups.Cast<ListViewGroup>().SingleOrDefault(g2 => g2.Header == newf.Group);
								if (newf.Family.Value == oldf.Family.Value && newf.ModID != oldf.ModID)
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
									var fu = new FacilityUpgrade(oldf, newf);
									item.Tag = fu;
									var cost = newf.Cost * Mod.Current.Settings.UpgradeFacilityPercentCost / 100;
									var eta = cost.Keys.Max(res => (double)(cost[res]) / (double)ConstructionQueue.Rate[res]);
									item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.CeilingString(1)));
									ilFacil.Images.Add(newf.Icon);
									lstUpgrades.Items.Add(item);
									if (fu.IsObsolete)
										item.ForeColor = Color.Gray;
									else if (fu.IsObsolescent)
										item.ForeColor = Color.Yellow;
									i++;
								}
							}
						}
					}
				}
			}
		}

		private void btnBottom_Click(object sender, EventArgs e)
		{
			var sel = SelectedOrders.ToArray();
			foreach (var item in sel.Select(o => new { Order = o, OldIndex = ConstructionQueue.Orders.IndexOf(o), NewIndex = ConstructionQueue.Orders.Count() }))
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(ConstructionQueue, item.Order, item.NewIndex - item.OldIndex);
				newCommands.Add(cmd);
				cmd.Execute();
			}
			BindQueueListView(sel);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			CancelChanges();
			newCommands.Clear();
			removedCommands.Clear();
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			foreach (var order in ConstructionQueue.Orders.ToArray())
				RemoveOrder(order);
			BindQueueListView();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			RemoveOrdersConfirm(SelectedOrders);
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			var sel = SelectedOrders.ToArray();
			int delta;
			if (chkExpanded.Checked)
				delta = 1;
			else
				delta = FindSameItemsCountBelow(sel.Last()) + sel.Count() - 1;
			foreach (var order in sel)
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(ConstructionQueue, order, delta);
				newCommands.Add(cmd);
				cmd.Execute();
			}
			BindQueueListView(sel);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveCommands();
			newCommands.Clear();
			removedCommands.Clear();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnTop_Click(object sender, EventArgs e)
		{
			var sel = SelectedOrders.ToArray();
			foreach (var item in sel.Select(o => new { Order = o, OldIndex = ConstructionQueue.Orders.IndexOf(o), NewIndex = sel.IndexOf(o) }))
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(ConstructionQueue, item.Order, item.NewIndex - item.OldIndex);
				newCommands.Add(cmd);
				cmd.Execute();
			}
			BindQueueListView(sel);
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			var sel = SelectedOrders.ToArray();
			int delta;
			if (chkExpanded.Checked)
				delta = -1;
			else
				delta = -FindSameItemsCountAbove(sel.First());
			foreach (var order in sel)
			{
				var cmd = new RearrangeOrdersCommand<ConstructionQueue>(ConstructionQueue, order, delta);
				newCommands.Add(cmd);
				cmd.Execute();
			}
			BindQueueListView(sel);
		}

		/// <summary>
		/// Are we building this thingy anywhere?
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		private bool BuildingAnywhere(IConstructionTemplate t)
		{
			return Galaxy.Current.FindSpaceObjects<IConstructor>().OwnedBy(Empire.Current).Any(o => o.ConstructionQueue != null && o.ConstructionQueue.Orders.Any(o2 => o2.Template == t));
		}

		private void CancelChanges()
		{
			ConstructionQueue.Orders.Clear();
			foreach (var order in oldQueue)
				ConstructionQueue.Orders.Add(order);
			Empire.Current.Commands.Remove(Empire.Current.Commands.OfType<ToggleOrdersOnHoldCommand>().SingleOrDefault(x => x.Executor == ConstructionQueue));
			Empire.Current.Commands.Remove(Empire.Current.Commands.OfType<ToggleRepeatOrdersCommand>().SingleOrDefault(x => x.Executor == ConstructionQueue));
			ConstructionQueue.AreOrdersOnHold = wasOnHold;
			ConstructionQueue.AreRepeatOrdersEnabled = wasRepeat;
		}

		private void chkExpanded_CheckedChanged(object sender, EventArgs e)
		{
			BindQueueListView(SelectedOrders.ToArray());
		}

		private void chkOnlyLatest_CheckedChanged(object sender, EventArgs e)
		{
			if (chkOnlyLatest.Checked)
			{
                // HACK - why are the zero IDs the good ones?!
				BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.ID == 0 && f.Cost.Any()).OnlyLatestVersions(f => f.Family));
				BindShipListView(Empire.Current.KnownDesigns.Where(d => d.Owner == Empire.Current && d.HasBeenUnlockedBy(Empire.Current) && !d.IsObsolete));
				BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatestVersions(f => f.Family));
			}
			else
            {
                // HACK - why are the zero IDs the good ones?!
                BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.ID == 0 && f.Cost.Any()));
				BindShipListView(Empire.Current.KnownDesigns.Where(d => d.Owner == Empire.Current && d.HasBeenUnlockedBy(Empire.Current)));
				BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()));
			}
		}

		private void ClearDetails()
		{
			txtName.Text = "(No Item)";
			resCostMin.Amount = 0;
			resCostOrg.Amount = 0;
			resCostRad.Amount = 0;
		}

		private void ConstructionQueueForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (newCommands.Any() || removedCommands.Any())
			{
				switch (MessageBox.Show("Save changes?", "FrEee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
				{
					case DialogResult.Yes:
						SaveCommands();
						DialogResult = DialogResult.OK;
						break;

					case DialogResult.No:
						CancelChanges();
						DialogResult = DialogResult.Cancel;
						break;

					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}

			// TODO - remember "only latest" between invocations of this form
		}

		private void ConstructionQueueForm_Load(object sender, EventArgs e)
		{
			Text = ConstructionQueue.Container.Name + " Construction Queue";
			resMineralsRate.Amount = ConstructionQueue.Rate[Resource.Minerals];
			resOrganicsRate.Amount = ConstructionQueue.Rate[Resource.Organics];
			resRadioactivesRate.Amount = ConstructionQueue.Rate[Resource.Radioactives];

			// bind constructable lists
			chkOnlyLatest_CheckedChanged(this, new EventArgs());

			// show existing queued items
			BindQueueListView();
			
			oldQueue = new List<IConstructionOrder>();
			foreach (var order in ConstructionQueue.Orders)
				oldQueue.Add(order);
		}

		private int CountPresentFacilities(IEnumerable<Colony> colonies, string facilityFamily)
		{
			return colonies.Sum(c => c.Facilities.Where(f => f.Template.Family == facilityFamily).Count());
		}

		private int CountPresentVehicles(IEnumerable<ISpaceObject> sobjs, string designBaseName)
		{
			return sobjs.OfType<IVehicle>().Union(sobjs.OfType<ICargoContainer>().SelectMany(cc => cc.AllUnits)).Where(v => v.Design.BaseName == designBaseName).Count();
		}

		private int CountQueuedFacilities(IEnumerable<ConstructionQueue> queues, string facilityFamily)
		{
			// facility upgrades don't change the facility family so we don't need to worry about them here
			return queues.Sum(c => c.Orders.OfType<ConstructionOrder<Facility, FacilityTemplate>>().Where(o => o.Template.Family == facilityFamily).Count());
		}

		private int CountQueuedUpgrades(IEnumerable<ConstructionQueue> queues, string facilityFamily)
		{
			return queues.Sum(c => c.Orders.OfType<UpgradeFacilityOrder>().Where(o => o.Upgrade.Family == facilityFamily).Count());
		}

		private int CountQueuedVehicles(IEnumerable<IConstructor> sobjs, string designBaseName)
		{
			// TODO - what about refits to or away from this design?
			return sobjs.Where(sobj => sobj.ConstructionQueue != null).SelectMany(sobj => sobj.ConstructionQueue.Orders).Select(o => o.Template).OfType<IDesign>().Where(d => d.BaseName == designBaseName).Count();
		}

		private int CountUpgradeableFacilities(IEnumerable<Colony> colonies, string facilityFamily)
		{
			return colonies.Sum(c => c.Facilities.Where(f => f.Template.Family == facilityFamily && f.IsObsolescent).Count());
		}

		private void lstFacilities_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var facil = (FacilityTemplate)e.Item.Tag;
			txtName.Text = facil.Name;
			resCostMin.Amount = facil.Cost[Resource.Minerals];
			resCostOrg.Amount = facil.Cost[Resource.Organics];
			resCostRad.Amount = facil.Cost[Resource.Radioactives];
			var p = ConstructionQueue.Container as Planet;
			if (p != null)
			{
				int present, queued;
				IEnumerable<Colony> colonies;

				colonies = new Colony[] { p.Colony };
				present = CountPresentFacilities(colonies, facil.Family);
				queued = CountQueuedFacilities(colonies.Select(c => c.ConstructionQueue), facil.Family);
				lblPresentLocal.Text = MakePresentQueuedString(present, queued);

				colonies = p.Sector.SpaceObjects.OfType<Planet>().OwnedBy(p.Owner).Select(p2 => p2.Colony);
				present = CountPresentFacilities(colonies, facil.Family);
				queued = CountQueuedFacilities(colonies.Select(c => c.ConstructionQueue), facil.Family);
				lblPresentSector.Text = MakePresentQueuedString(present, queued);

				colonies = p.StarSystem.FindSpaceObjects<Planet>().OwnedBy(p.Owner).Select(p2 => p2.Colony);
				present = CountPresentFacilities(colonies, facil.Family);
				queued = CountQueuedFacilities(colonies.Select(c => c.ConstructionQueue), facil.Family);
				lblPresentSystem.Text = MakePresentQueuedString(present, queued);

				colonies = p.Owner.OwnedSpaceObjects.OfType<Planet>().Select(p2 => p2.Colony);
				present = CountPresentFacilities(colonies, facil.Family);
				queued = CountQueuedFacilities(colonies.Select(c => c.ConstructionQueue), facil.Family);
				lblPresentEmpire.Text = MakePresentQueuedString(present, queued);
			}
			else
			{
				lblPresentLocal.Text = lblPresentSector.Text = lblPresentSystem.Text = lblPresentEmpire.Text = "-";
			}
		}

		private void lstFacilities_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstFacilities.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					// how many to add?
					var amount = 1;
					if (ModifierKeys.HasFlag(Keys.Shift))
						amount *= 10;
					if (ModifierKeys.HasFlag(Keys.Control))
						amount *= 100;

					var template = (FacilityTemplate)item.Tag;
					for (int i = 0; i < amount; i++)
					{
						var order = new ConstructionOrder<Facility, FacilityTemplate> { Template = template };
						ConstructionQueue.Orders.Add(order);
						var cmd = new AddOrderCommand<ConstructionQueue>
						(
							ConstructionQueue,
							order
						);
						newCommands.Add(cmd);
					}
					BindQueueListView();
				}
				else if (e.Button == MouseButtons.Right)
				{
					var facil = (FacilityTemplate)item.Tag;
					var report = new FacilityReport(facil);
					var form = report.CreatePopupForm(facil.Name);
					FindForm().ShowChildForm(form);
				}
			}
		}

		private void lstFacilities_MouseLeave(object sender, EventArgs e)
		{
			ClearDetails();
		}

		private void lstQueue_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstQueue.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					// how many to remove?
					var amount = 1;
					if (ModifierKeys.HasFlag(Keys.Shift))
						amount *= 10;
					if (ModifierKeys.HasFlag(Keys.Control))
						amount *= 100;

					// delete order
					var orders = (IEnumerable<IConstructionOrder>)item.Tag;
					var sel = new List<IConstructionOrder>();
					for (int i = orders.Count() - 1; i >= 0 && i >= orders.Count() - amount; i--)
						sel.Add(orders.ElementAt(i));
					RemoveOrdersConfirm(sel);
					BindQueueListView();
				}
			}
		}

		private void lstQueue_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstQueue.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Right)
				{
					var orders = (IEnumerable<IConstructionOrder>)item.Tag;
					var order = orders.First(); // all condensed orders should be for same template anyway
					Control report = null;
					string title = null;
					if (order.Template is IDesign)
					{
						var design = (IDesign)order.Template;
						report = new DesignReport(design);
						title = design.Name;
					}
					else if (order.Template is FacilityTemplate)
					{
						var ft = (FacilityTemplate)order.Template;
						report = new FacilityReport(ft);
						title = ft.Name;
					}

					if (report != null)
					{
						var form = report.CreatePopupForm(title);
						this.ShowChildForm(form);
					}
				}
			}
		}

		private void lstShips_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var d = (IDesign)e.Item.Tag;
			txtName.Text = d.Name;
			resCostMin.Amount = d.Cost[Resource.Minerals];
			resCostOrg.Amount = d.Cost[Resource.Organics];
			resCostRad.Amount = d.Cost[Resource.Radioactives];

			int present, queued;
			IEnumerable<IConstructor> sobjs;
			var emp = ConstructionQueue.Container.Owner;

			sobjs = new IConstructor[] { ConstructionQueue.Container };
			present = CountPresentVehicles(sobjs, d.BaseName);
			queued = CountQueuedVehicles(sobjs, d.BaseName);
			lblPresentLocal.Text = MakePresentQueuedString(present, queued);

			sobjs = ConstructionQueue.Container.Sector.SpaceObjects.OfType<IConstructor>().OwnedBy(emp); ;
			present = CountPresentVehicles(sobjs, d.BaseName);
			queued = CountQueuedVehicles(sobjs, d.BaseName);
			lblPresentSector.Text = MakePresentQueuedString(present, queued);

			sobjs = ConstructionQueue.Container.StarSystem.SpaceObjects.OfType<IConstructor>().OwnedBy(emp);
			present = CountPresentVehicles(sobjs, d.BaseName);
			queued = CountQueuedVehicles(sobjs, d.BaseName);
			lblPresentSystem.Text = MakePresentQueuedString(present, queued);

			sobjs = ConstructionQueue.Owner.OwnedSpaceObjects.OfType<IConstructor>();
			present = CountPresentVehicles(sobjs, d.BaseName);
			queued = CountQueuedVehicles(sobjs, d.BaseName);
			lblPresentEmpire.Text = MakePresentQueuedString(present, queued);
		}

		private void lstShips_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstShips.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				var design = (IDesign)item.Tag;
				if (e.Button == MouseButtons.Left)
				{
					// how many to add?
					var amount = 1;
					if (ModifierKeys.HasFlag(Keys.Shift))
						amount *= 10;
					if (ModifierKeys.HasFlag(Keys.Control))
						amount *= 100;

					// is this a new design we've never built before? then tell the server about it
					if (design.IsNew && !BuildingAnywhere(design))
						Empire.Current.Commands.Add(design.CreateCreationCommand());

					for (int i = 0; i < amount; i++)
					{
						// add to queue
						var order = design.CreateConstructionOrder(ConstructionQueue);
						ConstructionQueue.Orders.Add(order);
						var cmd = new AddOrderCommand<ConstructionQueue>
						(
							ConstructionQueue,
							order
						);
						newCommands.Add(cmd);
					}
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

		private void lstShips_MouseLeave(object sender, EventArgs e)
		{
			ClearDetails();
		}

		private void lstUpgrades_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var fu = (FacilityUpgrade)e.Item.Tag;
			txtName.Text = fu.Name;
			resCostMin.Amount = fu.Cost[Resource.Minerals];
			resCostOrg.Amount = fu.Cost[Resource.Organics];
			resCostRad.Amount = fu.Cost[Resource.Radioactives];
			var p = ConstructionQueue.Container as Planet;
			if (p != null)
			{
				int present, queued;
				IEnumerable<Colony> colonies;

				colonies = new Colony[] { p.Colony };
				present = CountUpgradeableFacilities(colonies, fu.Old.Family);
				queued = CountQueuedUpgrades(colonies.Select(c => c.ConstructionQueue), fu.Family);
				lblPresentLocal.Text = MakeUpgradePresentQueuedString(present, queued);

				colonies = p.Sector.SpaceObjects.OfType<Planet>().OwnedBy(p.Owner).Select(p2 => p2.Colony);
				present = CountUpgradeableFacilities(colonies, fu.Old.Family);
				queued = CountQueuedUpgrades(colonies.Select(c => c.ConstructionQueue), fu.Family);
				lblPresentSector.Text = MakeUpgradePresentQueuedString(present, queued);

				colonies = p.StarSystem.FindSpaceObjects<Planet>().OwnedBy(p.Owner).Select(p2 => p2.Colony);
				present = CountUpgradeableFacilities(colonies, fu.Old.Family);
				queued = CountQueuedUpgrades(colonies.Select(c => c.ConstructionQueue), fu.Family);
				lblPresentSystem.Text = MakeUpgradePresentQueuedString(present, queued);

				colonies = p.Owner.OwnedSpaceObjects.OfType<Planet>().Select(p2 => p2.Colony);
				present = CountUpgradeableFacilities(colonies, fu.Old.Family);
				queued = CountQueuedUpgrades(colonies.Select(c => c.ConstructionQueue), fu.Family);
				lblPresentEmpire.Text = MakeUpgradePresentQueuedString(present, queued);
			}
			else
			{
				lblPresentLocal.Text = lblPresentSector.Text = lblPresentSystem.Text = lblPresentEmpire.Text = "-";
			}
		}

		private void lstUpgrades_MouseDown(object sender, MouseEventArgs e)
		{
			var item = lstUpgrades.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					// how many to add?
					var amount = 1;
					if (ModifierKeys.HasFlag(Keys.Shift))
						amount *= 10;
					if (ModifierKeys.HasFlag(Keys.Control))
						amount *= 100;

					var upgrade = (FacilityUpgrade)item.Tag;

					for (int i = 0; i < amount; i++)
					{
						var order = new UpgradeFacilityOrder(upgrade);
						ConstructionQueue.Orders.Add(order);
						var cmd = new AddOrderCommand<ConstructionQueue>
						(
							ConstructionQueue,
							order
						);
						newCommands.Add(cmd);
					}
					BindQueueListView();
					IEnumerable<FacilityTemplate> templates;
					if (chkOnlyLatest.Checked)
						templates = Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatestVersions(f => f.Family);
					else
						templates = Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any());
					BindUpgradeListView(templates);
				}
				else if (e.Button == MouseButtons.Right)
				{
					var facil = (FacilityUpgrade)item.Tag;
					var report = new FacilityReport(facil);
					var form = report.CreatePopupForm(facil.New.Name);
					FindForm().ShowChildForm(form);
				}
			}
		}

		private void lstUpgrades_MouseLeave(object sender, EventArgs e)
		{
			ClearDetails();
		}

		private string MakePresentQueuedString(int present, int queued)
		{
			if (present == 0 && queued == 0)
				return "-";
			else if (present == 0)
				return queued + " queued";
			else if (queued == 0)
				return present.ToString();
			else
				return "{0} (+{1} queued)".F(present, queued);
		}

		private string MakeUpgradePresentQueuedString(int present, int queued)
		{
			if (present == 0 && queued == 0)
				return "-";
			else if (queued == 0)
				return present + " upgradeable";
			else if (present > queued)
				return queued + " / " + present + " queued";
			else if (present == queued)
				return queued + " queued";
			else // present < queued?! that can't happen for upgrades...
				return queued + " / " + present + " queued?!";
		}

		private void RemoveOrder(IConstructionOrder order, bool rebindGui = true)
		{
			var cmds = Empire.Current.Commands.OfType<AddOrderCommand<ConstructionQueue>>().Where(o => o.Order == order).ToArray();
			if (cmds.Any())
			{
				// remove add-order command since the order is new this turn
				foreach (var cmd in cmds)
					removedCommands.Add(cmd);
			}
			else
			{
				cmds = newCommands.OfType<AddOrderCommand<ConstructionQueue>>().Where(o => o.Order == order).ToArray();
				if (cmds.Any())
				{
					// Not only new this turn, but new this instance of this form!
					foreach (var cmd in cmds)
						newCommands.Remove(cmd);
				}
				else
				{
					// add remove-order command
					var cmd = new RemoveOrderCommand<ConstructionQueue>(ConstructionQueue, order);
					newCommands.Add(cmd);
				}
			}

			// remove the order
			ConstructionQueue.Orders.Remove(order);

			if (order.Template is IDesign)
			{
				var design = order.Template as IDesign;

				// is this a new design we've never built before and are not building any more of? then don't tell the server so other players don't know ;)
				if (design.IsNew && !BuildingAnywhere(design))
				{
					// HACK - why can there be multiple commands to create the same design?
					foreach (var cmd in Empire.Current.Commands.OfType<ICreateDesignCommand>().Where(c => c.Design == design).ToArray())
						Empire.Current.Commands.Remove(cmd);
				}
			}

			if (rebindGui)
			{
				BindQueueListView();
				if (chkOnlyLatest.Checked)
					BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatestVersions(f => f.Family));
				else
					BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()));
			}
		}

		private void RemoveOrders(IEnumerable<IConstructionOrder> orders)
		{
			foreach (var o in orders)
				RemoveOrder(o, false);
			BindQueueListView();
			if (chkOnlyLatest.Checked)
				BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()).OnlyLatestVersions(f => f.Family));
			else
				BindUpgradeListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().Where(f => f.Cost.Any()));
		}

		private void RemoveOrdersConfirm(IEnumerable<IConstructionOrder> orders)
		{
			if (orders.Count() > 1)
			{
				if (orders.Any(o => o.Item != null && o.Item.ConstructionProgress.Any(kvp => kvp.Value > 0)))
				{
					if (MessageBox.Show("One or more selected construction orders have already been started. Really remove them and lose progress?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
						RemoveOrders(orders);
				}
				else
					RemoveOrders(orders);
			}
			else if (orders.Count() == 1)
			{
				var o = orders.Single();
				if (o.Item != null && o.Item.ConstructionProgress.Any(kvp => kvp.Value > 0))
				{
					if (MessageBox.Show("Construction of " + o.Item + " has already started. Really remove this order and lose progress?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
						RemoveOrders(orders);
				}
				else
					RemoveOrders(orders);
			}
		}

		private void SaveCommands()
		{
			foreach (var cmd in newCommands)
				Galaxy.Current.CurrentEmpire.Commands.Add(cmd);
			foreach (var cmd in removedCommands)
				Galaxy.Current.CurrentEmpire.Commands.Remove(cmd);
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			OpenWikiPage();
		}

		private void chkRepeat_CheckedChanged(object sender, EventArgs e)
		{
			var cmd = (Empire.Current.Commands.Union(newCommands ?? Enumerable.Empty<ICommand>())).OfType<ToggleRepeatOrdersCommand>().SingleOrDefault(x => x.Executor == ConstructionQueue);
			if (cmd == null)
			{
				cmd = new ToggleRepeatOrdersCommand(ConstructionQueue, chkRepeat.Checked);
				newCommands.Add(cmd);
			}
			else
				cmd.AreRepeatOrdersEnabled = chkRepeat.Checked;
			cmd.Execute();
		}

		private void chkOnHold_CheckedChanged(object sender, EventArgs e)
		{
			var cmd = (Empire.Current.Commands.Union(newCommands ?? Enumerable.Empty<ICommand>())).OfType<ToggleOrdersOnHoldCommand>().SingleOrDefault(x => x.Executor == ConstructionQueue);
			if (cmd == null)
			{
				cmd = new ToggleOrdersOnHoldCommand(ConstructionQueue, chkOnHold.Checked);
				newCommands.Add(cmd);
			}
			else
				cmd.AreOrdersOnHold = chkOnHold.Checked;
			cmd.Execute();
		}
	}
}