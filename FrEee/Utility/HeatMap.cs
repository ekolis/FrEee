namespace FrEee.Utility;

/// <summary>
/// A heatmap, used for pathfinding and the like.
/// Maps points on a plane to heat values with positive values being hotter.
/// </summary>
public class HeatMap : SafeDictionary<IntVector2, double>
{
	public double this[int x, int y]
	{
		get => this[new IntVector2(x, y)];
		set => this[new IntVector2(x, y)] = value;
	}

	public void AddLinearGradientEightWay(IntVector2 pos, double startValue, int range, double deltaPerDistance)
	{
		for (var x = pos.X - range; x <= pos.X + range; x++)
		{
			for (var y = pos.Y - range; y <= pos.Y + range; y++)
			{
				var pos2 = new IntVector2(x, y);
				var dist = (pos2 - pos).LengthEightWay;
				var val = startValue + deltaPerDistance * dist;
				this[pos2] += val;
			}
		}
	}

	public IntVector2 FindMax(IntVector2 pos, int range)
	{
		IntVector2 result = null;
		for (var x = pos.X - range; x <= pos.X + range; x++)
		{
			for (var y = pos.Y - range; y <= pos.Y + range; y++)
			{
				if (this.ContainsKey(new IntVector2(x, y)) && (result == null || this[x, y] > this[result]))
					result = new IntVector2(x, y);
			}
		}
		return result ?? pos;
	}

	public IntVector2 FindMin(IntVector2 pos, int range)
	{
		IntVector2 result = null;
		for (var x = pos.X - range; x <= pos.X + range; x++)
		{
			for (var y = pos.Y - range; y <= pos.Y + range; y++)
			{
				if (this.ContainsKey(new IntVector2(x, y))  && (result == null || this[x, y] < this[result]))
					result = new IntVector2(x, y);
			}
		}
		return result ?? pos;
	}
}