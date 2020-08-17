using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object toward another space object.
	/// </summary>
	[Serializable]
	public class PursueOrder : PathfindingOrder
	{
		public PursueOrder(ISpaceObject target, bool avoidEnemies)
			: base(target, avoidEnemies)
		{
		}

		public override string Verb
		{
			get
			{
				if (KnownTarget == null)
					return "pursue";
				else if (AvoidEnemies && KnownTarget.Owner != null && (!(KnownTarget is ICombatant) || !((ICombatant)KnownTarget).IsHostileTo(Owner)))
					return "escort";
				else
					return "pursue";
			}
		}

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <returns></returns>
		public override IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start)
		{
			return Pathfinder.Pathfind(me, start, Destination, AvoidEnemies, true, me.DijkstraMap);
		}

		protected override bool AreWeThereYet(IMobileSpaceObject me) => me.Sector == Destination;
	}
}
