using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A colony on a planet.
	/// </summary>
	[Serializable]
	public class Colony : IOwnableAbilityObject, IFoggable, IContainable<Planet>, IIncomeProducer, IReferrable
	{
		public Colony()
		{
			Facilities = new List<Facility>();
			Population = new SafeDictionary<Race, long>();
			Cargo = new Cargo();
		}

		public AbilityTargets AbilityTarget => AbilityTargets.Planet;

		/// <summary>
		/// The anger level of each race on this colony.
		/// </summary>
		public SafeDictionary<Race, int> Anger { get; private set; } = new SafeDictionary<Race, int>();

		/// <summary>
		/// The anger changes this turn of each race on this colony.
		/// </summary>
		public SafeDictionary<Race, int> AngerDeltas { get; private set; } = new SafeDictionary<Race, int>();

		/// <summary>
		/// The average anger of all races on this colony, weighted by population.
		/// </summary>
		public int AverageAnger => (int)Anger.WeightedAverage(kvp => Population[kvp.Key], kvp => kvp.Value);

		/// <summary>
		/// The cargo stored on this colony.
		/// </summary>
		public Cargo Cargo { get; set; }

		public IEnumerable<IAbilityObject> Children => Facilities.Cast<IAbilityObject>().Concat(Cargo.Units.Cast<IAbilityObject>());

		/// <summary>
		/// This colony's construction queue.
		/// </summary>
		public ConstructionQueue? ConstructionQueue { get; set; }

		public Planet Container => Galaxy.Current.FindSpaceObjects<Planet>().SingleOrDefault(p => p.Colony == this);

		/// <summary>
		/// The facilities on this colony.
		/// </summary>
		public ICollection<Facility> Facilities { get; set; }

		public long ID
		{
			get;
			set;
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
		}

		public bool IsDisposed { get; set; }

		public bool IsHomeworld { get; set; }

		public bool IsMemory { get; set; }

		/// <summary>
		/// Ratio of population that has the "No Spaceports" ability.
		/// </summary>
		public double MerchantsRatio
		{
			get
			{
				var merchants = Population.Where(kvp => kvp.Key.HasAbility("No Spaceports")).Sum(kvp => kvp.Value);
				var totalPop = Population.Sum(kvp => kvp.Value);
				var ratio = (double)merchants / (double)totalPop;
				return ratio;
			}
		}

		/// <summary>
		/// The overall mood of the population of this colony.
		/// </summary>
		public Mood Mood => Mod.Current.Settings.MoodThresholds.Where(kvp => kvp.Value <= AverageAnger).WithMax(kvp => kvp.Value).Single().Key;

		/// <summary>
		/// The mood of each race on this colony.
		/// </summary>
		public IReadOnlyDictionary<Race, Mood> Moods => Anger.Select(kvp => new KeyValuePair<Race, Mood>(kvp.Key, Mod.Current.Settings.MoodThresholds.Where(kvp2 => kvp2.Value <= Anger[kvp.Key]).WithMax(kvp2 => kvp2.Value).Single().Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

		/// <summary>
		/// The empire which owns this colony.
		/// </summary>
		public Empire? Owner { get; set; }

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield return Container;
			}
		}

		/// <summary>
		/// The population of this colony, by race.
		/// </summary>
		public SafeDictionary<Race, long> Population { get; private set; }

		public ResourceQuantity? RemoteMiningIncomePercentages => Owner?.PrimaryRace?.IncomePercentages;

		public ResourceQuantity ResourceValue => Container.ResourceValue;

		[DoNotSerialize(false)]
		public Sector Sector { get => Container.Sector; set => throw new NotSupportedException("Can't set the sector of a colony."); }

		public ResourceQuantity StandardIncomePercentages
		{
			get
			{
				// do modifiers to income
				var totalpop = Population.Sum(kvp => kvp.Value);
				var popfactor = Mod.Current.Settings.GetPopulationProductionFactor(totalpop);
				var moodfactor = Mod.Current.Settings.MoodProductivityModifiers[Mood] / 100d;

				var result = new ResourceQuantity();

				foreach (var r in Resource.All)
				{
					var aptfactor = 1d;
					if (r.Aptitude?.Name != null)
						aptfactor = Population.Sum(kvp => (kvp.Key.Aptitudes[r.Aptitude.Name] / 100d) * (double)kvp.Value / (double)totalpop);
					var cultfactor = (100 + r.CultureModifier(Owner?.Culture)) / 100d;

					result += (int)(100 * popfactor * aptfactor * cultfactor * moodfactor) * r;
				}

				return result;
			}
		}

		public StarSystem? StarSystem => Container?.StarSystem;

		public double Timestamp { get; set; }

		public void ChangePopulation(long amount)
		{
			var pop = Population.ToArray();
			var total = Population.Sum(q => q.Value);
			foreach (var kvp in pop)
				Population[kvp.Key] += amount * kvp.Value / total;
		}

		public Visibility CheckVisibility(Empire emp)
		{
            // should be visible, assuming the planet is visible - we don't have colony cloaking at the moment...
            if (emp == Owner)
                return Visibility.Owned;
            else if (Container == null)
                return Visibility.Unknown; // HACK - why would a colony not be on a planet?!
            else
                return Container.CheckVisibility(emp);
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Container != null)
				Container.Colony = null;
			ConstructionQueue.SafeDispose();
			Galaxy.Current.UnassignID(this);
			if (!IsMemory)
				this.UpdateEmpireMemories();
			IsDisposed = true;
		}

		public bool IsObsoleteMemory(Empire emp) => Container == null || Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;

		public void Redact(Empire emp)
		{
			var visibility = CheckVisibility(emp);
			if (visibility < Visibility.Owned)
			{
				// can only see space used by cargo, not actual cargo
				Cargo.SetFakeSize(true);
			}
			if (visibility < Visibility.Scanned)
			{
				var unknownFacilityTemplate = FacilityTemplate.Unknown;
				var facilCount = Facilities.Count;
				Facilities.Clear();
				for (int i = 0; i < facilCount; i++)
					Facilities.Add(new Facility(unknownFacilityTemplate));
				Anger.Clear();
				AngerDeltas.Clear();
			}
			if (visibility < Visibility.Fogged)
				Dispose();
		}

		/// <summary>
		/// Triggers a happiness change at this colony.
		/// </summary>
		/// <param name="trigger">The trigger function.</param>
		public void TriggerHappinessChange(Func<HappinessModel, int> trigger)
		{
			foreach (var race in Population.Keys)
			{
				if (race.HappinessModel != null)
					AngerDeltas[race] += trigger(race.HappinessModel);
			}
		}
	}
}
