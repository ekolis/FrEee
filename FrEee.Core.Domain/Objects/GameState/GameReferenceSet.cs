using System;
using System.Collections.Generic;
using FrEee.Utility;

namespace FrEee.Objects.GameState
{
	[Serializable]
	public class GameReferenceSet<T>
		: ReferenceSet<GameReference<T>, T>
		where T : IReferrable
	{
		public GameReferenceSet()
			: base()
		{
		}

		public GameReferenceSet(IEnumerable<T> objs)
			: base(objs)
		{
		}
	}
}
