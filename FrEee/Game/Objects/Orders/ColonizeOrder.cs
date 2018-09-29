using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to colonize an uninhabited planet.
	/// </summary>
	[Serializable]
	public class ColonizeOrder : IOrder<IMobileSpaceObject>
	{
		public ColonizeOrder(Planet planet)
		{
			Owner = Empire.Current;
			Planet = planet;
		}

		public bool ConsumesMovement
		{
			get { return true; }
		}

		public long ID { get; set; }

		public bool IsComplete
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		/// <summary>
		/// The planet we are colonizing.
		/// </summary>
		[DoNotSerialize]
		public Planet Planet { get { return planet; } set { planet = value; } }

		private GalaxyReference<Empire> owner { get; set; }
		private GalaxyReference<Planet> planet { get; set; }

		public bool CheckCompletion(IMobileSpaceObject v)
		{
			return IsComplete;
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

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
			{
				if (v is SpaceVehicle sv)
					sv.Orders.Remove(this);
				else if (v is Fleet f)
					f.Orders.Remove(this);
				else if (v is Planet p)
					p.Orders.Remove(this);
			}
			Galaxy.Current.UnassignID(this);
		}

		public void Execute(IMobileSpaceObject sobj)
		{
			// error checking
			var errors = GetErrors(sobj);
			foreach (var error in errors)
				sobj.Owner.Log.Add(error);

			// let only one colony ship from a fleet colonize
			if (sobj is Fleet f)
				sobj = f.LeafVehicles.FirstOrDefault(q => q.HasAbility(Planet.ColonizationAbilityName));

			if (!errors.Any())
			{
				// colonize now!!!
				Planet.Colony = new Colony { Owner = sobj.Owner };
				Owner.TriggerHappinessChange(hm => hm.PlanetColonized);
				Planet.Colony.ConstructionQueue = new ConstructionQueue(Planet);
				if (sobj is ICargoContainer cc)
				{
					foreach (var kvp in cc.Cargo.Population)
					{
						// place population on planet
						Planet.AddPopulation(kvp.Key, kvp.Value);
					}
					foreach (var unit in cc.Cargo.Units)
					{
						// planet unit on planet
						Planet.AddUnit(unit);
					}
				}

				// ruins?
				var ruinsTechs = Planet.GetAbilityValue("Ancient Ruins").ToInt();
				for (int i = 0; i < ruinsTechs; i++)
				{
					var msg = "We have discovered new technology from the ancient ruins on " + Planet + ".";
					// pick a random tech that's unlocked but not fully researched and level it up
					var tech = Mod.Current.Technologies.Where(t => sobj.Owner.HasUnlocked(t) && sobj.Owner.ResearchedTechnologies[t] < t.MaximumLevel).PickRandom();
					if (tech == null)
						msg = "We have discovered ancient ruins on " + Planet + ", but there is nothing left for us to learn.";
					else
					{
						var oldlvl = sobj.Owner.ResearchedTechnologies[tech];
						var newStuff = tech.GetExpectedResults(sobj.Owner);
						sobj.Owner.ResearchedTechnologies[tech]++;
						var newlvl = sobj.Owner.ResearchedTechnologies[tech];
						var progress = sobj.Owner.ResearchProgress.SingleOrDefault(p => p.Item == tech);
						if (progress != null)
							progress.Value = 0;
						sobj.Owner.Log.Add(tech.CreateLogMessage("We have advanced from level " + oldlvl + " to level " + newlvl + " in " + tech + "!"));
						foreach (var item in newStuff)
							sobj.Owner.Log.Add(item.CreateLogMessage("We have unlocked a new " + item.ResearchGroup.ToLower() + ", the " + item + "!"));
					}
					if (i == 0)
						sobj.Owner.Log.Add(Planet.CreateLogMessage(msg));
				}

				// unique ruins?
				foreach (var abil in Planet.Abilities().Where(a => a.Rule.Name == "Ancient Ruins Unique"))
				{
					if (sobj.Owner.UniqueTechsFound.Contains(abil.Value1))
						sobj.Owner.Log.Add(Planet.CreateLogMessage("We have discovered \"unique\" technology from the ancient ruins on " + Planet + ", but it appears we have already found this one elsewhere. Perhaps it was not as unique as we had thought..."));
					else
					{
						sobj.Owner.Log.Add(Planet.CreateLogMessage("We have discovered new unique technology from the ancient ruins on " + Planet + "."));
						sobj.Owner.UniqueTechsFound.Add(abil.Value1);
						foreach (var tech in Mod.Current.Technologies.Where(t => t.UniqueTechID == abil.Value1 && sobj.Owner.HasUnlocked(t)))
							sobj.Owner.Log.Add(tech.CreateLogMessage("We have unlocked a new " + tech.ResearchGroup.ToLower() + ", the " + tech + "!"));
					}
				}

				// delete ruins and unique ruins abilities
				foreach (var a in Planet.IntrinsicAbilities.Where(a => a.Rule.Name == "Ancient Ruins" || a.Rule.Name == "Ancient Ruins Unique").ToArray())
					Planet.IntrinsicAbilities.Remove(a);

				// log it!
				sobj.Owner.Log.Add(Planet.CreateLogMessage(sobj + " has founded a new colony on " + Planet + "."));

				// update pursue/evade orders to target planet now instead of ship
				foreach (var o in Galaxy.Current.Referrables.OfType<PursueOrder>().Where(q => q.Target == sobj))
				{
					if (o.Owner.CanSee(sobj) && o.Owner.CanSee(Planet))
						o.Target = Planet;
				}
				foreach (var o in Galaxy.Current.Referrables.OfType<EvadeOrder>().Where(q => q.Target == sobj))
				{
					if (o.Owner.CanSee(sobj) && o.Owner.CanSee(Planet))
						o.Target = Planet;
				}

				// bye bye colony ship
				sobj.Dispose();
			}

			// either done colonizing, or we failed
			IsComplete = true;

			// spend time
			sobj.SpendTime(sobj.TimePerMove);
		}

		public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject sobj)
		{
			if (sobj.Sector != Planet.Sector)
			{
				// can't colonize here, maybe the GUI should have issued a move order?
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because it is not currently located at the planet.");
			}
			if (Planet.Colony != null)
			{
				// planet is already colonized!
				yield return Planet.CreateLogMessage(Planet + " cannot be colonized by " + sobj + " because there is already a colony there belonging to the " + Planet.Colony.Owner + ".");
			}
			if (!(sobj.HasAbility(Planet.ColonizationAbilityName) || sobj is Fleet f && f.LeafVehicles.Any(v => v.HasAbility(Planet.ColonizationAbilityName))))
			{
				// no such colony module
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because it lacks a " + Planet.Surface + " colony module.");
			}
			if (Galaxy.Current.CanColonizeOnlyBreathable && Planet.Atmosphere != sobj.Owner.PrimaryRace.NativeAtmosphere)
			{
				// can only colonize breathable atmosphere (due to game setup option)
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because we can only colonize " + sobj.Owner.PrimaryRace.NativeAtmosphere + " planets.");
			}
			if (Galaxy.Current.CanColonizeOnlyHomeworldSurface && Planet.Surface != sobj.Owner.PrimaryRace.NativeSurface)
			{
				// can only colonize breathable atmosphere (due to game setup option)
				yield return sobj.CreateLogMessage(sobj + " cannot colonize " + Planet + " because we can only colonize " + sobj.Owner.PrimaryRace.NativeSurface + " planets.");
			}
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// This type does not use client objects, so nothing to do here.
		}

		public override string ToString()
		{
			return "Colonize " + Planet.Name;
		}
	}
}