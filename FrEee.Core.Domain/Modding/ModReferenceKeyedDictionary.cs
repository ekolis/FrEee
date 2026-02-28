using FrEee.Utility;

namespace FrEee.Modding;

public class ModReferenceKeyedDictionary<TKey, TValue> : ReferenceKeyedDictionary<string, TKey, TValue>
	where TKey : IModObject
{
	protected override string ExtractID(TKey key)
	{
		return key.ModID;
	}

	protected override TKey LookUp(string id)
	{
		return Mod.Current.Find<TKey>(id);
	}
}