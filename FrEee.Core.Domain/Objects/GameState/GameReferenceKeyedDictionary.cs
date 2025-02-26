﻿using FrEee.Utility;

namespace FrEee.Objects.GameState;

public class GameReferenceKeyedDictionary<TKey, TValue> : ReferenceKeyedDictionary<long, GameReference<TKey>, TKey, TValue>
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
			dict[id] = (TKey)Game.Current.GetReferrable(id);
		return dict[id];
	}
}