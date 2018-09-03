using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class CombatSimulatorForm : GameForm
	{
		public CombatSimulatorForm(bool groundCombat)
		{
			InitializeComponent();

			IsGroundCombat = groundCombat;

			BindVehicleTypeList();
			BindDesignList();
			Empires = new HashSet<SimulatedEmpire>(Galaxy.Current.Empires.ExceptSingle((Empire)null).Select(e => new SimulatedEmpire(e)));
			BindEmpireList();

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
		}

		/// <summary>
		/// Is this a ground combat sim, or a space combat sim?
		/// </summary>
		public bool IsGroundCombat { get; private set; }

		private SimulatedEmpire CurrentEmpire { get; set; }

		private SimulatedSpaceObject CurrentSpaceObject { get; set; }

		private HashSet<SimulatedEmpire> Empires { get; set; }

		private void BindCargoList()
		{
			// TODO - bind cargo list
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
			{
				foreach (var simemp2 in Empires.ExceptSingle(simemp))
					simemp.Empire.EncounteredEmpires.Add(simemp2.Empire);
				lstEmpires.AddItemWithImage(null, simemp.Empire.Name, simemp, simemp.Empire.Icon);
			}
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

		private void btnAddPlanet_Click(object sender, EventArgs e)
		{
			// TODO - let player choose a planet?
			var template = Mod.Current.StellarObjectTemplates.OfType<Planet>().Where(p => p.Atmosphere == CurrentEmpire.Empire.PrimaryRace.NativeAtmosphere).PickRandom();
			var planet = template.Instantiate();
			planet.Name = "Planet";
			var sim = new SimulatedSpaceObject(planet);
			var simPlanet = (Planet)sim.SpaceObject;
			simPlanet.Colony = new Colony();
			simPlanet.Colony.Owner = CurrentEmpire.Empire;
			// TODO - let player choose population?
			simPlanet.Colony.Population.Add(CurrentEmpire.Empire.PrimaryRace, simPlanet.MaxPopulation);
			CurrentEmpire.SpaceObjects.Add(sim);
			BindSpaceObjectList();
		}

		private void btnAddUnit_Click(object sender, EventArgs e)
		{
			// TODO - add unit
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
			v.SupplyRemaining = v.SupplyStorage;
			CurrentEmpire.SpaceObjects.Add(sv);
			BindSpaceObjectList();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnDuplicateCargo_Click(object sender, EventArgs e)
		{
			// TODO - duplicate cargo
		}

		//#region Simulated object wrappers
		//moved to Free.Game.Objects SimulatedObjectWrappers.cs
		//#endregion
		private void btnDuplicateEmpire_Click(object sender, EventArgs e)
		{
			foreach (var simemp in Empires.Where(se => lstEmpires.HasItemSelected(se)).ToArray())
			{
				var newse = new SimulatedEmpire(simemp.Empire);

				// no duplicate empire names!
				if (Empires.Any(se => se.Empire.Name == newse.Empire.Name))
					newse.Empire.Name = "Empire #" + newse.Empire.ID;

				Empires.Add(newse);
			}
			BindEmpireList();
		}

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

		private void btnOK_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			Sector location = new Sector(new StarSystem(0), new System.Drawing.Point());
			foreach (ISpaceObject ispobj in (Empires.SelectMany(se => se.SpaceObjects.Select(ss => ss.SpaceObject))))
				ispobj.Sector = location;
			// create battle with all our combatants
			//var battle = new Battle_Space(Empires.SelectMany(se => se.SpaceObjects.Select(ss => ss.SpaceObject)));
			var battle = new SpaceBattle(location);

			// simulate the battle
			battle.Resolve();

			// show the results
			var form = new BattleResultsForm(battle);
			Cursor = Cursors.Default;
			this.ShowChildForm(form);
		}

		private void btnRemoveCargo_Click(object sender, EventArgs e)
		{
			// TODO - remove cargo
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

		private void chkForeign_CheckedChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void chkHideObsolete_CheckedChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		private void CombatSimulatorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (var se in Empires)
				se.Empire.Dispose();
		}

		private void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindDesignList();
		}

		// TODO - add population?
		private void lstDesigns_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstDesigns.GetItemAt(e.X, e.Y);
			if (item != null)
				btnAddVehicle_Click(sender, new EventArgs()); // click the add vehicle button
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
	}
}