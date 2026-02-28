using FrEee.Utility;

namespace FrEee.Objects.GameState;

public class GameReferenceKeyedDictionary<TKey, TValue> : ReferenceKeyedDictionary<long, TKey, TValue>
	where TKey : IReferrable
{
	protected override long ExtractID(TKey key)
	{
		return key.ID;
	}

	protected override TKey LookUp(long id)
	{
		return Game.Current.Find<TKey>(id);
	}
}