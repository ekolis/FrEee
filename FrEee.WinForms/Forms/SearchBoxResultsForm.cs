using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Forms
{
	public partial class SearchBoxResultsForm : Form
	{
		public SearchBoxResultsForm()
		{
			InitializeComponent();

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}
		}

		private IEnumerable<ISpaceObject> results;
		public IEnumerable<ISpaceObject> Results
		{
			get { return results; }
			set
			{
				listView.Initialize(32, 32);
				results = value.ToArray();
				foreach (var sobj in results)
					listView.AddItemWithImage(null, sobj.Name, sobj, sobj.Icon);
			}
		}

		public ISpaceObject SelectedObject
		{
			get
			{
				return listView.SelectedItems.Count == 0 ? null : (ISpaceObject)listView.SelectedItems[0].Tag;
			}
		}

		public delegate void ObjectSelectedDelegate(SearchBoxResultsForm sender, ISpaceObject sobj);

		public event ObjectSelectedDelegate ObjectSelected;

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (ObjectSelected != null)
				ObjectSelected(this, SelectedObject);
		}

		private void listView_SizeChanged(object sender, EventArgs e)
		{
			columnHeader1.Width = Width - 32;
		}
	}
}
