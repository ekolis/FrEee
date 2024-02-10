using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.GameState;

namespace FrEee.WinForms.Objects.GalaxyViewModes;

/// <summary>
/// Displays the relative concentrations of friendly, allied, neutral, and enemy colonies (by max breathable facility slots) using pie charts.
/// </summary>
public class ColoniesMode : PieMode
{
	public override string Name
	{
		get { return "Colonies"; }
	}

	protected override int GetAlpha(StarSystem sys)
	{
		var all = Galaxy.Current.StarSystemLocations.Select(l => new { System = l.Item, Planets = l.Item.FindSpaceObjects<Planet>().Where(p => p.Owner != null || Empire.Current.CanColonize(p)) });
		var maxSlots = all.Max(x => GetSlots(x.Planets));
		var planets = sys.FindSpaceObjects<Planet>().Owned().ToArray();
		var slotsHere = GetSlots(planets);
		return 255 * slotsHere / maxSlots;
	}

	protected override IEnumerable<Tuple<Color, float>> GetAmounts(StarSystem sys)
	{
		// find relative max breathable facility slots of friendly, allied, neutral, and and enemy colonies
		var planets = sys.FindSpaceObjects<Planet>().ToArray();
		var ownedPlanets = planets.Owned();
		var unownedPlanets = planets.Unowned();
		var byOwner = ownedPlanets.GroupBy(v => v.Owner);
		var friendly = GetSlots(byOwner.Where(f => f.Key == Empire.Current));
		var ally = GetSlots(byOwner.Where(f => f.Key.IsAllyOf(Empire.Current, sys)));
		var neutral = GetSlots(byOwner.Where(f => f.Key.IsNeutralTo(Empire.Current, sys)));
		var enemy = GetSlots(byOwner.Where(f => f.Key.IsEnemyOf(Empire.Current, sys)));
		var colonizablePlanets = unownedPlanets.Where(p => Empire.Current.CanColonize(p));
		//var uncolonizablePlanets = unownedPlanets.Except(colonizablePlanets);

		yield return Tuple.Create(Color.Blue, (float)friendly);
		yield return Tuple.Create(Color.Green, (float)ally);
		yield return Tuple.Create(Color.Yellow, (float)neutral);
		yield return Tuple.Create(Color.Red, (float)enemy);
		yield return Tuple.Create(Color.Gray, (float)GetSlots(colonizablePlanets));
		//yield return Tuple.Create(Color.DarkGray, (float)GetSlots(uncolonizablePlanets));
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