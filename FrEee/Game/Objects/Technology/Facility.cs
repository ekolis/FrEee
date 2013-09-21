using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
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
	public class Facility : IAbilityObject, IConstructable, IOwnable, IDamageable, IDisposable
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
			get { return Template.Abilities; }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

		public ResourceQuantity Cost
		{
			get { return Template.Cost; }
		}

		public ResourceQuantity ConstructionProgress
		{
			get;
			set;
		}

		[DoNotSerialize] public Image Icon
		{
			get { return Template.Icon; }
		}

		[DoNotSerialize] public Image Portrait
		{
			get { return Template.Portrait; }
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

		public string Name { get { return Template.Name; } }

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
			battle.Log.Add(this.CreateLogMessage(this + " takes " + realDamage + " points of damage!"));
			Hitpoints -= realDamage;
			if (Hitpoints <= 0)
				Dispose();
			return damage - realDamage;
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}


		public int Repair(int? amount = null)
		{
			if (amount == null)
			{
				Hitpoints = MaxHitpoints;
				return 0;
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
		public Planet FindContainer()
		{
			return Galaxy.Current.FindSpaceObjects<Planet>().Flatten().Flatten().SingleOrDefault(p => p.Colony != null && p.Colony.Facilities.Contains(this));
		}

		public void Dispose()
		{
			FindContainer().Colony.Facilities.Remove(this);
			// TODO - unassign ID, once facilities have ID's (they will need to have them so they can be scrapped/upgraded)
		}
	}
}
