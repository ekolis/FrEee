using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;

namespace FrEee.WinForms.Controls
{
	public partial class SearchBox : UserControl
	{
		public SearchBox()
		{
			InitializeComponent();
			lstItems = new ListView();
			lstItems.Width = Width;
			lstItems.Height = 128;
			lstItems.ItemSelectionChanged += lstItems_ItemSelectionChanged;
			lstItems.BackColor = Color.Black;
			lstItems.ForeColor = Color.White;
			lstItems.View = View.Details;
			lstItems.Columns.Add("", Width - 32);
			lstItems.HeaderStyle = ColumnHeaderStyle.None;
			popup = new ToolStripDropDown();
			host = new ToolStripControlHost(lstItems);
			host.Width = Width;
			host.Height = Height;

			popup.Items.Add(host);
		}

		void lstItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			HideResults();
			if (ObjectSelected != null)
				ObjectSelected(this, SelectedObject);
		}

		public delegate void ObjectSelectedDelegate(SearchBox sender, ISpaceObject sobj);

		public event ObjectSelectedDelegate ObjectSelected;

		private ListView lstItems;
		private ToolStripDropDown popup;
		private ToolStripControlHost host;

		private IEnumerable<ISpaceObject> objectsToSearch;
		public IEnumerable<ISpaceObject> ObjectsToSearch
		{
			get
			{
				return objectsToSearch;
			}
			set
			{
				objectsToSearch = value;
			}
		}

		/// <summary>
		/// The current star system. Items in this system will be shown first.
		/// </summary>
		public StarSystem StarSystem
		{
			get;
			set;
		}

		public ISpaceObject SelectedObject
		{
			get
			{
				return lstItems.SelectedItems.Count == 0 ? null : (ISpaceObject)lstItems.SelectedItems[0].Tag;
			}
		}

		public void ShowResults()
		{
			lstItems.Initialize(32, 32);
			// TODO - sort by warp distance
			foreach (var sobj in ObjectsToSearch.Where(o=> o.Name.ToLower().Contains(textBox.Text.ToLower())).OrderBy(o => o.FindStarSystem() == StarSystem ? 0 : 1))
				lstItems.AddItemWithImage(null, sobj.Name, sobj, sobj.Icon);
			popup.Show(PointToScreen(new Point(Location.X, Location.Y + Height)));
			textBox.Focus();
		}

		public void HideResults()
		{
			popup.Hide();
		}

		private void textBox_SizeChanged(object sender, EventArgs e)
		{
			Height = textBox.Height;
		}

		private void SearchBox_SizeChanged(object sender, EventArgs e)
		{
			host.Width = Width;
			lstItems.Width = Width;
		}

		private void SearchBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				// select first item
				if (lstItems.Items.Count > 0)
					lstItems.Items[0].Selected = true;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				// hide results
				HideResults();
			}
			else
			{
				// update results
				ShowResults();
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			// update results
			ShowResults();
		}
	}
}
