using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Interfaces;

namespace FrEee.Utility;

/// <summary>
/// A repository which stores some type of referrable object which can be accessed via an ID.
/// </summary>
/// <typeparam name="TValue">The type of object.</typeparam>
public class ReferrableRepository<T>
	: IReferrableRepository,
	IDictionary<long, T>
	where T : IReferrable
{
	public Type ReferrableType => typeof(T);

	public T this[long key] => Dictionary[key];

	public IEnumerable<long> Keys => Dictionary.Keys;
	public IEnumerable<T> Values => Dictionary.Values;
	public int Count => Dictionary.Count;

	/// <summary>
	/// The dictionary which stores the references to objects that are of type <see cref="T"/>.
	/// </summary>
	private IDictionary<long, T> Dictionary { get; set; } = new Dictionary<long, T>();

	/// <summary>
	/// Any sub-repositories storing concrete subtypes of type <see cref="T"/>.
	/// </summary>
	private ISet<IReferrableRepository> Subrepositories { get; set; } = new HashSet<IReferrableRepository>();

	/// <summary>
	/// Gets or creates a subrepository by the concrete object type stored.
	/// </summary>
	/// <typeparam name="T2">The concrete object type.</typeparam>
	/// <returns>The subrepository (or this repository if <see cref="T2"/> is the same as <see cref="T"/>).</returns>
	/// <exception cref="InvalidOperationException">if <see cref="T2"/> is an interface or abstract type.</exception>
	public ReferrableRepository<T2> GetSubrepository<T2>() where T2 : T
	{
		if (typeof(T2) == typeof(T))
		{
			return (ReferrableRepository<T2>)(object)this;
		}	
		if (typeof(T2).IsInterface || typeof(T2).IsAbstract)
		{
			throw new InvalidOperationException($"Can't get concrete referrable repository of interface/abstract type {typeof(T2)}.");
		}
		var result = Subrepositories.OfType<ReferrableRepository<T2>>().SingleOrDefault();
		if (result is null)
		{
			result = new ReferrableRepository<T2>();
			Subrepositories.Add(result);
		}
		return result;
	}

	/// <summary>
	/// Gets or creates a subrepository by the concrete object type stored.
	/// </summary>
	/// <param name="type">The concrete object type.</param>
	/// <returns>The subrepository (or this repository if <see cref="T2"/> is the same as <see cref="T"/>).</returns>
	/// <exception cref="InvalidOperationException">if <see cref="T2"/> is an interface or abstract type, or not a subtype of <see cref="T"/>.</exception>
	public IReferrableRepository GetSubrepository(Type type)
	{
		if (type == GetType())
		{
			return this;
		}
		if (type.IsInterface || type.IsAbstract)
		{
			throw new InvalidOperationException($"Can't get concrete referrable repository of interface/abstract type {type}.");
		}
		var result = Subrepositories.SingleOrDefault(q => q.ReferrableType == type);
		if (result is null)
		{
			result = (IReferrableRepository)typeof(ReferrableRepository<>).MakeGenericType(type).Instantiate();
			Subrepositories.Add(result);
		}
		return result;
	}

	ICollection<long> IDictionary<long, T>.Keys => Dictionary.Keys;
	ICollection<T> IDictionary<long, T>.Values => Dictionary.Values;
	public bool IsReadOnly => Dictionary.IsReadOnly;

	T IDictionary<long, T>.this[long key]
	{
		get => Dictionary[key];
		set => Add(key, value);
	}

	/// <summary>
	/// Assigns an ID to an object.
	/// Will dispose of an object that has a negative ID if it hasn't already been disposed of.
	/// </summary>
	/// <param name="r">The object.</param>
	/// <param name="id">The ID, or 0 to generate a new ID (unless the ID is already valid).</param>
	/// <returns>The new ID.</returns>
	public long Add(T r, long id = 0)
	{
		if (r.ID < 0 || r.IsDisposed)
		{
			if (!r.IsDisposed)
				r.Dispose();
			return r.ID;
		}

		if (r.HasValidID())
			return r.ID; // no need to reassign ID
		else if (Dictionary.ContainsKey(r.ID))
		{
			// HACK - already exists, just log an error but don't overwrite anything
			// we need to fix start combatants having the same IDs as the real objects...
			Console.Error.WriteLine($"The repository thinks that {Dictionary[r.ID]} has the ID {r.ID} but {r} claims to have that ID as well.");
			return r.ID;
		}

		var oldid = r.ID;
		long newid = oldid <= 0 ? id : oldid;

		while (newid <= 0 || Dictionary.ContainsKey(newid))
		{
			newid = RandomHelper.Range(1L, long.MaxValue);
		}
		r.ID = newid;
		Dictionary.Add(newid, r);
		if (r.GetType() != typeof(T))
		{
			GetSubrepository(r.GetType()).Add(r, id);
		}

		// clean up old IDs
		if (oldid > 0 && Dictionary.ContainsKey(oldid) && oldid != newid)
			Dictionary.Remove(oldid);

		return newid;
	}

	/// <summary>
	/// Unassigns an ID from a referrable, removing it from the repository.
	/// </summary>
	/// <param name="id"></param>
	public bool Remove(long id)
	{
		if (ContainsKey(id))
		{
			var r = this[id];
			r.ID = -1;
			Dictionary.Remove(id);
			GetSubrepository(r.GetType()).Remove(id);
			return true;
		}
		return false;
	}

	/// <summary>
	/// Unassigns an ID from a referrable, removing it from the repository.
	/// </summary>
	/// <param name="r"></param>
	public bool Remove(T r)
	{
		if (r == null || r.ID < 0)
			return false; // nothing to do
		if (ContainsKey(r.ID))
		{
			if (ReferenceEquals(this[r.ID],  r))
				Dictionary.Remove(r.ID);
			else
			{
				var repoThinksTheIDIs = this.SingleOrDefault(kvp => ReferenceEquals(kvp.Value, r)).Key;
				Dictionary.Remove(repoThinksTheIDIs);
				GetSubrepository(r.GetType()).Remove(repoThinksTheIDIs);
			}
		}
		else if (Values.Contains(r))
		{
			try
			{
				Dictionary.Remove(this.Single(kvp => ReferenceEquals(kvp.Value, r)));
				GetSubrepository(r.GetType()).Remove(r);
			}
			catch (InvalidOperationException ex)
			{
				// HACK - why is the item not being found? sequence contains no matching element? it's right there!
				Console.Error.WriteLine(ex);
			}
		}
		return true;
		//r.ID = -1;
	}

	/// <summary>
	/// Finds referrable objects in the repository.
	/// </summary>
	/// <typeparam name="T2"></typeparam>
	/// <param name="condition"></param>
	/// <returns></returns>
	public IEnumerable<T2> Find<T2>(Func<T2, bool>? condition = null) where T2 : T
	{
		if (condition == null)
			condition = t => true;
		return this.OfType<T2>().Where(condition);
	}

	public T? GetReferrable(long key)
	{
		if (!ContainsKey(key))
			return default;
		return this[key];
	}

	/// <summary>
	/// Finds a referrable of a specific type.
	/// </summary>
	/// <typeparam name="T2">The referrable's type.</typeparam>
	/// <param name="id">The referrable's ID.</param>
	/// <returns></returns>
	public T2? GetReferrable<T2>(long id)
		where T2 : T
	{
		if (typeof(T2).IsSealed)
		{
			return GetSubrepository<T2>().GetReferrable(id);
		}
		else
		{
			if (!typeof(T2).IsAbstract && !typeof(T2).IsInterface)
			{
				var r = GetSubrepository<T2>()[id];
				if (r is not null)
				{
					return r;
				}
			}
			foreach (var subrepo in Subrepositories)
			{
				if (typeof(T2) != subrepo.ReferrableType && typeof(T2).IsAssignableFrom(subrepo.ReferrableType))
				{
					var r = subrepo[id];
					if (r is not null)
					{
						return (T2)r;
					}
				}
			}
			return default;
		}
	}

	/// <summary>
	/// Finds the real version of a fake referrable.
	/// </summary>
	/// <typeparam name="T2"></typeparam>
	/// <param name="fakeobj">The fake referrable.</param>
	/// <returns></returns>
	public T2? GetReferrable<T2>(T2 fakeobj)
		where T2 : T
	{
		return GetReferrable<T2>(fakeobj.ID);
	}

	public bool ContainsKey(long key)
	{
		return Dictionary.ContainsKey(key);
	}

	public IEnumerator<KeyValuePair<long, T>> GetEnumerator()
	{
		return Dictionary.GetEnumerator();
	}

	public bool TryGetValue(long key, [MaybeNullWhen(false)] out T value)
	{
		return Dictionary.TryGetValue(key, out value);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Add(long key, T value) => Add(value, key);

	public void Add(KeyValuePair<long, T> item) => Add(item.Key, item.Value);

	public void Clear() => Dictionary.Clear();

	public bool Contains(KeyValuePair<long, T> item) => Dictionary.Contains(item);

	public void CopyTo(KeyValuePair<long, T>[] array, int arrayIndex) => Dictionary.CopyTo(array, arrayIndex);

	public bool Remove(KeyValuePair<long, T> item) => Remove(item.Key);

	long IReferrableRepository.Add(IReferrable r, long id)
	{
		return Add((T)r, id);
	}

	bool IReferrableRepository.Remove(IReferrable r)
	{
		return Remove((T)r);
	}

	IReferrable IReferrableRepository.this[long id] => this[id];
}
