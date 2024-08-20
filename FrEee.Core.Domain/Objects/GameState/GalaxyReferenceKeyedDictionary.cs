using FrEee.Utility;

namespace FrEee.Objects.GameState;

public class GalaxyReferenceKeyedDictionary<TKey, TValue> : ReferenceKeyedDictionary<long, GalaxyReference<TKey>, TKey, TValue>
		where TKey : IReferrable
{
	private SafeDictionary<long, TKey> dict = new SafeDictionary<long, TKey>();

	protected override long ExtractID(TKey key)
	{
		return key.ID;
	}

	protected override TKey LookUp(long id)
	{
		if (!dict.ContainsKey(id))
			dict[id] = (TKey)Galaxy.Current.GetReferrable(id);
		return dict[id];
	}
}