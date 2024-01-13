using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class ConstructionQueueListForm : GameForm
	{
		public ConstructionQueueListForm()
		{
			InitializeComponent();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); } catch { }

			RateMinerals.DefaultCellStyle.ForeColor = Resource.Minerals.Color;
			RateOrganics.DefaultCellStyle.ForeColor = Resource.Organics.Color;
			RateRadioactives.DefaultCellStyle.ForeColor = Resource.Radioactives.Color;

			// TODO - galaxy view background image can depend on galaxy template?
			galaxyView.BackgroundImage = Pictures.GetModImage(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Pictures", "UI", "Map", "quadrant"));
		}

		private void BindQueueList()
		{
			constructionQueueBindingSource.DataSource = Empire.Current.ConstructionQueues.ToArray();
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

		private void gridQueues_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == gridQueues.Columns.IndexOf(FirstItemEta))
			{
				if (e.Value != null && (double)e.Value <= 1.0)
					e.CellStyle.ForeColor = Color.Cyan;
			}
			else if (e.ColumnIndex == gridQueues.Columns.IndexOf(Eta))
			{
				if (e.Value == null || (double)e.Value < 1.0)
				{
					e.CellStyle.BackColor = Color.Orange;
					e.CellStyle.ForeColor = Color.Black;
				}
			}
			else if (e.ColumnIndex == gridQueues.Columns.IndexOf(CargoStorageFree) ||
				e.ColumnIndex == gridQueues.Columns.IndexOf(CargoStorageFreeInSector))
			{
				e.Value = ((int)e.Value).Kilotons();
			}

			if (e.ColumnIndex == gridQueues.Columns.IndexOf(Eta) ||
				e.ColumnIndex == gridQueues.Columns.IndexOf(FirstItemEta))
			{
				if (e.Value != null)
					e.Value = ((double)e.Value).CeilingString(1);
			}
		}

		private void gridQueues_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.RowIndex < gridQueues.Rows.Count)
			{
				var row = gridQueues.Rows[e.RowIndex];
				var queue = (ConstructionQueue)row.DataBoundItem;
				var sys = queue.Container.StarSystem;
				galaxyView.SelectedStarSystem = sys;
				starSystemView.StarSystem = sys;
				starSystemView.SelectedSpaceObject = queue.Container;
				starSystemView.SelectedSector = queue.Container.Sector;
			}
		}

		private void gridQueues_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			galaxyView.SelectedStarSystem = null;
			starSystemView.StarSystem = null;
			starSystemView.SelectedSpaceObject = null;
			starSystemView.SelectedSector = null;
		}

		private void gridQueues_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
		}

		private void gridQueues_SelectionChanged(object sender, EventArgs e)
		{
			gridQueues.ClearSelection();
		}
	}
}