using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to load cargo.
	/// </summary>
	public class LoadCargoOrder : IOrder<ICargoContainer>
	{
		public LoadCargoOrder(ICargoContainer origin)
		{
			Owner = Empire.Current;
			Origin = origin;
			racePopulationToLoad = new SafeDictionary<Reference<Race>, long?>();
			AnyPopulationToLoad = 0;
			designUnitsToLoad = new SafeDictionary<Reference<IDesign<Unit>>, int?>();
			anyUnitsToLoad = new SafeDictionary<VehicleTypes, int?>();
			if (Galaxy.Current != null && Galaxy.Current.PlayerNumber > 0)
				Galaxy.Current.Register(this);
		}

		/// <summary>
		/// The cargo container from which cargo is being loaded.
		/// </summary>
		[DoNotSerialize]
		public ICargoContainer Origin { get { return origin.Value; } set { origin = value.Reference(); } }

		private Reference<ICargoContainer> origin { get; set; }

		#region Race specific population loading
		public IEnumerable<KeyValuePair<Race, long?>> RacePopulationToLoad
		{
			get
			{
				return racePopulationToLoad.Select(kvp => new KeyValuePair<Race, long?>(kvp.Key.Value, kvp.Value));
			}
		}

		public void SetRacePopulationToLoad(Race race, long? pop)
		{
			racePopulationToLoad[race.Reference()] = pop;
		}

		public long? GetRacePopulationToLoad(Race race)
		{
			return racePopulationToLoad[race];
		}

		/// <summary>
		/// Population of specific races to load. Null values mean all population.
		/// </summary>
		private SafeDictionary<Reference<Race>, long?> racePopulationToLoad { get; set; }
		#endregion

		#region Generic population loading

		/// <summary>
		/// Population of any race to load. Null means all population.
		/// </summary>
		public long? AnyPopulationToLoad { get; set; }

		#endregion

		#region Design specific unit loading
		public IEnumerable<KeyValuePair<IDesign<Unit>, int?>> DesignUnitsToLoad
		{
			get
			{
				return designUnitsToLoad.Select(kvp => new KeyValuePair<IDesign<Unit>, int?>(kvp.Key.Value, kvp.Value));
			}
		}

		public void SetDesignUnitsToLoad(IDesign<Unit> design, int? count)
		{
			designUnitsToLoad[design.Reference()] = count;
		}

		public int? GetDesignUnitsToLoad(IDesign<Unit> design)
		{
			return designUnitsToLoad[design.Reference()];
		}

		/// <summary>
		/// Units of specific design to load. Null values mean all units.
		/// </summary>
		private SafeDictionary<Reference<IDesign<Unit>>, int?> designUnitsToLoad { get; set; }
		#endregion

		#region Generic unit loading
		public IEnumerable<KeyValuePair<VehicleTypes, int?>> AnyUnitsToLoad
		{
			get
			{
				return anyUnitsToLoad;
			}
		}

		public void SetAnyUnitsToLoad(VehicleTypes vt, int? count)
		{
			anyUnitsToLoad[vt] = count;
		}

		public int? GetAnyUnitsToLoad(VehicleTypes vt)
		{
			return anyUnitsToLoad[vt];
		}

		/// <summary>
		/// Units of general vehicle types to load. Null values mean all units.
		/// </summary>
		private SafeDictionary<VehicleTypes, int?> anyUnitsToLoad { get; set; }
		#endregion

		public void Execute(ICargoContainer src)
		{
			if (IsComplete)
				return; // already loaded!

			if (src.FindSector() != Origin.FindSector())
			{
				// TODO - beaming cargo from ship to ship? that would be cool!
				// but for now, cargo transfers only work in a single sector
				return;
			}

			// load population from storage
			foreach (var kvp in RacePopulationToLoad.ToArray())
			{
				// how much we can safely load?
				// TODO - moddable population size (per race?)
				var canLoadKT = src.CargoStorage - src.Cargo.Size;
				var canLoadPop = (long)(canLoadKT / 5 * 1e6);

				// load population
				var loadable = Math.Min(canLoadPop, Origin.Cargo.Population[kvp.Key]);
				Origin.Cargo.Population[kvp.Key] -= loadable;
				src.Cargo.Population[kvp.Key] += loadable;
				racePopulationToLoad[kvp.Key] -= loadable;
			}

			// any population
			foreach (var kvp in Origin.Cargo.Population.ToArray())
			{
				if (AnyPopulationToLoad != null && AnyPopulationToLoad <= 0)
					break;

				// how much we can safely load?
				// TODO - moddable population size (per race?)
				var canLoadKT = src.CargoStorage - src.Cargo.Size;
				var canLoadPop = (long)(canLoadKT / 5 * 1e6);

				// load population
				var loadable = Math.Min(canLoadPop, Origin.Cargo.Population[kvp.Key]);
				Origin.Cargo.Population[kvp.Key] -= loadable;
				src.Cargo.Population[kvp.Key] += loadable;
				AnyPopulationToLoad -= loadable;
			}

			// load population from colony
			Colony colony = null;
			if (Origin is Planet)
			{
				var p = (Planet)Origin;
				colony = p.Colony;
			}
			else if (Origin is Colony)
				colony = (Colony)Origin;
			if (colony != null)
			{
				// race population
				foreach (var kvp in RacePopulationToLoad.ToArray())
				{
					// how much we can safely load?
					// TODO - moddable population size (per race?)
					var canLoadKT = src.CargoStorage - src.Cargo.Size;
					var canLoadPop = (long)(canLoadKT / 5 * 1e6);

					// load population
					var loadable = Math.Min(canLoadPop, colony.Population[kvp.Key]);
					colony.Population[kvp.Key] -= loadable;
					src.Cargo.Population[kvp.Key] += loadable;
					racePopulationToLoad[kvp.Key] -= loadable;
				}

				// any population
				foreach (var kvp in colony.Population.ToArray())
				{
					if (AnyPopulationToLoad != null && AnyPopulationToLoad <= 0)
						break;

					// how much we can safely load?
					// TODO - moddable population size (per race?)
					var canLoadKT = src.CargoStorage - src.Cargo.Size;
					var canLoadPop = (long)(canLoadKT / 5 * 1e6);

					// load population
					var loadable = Math.Min(canLoadPop, colony.Population[kvp.Key]);
					colony.Population[kvp.Key] -= loadable;
					src.Cargo.Population[kvp.Key] += loadable;
					AnyPopulationToLoad -= loadable;
				}
			}

			// TODO - load units

			// all done?
			if (racePopulationToLoad.Values.All(v => v == null || v <= 0) &&
				(AnyPopulationToLoad == null || AnyPopulationToLoad <= 0) &&
				designUnitsToLoad.Values.All(v => v == null || v <= 0) &&
				anyUnitsToLoad.Values.All(v => v == null || v <= 0))
				IsComplete = true;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public override string ToString()
		{
			var loads = new List<string>();
			foreach (var kvp in RacePopulationToLoad)
			{
				if (kvp.Value == null)
					loads.Add("All " + kvp.Key);
				else if (kvp.Value > 0)
					loads.Add(kvp.Value.Value.ToUnitString(true) + " " + kvp.Key);
			}
			if (AnyPopulationToLoad == null)
				loads.Add("All Population");
			else if (AnyPopulationToLoad.Value > 0)
				loads.Add(AnyPopulationToLoad.Value.ToUnitString(true) + " population");
			foreach (var kvp in DesignUnitsToLoad)
			{
				if (kvp.Value == null)
					loads.Add("All " + kvp.Key);
				else if (kvp.Value > 0)
					loads.Add(kvp.Value.Value.ToUnitString() + " " + kvp.Key);
			}
			foreach (var kvp in AnyUnitsToLoad)
			{
				if (kvp.Value == null)
					loads.Add("All " + kvp.Key);
				else if (kvp.Value > 0)
					loads.Add(kvp.Value.Value.ToUnitString() + " " + kvp.Key);
			}

			if (loads.Any())
				return "Load " + string.Join(", ", loads.ToArray());
			else
				return "Load Nothing";
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
		}

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		public Empire Owner
		{
			get;
			private set;
		}

		/// <summary>
		/// Orders are visible only to their owners.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Visible;
			return Visibility.Unknown;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			// This type does not use client objects, so nothing to do here.
		}
	}
}
