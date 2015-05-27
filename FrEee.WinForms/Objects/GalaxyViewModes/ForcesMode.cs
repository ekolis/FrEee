using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays the relative concentrations of friendly, allied, neutral, and enemy forces (by tonnage) using pie charts.
	/// </summary>
	public class ForcesMode : IGalaxyViewMode
	{
		public void Draw(StarSystem sys, Graphics g, PointF pos, float size)
		{
			// find relative tonnage here vs. max tonnage in any system to determine brightness of colors
			var forces = Galaxy.Current.StarSystemLocations.Select(l => new { System = l.Item, Vehicles = l.Item.FindSpaceObjects<SpaceVehicle>()});
			var maxTonnage = forces.Max(f => f.Vehicles.Sum(v => v.Design.Hull.Size));
			var vehicles = sys.FindSpaceObjects<SpaceVehicle>().ToArray();
			var tonnageHere = vehicles.Sum(v => v.Design.Hull.Size);
			var brightness = 255 * tonnageHere / maxTonnage;

			if (tonnageHere == 0)
			{
				// nobody's here, draw a gray outline
				g.DrawEllipse(Pens.Gray, pos, size);
			}
			else
			{
				// find relative tonnage of friendly, allied, and enemy forces
				var byOwner = vehicles.GroupBy(v => v.Owner);
				var friendlyTonnage = byOwner.Where(f => f.Key == Empire.Current).SelectMany(f => f).Sum(v => v.Design.Hull.Size);
				var allyTonnage = byOwner.Where(f => f.Key.IsAllyOf(Empire.Current, sys)).SelectMany(f => f).Sum(v => v.Design.Hull.Size);
				var neutralTonnage = byOwner.Where(f => f.Key.IsNeutralTo(Empire.Current, sys)).SelectMany(f => f).Sum(v => v.Design.Hull.Size);
				var enemyTonnage = byOwner.Where(f => f.Key.IsEnemyOf(Empire.Current, sys)).SelectMany(f => f).Sum(v => v.Design.Hull.Size);

				// find pie dimensions
				var friendlyStart = 0f;
				var friendlyArc = 360f * friendlyTonnage / tonnageHere;
				var allyStart = friendlyArc;
				var allyArc = 360f * allyTonnage / tonnageHere;
				var neutralStart = friendlyArc + allyArc;
				var neutralArc = 360f * neutralTonnage / tonnageHere;
				var enemyStart = friendlyArc + allyArc + neutralArc;
				var enemyArc = 360f * enemyTonnage / tonnageHere;

				// draw & fill pie chart
				g.FillPie(new SolidBrush(Color.FromArgb(brightness, Color.Blue)), pos, size, friendlyStart, friendlyArc);
				g.DrawPie(Pens.Blue, pos, size, friendlyStart, friendlyArc);
				g.FillPie(new SolidBrush(Color.FromArgb(brightness, Color.Green)), pos, size, allyStart, allyArc);
				g.DrawPie(Pens.Green, pos, size, allyStart, allyArc);
				g.FillPie(new SolidBrush(Color.FromArgb(brightness, Color.Yellow)), pos, size, neutralStart, neutralArc);
				g.DrawPie(Pens.Yellow, pos, size, neutralStart, neutralArc);
				g.FillPie(new SolidBrush(Color.FromArgb(brightness, Color.Red)), pos, size, enemyStart, enemyArc);
				g.DrawPie(Pens.Red, pos, size, enemyStart, enemyArc);
			}
		}

		public string Name
		{
			get { return "Forces"; }
		}
	}
}
