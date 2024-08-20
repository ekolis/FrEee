using System;
using System.Collections.Generic;
using FrEee.Utility;

namespace FrEee.Modding
{
	[Serializable]
	public class ModReferenceSet<T>
		: ReferenceSet<ModReference<T>, T>
		where T : IModObject
	{
		public ModReferenceSet()
			: base()
		{
		}

		public ModReferenceSet(IEnumerable<T> objs)
			: base(objs)
		{
		}
	}
}
