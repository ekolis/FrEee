using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Serialization;

namespace FrEee.Objects.GameState
{
    [Serializable]
	public class GalaxyReferenceSet<T>
		: ReferenceSet<GalaxyReference<T>, T>
		where T : IReferrable
	{
		public GalaxyReferenceSet()
			: base()
		{
		}

		public GalaxyReferenceSet(IEnumerable<T> objs)
			: base(objs)
		{
		}
	}
}
