using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using FrEee.Extensions;

namespace FrEee.UI.Blazor.Components
{
	public class PieChartViewModel<T> : INotifyPropertyChanged
		where T : INumber<T>, IMultiplyOperators<T, int, T>, IDivisionOperators<T, int, T>
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// The pie chart data entries.
		/// </summary>
		public IEnumerable<Entry> Entries { get; set; } = new HashSet<Entry>();

		public IEnumerable<(Entry Entry, T PreviousSum)> SortedEntries
		{
			get
			{
				var sum = T.Zero;
				foreach (var entry in Entries.OrderByDescending(q => q.Value))
				{
					yield return (entry, sum);
					sum += entry.Value;
				}
			}
		}

		public T Sum => Entries.Sum(q => q.Value);

		public string GradientString
		{
			get
			{
				var result = "";
				foreach (var x in SortedEntries)
				{
					var degrees = x.PreviousSum * 360 / Sum;
					var newDegrees = (x.PreviousSum + x.Entry.Value) * 360 / Sum;
					result += $"#{x.Entry.Color.ToRgba()} {degrees}deg {newDegrees}deg,";
				}
				if (result.EndsWith(","))
				{
					result = result.Substring(0, result.Length - 1);
				}
				return result;
			}
		}

		/// <summary>
		/// Action to take when the pie chart is clicked.
		/// </summary>
		public Action OnClick { get; set; } = () => { };

		/// <summary>
		/// An entry in the pie chart data.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Label"></param>
		/// <param name="Color"></param>
		/// <param name="Value"></param>
		public record Entry(string Text, Color Color, T Value);
	}
}
