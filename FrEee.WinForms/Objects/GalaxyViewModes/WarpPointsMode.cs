using FrEee.Objects.Space;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.WinForms.Objects.GalaxyViewModes;

/// <summary>
/// Displays the relative prevalence of explored and unexplored warp points (explored = blue, unexplored = red).
/// </summary>
public class WarpPointsMode : PieMode
{
	public override string Name
	{
		get { return "Warp Points"; }
	}

	protected override int GetAlpha(StarSystem sys)
	{
		return 255;
	}

	protected override IEnumerable<Tuple<Color, float>> GetAmounts(StarSystem sys)
	{
		var wps = GetWarpPoints(sys);
		if (!wps.Any())
			yield return Tuple.Create(Color.Gray, 1f);
		else
		{
			yield return Tuple.Create(Color.Blue, (float)GetExplored(wps));
			yield return Tuple.Create(Color.Red, (float)GetUnexplored(wps));
		}
	}

	private int GetExplored(IEnumerable<WarpPoint> wps)
	{
		return wps.Where(w => w.Target != null).Count();
	}

	private int GetUnexplored(IEnumerable<WarpPoint> wps)
	{
		return wps.Where(w => w.Target == null).Count();
	}

	private IEnumerable<WarpPoint> GetWarpPoints(StarSystem s)
	{
		return s.SpaceObjects.OfType<WarpPoint>();
	}
}