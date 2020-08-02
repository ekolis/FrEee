using System;
using System.Collections.Generic;
using System.Linq;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;

using static System.Math;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	/// <summary>
	/// A battle which takes place... IIIN... SPAAACE!!!
	/// </summary>
	/// <seealso cref="Battle" />
	public class SpaceBattle : Battle
	{
		public SpaceBattle(Sector location)
			: base()
		{
			Sector = location ?? throw new Exception("Space battles require a sector location.");

			// TODO - what about cloaked ships? should they not participate in combat?
			Empires = Sector.SpaceObjects.OfType<ICombatSpaceObject>().Select(sobj => sobj.Owner).Where(emp => emp != null).Distinct().ToArray();
			Combatants = new HashSet<ICombatant>(Sector.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(Sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)).Where(o => !(o is Fleet)));

			Initialize(new HashSet<ICombatant>(Sector.SpaceObjects.OfType<ICombatant>().Where(o => o.Owner != null).Union(Sector.SpaceObjects.OfType<Fleet>().SelectMany(f => f.Combatants)).Where(o => !(o is Fleet))));
		}

		public override int DamagePercentage => 100;

		public override void Initialize(IEnumerable<ICombatant> combatants)
		{
			base.Initialize(combatants);

			Empires = Sector?.SpaceObjects.OfType<ICombatSpaceObject>().Select(sobj => sobj.Owner).Where(emp => emp != null).Distinct().ToArray();

			int moduloID = (int)(Sector?.StarSystem.ID ?? 0 % 100000);
			Dice = new PRNG((int)(moduloID / Galaxy.Current.Timestamp * 10));
		}

		public override void PlaceCombatants(SafeDictionary<ICombatant, IntVector2> locations)
		{
			if (Sector?.SpaceObjects.OfType<WarpPoint>().Any() ?? false)
			{
				// HACK - warp point in sector, assume someone warped
				// TODO - do this for warp point exits instead since warp points may be one way
				// warp battles start with everyone mashed together to allow blockades
				foreach (var c in Combatants.OrderByDescending(q => q.Size))
					PlaceCombatant(locations, 0, 0, c);
			}
			else
			{
				// place all combatants at the points of a regular polygon
				var sideLength = 20 + (int)Math.Ceiling((double)Combatants.GroupBy(q => q.Owner).Max(q => q.Count())); // make sure no one can shoot each other at the start
																													   // https://stackoverflow.com/questions/32169875/calculating-the-coordinates-of-a-regular-polygon-given-its-center-and-its-side-l
				var radius = sideLength / (2 * Sin(PI / Empires.Count()));
				var combs = Combatants.ToArray();
				for (int i = 0; i < Empires.Count(); i++)
				{
					var x = radius * Cos(PI / Empires.Count() * (1 + 2 * i));
					var y = radius * Sin(PI / Empires.Count() * (1 + 2 * i));
					foreach (var comb in Combatants.Where(q => q.Owner == Empires.ElementAt(i)).OrderByDescending(q => q.Size))
					{
						PlaceCombatant(locations, x, y, comb);
					}
				}
			}
		}

		private void PlaceCombatant(SafeDictionary<ICombatant, IntVector2> locations, double x, double y, ICombatant comb)
		{
			// scramble all tile-filling combatants in rings around the largest
			if (comb.FillsCombatTile)
			{
				for (int r = 0; ; r++)
				{
					bool done = false;
					var tiles = IntVector2.AtRadius(r);
					foreach (var tile in tiles.Shuffle(Dice))
					{
						var atHere = locations.Where(q => q.Key.FillsCombatTile && q.Value == tile);
						if (!atHere.Any())
						{
							locations.Add(comb, new IntVector2((int)x + tile.X, (int)y + tile.Y));
							done = true;
							break;
						}
					}
					if (done)
						break;
				}
			}
			else // put non-filling combatants in the center
				locations.Add(comb, new IntVector2((int)x, (int)y));
		}

		/// <summary>
		/// Battles are named after any stellar objects in their sector; failing that, they are named after the star system and sector coordinates.
		/// </summary>
		public override string Name
		{
			get
			{
				if (Sector?.SpaceObjects.OfType<StellarObject>().Any() ?? false)
					return "Battle at " + Sector.SpaceObjects.OfType<StellarObject>().Largest();
				var coords = Sector?.Coordinates ?? new System.Drawing.Point(0, 0);
				return "Battle at " + Sector?.StarSystem ?? string.Empty + " sector (" + coords.X + ", " + coords.Y + ")";
			}
		}

		public override int MaxRounds => Mod.Current.Settings.SpaceCombatTurns;

		public override void ModifyHappiness()
		{
			foreach (var e in Empires ?? Enumerable.Empty<Empire>())
			{
				switch (this.ResultFor(e))
				{
					case "victory":
						e.TriggerHappinessChange(StarSystem, hm => hm.BattleInSystemWin);
						e.TriggerHappinessChange(Sector, hm => hm.BattleInSectorWin);
						break;
					case "defeat":
						e.TriggerHappinessChange(StarSystem, hm => hm.BattleInSystemLoss);
						e.TriggerHappinessChange(Sector, hm => hm.BattleInSectorLoss);
						break;
					case "stalemate":
					case "Pyrrhic victory":
						e.TriggerHappinessChange(StarSystem, hm => hm.BattleInSystemStalemate);
						e.TriggerHappinessChange(Sector, hm => hm.BattleInSectorStalemate);
						break;
				}
			}
		}
	}
}
