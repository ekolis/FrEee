using System.Drawing;

namespace FrEee.UI.Blazor.Components;

public class GraphSeries
{
	public Color Color { get; set; } = Color.White;
	public string Name { get; set; } = string.Empty;
	public IEnumerable<double> DataPoints { get; set; } = Enumerable.Empty<double>();
}

public class LineGraphViewModel : ViewModelBase
{
	public string Title { get; set; } = string.Empty;
	public ICollection<GraphSeries> Series { get; set; } = new List<GraphSeries>();
	public bool RoundMaxToMultipleOfPowerOfTen { get; set; } = false;
	public int Width { get; set; } = 400;
	public int Height { get; set; } = 200;
}
