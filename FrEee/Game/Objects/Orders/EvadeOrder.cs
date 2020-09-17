using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to move a mobile space object away from another space object.
	/// The direction will be chosen at random
	/// </summary>
	[Serializable]
	public class EvadeOrder : PathfindingOrder
	{
		public EvadeOrder(ISpaceObject target, bool avoidEnemies)
			: base(target, avoidEnemies)
		{
		}

		public override string Verb => "evade";

		/// <summary>
		/// Finds the path for executing this order.
		/// </summary>
		/// <param name="sobj">The space object executing the order.</param>
		/// <returns></returns>
		public override IEnumerable<Sector?>? Pathfind(IMobileSpaceObject me, Sector start)
		{
			if (Target is IMobileSpaceObject)
			{
				if (me.CanWarp && !Target.CanWarp)
				{
					// warping via any warp point that leads outside the system should be safe, so prioritize those!
					var sys = me.FindStarSystem();
					var paths = sys.FindSpaceObjects<WarpPoint>()
						.Where(wp => wp.TargetStarSystemLocation is null || wp.TargetStarSystemLocation.Item != sys)
						.Select(wp => new { WarpPoint = wp, Path = Pathfinder.Pathfind(me, start, wp.Sector, AvoidEnemies, true, me.DijkstraMap) });
					if (paths.Any())
					{
						// found a warp point to flee to!
						var shortest = paths.WithMin(path => path.Path.Count()).PickRandom();
						return shortest?.Path.Concat(new Sector?[] { shortest.WarpPoint.Target });
					}
				}

				// see how he can reach us, and go somewhere away from him (that would take longer for him to get to than
				var dijkstraMap = Pathfinder.CreateDijkstraMap((IMobileSpaceObject)Target, Target.Sector, me.FindSector(), false, true);
				var canMoveTo = Pathfinder.GetPossibleMoves(me.Sector, me.CanWarp, me.Owner);
				var goodMoves = canMoveTo.Where(s => !dijkstraMap.Values.SelectMany(set => set).Any(n => n.Location == s));

				if (goodMoves.Any())
				{
					// just go there and recompute the path next time we can move - the enemy may have moved too
					return new Sector?[] { goodMoves.PickRandom() };
				}
				else
				{
					// trapped...
					return Enumerable.Empty<Sector>();
				}
			}
			else
			{
				// target is immobile! no need to flee, unless it's in the same sector
				if (Target?.Sector == me.FindSector())
				{
					// don't need to go through warp points to evade it, the warp points might be one way!
					var moves = Pathfinder.GetPossibleMoves(me.Sector, false, me.Owner);
					return new Sector?[] { moves.PickRandom() };
				}
				else
					return Enumerable.Empty<Sector>();
			}
		}

		// gotta keep on running...
		protected override bool AreWeThereYet(IMobileSpaceObject me) => Target == null || Target.IsDisposed;
	}
}
