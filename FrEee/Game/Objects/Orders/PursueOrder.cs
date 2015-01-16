using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using System.Drawing;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.LogMessages;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object toward another space object.
	/// </summary>
	[Serializable]
	public class PursueOrder<T> : PathfindingOrder<T>
		where T : IMobileSpaceObject
	{
		public PursueOrder(ISpaceObject target, bool avoidEnemies)
			: base(target, avoidEnemies)
		{
			
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

		public override string Verb
		{
			get
			{
				if (KnownTarget == null)
					return "pursue";
				else if (AvoidEnemies && KnownTarget.Owner != null && (!(KnownTarget is ICombatant) || !(KnownTarget as ICombatant).IsHostileTo(Owner)))
					return "escort";
				else
					return "pursue";
			}
		}
	}
}
