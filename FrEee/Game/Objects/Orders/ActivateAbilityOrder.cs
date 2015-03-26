using FrEee.Game.Interfaces;
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

		private Reference<IAbilityObject> source { get; set; }

		/// <summary>
		/// The source of the ability. Probably a component, facility, or hull.
		/// </summary>
		[DoNotSerialize]
		public IAbilityObject Source { get { return source.Value; } set { source = value.Reference(); } }

		private Reference<Ability> ability { get; set; }

		/// <summary>
		/// What ability to activate?
		/// </summary>
		[DoNotSerialize]
		public Ability Ability { get { return ability.Value; } set { ability = value.Reference(); } }

		private Reference<IReferrable> target { get; set; }

		/// <summary>
		/// What are we activating the ability "against"? Like, what warp point are we destroying, or whatever? Or null if there's no target (creating a warp point, or activating self destruct, or whatever)
		/// </summary>
		[DoNotSerialize]
		public IReferrable Target { get { return target.Value; } set { target = value.Reference(); } }

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
					// TODO - activate emergency propulsion
					// this might require a non-serialized "emergency speed" property on ships that factors into their speed calcs
				}
				else if (Ability.Rule.Matches("Self-Destruct"))
				{
					// destroy ship/planet/whatever BOOM!
					// TODO - make sure units with self destruct ability don't allow a ship or planet carrying them in cargo to self destruct...
					var sys = executor.StarSystem;
					executor.Dispose();
					sys.UpdateEmpireMemories();
					Owner.RecordLog(executor, executor + " has successfully self-destructed.");
				}
				else if (Ability.Rule.Matches("Open Warp Point"))
				{

				}
				else if (Ability.Rule.Matches("Close Warp Point"))
				{

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
					if (Source is IDamageable)
						(Source as IDamageable).Hitpoints = 0;
					if (Source is IHull)
						executor.Dispose(); // hull destruction kills the whole ship!
				}

				// destroy entire space object if necessary
				if (Source.HasAbility("Space Object Destroyed On Use"))
				{
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

		private Reference<Empire> owner { get; set; }

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
