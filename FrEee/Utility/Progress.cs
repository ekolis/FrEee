using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	public class Progress
	{
		public Progress(int value, int maximum, int incrementalProgress = 0)
		{
			Value = value;
			Maximum = maximum;
			IncrementalProgress = incrementalProgress;
		}

		public int Value { get; set; }
		public int Maximum { get; set; }
		public int IncrementalProgress { get; set; }
	}
}
