using System.ComponentModel;
using System.Drawing;
using FrEee.Extensions;
using FrEee.Utility;

namespace FrEee.UI.Blazor.Components
{
	public class ProgressBarViewModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		public long Maximum { get; set; } = 0;

        public long Value { get; set; } = 0;

        public long Increment { get; set; } = 0;

        public Color BarColor { get; set; } = Color.Blue;

        public string LeftText { get; set; } = "";

		public string CenterText
		{
			get
			{
				switch (ProgressDisplayType)
				{
					case ProgressDisplayType.None:
						return "";
					case ProgressDisplayType.Percentage:
						return Math.Round(((double)Value / (double)Maximum * 100)) + "%";
					case ProgressDisplayType.Numeric:
						return Value.ToUnitString(true) + " / " + Maximum.ToUnitString(true);
					case ProgressDisplayType.Both:
						return Math.Round(((double)Value / (double)Maximum * 100)) + "% (" + Value.ToUnitString(true) + " / " + Maximum.ToUnitString(true) + ")";
					default:
						return "";
				}
			}
		}

		public string RightText { get; set; } = "";

        public Action OnClick { get; set; } = () => { };

        public Color IncrementColor1 => Color.FromArgb(BarColor.A / 2, BarColor);

        public Color IncrementColor2 => Color.FromArgb(BarColor.A / 4, BarColor);

        public Color IncrementColor3 => Color.FromArgb(BarColor.A / 8, BarColor);

		public Progress Progress
		{
			get
			{
				return new Progress(Value, Maximum, Increment);
			}
			set
			{
				Value = value.Value;
				Maximum = value.Maximum;
				Increment = value.IncrementalProgressBeforeDelay;
			}
		}

		public ProgressDisplayType ProgressDisplayType { get; set; } = ProgressDisplayType.Percentage;

		/// <summary>
		/// Calculates the per-turn progress percentage to show on the bar after a certain number of turns.
		/// </summary>
		/// <param name="turnsAhead">The number of turns to look ahead.</param>
		/// <returns>
		/// The progress percentage achieved on the specified turn (not incremental).
		/// Capped so that the total progress never exceeds 100%.
		/// </returns>
		public double GetProgressPercentage(int turnsAhead)
		{
			// calculate starting point for current turn
			var currentValue = Progress.Value;
			var totalValue = currentValue;

			// calculate progress for future turns
			for (var turn = 1; turn <= turnsAhead; turn++)
			{
				// apply incremental progress
				currentValue = Increment;
				totalValue += currentValue;

				// cap at maximum
				if (totalValue > Maximum)
				{
					currentValue -= totalValue - Maximum;
					totalValue = Maximum;
				}
			}

			// calculate and return percentage
			var max = Maximum > 0 ? Maximum : 1; // avoid divide by zero
			var currentPercentage = currentValue * 100d / max; // allow fractional percentages to prevent rounding error
			return currentPercentage;
		}
	}
}
