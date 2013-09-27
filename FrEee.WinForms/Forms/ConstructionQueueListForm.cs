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
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.IO;

namespace FrEee.WinForms.Forms
{
	public partial class ConstructionQueueListForm : Form
	{
		public ConstructionQueueListForm()
		{
			InitializeComponent();

			try {this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon);} catch {}

			RateMinerals.DefaultCellStyle.ForeColor = Resource.Minerals.Color;
			RateOrganics.DefaultCellStyle.ForeColor = Resource.Organics.Color;
			RateRadioactives.DefaultCellStyle.ForeColor = Resource.Radioactives.Color;

			// TODO - galaxy view background image can depend on galaxy template?
			galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine("Pictures", "UI", "Map", "quadrant"));
		}

		private void BindQueueList()
		{
			constructionQueueBindingSource.DataSource = Galaxy.Current.Referrables.OfType<ConstructionQueue>().Where(q => q.Owner == Empire.Current && q.Rate.Any(r => r.Value > 0));
		}

		private void ConstructionQueueListForm_Load(object sender, EventArgs e)
		{
			BindQueueList();
		}

		private void gridQueues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var queue = (ConstructionQueue)gridQueues.Rows[e.RowIndex].DataBoundItem;
				var form = new ConstructionQueueForm(queue);
				this.ShowChildForm(form);
				if (form.DialogResult == DialogResult.OK)
					BindQueueList();
			}
		}

		private void gridQueues_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			if (e.StateChanged == DataGridViewElementStates.Selected)
			{
				var queue = (ConstructionQueue)e.Row.DataBoundItem;
				var sys = queue.SpaceObject.StarSystem;
				galaxyView.SelectedStarSystem = sys;
			}
		}

		private void gridQueues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == gridQueues.Columns.IndexOf(CargoStorageFree) || 
				e.ColumnIndex == gridQueues.Columns.IndexOf(CargoStorageFreeInSector))
			{
				e.Value = ((int)e.Value).Kilotons();
			}
		}
	}
}
