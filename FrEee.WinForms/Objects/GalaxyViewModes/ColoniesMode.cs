using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Objects.GalaxyViewModes
{
	/// <summary>
	/// Displays the relative concentrations of friendly, allied, neutral, and enemy colonies (by max breathable facility slots) using pie charts.
	/// </summary>
	public class ColoniesMode : PieMode
	{
		public override string Name
		{
			get { return "Colonies"; }
		}

		protected override IEnumerable<Tuple<Color, float>> GetAmounts(StarSystem sys)
		{
			// find relative max breathable facility slots of friendly, allied, neutral, and and enemy colonies
			var planets = sys.FindSpaceObjects<Planet>().Owned().ToArray();
			var byOwner = planets.GroupBy(v => v.Owner);
			var friendly = GetSlots(byOwner.Where(f => f.Key == Empire.Current));
			var ally = GetSlots(byOwner.Where(f => f.Key.IsAllyOf(Empire.Current, sys)));
			var neutral = GetSlots(byOwner.Where(f => f.Key.IsNeutralTo(Empire.Current, sys)));
			var enemy = GetSlots(byOwner.Where(f => f.Key.IsEnemyOf(Empire.Current, sys)));

			yield return Tuple.Create(Color.Blue, (float)friendly);
			yield return Tuple.Create(Color.Green, (float)ally);
			yield return Tuple.Create(Color.Yellow, (float)neutral);
			yield return Tuple.Create(Color.Red, (float)enemy);
		}

		protected override int GetAlpha(StarSystem sys)
		{
			var all = Galaxy.Current.StarSystemLocations.Select(l => new { System = l.Item, Planets = l.Item.FindSpaceObjects<Planet>().Owned() });
			var maxSlots = all.Max(x => GetSlots(x.Planets));
			var planets = sys.FindSpaceObjects<Planet>().Owned().ToArray();
			var slotsHere = GetSlots(planets);
			return 255 * slotsHere / maxSlots;
		}

		private int GetSlots(IEnumerable<Planet> ps)
		{
			return ps.Sum(p => p.Size.MaxFacilities);
		}

		private int GetSlots(IEnumerable<IEnumerable<Planet>> gs)
		{
			return gs.Sum(g => GetSlots(g));
		}
	}
}
