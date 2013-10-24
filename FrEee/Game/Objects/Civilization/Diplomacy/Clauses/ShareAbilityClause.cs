using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// Allows empires to share usage of abilities that can affect other objects, such as resupply.
	/// </summary>
	public class ShareAbilityClause : Clause
	{
		public ShareAbilityClause(Empire giver, Empire receiver, AbilityRule rule, SharingPriority priority)
			: base(giver, receiver)
		{
			AbilityRule = rule;
			Priority = priority;
		}

		/// <summary>
		/// The type of ability being shared.
		/// </summary>
		public AbilityRule AbilityRule { get; set; }

		/// <summary>
		/// The priority of sharing with this empire.
		/// </summary>
		public SharingPriority Priority { get; set; }

		/// <summary>
		/// Doesn't do anything; this clause just affects ability scopes.
		/// </summary>
		public override void PerformAction()
		{
			
		}

		public override string Description
		{
			get
			{
				return Giver.WeOrName() + " will share the " + AbilityRule.Name + " ability with " + Receiver.UsOrName() + " at " + Priority.ToString().ToLower() + " priority.";
			}
		}
	}
}
