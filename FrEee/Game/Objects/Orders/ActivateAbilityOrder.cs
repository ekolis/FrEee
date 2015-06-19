﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Modding;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to activate some sort of ability, like self-destruct or create a star.
	/// </summary>
	public class ActivateAbilityOrder : IOrder<IMobileSpaceObject>
	{
		public ActivateAbilityOrder(IAbilityObject source, Ability ability, IReferrable target)
		{
			Owner = Empire.Current;
			Source = source;
			Ability = ability;
			Target = target;
		}

		private GalaxyReference<IAbilityObject> source { get; set; }

		/// <summary>
		/// The source of the ability. Probably a component, facility, or hull.
		/// </summary>
		[DoNotSerialize]
		public IAbilityObject Source { get { return source.Value; } set { source = value.ReferViaGalaxy(); } }

		private GalaxyReference<Ability> ability { get; set; }

		/// <summary>
		/// What ability to activate?
		/// </summary>
		[DoNotSerialize]
		public Ability Ability { get { return ability.Value; } set { ability = value.ReferViaGalaxy(); } }

		private GalaxyReference<IReferrable> target { get; set; }

		/// <summary>
		/// What are we activating the ability "against"? Like, what warp point are we destroying, or whatever? Or null if there's no relevant target
		/// </summary>
		[DoNotSerialize]
		public IReferrable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		public void Execute(IMobileSpaceObject executor)
		{
			// error checking
			var errors = GetErrors(executor);
			if (executor.Owner != null)
			{
				foreach (var error in errors)
					executor.Owner.Log.Add(error);
			}

			if (!errors.Any())
			{
				// TODO - custom activatable abilities using scripts
				if (Ability.Rule.Matches("Emergency Resupply"))
				{
					executor.SupplyRemaining += Ability.Value1.Value.ToInt();
					// TODO - normalize supplies on other stuff, not just space vehicles
					if (executor is SpaceVehicle)
						(executor as SpaceVehicle).NormalizeSupplies();
					Owner.RecordLog(executor, executor + " has activated its emergency resupply pod and now has " + executor.SupplyRemaining.ToUnitString(true) + " supplies.");
				}
				else if (Ability.Rule.Matches("Emergency Energy"))
				{
					if (executor is Vehicle)
					{
						var v = executor as Vehicle;
						v.EmergencySpeed += Ability.Value1.Value.ToInt();
						Owner.RecordLog(executor, executor + " has activated its emergency propulsion and has had its speed boosted to " + v.Speed + " temporarily.");
					}
					else
					{
						Owner.RecordLog(executor, executor + " cannot activate emergency propulsion because it is not a vehicle.");
						return;
					}
				}
				else if (Ability.Rule.Matches("Self-Destruct"))
				{
					// destroy ship/planet/whatever BOOM!
					// TODO - make sure units with self destruct ability don't allow a ship or planet carrying them in cargo to self destruct...
					Owner.RecordLog(executor, executor + " has successfully self-destructed.");
					executor.DisposeAndLog("The {0}'s {2} has apparently self-destructed.".F(Owner, executor), Owner);
				}
				else if (Ability.Rule.Matches("Open Warp Point"))
				{
					var fromSector = executor.Sector;
					var fromSys = executor.StarSystem;
					if (fromSector == null || fromSys == null)
					{
						Owner.RecordLog(executor, executor + " cannot open a warp point because it is not located in space.");
						return;
					}
					var toSys = Target as StarSystem;
					if (toSys == null && Target is ILocated)
						toSys = (Target as ILocated).StarSystem;
					if (toSys == null)
					{
						Owner.RecordLog(executor, executor + " cannot open a warp point because no target system is specified.");
						return;
					}
					if (fromSys.Coordinates.EightWayDistance(toSys.Coordinates) > Ability.Value1.Value.ToInt())
					{
						Owner.RecordLog(executor, executor + " cannot open a warp point to " + toSys + " because " + toSys + " is too far away.");
						return;
					}
					// TODO - limit to one warp point between any two systems?
					if (Ability.BurnSupplies())
					{
						// find suitable warp point templates
						var wpt1 = Mod.Current.StellarObjectTemplates.OfType<WarpPoint>().Where(wp => !wp.IsUnusual).PickRandom();
						var wpt2 = Mod.Current.StellarObjectTemplates.OfType<WarpPoint>().Where(wp => !wp.IsUnusual).PickRandom();

						// figure out where the warp point goes, according to our game setup's warp point placement strategy
						// only in the target system - in the source system we get a warp point at the sector where the WP opener was
						var toSector = Galaxy.Current.WarpPointPlacementStrategy.GetWarpPointSector(fromSys.Location, toSys.Location);

						// create the warp points
						var wp1 = wpt1.Instantiate();
						var wp2 = wpt2.Instantiate();

						// configure the warp points
						wp1.IsOneWay = false;
						wp1.Name = "Warp Point to " + toSys;
						wp1.Target = toSector;
						fromSector.Place(wp1);
						wp2.IsOneWay = false;
						wp2.Name = "Warp Point to " + fromSys;
						wp2.Target = fromSector;
						toSector.Place(wp2);

						// let empires know that warp points were created
						Owner.RecordLog(wp1, "{0} has opened a warp point connecting {1} to {2}.".F(executor, fromSys, toSys));
						wp1.UpdateEmpireMemories("A new warp point to {0} has been created by the {1} in the {2} system.".F(toSys, Owner, fromSys), Owner);
						wp2.UpdateEmpireMemories("A new warp point to {0} has appeared in the {1} system.".F(fromSys, toSys), Owner);
					}
					else
					{
						Owner.RecordLog(executor, executor + " cannot open a warp point because it lacks the necessary supplies.");
						return;
					}
				}
				else if (Ability.Rule.Matches("Close Warp Point"))
				{
					var wp = Target as WarpPoint;

					// sanity checking, make sure we have a valid warp point to close here
					if (wp == null)
					{
						Owner.RecordLog(executor, executor + " cannot close a warp point because no warp point is specified to close.");
						return;
					}
					if (wp.Sector != executor.Sector)
					{
						Owner.RecordLog(executor, executor + " cannot close " + wp + " because " + wp + " is not in the same sector as " + executor + ".");
						return;
					}

					// need to close warp points on the other side too
					var otherwps = wp.Target.SpaceObjects.OfType<WarpPoint>().Where(w => w.Target == wp.Sector);

					// check for supplies and close WP
					if (Ability.BurnSupplies())
					{
						Owner.RecordLog(wp, "We have successfully closed the warp point connecting {0} to {1}.".F(wp.StarSystem, wp.Target.StarSystem));
						wp.DisposeAndLog("The {0} has closed the warp point connecting {1} to {2}.", Owner);
						foreach (var otherwp in otherwps)
							otherwp.DisposeAndLog("The warp point connecting {0} to {1} has closed.", Owner);
					}
					else
					{
						Owner.RecordLog(executor, executor + " cannot close " + wp + " because it lacks the necessary supplies.");
					}

				}
				else if (Ability.Rule.Matches("Create Planet"))
				{

				}
				else if (Ability.Rule.Matches("Destroy Planet"))
				{

				}
				else if (Ability.Rule.Matches("Create Star"))
				{

				}
				else if (Ability.Rule.Matches("Destroy Star"))
				{

				}
				else if (Ability.Rule.Matches("Create Storm"))
				{

				}
				else if (Ability.Rule.Matches("Destroy Storm"))
				{

				}
				else if (Ability.Rule.Matches("Create Nebula"))
				{

				}
				else if (Ability.Rule.Matches("Destroy Nebula"))
				{

				}
				else if (Ability.Rule.Matches("Create Black Hole"))
				{

				}
				else if (Ability.Rule.Matches("Destroy Black Hole"))
				{

				}
				else if (Ability.Rule.Matches("Create Constructed Planet From Star"))
				{
					
				}
				else if (Ability.Rule.Matches("Create Constructed Planet From Planet"))
				{

				}
				else if (Ability.Rule.Matches("Create Constructed Planet From Storm"))
				{

				}
				else if (Ability.Rule.Matches("Create Constructed Planet From Warp Point"))
				{

				}
				else if (Ability.Rule.Matches("Create Constructed Planet From Asteroids"))
				{

				}
				else if (Ability.Rule.Matches("Create Constructed Planet From Space"))
				{

				}

				// destroy component/etc. if necessary
				if (Source.HasAbility("Destroyed On Use"))
				{
					// TODO - log destruction
					if (Source is IDamageable)
						(Source as IDamageable).Hitpoints = 0;
					if (Source is IHull)
						executor.Dispose(); // hull destruction kills the whole ship!
				}

				// destroy entire space object if necessary
				if (Source.HasAbility("Space Object Destroyed On Use"))
				{
					// TODO - log destruction
					if (executor is Planet)
					{
						var p = executor as Planet;
						p.ConvertToAsteroidField();
					}
					else
					{
						executor.Dispose();
					}
				}
			}
		}

		public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor)
		{
			if (!Source.IntrinsicAbilities.Contains(Ability))
				yield return executor.CreateLogMessage(executor + " does not intrinsically possess the ability \"" + Ability + "\" with ID=" + Ability.ID + ".");
			if (!Ability.Rule.IsActivatable)
				yield return executor.CreateLogMessage("The ability \"" + Ability + "\" cannot be activated. It is a passive ability.");
			if (Source is IDamageable && (Source as IDamageable).IsDestroyed)
				yield return executor.CreateLogMessage(executor +" cannot activate " + Source + "'s ability because " + Source + " is destroyed.");
		}

		public bool CheckCompletion(IMobileSpaceObject executor)
		{
			return IsComplete;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public bool ConsumesMovement
		{
			get { return false; }
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var sobj in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
				sobj.RemoveOrder(this);
			Galaxy.Current.UnassignID(this);
		}

		private GalaxyReference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }


		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// no new client objects here, nothing to do
		}
	}
}
