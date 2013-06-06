using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	public struct Progress
	{
		public Progress(int value, int maximum, int incrementalProgress = 0)
			: this()
		{
			Value = value;
			Maximum = maximum;
			IncrementalProgress = incrementalProgress;
		}

		public int Value { get; set; }
		public int Maximum { get; set; }
		public int IncrementalProgress { get; set; }

		/// <summary>
		/// Estimated time to completion (null for never).
		/// </summary>
		public int? Eta
		{
			get
			{
				if (IncrementalProgress <= 0)
					return null;
				return (Maximum - Value) / IncrementalProgress;
			}
		}
	}
}
