using FrEee.Objects.Civilization;
using FrEee.Objects.Commands;
using FrEee.Objects.Space;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Technology;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.Orders;
using FrEee.Modding.Abilities;

namespace FrEee.UI.WinForms.Forms;

public partial class ActivateAbilityForm : GameForm
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
			// TODO - "Space Object Destroyed On Use" ability
		});

		gridAbilities.DataSource = abils;
	}

	private IMobileSpaceObject sobj;

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		var selRows = gridAbilities.SelectedRows.Cast<DataGridViewRow>();
		if (selRows.Count() != 1)
			// TODO - allow activation of multiple abilities at once
			MessageBox.Show("Please select a single ability to activate.");
		else
		{
			dynamic sel = selRows.Single().DataBoundItem;
			DialogResult result;
			// TODO - "Space Object Destroyed On Use" ability
			if (sel.IsDestroyedOnUse)
			{
				IAbilityObject toBeDestroyed = sel.Source is IHull ? sobj : sel.Source;
				result = MessageBox.Show("Activate this ability of " + sel.Source + "?\n" + sel.Ability + "\n" + toBeDestroyed + " will be destroyed!", "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			}
			else
				result = MessageBox.Show("Activate this ability of " + sel.Source + "?\n" + sel.Ability, "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				bool needsTarget;
				IEnumerable<IReferrable> targets = Enumerable.Empty<IReferrable>();
				IReferrable target = null;
				string targetType = "targets";
				var abil = sel.Ability as Ability;
				var src = sel.Source as IAbilityObject;

				Func<string> customCheck = () => null; // custom check for space objects being able to do stuff
													   // TODO - move custom check functions into a utility class for use on the server side too
				if (abil.Rule.Matches("Emergency Resupply"))
				{
					// TODO - allow resupplying other ships nearby?
					needsTarget = false;
				}
				else if (abil.Rule.Matches("Emergency Energy"))
				{
					needsTarget = false;
				}
				else if (abil.Rule.Matches("Self-Destruct"))
				{
					needsTarget = false;
				}
				else if (abil.Rule.Matches("Open Warp Point"))
				{
					// find systems in range
					needsTarget = true;
					targets = Game.Current.StarSystemLocations.Where(l => sobj.StarSystem != l.Item && sobj.StarSystem.Coordinates.EightWayDistance(l.Location) <= abil.Value1.ToInt()).Select(l => l.Item);
					targetType = "star systems within {0} light-years".F(abil.Value1);
				}
				else if (abil.Rule.Matches("Close Warp Point"))
				{
					// find warp points in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<WarpPoint>();
					targetType = "warp points";
				}
				else if (abil.Rule.Matches("Create Planet"))
				{
					// find asteroids in sector, and make sure there's at least one star
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<AsteroidField>();
					targetType = "asteroid fields";
					customCheck = () =>
					{
						if (!sobj.StarSystem.FindSpaceObjects<Star>().Any())
							return "We can't create a planet in a system without a star.";
						return null;
					};
				}
				else if (abil.Rule.Matches("Destroy Planet"))
				{
					// find planets in sector that are small enough to destroy
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<Planet>().Where(p => (int)p.StellarSize <= abil.Value1.ToInt());
					targetType = "planets not exceeding size {0}".F(abil.Value1);
				}
				else if (abil.Rule.Matches("Create Star"))
				{
					// make sure this isn't a special star system but it doesn't have any stars in it
					needsTarget = false;
					customCheck = () =>
						{
							// TODO - implement star system physical type
							/*
							if (sobj.StarSystem.PhysicalType != StarSystemPhysicalType.Empty)
								return "We can only create a star in an empty system.";
							 */
							return null;
						};
				}
				else if (abil.Rule.Matches("Destroy Star"))
				{
					// find stars in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<Star>();
					targetType = "stars";
				}
				else if (abil.Rule.Matches("Create Storm"))
				{
					needsTarget = false;
				}
				else if (abil.Rule.Matches("Destroy Storm"))
				{
					// find storms in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<Storm>();
					targetType = "storms";
				}
				else if (abil.Rule.Matches("Create Nebula"))
				{
					// don't need to pick a star, just make sure one exists
					needsTarget = false;
					customCheck = () =>
					{
						if (!sobj.Sector.SpaceObjects.OfType<Star>().Any())
							return "Creating a nebula requires a star in the sector.";
						return null;
					};
				}
				else if (abil.Rule.Matches("Destroy Nebula"))
				{
					needsTarget = false;
					customCheck = () =>
						{
							// TODO - implement star system physical type
							/*
							if (sobj.StarSystem.PhysicalType != StarSystemPhysicalType.Nebulae)
								return "There is no nebula in this system to destroy.";
							 */
							return null;
						};
				}
				else if (abil.Rule.Matches("Create Black Hole"))
				{
					// don't need to pick a star, just make sure one exists
					needsTarget = false;
					customCheck = () =>
						{
							if (!sobj.Sector.SpaceObjects.OfType<Star>().Any())
								return "Creating a black hole requires a star in the sector.";
							return null;
						};
				}
				else if (abil.Rule.Matches("Destroy Black Hole"))
				{
					needsTarget = false;
					customCheck = () =>
					{
						// TODO - implement star system physical type
						/*
						if (sobj.StarSystem.PhysicalType != StarSystemPhysicalType.BlackHole)
							return "There is no black hole in this system to destroy.";
						 */
						return null;
					};
				}
				else if (abil.Rule.Matches("Create Constructed Planet From Star"))
				{
					// find stars in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<Star>();
					targetType = "stars";
				}
				else if (abil.Rule.Matches("Create Constructed Planet From Planet"))
				{
					// find planets in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<Planet>();
					targetType = "planets";
				}
				else if (abil.Rule.Matches("Create Constructed Planet From Storm"))
				{
					// find storms in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<Storm>();
					targetType = "storms";
				}
				else if (abil.Rule.Matches("Create Constructed Planet From Warp Point"))
				{
					// find warp points in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<WarpPoint>();
					targetType = "warp points";
				}
				else if (abil.Rule.Matches("Create Constructed Planet From Asteroids"))
				{
					// find asteroids in sector
					needsTarget = true;
					targets = sobj.Sector.SpaceObjects.OfType<AsteroidField>();
				}
				else if (abil.Rule.Matches("Create Constructed Planet From Space"))
				{
					needsTarget = false;
				}
				else
				{
					MessageBox.Show(abil + " cannot be activated.");
					return;
				}

				// check for targets
				if (needsTarget)
				{
					if (!targets.Any())
					{
						MessageBox.Show("There are no suitable {0} available.".F(targetType));
						return;
					}
				}

				// check custom conditions
				var error = customCheck();
				if (error != null)
				{
					MessageBox.Show(error);
					return;
				}

				// pick target
				if (needsTarget)
				{
					var picker = new GenericPickerForm(targets);
					this.ShowChildForm(picker);
					if (picker.DialogResult == DialogResult.OK)
						target = picker.SelectedObject as IReferrable;
					else
						return;
				}

				// issue command
				var order = new ActivateAbilityOrder(sel.Source, sel.Ability, target);
				var cmd = new AddOrderCommand(sobj, order);
				cmd.Execute();
				Empire.Current.Commands.Add(cmd);
				Close();
			}
		}
	}
}