using FrEee.Objects.Space;
using FrEee.UI.WinForms.Utility.Extensions;
using System.Drawing;
using System.Linq;

namespace FrEee.UI.WinForms.Objects.GalaxyViewModes;

/// <summary>
/// A galaxy view mode that uses ARGB values to indicate up to four data values at once.
/// </summary>
public abstract class ArgbMode : IGalaxyViewMode
{
	public abstract string Name { get; }

	public void Draw(StarSystem sys, Graphics g, PointF pos, float size)
	{
		// draw circle
		var color = GetColor(sys);
		g.FillEllipse(new SolidBrush(color), pos, size);
		g.DrawEllipse(new Pen(Saturate(color)), pos, size);
	}

	/// <summary>
	/// Color for any given star system, excluding alpha.
	/// </summary>
	protected abstract Color GetColor(StarSystem sys);

	protected int Weight(float amount, float max)
	{
		return (int)(255 * amount / max);
	}

	private Color Saturate(Color c)
	{
		if (c.R == 0 && c.G == 0 && c.B == 0)
			return Color.White;

		var max = new int[] { c.R, c.G, c.B }.Max();
		return Color.FromArgb(255 * c.R / max, 255 * c.G / max, 255 * c.B / max);
	}
}