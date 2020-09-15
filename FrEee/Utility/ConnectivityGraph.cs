using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Utility
{
	/// <summary>
	/// A connectivity graph.
	/// </summary>
	/// <typeparam name="T">The type of objects being connected.</typeparam>
	public class ConnectivityGraph<T> : ISet<T>
	{
		public ConnectivityGraph()
		{
		}

		public ConnectivityGraph(IEnumerable<T> items)
		{
			foreach (var item in items)
				Add(item);
		}

		public ConnectivityGraph(ConnectivityGraph<T> sub1, ConnectivityGraph<T> sub2)
		{
			foreach (var item in sub1.Union(sub2))
				Add(item);
		}

		/// <summary>
		/// Any connections that have been made.
		/// </summary>
		public SafeDictionary<T, HashSet<T>> Connections => connections;

		public int Count => items.Count;

		/// <summary>
		/// Is the graph connected? That is, are any two nodes connected? Or, is there no more than one subgraph?
		/// </summary>
		public bool IsConnected => Subgraphs.Count() <= 1;

		public bool IsReadOnly => items.IsReadOnly;

		/// <summary>
		/// Any discrete connected subgraphs of this graph.
		/// </summary>
		public IEnumerable<ConnectivityGraph<T>> Subgraphs
		{
			get
			{
				return subgraphs.Concat(singletons.Select(item =>
					{
						var sub = new ConnectivityGraph<T>();
						sub.Add(item);
						return sub;
					})).Where(sg => sg != null); // HACK - null subgraphs?!
			}
		}

		private SafeDictionary<T, HashSet<T>> connections = new SafeDictionary<T, HashSet<T>>(true);

		private ISet<T> items = new HashSet<T>();

		private List<T> singletons = new List<T>();

		private List<ConnectivityGraph<T>> subgraphs = new List<ConnectivityGraph<T>>();

		public bool Add(T item)
		{
			singletons.Add(item);
			return items.Add(item);
		}

		void ICollection<T>.Add(T item)
		{
			singletons.Add(item);
			items.Add(item);
		}

		/// <summary>
		/// Are two nodes connected through the graph?
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		public bool AreConnected(T node1, T node2, bool eitherWay = true)
		{
			if (eitherWay)
				return ComputeDistance(node1, node2) != null || ComputeDistance(node2, node1) != null;
			else
				return ComputeDistance(node1, node2) != null;
		}

		/// <summary>
		/// Are two nodes directly connected?
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		public bool AreDirectlyConnected(T node1, T node2, bool eitherWay = true)
		{
			if (eitherWay)
				return ComputeDistance(node1, node2) <= 1 || ComputeDistance(node2, node1) <= 1;
			else
				return ComputeDistance(node1, node2) <= 1;
		}

		public void Clear()
		{
			singletons.Clear();
			items.Clear();
			connections.Clear();
		}

		/// <summary>
		/// Computes the subgraph of nodes that are accessible from the starting location.
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public ConnectivityGraph<T> ComputeAccessFrom(T start)
		{
			var subgraph = new ConnectivityGraph<T>();
			ComputeAccessFromRecursive(start, subgraph);
			return subgraph;
		}

		/// <summary>
		/// Computes the subgraph of nodes that can access the starting location.
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public ConnectivityGraph<T> ComputeAccessTo(T end)
		{
			var subgraph = new ConnectivityGraph<T>();
			ComputeAccessToRecursive(end, subgraph);
			return subgraph;
		}

		/// <summary>
		/// Computes the distance between two nodes.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <param name="traversed"></param>
		/// <returns>The distance, or null if either the destination cannot be reached or the start node has already been traversed..</returns>
		public int? ComputeDistance(T start, T end, ICollection<T>? traversed = null)
		{
			if (traversed == null)
				traversed = new HashSet<T>();

			if (traversed.Contains(start))
				return null;

			traversed.Add(start);

			if ((object?)start == (object?)end)
				return 0; // we're already there!

			var exits = GetExits(start);

			if (!exits.Any())
				return null; // can't get anywhere from here!

			var path = Pathfinder.Pathfind(start, end, this);

			if (path == null || !path.Any())
				return null; // can't get there from here!

			return path.Count();
		}

		/// <summary>
		/// Computes the subgraph of nodes that are accessible from the starting location or can access it.
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public ConnectivityGraph<T> ComputeSubgraph(T node)
		{
			var subgraph = new ConnectivityGraph<T>();
			ComputeSubgraphRecursive(node, subgraph);
			return subgraph;
		}

		/// <summary>
		/// Connects two nodes.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="twoWay">Connect both ways?</param>
		public void Connect(T start, T end, bool twoWay = false)
		{
			if (!connections.ContainsKey(start) || !connections[start].Contains(end))
			{
				var sub1 = Subgraphs.Single(s => s.Contains(start));
				var sub2 = Subgraphs.Single(s => s.Contains(end));
				if (sub1 != sub2)
				{
					var sub3 = new ConnectivityGraph<T>(sub1, sub2);
					subgraphs.Remove(sub1);
					subgraphs.Remove(sub2);
					subgraphs.Add(sub3);
				}
				connections[start].Add(end);
				singletons.Remove(start);
				singletons.Remove(end);
			}
			if (twoWay)
				Connect(end, start, false);
		}

		public bool Contains(T item)
		{
			return items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Disconnects two nodes.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="twoWay">Disconnect both ways?</param>
		public void Disconnect(T start, T end, bool twoWay = false)
		{
			if (connections.ContainsKey(start) && connections[start].Contains(end))
			{
				var sub = Subgraphs.Single(s => s.Contains(start));
				var subs = sub.Subdivide();
				subgraphs.Remove(sub);
				subgraphs.AddRange(subs);
				connections[start].Remove(end);
				if (!GetExits(start).Any() && !GetEntrances(start).Any())
					singletons.Add(start);
				if (!GetEntrances(end).Any() && !GetExits(end).Any())
					singletons.Add(end);
			}
			if (twoWay)
				Disconnect(end, start, false);
		}

		public void ExceptWith(IEnumerable<T> other)
		{
			// TODO - reimplement ExceptWith
			throw new NotSupportedException();
		}

		/// <summary>
		/// Finds the entrances to a node.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public IEnumerable<T> GetEntrances(T node)
		{
			return Connections.Where(kvp => kvp.Value.Contains(node)).Select(kvp => kvp.Key);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Finds the exits from a node.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public IEnumerable<T> GetExits(T node)
		{
			return Connections[node];
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			// TODO - reimplement IntersectWith
			throw new NotSupportedException();
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return items.IsProperSubsetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return items.IsProperSupersetOf(other);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return items.IsSupersetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return items.IsSupersetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return items.Overlaps(other);
		}

		public bool Remove(T item)
		{
			singletons.Remove(item);
			var result = items.Remove(item);
			foreach (var kvp in connections.ToArray())
			{
				if ((object?)kvp.Key == (object?)item)
					connections.Remove(item);
				else if (kvp.Value.Contains(item))
					kvp.Value.Remove(item);
			}
			return result;
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return items.SetEquals(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			// TODO - reimplement SymmetricExceptWith
			throw new NotSupportedException();
		}

		public void UnionWith(IEnumerable<T> other)
		{
			singletons.AddRange(other);
			items.UnionWith(other);
		}

		private void ComputeAccessFromRecursive(T start, ConnectivityGraph<T> subgraph)
		{
			foreach (var exit in GetExits(start).Where(exit => !subgraph.Contains(exit)))
			{
				subgraph.Add(exit);
				subgraph.Connect(start, exit);
				ComputeAccessFromRecursive(exit, subgraph);
			}
		}

		private void ComputeAccessToRecursive(T end, ConnectivityGraph<T> subgraph)
		{
			foreach (var entrance in GetEntrances(end).Where(entrance => !subgraph.Contains(entrance)))
			{
				subgraph.Add(entrance);
				subgraph.Connect(entrance, end);
				ComputeAccessFromRecursive(entrance, subgraph);
			}
		}

		private void ComputeSubgraphRecursive(T node, ConnectivityGraph<T> subgraph)
		{
			foreach (var exit in GetExits(node).Where(exit => !subgraph.Contains(exit)))
			{
				subgraph.Add(exit);
				subgraph.Connect(node, exit);
				ComputeAccessFromRecursive(exit, subgraph);
			}
			foreach (var entrance in GetEntrances(node).Where(entrance => !subgraph.Contains(entrance)))
			{
				subgraph.Add(entrance);
				subgraph.Connect(entrance, node);
				ComputeAccessFromRecursive(entrance, subgraph);
			}
		}

		/// <summary>
		/// Subdivides the graph into discrete connected subgraphs.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<ConnectivityGraph<T>> Subdivide()
		{
			var subgraphs = new List<ConnectivityGraph<T>>();
			while (true)
			{
				// pick a node that we haven't already covered
				var nodes = this.Where(n => !subgraphs.Any(sg => sg.Contains(n)));
				if (!nodes.Any())
					break;
				var node = nodes.PickRandom();
				var subgraph = ComputeSubgraph(node);
				subgraphs.Add(subgraph);
			}
			return subgraphs.ToList();
		}
	}
}
