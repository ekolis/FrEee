using FrEee.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Extensions;
using System;

namespace FrEee.Utility
{
	public class GalaxyProgress<T> : Progress<GalaxyReference<T>, T>
		where T: IReferrable
	{
		public GalaxyProgress(T item, long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = 0, long extraIncrementalProgressAfterDelay = 0)
			: base(item, value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
		{
		}
	}

	public class ModProgress<T> : Progress<ModReference<T>, T>
			where T : IModObject
	{
		public ModProgress(T item, long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = 0, long extraIncrementalProgressAfterDelay = 0)
			: base(item, value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
		{
		}
	}

	/// <summary>
	/// Progress towards completing something.
	/// </summary>
	public class Progress
	{
		public Progress(long value, long maximum, long incrementalProgressBeforeDelay = 0, double? delay = null, long extraIncrementalProgressAfterDelay = 0)
		{
			Value = value;
			Maximum = maximum;
			IncrementalProgressBeforeDelay = incrementalProgressBeforeDelay;
			Delay = delay;
			ExtraIncrementalProgressAfterDelay = extraIncrementalProgressAfterDelay;
		}

		public double? Delay { get; set; }

		/// <summary>
		/// ETA rounded up to the next turn.
		/// </summary>
		public int? Eta
		{
			get
			{
				if (RawEta == null)
					return null;
				return (int)Math.Ceiling(RawEta.Value);
			}
		}

		public long ExtraIncrementalProgressAfterDelay { get; set; }
		public long IncrementalProgressBeforeDelay { get; set; }
		public long Maximum { get; set; }

		/// <summary>
		/// Estimated time to completion (null for never).
		/// </summary>
		public double? RawEta
		{
			get
			{
				if (Delay == double.PositiveInfinity)
					return null; // When the delay is infinite, like the research queue having 0 research points, it never finishes
				else if (IncrementalProgressBeforeDelay <= 0 && ExtraIncrementalProgressAfterDelay <= 0)
					return null;
				else if (Delay == null)
				{
					if (IncrementalProgressBeforeDelay <= 0)
						return null;
					else
						return (double)(Maximum - Value) / (double)(IncrementalProgressBeforeDelay);
				}
				else if (Delay <= 0)
					return (double)(Maximum - Value) / (double)(IncrementalProgressBeforeDelay + ExtraIncrementalProgressAfterDelay);
				else
				{
					var progressAfterDelay = new Progress(0, Maximum, IncrementalProgressBeforeDelay + ExtraIncrementalProgressAfterDelay);
					return progressAfterDelay.RawEta + Delay;
				}
			}
		}

		public long Value { get; set; }

		public override string ToString()
		{
			if (ExtraIncrementalProgressAfterDelay != 0)
				return string.Format("{0} / {1} ({2:+#;-#;0} per turn, {3:+#;-#;0} more per turn after {4} turns)", Value, Maximum, IncrementalProgressBeforeDelay, ExtraIncrementalProgressAfterDelay, Delay);
			else
				return string.Format("{0} / {1} ({2:+#;-#;0} per turn", Value, Maximum, IncrementalProgressBeforeDelay);
		}
	}

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