using AutoMapper;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A large immobile installation on a colony.
	/// </summary>
	[Serializable]
	public class Facility : IAbilityObject, IConstructable, IOwnable, IDamageable, IDisposable, IContainable<Planet>, IFoggable
	{
		public Facility(FacilityTemplate template)
		{
			Template = template;
			ConstructionProgress = new ResourceQuantity();
			Hitpoints = MaxHitpoints;
		}

		public Empire Owner { get; set; }

		/// <summary>
		/// The template for this facility.
		/// Specifies the basic stats of the facility and its abilities.
		/// </summary>
		public FacilityTemplate Template { get; private set; }

		public IEnumerable<Ability> Abilities
		{
			get
			{
				if (Template == null)
					return Enumerable.Empty<Ability>();
				return Template.Abilities;
			}
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

		public ResourceQuantity Cost
		{
			get
			{
				if (Template == null)
					return new ResourceQuantity();
				return Template.Cost;
			}
		}

		public ResourceQuantity ConstructionProgress
		{
			get;
			set;
		}

		[DoNotSerialize]
		public Image Icon
		{
			get
			{
				if (Template == null)
					return null;
				return Template.Icon;
			}
		}

		[DoNotSerialize]
		public Image Portrait
		{
			get
			{
				if (Template == null)
					return null;
				return Template.Portrait;
			}
		}

		/// <summary>
		/// Places the facility.
		/// </summary>
		/// <param name="sobj">Must be a colonized planet.</param>
		public void Place(ISpaceObject sobj)
		{
			if (sobj is Planet)
			{
				var planet = (Planet)sobj;
				if (planet.Colony == null)
					throw new ArgumentException("Facilities can only be placed on colonized planets.");
				if (planet.Colony.Facilities.Count >= planet.MaxFacilities)
					planet.Colony.Owner.Log.Add(planet.CreateLogMessage(this + " cannot be constructed at " + planet + " because there is no more space available for facilities there."));
				else
					planet.Colony.Facilities.Add(this);
			}
			else
				throw new ArgumentException("Facilities can only be placed on colonized planets.");
		}

		public string Name
		{
			get
			{
				if (Template == null)
					return "(Unknown)"; 
				return Template.Name;
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public int Hitpoints
		{
			get;
			set;
		}

		/// <summary>
		/// Facilities do not have shields, though they may provide them to colonies.
		/// </summary>
		[DoNotSerialize]
		[IgnoreMap]
		public int NormalShields
		{
			get
			{
				return 0;
			}
			set
			{
				throw new NotSupportedException("Facilities do not have shields, though they may provide them to colonies.");
			}
		}

		/// <summary>
		/// Facilities do not have shields, though they may provide them to colonies.
		/// </summary>
		[DoNotSerialize]
		[IgnoreMap]
		public int PhasedShields
		{
			get
			{
				return 0;
			}
			set
			{
				throw new NotSupportedException("Facilities do not have shields, though they may provide them to colonies.");
			}
		}

		public int MaxHitpoints
		{
			get
			{
				// TODO - moddable facility HP
				return 1000;
			}
		}

		public int MaxNormalShields
		{
			get { return 0; }
		}

		public int MaxPhasedShields
		{
			get { return 0; }
		}

		public void ReplenishShields()
		{
			// do nothing
		}

		public int TakeDamage(DamageType dmgType, int damage, Combat.Battle battle)
		{
			// TODO - take into account damage types
			int realDamage;
			realDamage = Math.Min(Hitpoints, damage);
			Hitpoints -= realDamage;
			if (IsDestroyed)
			{
				battle.Log.Add(this.CreateLogMessage(this + " takes " + realDamage + " points of damage and is destroyed!"));
				Dispose();
			}
			else
			{
				battle.Log.Add(this.CreateLogMessage(this + " takes " + realDamage + " points of damage!"));
			}
			return damage - realDamage;
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}


		public int? Repair(int? amount = null)
		{
			if (amount == null)
			{
				Hitpoints = MaxHitpoints;
				return amount;
			}
			else
			{
				var actual = Math.Min(MaxHitpoints - Hitpoints, amount.Value);
				Hitpoints += actual;
				return amount.Value - actual;
			}
		}


		public int HitChance
		{
			// TODO - moddable facility hit chance
			get { return 1000; }
		}

		/// <summary>
		/// Finds the planet which contains this facility.
		/// </summary>
		/// <returns></returns>
		public Planet Container
		{
			get
			{
				return Galaxy.Current.FindSpaceObjects<Planet>().Flatten().Flatten().SingleOrDefault(p => p.Colony != null && p.Colony.Facilities.Contains(this));
			}
		}

		public void Dispose()
		{
			if (Container != null)
				Container.Colony.Facilities.Remove(this);
		}

		/// <summary>
		/// Facilities are visible to anyone who can see the colony containing them.
		/// Of course, they can't see all the details...
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (Container == null)
				return Visibility.Unknown;
			return Container.CheckVisibility(emp);
		}

		public void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);
			if (vis < Visibility.Scanned)
			{
				Hitpoints = 0;
				Template = null;
			}
			// TODO - remember previously scanned facilities
			if (vis < Visibility.Fogged)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public bool IsKnownToBeDestroyed
		{
			get;
			set;
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsVisibleTo(Empire emp)
		{
			return CheckVisibility(emp) >= Visibility.Visible;
		}
	}
}
