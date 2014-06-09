using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
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
	public class Facility : IOwnableAbilityObject, IConstructable, IDamageable, IDisposable, IContainable<Planet>, IFormulaHost
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

		IConstructionTemplate IConstructable.Template
		{
			get { return Template; }
		}

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
			get { return Template.Cost.Evaluate(this); }
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

		public string Name { get { return Template.Name.Evaluate(this); } }

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
		[DoNotSerialize(false)]
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
		[DoNotSerialize(false)]
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

		public void ReplenishShields(int? amount = null)
		{
			// do nothing
		}

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			int damage = hit.NominalDamage;
			var realhit = new Hit(hit.Shot, this, damage);
			var df = realhit.Shot.DamageType.FacilityDamage.Evaluate(realhit);
			var dp = realhit.Shot.DamageType.FacilityPiercing.Evaluate(realhit);
			var factoredDmg = df.PercentOfRounded(damage);
			var piercing = dp.PercentOfRounded(damage);
			var realdmg = Math.Min(Hitpoints, factoredDmg);
			var nominalDamageSpent = realdmg / (df + dp);
			Hitpoints -= realdmg;
			if (IsDestroyed)
				Dispose();
			return damage - nominalDamageSpent;
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
				return Galaxy.Current.FindSpaceObjects<Planet>().SingleOrDefault(p => p.Colony != null && p.Colony.Facilities.Contains(this));
			}
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Container != null)
			{
				Container.Colony.Facilities.Remove(this);
				Container.Colony.UpdateEmpireMemories();
			}
		}

		// TODO - dynamic formula evaluation

		public IDictionary<string, object> Variables
		{
			get
			{
				return new Dictionary<string, object>
				{
					{"colony", Container.Colony},
					{"planet", Container},
					{"empire", Owner}
				};
			}
		}

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Facility; }
		}


		public int ShieldHitpoints
		{
			get { return 0; }
		}

		/// <summary>
		/// TODO - "armor" facilities that are hit before other facilities on a planet?
		/// </summary>
		public int ArmorHitpoints
		{
			get { return 0; }
		}

		public int HullHitpoints
		{
			get { return Hitpoints; }
		}

		public int MaxShieldHitpoints
		{
			get { return 0; }
		}

		public int MaxArmorHitpoints
		{
			get { return 0; }
		}

		public int MaxHullHitpoints
		{
			get { return MaxHitpoints; }
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { return Abilities; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { yield break; }
		}

		public IAbilityObject Parent
		{
			get { return Container; }
		}

		public bool IsDisposed { get; set; }
	}
}
