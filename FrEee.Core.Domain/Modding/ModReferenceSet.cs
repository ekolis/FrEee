using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Serialization;
using FrEee.Objects.GameState;

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
