using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Space;
using FrEee.Modding;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// A colony on a planet.
	/// </summary>
	[Serializable]
	public class Colony : IOwnableAbilityObject, IFoggable, IContainable<Planet>, IIncomeProducer
	{
		public Colony()
		{
			Facilities = new List<Facility>();
			Population = new SafeDictionary<Race, long>();
			Cargo = new Cargo();
		}

		/// <summary>
		/// The empire which owns this colony.
		/// </summary>
		public Empire Owner { get; set; }

		/// <summary>
		/// The facilities on this colony.
		/// </summary>
		public ICollection<Facility> Facilities { get; set; }

		/// <summary>
		/// The population of this colony, by race.
		/// </summary>
		public SafeDictionary<Race, long> Population { get; private set; }

		/// <summary>
		/// This colony's construction queue.
		/// </summary>
		public ConstructionQueue ConstructionQueue
		{
			get;
			set;
		}

		/// <summary>
		/// The cargo stored on this colony.
		/// </summary>
		public Cargo Cargo { get; set; }

		public Visibility CheckVisibility(Empire emp)
		{
			// should be visible, assuming the planet is visible - we don't have colony cloaking at the moment...
			if (emp == Owner)
				return Visibility.Owned;
			else
				return Visibility.Visible;
		}

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
			}
			if (visibility < Visibility.Fogged)
				Dispose();
		}

		public long ID
		{
			get;
			set;
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

		public Planet Container
		{
			get
			{
				return Galaxy.Current.FindSpaceObjects<Planet>().SingleOrDefault(p => p.Colony == this);
			}
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp { get; set; }


		public bool IsObsoleteMemory(Empire emp)
		{
			return Container == null || Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Planet; }
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { yield break; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get
			{
				return Facilities.Cast<IAbilityObject>().Union(Cargo.Units.Cast<IAbilityObject>());
			}
		}

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				yield return Container;
			}
		}

		public bool IsDisposed { get; set; }

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

		public ResourceQuantity StandardIncomePercentages
		{
			get
			{
				// do modifiers to income
				var totalpop = Population.Sum(kvp => kvp.Value);
				var popfactor = Mod.Current.Settings.GetPopulationProductionFactor(totalpop);

				var result = new ResourceQuantity();

				foreach (var r in Resource.All)
				{
					var aptfactor = 1d;
					if (r.Aptitude != null)
						aptfactor = Population.Sum(kvp => (kvp.Key.Aptitudes[r.Aptitude.Name] / 100d) * (double)kvp.Value / (double)totalpop);
					var cultfactor = (100 + r.CultureModifier(Owner.Culture)) / 100d;

					result += (int)(100 * popfactor * aptfactor * cultfactor) * r;
				}

				return result;
			}
		}

		public ResourceQuantity RemoteMiningIncomePercentages
		{
			get { return Owner.PrimaryRace.IncomePercentages; }
		}


		public ResourceQuantity ResourceValue
		{
			get { return Container.ResourceValue; }
		}

		[DoNotSerialize(false)]
		public Sector Sector { get => Container.Sector; set => throw new NotSupportedException("Can't set the sector of a colony."); }

		public StarSystem StarSystem => Container?.StarSystem;
	}
}
