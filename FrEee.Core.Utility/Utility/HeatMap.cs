using FrEee.Utility;
using FrEee.Utility;
namespace FrEee.Utility;

/// <summary>
/// A heatmap, used for pathfinding and the like.
/// Maps points on a plane to heat values with positive values being hotter.
/// </summary>
public class HeatMap : SafeDictionary<Vector2<int>, double>
{
	public double this[int x, int y]
	{
		get => this[new Vector2<int>(x, y)];
		set => this[new Vector2<int>(x, y)] = value;
	}

	public void AddLinearGradientEightWay(Vector2<int> pos, double startValue, int range, double deltaPerDistance)
	{
		for (var x = pos.X - range; x <= pos.X + range; x++)
		{
			for (var y = pos.Y - range; y <= pos.Y + range; y++)
			{
				var pos2 = new Vector2<int>(x, y);
				var dist = (pos2 - pos).LengthEightWay;
				var val = startValue + deltaPerDistance * dist;
				this[pos2] += val;
			}
		}
	}

	public Vector2<int> FindMax(Vector2<int> pos, int range)
	{
		Vector2<int>? result = null;
		for (var x = pos.X - range; x <= pos.X + range; x++)
		{
			for (var y = pos.Y - range; y <= pos.Y + range; y++)
			{
				if (this.ContainsKey(new Vector2<int>(x, y)) && (result == null || this[x, y] > this[result]))
					result = new Vector2<int>(x, y);
			}
		}
		return result ?? pos;
	}

	public Vector2<int> FindMin(Vector2<int> pos, int range)
	{
		Vector2<int>? result = null;
		for (var x = pos.X - range; x <= pos.X + range; x++)
		{
			for (var y = pos.Y - range; y <= pos.Y + range; y++)
			{
				if (this.ContainsKey(new Vector2<int>(x, y))  && (result == null || this[x, y] < this[result]))
					result = new Vector2<int>(x, y);
			}
		}
		return result ?? pos;
	}
}
