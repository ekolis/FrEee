using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Utility
{
	/// <summary>
	/// Progress towards completing something.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Progress<TRef, T> : Progress
		where TRef : IReference<T>
	{
		public Progress(T item, long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = 0, long extraIncrementalProgressAfterDelay = 0)
			: base(value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
		{
			Item = item;
		}

		/// <summary>
		/// The item being worked towards.
		/// </summary>
		public T Item { get { return item.Value; } set { item = value.Refer<TRef, T>(); } }

		private TRef item { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Item, base.ToString());
		}
	}
}
