using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
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
			Text = queue.SpaceObject.Name + " Construction Queue";
			resMineralsRate.Amount = queue.Rate["Minerals"];
			resOrganicsRate.Amount = queue.Rate["Organics"];
			resRadioactivesRate.Amount = queue.Rate["Radioactives"];

			// add facilities to constructable items
			// TODO - hide unresearched facilities
			int i = 0;
			var il = new ImageList();
			il.ImageSize = new Size(32, 32);
			lstFacilities.LargeImageList = il;
			lstFacilities.SmallImageList = il;
			foreach (var facil in Mod.Current.Facilities)
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
				il.Images.Add(facil.Icon);
				lstFacilities.Items.Add(item);
				i++;
			}
		}

		public ConstructionQueue ConstructionQueue { get; private set; }

		private void lstFacilities_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var facil =(Facility)e.Item.Tag;
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
	}
}
