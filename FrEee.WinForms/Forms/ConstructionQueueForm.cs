using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
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

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private void ConstructionQueueForm_Load(object sender, EventArgs e)
		{
			Text = ConstructionQueue.SpaceObject.Name + " Construction Queue";
			resMineralsRate.Amount = ConstructionQueue.Rate[Resource.Minerals];
			resOrganicsRate.Amount = ConstructionQueue.Rate[Resource.Organics];
			resRadioactivesRate.Amount = ConstructionQueue.Rate[Resource.Radioactives];

			// add ships/bases to constructable items
			BindShipListView(Empire.Current.KnownDesigns.Where(d => d.Owner == Empire.Current && d.HasBeenUnlockedBy(Empire.Current)));

			// add facilities to constructable items
			BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>());

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
				var duration = Math.Ceiling(order.Template.Cost.Keys.Max(res => (double)order.Template.Cost[res] / (double)ConstructionQueue.Rate[res]));
				var remainingCost = order.Template.Cost - (order.Item == null ? new ResourceQuantity() : order.Item.ConstructionProgress);
				double progress;
				var minprogress = (double)order.Item.ConstructionProgress[Resource.Minerals] / (double)order.Item.Cost[Resource.Minerals];
				var orgprogress = (double)order.Item.ConstructionProgress[Resource.Organics] / (double)order.Item.Cost[Resource.Organics];
				var radprogress = (double)order.Item.ConstructionProgress[Resource.Radioactives] / (double)order.Item.Cost[Resource.Radioactives];
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(minprogress) ? "-" : (int)Math.Round(minprogress * 100) + "%", Resource.Minerals.Color, lstQueue.BackColor, lstQueue.Font));
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(orgprogress) ? "-" : (int)Math.Round(orgprogress * 100) + "%", Resource.Organics.Color, lstQueue.BackColor, lstQueue.Font));
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, double.IsNaN(radprogress) ? "-" : (int)Math.Round(radprogress * 100) + "%", Resource.Radioactives.Color, lstQueue.BackColor, lstQueue.Font));
				var eta = Math.Ceiling(remainingCost.Keys.Max(res => (double)(remainingCost[res] + prevCost[res]) / (double)ConstructionQueue.Rate[res]));
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.ToString()));
				item.ImageIndex = i;
				il.Images.Add(order.Template.Icon);
				lstQueue.Items.Add(item);
				i++;
				prevCost += remainingCost;
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
				BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>().OnlyLatest(f => f.Family));
			else
				BindFacilityListView(Empire.Current.UnlockedItems.OfType<FacilityTemplate>());
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
					var eta = Math.Ceiling(facil.Cost.Keys.Max(res => (double)(facil.Cost[res]) / (double)ConstructionQueue.Rate[res]));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta.ToString()));
					ilFacil.Images.Add(facil.Icon);
					lstFacilities.Items.Add(item);
					i++;
				}
			}
		}

		private void BindShipListView(IEnumerable<IDesign> designs)
		{
			lstShips.Initialize(32, 32);
			foreach (var design in designs.Where(d => ConstructionQueue.CanConstruct(d)))
			{
				var eta = Math.Ceiling(design.Cost.Keys.Max(res => (double)(design.Cost[res]) / (double)ConstructionQueue.Rate[res]));
				lstShips.AddItemWithImage(design.Role, design.Name, design, design.Icon, eta.ToString());
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

		private void lstShips_MouseClick(object sender, MouseEventArgs e)
		{
			var item = lstShips.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					var design = (IDesign)item.Tag;
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
					// TODO - display detailed report on ship design
				}
			}
		}

		private void lstQueue_MouseClick(object sender, MouseEventArgs e)
		{
			var item = lstQueue.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				if (e.Button == MouseButtons.Left)
				{
					var order = (IConstructionOrder)item.Tag;
					var cmd = new RemoveOrderCommand<ConstructionQueue>(Empire.Current, ConstructionQueue, order);
					ConstructionQueue.Orders.Remove(order);
					newCommands.Add(cmd);
					BindQueueListView();
				}
				else if (e.Button == MouseButtons.Right)
				{
					// TODO - display detailed report on construction item
				}
			}
		}

		private void lstFacilities_MouseClick(object sender, MouseEventArgs e)
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
	}
}
