using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Utility
{
	/// <summary>
	/// A connectivity graph.
	/// </summary>
	/// <typeparam name="T">The type of objects being connected.</typeparam>
	public class ConnectivityGraph<T> : ISet<T>
	{
		/// <summary>
		/// Connects two nodes.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="twoWay">Connect both ways?</param>
		public void Connect(T start, T end, bool twoWay = false)
		{
			var link = Tuple.Create(start, end);
			if (!connections.Contains(link))
				connections.Add(link);
			if (twoWay)
				Connect(end, start, false);
		}

		/// <summary>
		/// Disconnects two nodes.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="twoWay">Disconnect both ways?</param>
		public void Disconnect(T start, T end, bool twoWay = false)
		{
			var link = Tuple.Create(start, end);
			if (connections.Contains(link))
				connections.Remove(link);
			if (twoWay)
				Disconnect(end, start, false);
		}

		private ISet<Tuple<T, T>> connections = new HashSet<Tuple<T, T>>();

		/// <summary>
		/// Computes the distance between two nodes.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <param name="traversed"></param>
		/// <returns>The distance, or null if either the destination cannot be reached or the start node has already been traversed..</returns>
		public int? ComputeDistance(T start, T end, ICollection<T> traversed = null)
		{
			if (traversed == null)
				traversed = new HashSet<T>();

			if (traversed.Contains(start))
				return null;

			traversed.Add(start);

			if ((object)start == (object)end)
				return 0; // we're already there!

			var exits = GetExits(start);

			if (!exits.Any())
				return null; // can't get anywhere from here!

			// yay recursion?
			var distances = exits.Select(exit => ComputeDistance(exit, end, traversed)).Where(dist => dist != null).ToArray();

			if (!distances.Any())
				return null; // can't get there from here!

			return distances.Min(dist => dist.Value) + 1;
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
		/// Finds the entrances to a node.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public IEnumerable<T> GetEntrances(T node)
		{
			return Connections.Where(t => (object)t.Item2 == (object)node).Select(t => t.Item1);
		}

		/// <summary>
		/// Finds the exits from a node.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public IEnumerable<T> GetExits(T node)
		{
			return Connections.Where(t => (object)t.Item1 == (object)node).Select(t => t.Item2);
		}

		/// <summary>
		/// Are two nodes connected, either way?
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		public bool AreConnected(T node1, T node2)
		{
			return ComputeDistance(node1, node2) != null || ComputeDistance(node2, node1) != null;
		}

		/// <summary>
		/// Is the graph connected? That is, are any two nodes connected?
		/// </summary>
		public bool IsConnected
		{
			get
			{
				foreach (var n1 in this)
				{
					foreach (var n2 in this)
					{
						if (!AreConnected(n1, n2))
							return false;
					}
				}
				return true;
			}
		}

		/// <summary>
		/// Subdivides the graph into discrete connected subgraphs.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ConnectivityGraph<T>> Subdivide()
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
			return subgraphs;
		}

		/// <summary>
		/// Any connections that have been made.
		/// </summary>
		public IEnumerable<Tuple<T, T>> Connections
		{
			get { return connections; }
		}

		private ISet<T> items = new HashSet<T>();

		#region ISet implementation

		public bool Add(T item)
		{
			return items.Add(item);
		}

		public void ExceptWith(IEnumerable<T> other)
		{
			items.ExceptWith(other);
			var toRemove = connections.Where(link => !(items.Contains(link.Item1) || items.Contains(link.Item2)));
			foreach (var link in toRemove)
				connections.Remove(link);
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			items.IntersectWith(other);
			var toRemove = connections.Where(link => !(items.Contains(link.Item1) || items.Contains(link.Item2)));
			foreach (var link in toRemove)
				connections.Remove(link);
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

		public bool SetEquals(IEnumerable<T> other)
		{
			return items.SetEquals(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			items.SymmetricExceptWith(other);
			var toRemove = connections.Where(link => !(items.Contains(link.Item1) || items.Contains(link.Item2)));
			foreach (var link in toRemove)
				connections.Remove(link);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			items.UnionWith(other);
		}

		void ICollection<T>.Add(T item)
		{
			items.Add(item);
		}

		public void Clear()
		{
			items.Clear();
			connections.Clear();
		}

		public bool Contains(T item)
		{
			return items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return items.Count; }
		}

		public bool IsReadOnly
		{
			get { return items.IsReadOnly; }
		}

		public bool Remove(T item)
		{
			var result = items.Remove(item);
			var toRemove = connections.Where(link => (object)link.Item1 == (object)item || (object)link.Item2 == (object)item);
			foreach (var link in toRemove)
				connections.Remove(link);
			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
