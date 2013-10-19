using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Utility
{
	/// <summary>
	/// Dictionary which is keyed by named objects.
	/// Values can be referred to by key or by name.
	/// Referring to nonexistent names can throw an exception when setting values, however.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class NamedDictionary<TKey, TValue> : SafeDictionary<TKey, TValue>
		where TKey : INamed
	{
		public TValue this[string name]
		{
			get
			{
				var key = Keys.FindByName(name);
				return this[key];
			}
			set
			{
				var key = Keys.FindByName(name);
				if (key == null)
					throw new Exception("Cannot find key " + name + ".");
				this[key] = value;
			}
		}
	}
}
