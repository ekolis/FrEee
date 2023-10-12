using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat.Grid;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Events;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.VictoryConditions;
using FrEee.Game.Setup;
using FrEee.Game.Setup.WarpPointPlacementStrategies;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Microsoft.Scripting.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A galaxy in which the game is played.
	/// </summary>
	[Serializable]
	public class Galaxy : ICommonAbilityObject
	{
		public Galaxy(Game game)
		{
			Game = game;
			StarSystemLocations = new List<ObjectLocation<StarSystem>>();
			Battles = new HashSet<IBattle>();
		}

		/// <summary>
		/// The game to which this galaxy belongs.
		/// </summary>
		public Game Game { get; private set; }

		/// <summary>
		/// The current galaxy. Shouldn't change except at loading a game or turn processing.
		/// </summary>
		// public static Galaxy Current { get; set; }

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Galaxy; }
		}

		/// <summary>
		/// The battles which have taken place this turn.
		/// </summary>
		public ICollection<IBattle> Battles { get; private set; }

		public IEnumerable<IAbilityObject> Children
		{
			get { return StarSystemLocations.Select(l => l.Item); }
		}

		public int Height
		{
			get;
			set;
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			// TODO - galaxy wide abilities?
			get { yield break; }
		}

		public bool IsAnalysisAllowed { get; set; }

		public int MaxX
		{
			get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.X); }
		}

		public int MaxY
		{
			get { return StarSystemLocations.MaxOrDefault(ssl => ssl.Location.Y); }
		}

		public int MinX
		{
			get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.X); }
		}

		public int MinY
		{
			get { return StarSystemLocations.MinOrDefault(ssl => ssl.Location.Y); }
		}

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield break;
			}
		}	

		/// <summary>
		/// The locations of the star systems in the galaxy.
		/// </summary>
		public ICollection<ObjectLocation<StarSystem>> StarSystemLocations { get; private set; }

		/// <summary>
		/// Vertical space occupied by star systems.
		/// </summary>
		public int UsedHeight
		{
			get
			{
				if (!StarSystemLocations.Any())
					return 0;
				return StarSystemLocations.Max(ssl => ssl.Location.Y) - StarSystemLocations.Min(ssl => ssl.Location.Y) + 1;
			}
		}

		/// <summary>
		/// Horizontal space occuped by star systems.
		/// </summary>
		public int UsedWidth
		{
			get
			{
				if (!StarSystemLocations.Any())
					return 0;
				return StarSystemLocations.Max(ssl => ssl.Location.X) - StarSystemLocations.Min(ssl => ssl.Location.X) + 1;
			}
		}

		public int Width
		{
			get;
			set;
		}

		/// <summary>
		/// Searches for space objects matching criteria.
		/// </summary>
		/// <typeparam name="T">The type of space object.</typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns>The matching space objects.</returns>
		public IEnumerable<T> FindSpaceObjects<T>(Func<T, bool> criteria = null)
		{
			return StarSystemLocations.SelectMany(l => l.Item.FindSpaceObjects<T>(criteria));
		}

		public IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp)
		{
			return StarSystemLocations.Select(ssl => ssl.Item).Concat(StarSystemLocations.SelectMany(ssl => ssl.Item.GetContainedAbilityObjects(emp)));
		}

		public Sector PickRandomSector(PRNG? prng = null)
		{
			return StarSystemLocations.PickRandom(prng).Item.PickRandomSector(prng);
		}

		

		/// <summary>
		/// Disposes of any space objects that aren't in space, under construction, or part of the mod definition.
		/// </summary>
		private void SpaceObjectCleanup()
		{
			foreach (var sobj in Game.Referrables.OfType<ISpaceObject>().ToArray())
			{
				bool dispose = true;
				if (sobj.Sector != null)
					dispose = false; // save space objects that are in space
				else if (this is IUnit u && u.FindContainer() != null) // save units that are in cargo
					dispose = false;
				else if (Game.Mod.StellarObjectTemplates.Contains(sobj as StellarObject))
					dispose = false; // save stellar objects that are part of the mod templates
				else if (Game.Referrables.OfType<ConstructionQueue>().Any(q => q.Orders.Any(o => o.Item == sobj as IConstructable)))
					dispose = false; // save constructable space objects under construction
				if (dispose)
					sobj.Dispose();
			}
		}
	}
}
