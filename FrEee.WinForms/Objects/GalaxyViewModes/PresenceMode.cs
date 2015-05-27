using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays the presence of empires in star systems using pies with equal slices of each present empire's color, similar to SE3.
	/// </summary>
	public class PresenceMode : IGalaxyViewMode
	{
		public void Draw(StarSystem sys, Graphics g, PointF pos, float size)
		{
			// draw circle for star system
			// do SE3-style split circles for contested systems because they are AWESOME!
			var owners = sys.FindSpaceObjects<ISpaceObject>().Select(x => x.Owner).Distinct().Where(o => o != null);
			if (owners.Count() == 0)
				g.FillEllipse(Brushes.Gray, pos, size);
			else
			{
				var arcSize = 360f / owners.Count();
				int i = 0;
				foreach (var owner in owners)
				{
					g.FillPie(new SolidBrush(owner.Color), pos, size, i * arcSize, arcSize);
					i++;
				}
			}
		}

		public string Name
		{
			get { return "Presence"; }
		}
	}
}
