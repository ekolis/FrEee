﻿using FrEee.Game.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class SearchBoxResultsForm : Form
	{
		public SearchBoxResultsForm()
		{
			InitializeComponent();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }
		}

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

		private IEnumerable<ISpaceObject> results;

		private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (ObjectSelected != null)
				ObjectSelected(this, SelectedObject);
		}

		private void listView_SizeChanged(object sender, EventArgs e)
		{
			columnHeader1.Width = Width - 32;
		}

		public event ObjectSelectedDelegate ObjectSelected;

		public delegate void ObjectSelectedDelegate(SearchBoxResultsForm sender, ISpaceObject sobj);
	}
}