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
	public partial class ConstructionQueueListForm : Form
	{
		public ConstructionQueueListForm()
		{
			InitializeComponent();
		}

		private void BindQueueList()
		{
			constructionQueueBindingSource.DataSource = Galaxy.Current.Referrables.OfType<ConstructionQueue>();
		}

		private void ConstructionQueueListForm_Load(object sender, EventArgs e)
		{
			BindQueueList();
		}

		private void gridQueues_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0
			{
				var queue = (ConstructionQueue)gridQueues.Rows[e.RowIndex].DataBoundItem;
				var form = new ConstructionQueueForm(queue);
				this.ShowChildForm(form);
				if (form.DialogResult == DialogResult.OK)
					BindQueueList();
			}
		}
	}
}
