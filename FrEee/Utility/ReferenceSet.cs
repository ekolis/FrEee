﻿using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Utility
{
	[Serializable]
	public class GalaxyReferenceSet<T> : ReferenceSet<GalaxyReference<T>, T>
	{
		public GalaxyReferenceSet()
			: base()
		{
		}

		public GalaxyReferenceSet(IEnumerable<T> objs)
			: base(objs)
		{
		}
	}

	[Serializable]
	public class ModReferenceSet<T> : ReferenceSet<ModReference<T>, T>
			where T : IModObject
	{
		public ModReferenceSet()
			: base()
		{
		}

		public ModReferenceSet(IEnumerable<T> objs)
			: base(objs)
		{
		}
	}

	/// <summary>
	/// A set of references.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ReferenceSet<TRef, T> : ISet<T>, IPromotable, IReferenceEnumerable
		where TRef : IReference<T>
	{
		public ReferenceSet()
		{
			set = new HashSet<TRef>();
		}

		public ReferenceSet(IEnumerable<T> objs)
			: this()
		{
			foreach (var obj in objs)
				Add(obj);
		}

		public int Count
		{
			get { return set.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		private HashSet<TRef> set { get; set; }

		private ISet<T> Set { get { return new HashSet<T>(set.Select(r => r.Value)); } }

		public bool Add(T item)
		{
			return set.Add(MakeReference(item));
		}

		void ICollection<T>.Add(T item)
		{
			set.Add(MakeReference(item));
		}

		public void Clear()
		{
			set.Clear();
		}

		public bool Contains(T item)
		{
			return Set.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Set.CopyTo(array, arrayIndex);
		}

		public void ExceptWith(IEnumerable<T> other)
		{
			set.Clear();
			var result = Set;
			result.ExceptWith(other);
			foreach (var item in result)
				Add(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Set.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			set.Clear();
			var result = Set;
			result.IntersectWith(other);
			foreach (var item in result)
				Add(item);
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return Set.IsProperSubsetOf(other);
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return Set.IsProperSupersetOf(other);
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return Set.IsSubsetOf(other);
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return Set.IsSupersetOf(other);
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			return Set.Overlaps(other);
		}

		public bool Remove(T item)
		{
			if (item == null)
				return set.RemoveWhere(x => !x.HasValue) > 0; // TODO - remvoe only one null?
			else
				return set.Remove(MakeReference(item));
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				foreach (var r in set.OfType<IPromotable>())
					r.ReplaceClientIDs(idmap, done);
			}
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			return Set.SetEquals(other);
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			set.Clear();
			var result = Set;
			result.SymmetricExceptWith(other);
			foreach (var item in result)
				Add(item);
		}

		public void UnionWith(IEnumerable<T> other)
		{
			set.Clear();
			var result = Set;
			result.UnionWith(other);
			foreach (var item in result)
				Add(item);
		}

		private static TRef MakeReference(T item)
		{
			if (item == null)
				return (TRef)typeof(TRef).Instantiate();
			return (TRef)typeof(TRef).Instantiate(item);
		}
	}
}