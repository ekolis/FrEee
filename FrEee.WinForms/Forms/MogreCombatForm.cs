using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Vehicles;
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
	public partial class MogreCombatForm : Form
	{
		public MogreCombatForm(Battle_Space battle)
		{
			InitializeComponent();
			Battle = battle;
			Text = battle.Name;
		}

		public Battle_Space Battle { get; private set; }

		public SpaceVehicle SelectedVehicle
		{
			get { return spaceVehicleReport.Vehicle; }
			set { spaceVehicleReport.Vehicle = value; }
		}

		public void BindSelection()
		{
			spaceVehicleReport.Bind();
		}

		private IEnumerable<SpaceVehicle> AliveVehicles
		{
			get { return Battle.WorkingCombatants.OfType<SpaceVehicle>().Where(v => !v.IsDestroyed); }
		}

		private void btnPreviousShip_Click(object sender, EventArgs e)
		{
			var idx = AliveVehicles.ToList().IndexOf(SelectedVehicle) - 1;
			if (idx < 0)
				idx = AliveVehicles.Count() - 1;
			if (idx < 0)
				SelectedVehicle = null;
			else
				SelectedVehicle = AliveVehicles.ElementAt(idx);
		}

		private void btnNextShip_Click(object sender, EventArgs e)
		{
			var idx = AliveVehicles.ToList().IndexOf(SelectedVehicle) + 1;
			if (idx >= AliveVehicles.Count())
				idx = 0;
			if (AliveVehicles.Count() == 0)
				SelectedVehicle = null;
			else
				SelectedVehicle = AliveVehicles.ElementAt(idx);
		}

        public void updateText(string txt)
        {
            txtLog.Text = txt; 
        }
	}
}
