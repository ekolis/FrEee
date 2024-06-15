using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Utility;
using Microsoft.AspNetCore.Components;
using FrEee.Extensions;
using FrEee.Utility;

namespace FrEee.UI.Blazor.Views
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
	}
}
