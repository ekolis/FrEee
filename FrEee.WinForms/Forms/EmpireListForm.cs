using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Forms
{
	public partial class EmpireListForm : Form
	{
		public EmpireListForm()
		{
			InitializeComponent();
			try { base.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
			BindEmpires();
		}

		private void BindEmpires()
		{
			lstEmpires.Initialize(128, 128);
			foreach (var emp in Galaxy.Current.Empires)
			{
				var item = lstEmpires.AddItemWithImage(null, emp.Name, emp, emp.Portrait);
				if (emp == Empire.Current)
					item.Selected = true;
			}
		}

		private void BindEmpire(Empire emp)
		{
			// TODO - bind empire
		}

		private void lstEmpires_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			var item = e.Item;
			if (e.IsSelected)
				BindEmpire((Empire)e.Item.Tag);
			else
				BindEmpire(null);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
