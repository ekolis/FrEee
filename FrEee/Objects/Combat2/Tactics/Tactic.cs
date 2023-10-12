using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat2.Tactics
{
	/// <summary>
	/// A combat tactic used by a space object.
	/// </summary>
	/// <remarks>
	/// A tactic is actually a special kind of tactic block with specific inputs and outputs.
	/// </remarks>
	public class Tactic : TacticBlock, IPromotable, IOwnable, IReferrable, IFoggable
	{
		public Tactic(Empire owner)
			: base(null, "Tactic")
		{
			Owner = owner;
			Inputs["combatant"] = new TacticObjectInput(this, typeof(ICombatant)); // combatant executing the tactic
			Inputs["combatants"] = new TacticObjectInput(this, typeof(IEnumerable<ICombatant>)); // all combatants in battle
			Outputs["waypoint"] = new TacticConnectionOutput(this, true, typeof(CombatWaypoint)); // location and speed to match
			Outputs["targets"] = new TacticConnectionOutput(this, false, typeof(IEnumerable<ICombatant>)); // targets for each weapon group in order
			WeaponGroups = new SafeDictionary<MountedComponentTemplate, int>();
		}

		public Visibility CheckVisibility(Empire emp)
		{
			if (Owner == emp)
				return Visibility.Owned;

			// unowned tactics are as visible as their most visible design using them
			var designs = Galaxy.Current.Referrables.OfType<IDesign>().Where(d => d.Tactic == this);
			return designs.Max(d => d.CheckVisibility(emp));
		}

		/// <summary>
		/// Weapon groups assigned to this tactic.
		/// Default weapon group is group zero.
		/// A design will only use as many weapon groups as it has Multiplex Tracking ability (but a minimum of one group, and point defense and warhead weapons don't count against the total).
		/// </summary>
		public SafeDictionary<MountedComponentTemplate, int> WeaponGroups { get; private set; }

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// nothing to do
		}

		public Empire Owner
		{
			get;
			private set;
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
			if (!IsDisposed)
			{
				IsDisposed = true;
				Galaxy.Current.UnassignID(this);
			}
		}

		public void Redact(Empire emp)
		{
			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp
		{
			get;
			set;
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}
	}
}
