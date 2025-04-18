using System;

namespace FrEee.Utility;

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
