using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Serialization;

namespace FrEee.Utility
{
	public class GalaxyProgress<T> : Progress<GalaxyReference<T>, T>
	where T : IReferrable
	{
		public GalaxyProgress(T item, long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = 0, long extraIncrementalProgressAfterDelay = 0)
			: base(item, value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
		{
		}
	}
}
