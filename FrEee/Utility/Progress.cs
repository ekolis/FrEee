using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Progress towards completing something.
	/// </summary>
	public class Progress
	{
		public Progress(int value, int maximum, int incrementalProgressBeforeDelay = 0, double? delay = null, int extraIncrementalProgressAfterDelay = 0)
		{
			Value = value;
			Maximum = maximum;
			IncrementalProgressBeforeDelay = incrementalProgressBeforeDelay;
			Delay = delay;
			ExtraIncrementalProgressAfterDelay = extraIncrementalProgressAfterDelay;
		}

		public int Value { get; set; }
		public int Maximum { get; set; }
		public int IncrementalProgressBeforeDelay { get; set; }
		public double? Delay { get; set; }
		public int ExtraIncrementalProgressAfterDelay { get; set; }

		/// <summary>
		/// Estimated time to completion (null for never).
		/// </summary>
		public double? RawEta
		{
			get
			{
				if (IncrementalProgressBeforeDelay <= 0 && ExtraIncrementalProgressAfterDelay <= 0)
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
					var progressAfterDelay = new Progress(Value, Maximum, ExtraIncrementalProgressAfterDelay);
					return progressAfterDelay.RawEta + Delay;
				}
			}
		}

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
	}

	/// <summary>
	/// Progress towards completing something.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Progress<T> : Progress
	{
		public Progress(T item, int value, int maximum, int incrementalProgressBeforeDelay = 0, double? delay = 0, int extraIncrementalProgressAfterDelay = 0)
			: base(value, maximum, incrementalProgressBeforeDelay, delay, extraIncrementalProgressAfterDelay)
		{
			Item = item;
		}

		/// <summary>
		/// The item being worked towards.
		/// </summary>
		public T Item { get; set; }
	}
}
