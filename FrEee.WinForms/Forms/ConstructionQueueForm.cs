using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Orders;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility.Extensions;
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
		}

		private void ConstructionQueueForm_Load(object sender, EventArgs e)
		{
			Text = ConstructionQueue.SpaceObject.Name + " Construction Queue";
			resMineralsRate.Amount = ConstructionQueue.Rate["Minerals"];
			resOrganicsRate.Amount = ConstructionQueue.Rate["Organics"];
			resRadioactivesRate.Amount = ConstructionQueue.Rate["Radioactives"];

			// add facilities to constructable items
			// TODO - hide unresearched facilities
			BindFacilityListView(Mod.Current.FacilityTemplates);

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
			lblFacilityName.Text = facil.Name;
			resFacilityMineralsCost.Amount = facil.Cost["Minerals"];
			resFacilityOrganicsCost.Amount = facil.Cost["Organics"];
			resFacilityRadioactivesCost.Amount = facil.Cost["Radioactives"];
		}

		private void lstFacilities_MouseLeave(object sender, EventArgs e)
		{
			lblFacilityName.Text = "(No Facility)";
			resFacilityMineralsCost.Amount = 0;
			resFacilityOrganicsCost.Amount = 0;
			resFacilityRadioactivesCost.Amount = 0;
		}

		private void lstFacilities_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (lstFacilities.SelectedItems.Count == 1)
			{
				var item = lstFacilities.SelectedItems[0];
				var template = (FacilityTemplate)item.Tag;
				var order = new ConstructionOrder<Facility, FacilityTemplate> { Template = template, Item = template.Instantiate() };
				ConstructionQueue.Orders.Add(order);
				var cmd = new AddOrderCommand<ConstructionQueue, IConstructionOrder>
				(
					Galaxy.Current.CurrentEmpire,
					ConstructionQueue,
					order
				);
				newCommands.Add(cmd);
				BindQueueListView();
			}
		}

		private void BindQueueListView()
		{
			lstQueue.Items.Clear();
			var il = new ImageList();
			il.ImageSize = new Size(32, 32);
			lstQueue.SmallImageList = il;
			lstQueue.LargeImageList = il;
			int i = 0;
			foreach (var order in ConstructionQueue.Orders)
			{
				var item = new ListViewItem(order.Item.Name);
				var duration = Math.Ceiling(order.Item.Cost.Keys.Max(res => (double)order.Item.Cost[res] / (double)ConstructionQueue.Rate[res]));
				var remainingCost = order.Item.Cost - order.Item.ConstructionProgress;
				double progress;
				if (order.Item.ConstructionProgress.Any())
					progress = order.Item.ConstructionProgress.Min(kvp => (double)kvp.Value / (double)order.Item.Cost[kvp.Key]);
				else
					progress = 0d;
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, (int)Math.Round(progress * 100) + "%"));
				var eta = Math.Ceiling(remainingCost.Keys.Max(res => (double)remainingCost[res] / (double)ConstructionQueue.Rate[res]));
				item.SubItems.Add(new ListViewItem.ListViewSubItem(item, eta + " turns"));
				item.ImageIndex = i;
				il.Images.Add(order.Item.Icon);
				lstQueue.Items.Add(item);
				i++;
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
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			CancelChanges();
			newCommands.Clear();
			Close();
		}

		private void chkOnlyLatest_CheckedChanged(object sender, EventArgs e)
		{
			if (chkOnlyLatest.Checked)
				BindFacilityListView(Mod.Current.FacilityTemplates.OnlyLatest(f => f.Family));
			else
				BindFacilityListView(Mod.Current.FacilityTemplates);
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
					ilFacil.Images.Add(facil.Icon);
					lstFacilities.Items.Add(item);
					i++;
				}
			}
		}
	}
}
