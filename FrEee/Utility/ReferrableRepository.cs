using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;

namespace FrEee.Utility;

/// <summary>
/// A repository which stores some type of referrable object which can be accessed via an ID.
/// </summary>
/// <typeparam name="TValue">The type of object.</typeparam>
public class ReferrableRepository
	: IReadOnlyDictionary<long, IReferrable>
{
	public IReferrable this[long key] => Dictionary[key];

	public IEnumerable<long> Keys => Dictionary.Keys;
	public IEnumerable<IReferrable> Values => Dictionary.Values;
	public int Count => Dictionary.Count;

	/// <summary>
	/// The dictionary which stores the references.
	/// </summary>
	private IDictionary<long, IReferrable> Dictionary { get; set; } = new Dictionary<long, IReferrable>();

	/// <summary>
	/// Assigns an ID to an object.
	/// Will dispose of an object that has a negative ID if it hasn't already been disposed of.
	/// </summary>
	/// <param name="r">The object.</param>
	/// <param name="id">The ID, or 0 to generate a new ID (unless the ID is already valid).</param>
	/// <returns>The new ID.</returns>
	public long AssignID(IReferrable r, long id = 0)
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

		// clean up old IDs
		if (oldid > 0 && Dictionary.ContainsKey(oldid) && oldid != newid)
			Dictionary.Remove(oldid);

		return newid;
	}

	/// <summary>
	/// Unassigns an ID from a referrable, removing it from the repository.
	/// </summary>
	/// <param name="id"></param>
	public void UnassignID(long id)
	{
		if (ContainsKey(id))
		{
			var r = this[id];
			r.ID = -1;
			Dictionary.Remove(id);
		}
	}

	/// <summary>
	/// Unassigns an ID from a referrable, removing it from the repository.
	/// </summary>
	/// <param name="r"></param>
	public void UnassignID(IReferrable r)
	{
		if (r == null || r.ID < 0)
			return; // nothing to do
		if (ContainsKey(r.ID))
		{
			if (this[r.ID] == r)
				Dictionary.Remove(r.ID);
			else
			{
				var repoThinksTheIDIs = this.SingleOrDefault(kvp => kvp.Value == r).Key;
				Dictionary.Remove(repoThinksTheIDIs);
			}
		}
		else if (Values.Contains(r))
		{
			try
			{
				Dictionary.Remove(this.Single(kvp => kvp.Value == r));
			}
			catch (InvalidOperationException ex)
			{
				// HACK - why is the item not being found? sequence contains no matching element? it's right there!
				Console.Error.WriteLine(ex);
			}
		}
		//r.ID = -1;
	}

	/// <summary>
	/// Finds referrable objects in the repository.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="condition"></param>
	/// <returns></returns>
	public IEnumerable<T> Find<T>(Func<T, bool>? condition = null) where T : IReferrable
	{
		if (condition == null)
			condition = t => true;
		return this.OfType<T>().Where(condition);
	}

	public IReferrable? GetReferrable(long key)
	{
		if (!ContainsKey(key))
			return null;
		return this[key];
	}

	/// <summary>
	/// Finds the real version of a fake referrable.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="fakeobj">The fake referrable.</param>
	/// <returns></returns>
	public T? GetReferrable<T>(T fakeobj)
		where T : IReferrable
	{
		return (T)GetReferrable(fakeobj.ID);
	}

	public bool ContainsKey(long key)
	{
		return Dictionary.ContainsKey(key);
	}

	public IEnumerator<KeyValuePair<long, IReferrable>> GetEnumerator()
	{
		return Dictionary.GetEnumerator();
	}

	public bool TryGetValue(long key, [MaybeNullWhen(false)] out IReferrable value)
	{
		return Dictionary.TryGetValue(key, out value);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
