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
		/// Navigation for space objects.
		/// </summary>
		/// <param name="me"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="avoidEnemies"></param>
		/// <returns></returns>
		public static IEnumerable<Sector> Pathfind(IMobileSpaceObject me, Sector start, Sector end, bool avoidEnemies)
		{
			var map = CreateDijkstraMap(me, start, end, avoidEnemies);

			if (!map.Any())
				return Enumerable.Empty<Sector>(); // nowhere to go!

			if (map.Any(n => n.Location == end))
			{
				// can reach it
				var nodes = new List<DijkstraNode<Sector>>();
				var node = map.Where(n => n.Location == end).OrderBy(n => n.Cost).First();
				while (node != null)
				{
					nodes.Add(node);
					node = node.PreviousNode;
				}
				return nodes.Select(n => n.Location).Where(s => s != start).Reverse();
			}
			else
			{
				// can't reach it; get as close as possible
				var reverseMap = CreateDijkstraMap(null, end, start, false);
				var target = reverseMap.Where(n => map.Any(n2 => n2.Location == n.Location)).WithMin(n => n.Cost).FirstOrDefault();
				if (target == null)
					return Enumerable.Empty<Sector>(); // can't go anywhere
				else
				{
					// go to the closest point
					var nodes = new List<DijkstraNode<Sector>>();
					var node = map.Where(n => n.Location == target.Location).OrderBy(n => n.Cost).First();
					while (node != null)
					{
						nodes.Add(node);
						node = node.PreviousNode;
					}
					return nodes.Select(n => n.Location).Where(s => s != start).Reverse();
				}
			}
		}

		public static IEnumerable<DijkstraNode<Sector>> CreateDijkstraMap(IMobileSpaceObject me, Sector start, Sector end, bool avoidEnemies)
		{
			var startSys = start.FindStarSystem();

			// pathfind!
			// step 1: empty priority queue with cost to reach each node
			var queue = new List<DijkstraNode<Sector>>();

			// step 2: empty set of previously visited nodes, along with costs and previous-node references
			var visited = new List<DijkstraNode<Sector>>();

			// step 3: add start node and cost
			queue.Add(new DijkstraNode<Sector>(start, 0, null));

			// step 4: quit if there are no nodes (all paths exhausted without finding goal)
			bool success = false;
			while (queue.Any() && !success)
			{
				// step 5: take lowest cost node out of queue
				// also prefer straight line movement to diagonal
				var minCost = queue.Min(n => n.Cost);
				var node = queue.Where(n => n.Cost == minCost).First();
				queue.Remove(node);

				// step 6: if node is the goal, stop - success!
				if (node.Location == end)
					success = true;

				// step 7: check possible moves
				var moves = new List<Sector>();
				var s = node.Location;

				// normal moves
				foreach (var dx in new int[] { 0, -1, 1 })
				{
					foreach (var dy in new int[] { 0, -1, 1 })
					{
						if (dx == 0 && dy == 0)
							continue; // no need to sit still!
						var sys = s.FindStarSystem();
						var coords = s.Coordinates;
						if (Math.Abs(coords.X + dx) <= sys.Radius && Math.Abs(coords.Y + dy) <= sys.Radius)
							moves.Add(sys.GetSector(new Point(coords.X + dx, coords.Y + dy)));
					}
				}

				// warp points
				foreach (var wp in s.SpaceObjects.OfType<WarpPoint>())
					moves.Add(wp.Target);

				// step 7a: remove blocked points (aka calculate cost)
				if (avoidEnemies)
					moves = moves.Where(m => !m.SpaceObjects.Any(sobj => sobj.IsHostileTo(me.Owner))).ToList();

				// step 7b: update priority queue
				foreach (var move in moves)
				{
					if (!visited.Any(n => n.Location == move))
					{
						// didn't visit yet
						var newnode = new DijkstraNode<Sector>(move, node.Cost + 1, node);
						queue.Add(newnode);
						visited.Add(newnode);
					}
					else
					{
						// already visited - but is it by a longer path?
						var items = queue.Where(n => n.Location == move && n.Cost > node.Cost + 1);
						if (items.Any())
						{
							foreach (var old in items.ToArray())
								queue.Remove(old);
							var newnode = new DijkstraNode<Sector>(move, node.Cost + 1, node);
							queue.Add(newnode);
							visited.Add(newnode);
						}
					}
				}
			}

			return visited;
		}

		public class DijkstraNode<T>
		{
			public DijkstraNode(T location, int cost, DijkstraNode<T> previousNode)
			{
				Location = location;
				Cost = cost;
				PreviousNode = previousNode;
			}

			public T Location { get; set; }

			public int Cost { get; set; }

			public DijkstraNode<T> PreviousNode { get; set; }

			public override string ToString()
			{
				return "From " + PreviousNode.Location + " to " + Location + " (cost=" + Cost + ")";
			}
		}
	}
}
