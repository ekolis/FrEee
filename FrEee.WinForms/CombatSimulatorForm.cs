using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Forms;
using FrEee.WinForms.MogreCombatRender;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms
{
	public partial class CombatSimulatorForm : Form
	{
		public CombatSimulatorForm(bool groundCombat)
		{
			InitializeComponent();

			IsGroundCombat = groundCombat;

			BindVehicleTypeList();
			BindDesignList();
			Empires = new HashSet<SimulatedEmpire>(Galaxy.Current.Empires.Except((Empire)null).Select(e => new SimulatedEmpire(e)));
			BindEmpireList();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
		}

		/// <summary>
		/// Is this a ground combat sim, or a space combat sim?
		/// </summary>
		public bool IsGroundCombat { get; private set; }

		#region Data binding
		private void BindVehicleTypeList()
		{
			ddlVehicleType.Items.Clear();
			ddlVehicleType.Items.Add(new { Name = "All", VehicleTypes = VehicleTypes.All });
			ddlVehicleType.Items.Add(new { Name = "Ships/Bases", VehicleTypes = VehicleTypes.Ship | VehicleTypes.Base });
			ddlVehicleType.Items.Add(new { Name = "Units", VehicleTypes = VehicleTypes.Fighter | VehicleTypes.Satellite | VehicleTypes.Drone | VehicleTypes.Troop | VehicleTypes.Mine | VehicleTypes.WeaponPlatform });
			ddlVehicleType.Items.Add(new { Name = "Space", VehicleTypes = VehicleTypes.Ship | VehicleTypes.Base | VehicleTypes.Fighter | VehicleTypes.Satellite | VehicleTypes.Drone | VehicleTypes.Mine });
			ddlVehicleType.Items.Add(new { Name = "Ground", VehicleTypes = VehicleTypes.Troop | VehicleTypes.WeaponPlatform });
			ddlVehicleType.SelectedItem = ddlVehicleType.Items[0];
		}

		private void BindDesignList()
		{
			var emp = Galaxy.Current.CurrentEmpire;
			IEnumerable<IDesign> designs = emp.KnownDesigns;

			// filter by vehicle type
			var item = (dynamic)ddlVehicleType.SelectedItem;
			var vehicleTypeFilter = (VehicleTypes)item.VehicleTypes;
			designs = designs.Where(d => vehicleTypeFilter.HasFlag(d.VehicleType));

			// filter by ours/foreign (using an exclusive or)
			designs = designs.Where(d => d.Owner == emp ^ chkForeign.Checked);

			// filter by obsoleteness
			if (chkHideObsolete.Checked)
				designs = designs.Where(d => !d.IsObsolete);

			// display it!
			lstDesigns.Initialize(32, 32);
			foreach (var design in designs)
				lstDesigns.AddItemWithImage(design.Role, design.Name, design, design.Icon, design.Cost.Sum(kvp => kvp.Value).ToUnitString());
		}

		private void BindEmpireList()
		{
			lstEmpires.Initialize(32, 32);
			foreach (var simemp in Empires)
				lstEmpires.AddItemWithImage(null, simemp.Empire.Name, simemp, simemp.Empire.Icon);
		}

		private void BindSpaceObjectList()
		{
			lstSpaceObjects.Initialize(32, 32);
			if (CurrentEmpire != null)
			{
				foreach (var simsobj in CurrentEmpire.SpaceObjects)
					lstSpaceObjects.AddItemWithImage(simsobj.SpaceObject.WeaponTargetType.ToSpacedString(), simsobj.SpaceObject.Name, simsobj, simsobj.SpaceObject.Icon);
			}
			CurrentSpaceObject = null;
			BindCargoList();
		}

		private void BindCargoList()
		{
			// TODO - bind cargo list
		}

		private void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void chkHideObsolete_CheckedChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void chkForeign_CheckedChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void lstDesigns_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstDesigns.SelectedItems.Count > 0)
				designReport.Design = (IDesign)lstDesigns.SelectedItems[0].Tag;
			else
				designReport.Design = null;
		}

		private void lstEmpires_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstEmpires.SelectedItems.Count == 1)
				CurrentEmpire = (SimulatedEmpire)lstEmpires.SelectedItems[0].Tag;
			else
				CurrentEmpire = null;
			BindSpaceObjectList();
		}

		private void lstSpaceObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindCargoList();
		}
		#endregion

		#region Simulated object wrappers
		private class SimulatedEmpire : IDisposable
		{
			public SimulatedEmpire(Empire emp)
			{
				Empire = emp.CopyAndAssignNewID();
				SpaceObjects = new HashSet<SimulatedSpaceObject>();
			}

			public Empire Empire { get; private set; }

			public ISet<SimulatedSpaceObject> SpaceObjects { get; private set; }

			public void Dispose()
			{
				Empire.Dispose();
				foreach (var sobj in SpaceObjects)
					sobj.Dispose();
			}
		}

		private class SimulatedSpaceObject : IDisposable
		{
			public SimulatedSpaceObject(ICombatSpaceObject sobj)
			{
				SpaceObject = sobj;
				Units = new HashSet<SimulatedUnit>();
			}

			public ICombatSpaceObject SpaceObject { get; private set; }

			public ISet<SimulatedUnit> Units { get; private set; }

			// TODO - population in cargo?

			// TODO - enemy troops in cargo? or can those go under Units?

			public void Dispose()
			{
				SpaceObject.Dispose();
				foreach (var u in Units)
					u.Dispose();
			}
		}

		private class SimulatedUnit : IDisposable
		{
			public SimulatedUnit(IUnit u)
			{
				Unit = u;
			}

			public IUnit Unit { get; private set; }

			public void Dispose()
			{
				Unit.Dispose();
			}
		}
		#endregion

		private HashSet<SimulatedEmpire> Empires { get; set; }

		private SimulatedEmpire CurrentEmpire { get; set; }

		private SimulatedSpaceObject CurrentSpaceObject { get; set; }

		#region Empire controls
		private void btnDuplicateEmpire_Click(object sender, EventArgs e)
		{
			foreach (var simemp in Empires.Where(se => lstEmpires.HasItemSelected(se)).ToArray())
				Empires.Add(new SimulatedEmpire(simemp.Empire));
			BindEmpireList();
		}

		private void btnRemoveEmpire_Click(object sender, EventArgs e)
		{
			if (lstEmpires.Items.Count == lstEmpires.SelectedItems.Count)
			{
				MessageBox.Show("You cannot remove the last empire from the combat simulation.");
				return; // don't allow deleting the last item!
			}

			foreach (var simemp in Empires.Where(se => lstEmpires.HasItemSelected(se)).ToArray())
			{
				Empires.Remove(simemp);
				simemp.Dispose();
			}
			BindEmpireList();
		}
		#endregion

		#region Space object controls
		private void btnDuplicateSpaceObject_Click(object sender, EventArgs e)
		{
			if (CurrentEmpire == null)
			{
				MessageBox.Show("Please select an empire's vehicle(s) before clicking \"Duplicate Space Object\".");
				return;
			}

			foreach (var simsobj in CurrentEmpire.SpaceObjects.Where(ss => lstSpaceObjects.HasItemSelected(ss)).ToArray())
				CurrentEmpire.SpaceObjects.Add(new SimulatedSpaceObject(simsobj.SpaceObject));
			BindSpaceObjectList();
		}

		private void btnAddVehicle_Click(object sender, EventArgs e)
		{
			var dsn = designReport.Design;
			if (CurrentEmpire == null)
			{
				MessageBox.Show("Please select an empire before clicking\"Add Vehicle\".");
				return;
			}
			if (dsn == null)
			{
				MessageBox.Show("Please select a design before clicking \"Add Vehicle\".");
				return;
			}
			if (!(dsn is IDesign<SpaceVehicle>))
			{
				MessageBox.Show("Only space vehicle designs can be added to the vehicle list.");
				return;
			}

			// need to set owner *after* copying vehicle!
			var sv = new SimulatedSpaceObject((SpaceVehicle)dsn.Instantiate());
			var v = (SpaceVehicle)sv.SpaceObject;
			v.Owner = CurrentEmpire.Empire;
			CurrentEmpire.SpaceObjects.Add(sv);
			BindSpaceObjectList();
		}

		private void btnAddPlanet_Click(object sender, EventArgs e)
		{
			// TODO - planets in combat
			MessageBox.Show("Sorry, planetary combat is not yet implemented.");
		}

		private void btnRemoveSpaceObject_Click(object sender, EventArgs e)
		{
			if (CurrentEmpire == null)
				return; // nothing to do

			foreach (var simsobj in CurrentEmpire.SpaceObjects.Where(ss => lstSpaceObjects.HasItemSelected(ss)).ToArray())
			{
				CurrentEmpire.SpaceObjects.Remove(simsobj);
				simsobj.Dispose();
			}
			BindSpaceObjectList();
		}
		#endregion


		#region Cargo controls
		private void btnDuplicateCargo_Click(object sender, EventArgs e)
		{
			// TODO - duplicate cargo
		}

		private void btnAddUnit_Click(object sender, EventArgs e)
		{
			// TODO - add unit
		}

		// TODO - add population?

		private void btnRemoveCargo_Click(object sender, EventArgs e)
		{
			// TODO - remove cargo
		}
		#endregion

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			// create battle with all our combatants
			var battle = new Battle_Space(Empires.SelectMany(se => se.SpaceObjects.Select(ss => ss.SpaceObject)));
			
			// simulate the battle
			battle.Resolve();

			// show the results
			var form = new BattleResultsForm(battle);
			this.ShowChildForm(form);
		}

		private void lstDesigns_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstDesigns.GetItemAt(e.X, e.Y);
			if (item != null)
				btnAddVehicle_Click(sender, new EventArgs()); // click the add vehicle button
		}

	}
}
