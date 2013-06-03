using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Finds paths avoiding obstacles.
	/// Adapted from http://roguebasin.roguelikedevelopment.org/index.php?title=Pathfinding
	/// </summary>
	public static class Pathfinder
	{
		/// <summary>
		/// Basic pathfinding algorithm.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="radius"></param>
		/// <param name="obstacles"></param>
		/// <returns></returns>
		public static IEnumerable<Point> FindPath(Point start, Point end, int radius, IEnumerable<Point> obstacles)
		{
			// step 1: empty priority queue with cost to reach each node
			var queue = new List<Tuple<Point, int>>();

			// step 2: empty set of previously visited nodes, along with costs and previous-node references
			var visited = new List<Tuple<Point, int, Point>>();

			// step 3: add start node and cost
			queue.Add(Tuple.Create(start, 0));

			// step 4: quit if there are no nodes (all paths exhausted without finding goal)
			bool success = false;
			while (queue.Any() && !success)
			{
				// step 5: take lowest cost node out of queue
				var minCost = queue.Min(n => n.Item2);
				var node = queue.Where(n => n.Item2 == minCost).First();
				queue.Remove(node);

				// step 6: if node is the goal, stop - success!
				if (node.Item1 == end)
					success = true;

				// step 7: check possible moves
				var moves = new List<Point>();
				var p = node.Item1;
				for (int dx = -1; dx <= 1; dx++)
				{
					for (int dy = -1; dy <= 1; dy++)
					{
						if (dx == 0 && dy == 0)
							continue; // no need to sit still!
						if (Math.Abs(p.X + dx) <= radius && Math.Abs(p.Y + dy) <= radius)
							moves.Add(new Point(p.X + dx, p.Y + dy));
					}
				}
				
				// step 7a: remove blocked points (aka calculate cost)
				moves = moves.Where(m => !obstacles.Contains(m)).ToList();

				// step 7b: update priority queue
				foreach (var move in moves)
				{
					if (!visited.Any(n => n.Item1 == move))
					{
						// didn't visit yet
						queue.Add(Tuple.Create(move, node.Item2 + 1));
						visited.Add(Tuple.Create(move, node.Item2 + 1, node.Item1));
					}
					else
					{
						// already visited - but is it by a longer path?
						var items = queue.Where(n => n.Item1 == move && n.Item2 > node.Item2 + 1);
						if (items.Any())
						{
							foreach (var old in items.ToArray())
								queue.Remove(old);
							queue.Add(Tuple.Create(move, node.Item2 + 1));
							visited.Add(Tuple.Create(move, node.Item2 + 1, node.Item1));
						}
					}
				}

			}

			if (!visited.Any())
				return Enumerable.Empty<Point>(); // nowhere to go!

			// find best path
			var closeness = Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
			var bestCloseness = visited.Min(n => Math.Abs(n.Item1.X - end.X) + Math.Abs(n.Item1.Y - end.Y));
			if (closeness <= bestCloseness)
				return Enumerable.Empty<Point>(); // can't get any closer!
			var closeNodes = visited.Where(n => Math.Abs(n.Item1.X - end.X) + Math.Abs(n.Item1.Y - end.Y) == bestCloseness);
			var distTraveled = closeNodes.Min(n => n.Item2);
			var tail = closeNodes.First(n => n.Item2 == distTraveled);
			var path = new List<Point>();
			while (tail != null)
			{
				path.Insert(0, tail.Item1);
				tail = visited.Where(n => n.Item1 == tail.Item3 && n.Item2 == tail.Item2 - 1).FirstOrDefault();
			}
			return path;
		}

		/// <summary>
		/// Navigation for space objects.
		/// </summary>
		/// <param name="me"></param>
		/// <param name="end"></param>
		/// <param name="avoidEnemies"></param>
		/// <returns></returns>
		public static IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector end, bool avoidEnemies)
		{
			var start = me.FindSector();
			var startSys = start.FindStarSystem();
			if (startSys == end.FindStarSystem())
			{
				// pathfind within system
				var hereCoords = start.Coordinates;
				var thereCoords = end.Coordinates;
				var obstacles = new List<Point>();
				if (avoidEnemies)
				{
					for (int x = -startSys.Radius; x <= startSys.Radius; x++)
					{
						for (int y = -startSys.Radius; y <= startSys.Radius; y++)
						{
							var p = new Point(x, y);
							if (startSys.GetSector(p).SpaceObjects.Any(sobj => sobj.IsHostileTo(me.Owner)))
								obstacles.Add(p);
						}
					}
				}
				var path = FindPath(start.Coordinates, end.Coordinates, startSys.Radius, obstacles);
				return path.Select(p => startSys.GetSector(p));
			}
			else
			{
				// TODO - pathfind via warp points
				return Enumerable.Empty<Sector>();
			}
		}
	}
}
