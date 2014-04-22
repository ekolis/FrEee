using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;
using Newtonsoft.Json;

namespace FrEee.Utility
{
	/// <summary>
	/// Dictionary which is keyed by named objects.
	/// Values can be referred to by key or by name.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[JsonArray]
	public class NamedDictionary<TKey, TValue> : SafeDictionary<TKey, TValue>
		where TKey : INamed
	{
		public TValue this[string name]
		{
			get
			{
				return this[Keys.FindByName(name)];
			}
			set
			{
				this[Keys.FindByName(name)] = value;
			}
		}
	}
}
