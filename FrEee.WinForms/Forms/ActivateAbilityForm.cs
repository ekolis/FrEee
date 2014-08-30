using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Orders;
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
	public partial class ActivateAbilityForm : Form
	{
		public ActivateAbilityForm(IMobileSpaceObject sobj)
		{
			InitializeComponent();

			this.sobj = sobj;

			var abils = sobj.ActivatableAbilities().Select(kvp => new
				{
					Ability = kvp.Key,
					Source = kvp.Value,
					IsDestroyedOnUse = kvp.Value.HasAbility("Destroyed On Use"),
				});

			gridAbilities.DataSource = abils;
		}

		private IMobileSpaceObject sobj;

		private void btnOK_Click(object sender, EventArgs e)
		{
			var selRows = gridAbilities.SelectedRows.Cast<DataGridViewRow>();
			if (selRows.Count() != 1)
				MessageBox.Show("Please select a single ability to activate.");
			else
			{
				dynamic sel = selRows.Single().DataBoundItem;
				DialogResult result;
				if (sel.IsDestroyedOnUse)
				{
					IAbilityObject toBeDestroyed = sel.Source is IHull ? sobj : sel.Source;
					result = MessageBox.Show("Activate this ability of " + sel.Source + "?\n" + sel.Ability + "\n" + toBeDestroyed + " will be destroyed!", "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				}
				else
					result = MessageBox.Show("Activate this ability of " + sel.Source + "?\n" + sel.Ability, "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.Yes)
				{
					var order = new ActivateAbilityOrder(sel.Ability, null); // TODO - let the user pick a target for the ability (such as destroy planet, which planet to destroy?)
					var cmd = new AddOrderCommand<IMobileSpaceObject>(sobj, order);
					cmd.Execute();
					Empire.Current.Commands.Add(cmd);
					Close();
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
