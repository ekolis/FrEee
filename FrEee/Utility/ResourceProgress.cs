using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility
{
	/// <summary>
	/// Progress towards completing something.
	/// </summary>
	public class ResourceProgress : IEnumerable<KeyValuePair<Resource, Progress>>
	{
		public ResourceProgress(ResourceQuantity value, ResourceQuantity maximum, ResourceQuantity incrementalProgress)
		{
			Value = value;
			Maximum = maximum;
			IncrementalProgress = incrementalProgress;
		}

		public ResourceQuantity Value { get; set; }
		public ResourceQuantity Maximum { get; set; }
		public ResourceQuantity IncrementalProgress { get; set; }

		public override string ToString()
		{
			return string.Format("{0} / {1} ({2:+#;-#;0} per turn", Value, Maximum, IncrementalProgress);
		}

		public IEnumerator<KeyValuePair<Resource, Progress>> GetEnumerator()
		{
			foreach (var r in Value.Keys.Union(Maximum.Keys).Union(IncrementalProgress.Keys))
				yield return new KeyValuePair<Resource, Progress>(r, new Progress(Value[r], Maximum[r], IncrementalProgress[r]));
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
