using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Modding;

public class ModReferenceKeyedDictionary<TKey, TValue> : ReferenceKeyedDictionary<string, ModReference<TKey>, TKey, TValue>
		where TKey : IModObject
{
	private SafeDictionary<string, TKey> dict = new();

	protected override string ExtractID(TKey key)
	{
		return key.ModID;
	}

	protected override TKey LookUp(string id)
	{
		if (!dict.ContainsKey(id))
			dict[id] = Mod.Current.Find<TKey>(id);
		return dict[id];
	}
}