using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FrEee.Game.Objects.Technology
{
	/// <summary>
	/// A large immobile installation on a colony.
	/// </summary>
	[Serializable]
	public class Facility : IOwnableAbilityObject, IConstructable, IDamageable, IDisposable, IContainable<Planet>, IFormulaHost, IRecyclable, IUpgradeable<Facility>
	{
		public Facility(FacilityTemplate template)
		{
			Template = template;
			ConstructionProgress = new ResourceQuantity();
			Hitpoints = MaxHitpoints;
		}

		public IEnumerable<Ability> Abilities
		{
			get { return Template.Abilities; }
		}

		public AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Facility; }
		}

		/// <summary>
		/// TODO - "armor" facilities that are hit before other facilities on a planet?
		/// </summary>
		public int ArmorHitpoints
		{
			get { return 0; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { yield break; }
		}

		public ResourceQuantity ConstructionProgress
		{
			get;
			set;
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

		public ResourceQuantity Cost
		{
			get { return Template.Cost.Evaluate(this); }
		}

		public int HitChance
		{
			// TODO - moddable facility hit chance
			get { return 1000; }
		}

		public int Hitpoints
		{
			get;
			set;
		}

		public int HullHitpoints
		{
			get { return Hitpoints; }
		}

		[DoNotSerialize]
		public Image Icon
		{
			get { return Template.Icon; }
		}

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths
		{
			get
			{
				return Template.IconPaths;
			}
		}

		public long ID
		{
			get;
			set;
		}

		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { return Abilities; }
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}

		public bool IsDisposed { get; set; }

		public bool IsObsolescent
		{
			get
			{
				return Template.IsObsolescent;
			}
		}

		/// <summary>
		/// Facilities cannot be manually obsoleted; they are only obsoleted by unlocking new ones.
		/// </summary>
		public bool IsObsolete
		{
			get { return this.IsObsolescent; }
		}

		public Facility LatestVersion
		{
			get
			{
				if (IsObsolescent)
					return Template.Instantiate();
				else
					return this;
			}
		}

		public int MaxArmorHitpoints
		{
			get { return 0; }
		}

		public int MaxHitpoints
		{
			get
			{
				// TODO - moddable facility HP
				return Cost.Sum(x => x.Value) / 10;
			}
		}

		public int MaxHullHitpoints
		{
			get { return MaxHitpoints; }
		}

		public int MaxNormalShields
		{
			get { return 0; }
		}

		public int MaxPhasedShields
		{
			get { return 0; }
		}

		public int MaxShieldHitpoints
		{
			get { return 0; }
		}

		public string Name { get { return Template.Name.Evaluate(this); } }

		public IEnumerable<Facility> NewerVersions
		{
			get { return Galaxy.Current.FindSpaceObjects<Planet>().Where(p => p.HasColony).SelectMany(p => p.Colony.Facilities).Where(f => Template.UpgradesTo(f.Template)); }
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

		public IEnumerable<Facility> OlderVersions
		{
			get { return Galaxy.Current.FindSpaceObjects<Planet>().Where(p => p.HasColony).SelectMany(p => p.Colony.Facilities).Where(f => f.Template.UpgradesTo(Template)); }
		}

		[DoNotSerialize(false)]
		public Empire Owner
		{
			get
			{
				return Container?.Owner;
			}
			set
			{
				// HACK - transfer ownership of entire colony since facilities can only belong to colony owner anyway
				if (Container != null && Container.Colony != null)
					Container.Colony.Owner = value;
			}
		}

		public IEnumerable<IAbilityObject> Parents
		{
			get
			{
				if (Container != null)
					yield return Container;
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

		[DoNotSerialize]
		public Image Portrait
		{
			get { return Template.Portrait; }
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				return Template.PortraitPaths;
			}
		}

		public IMobileSpaceObject RecycleContainer
		{
			get { return Container; }
		}

		public ResourceQuantity ScrapValue
		{
			get { return Cost * Mod.Current.Settings.ScrapFacilityReturnRate / 100; }
		}

		public int ShieldHitpoints
		{
			get { return 0; }
		}

		/// <summary>
		/// The template for this facility.
		/// Specifies the basic stats of the facility and its abilities.
		/// </summary>
		[DoNotSerialize]
		public FacilityTemplate Template { get { return template; } private set { template = value; } }

		IConstructionTemplate IConstructable.Template
		{
			get { return Template; }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

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

		private ModReference<FacilityTemplate> template { get; set; }

		public void Dispose()
		{
			if (IsDisposed)
				return;
			if (Container != null)
			{
				var col = Container.Colony;
				col.Facilities.Remove(this);
				col.UpdateEmpireMemories();
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

		// TODO - dynamic formula evaluation
		public void Recycle(IRecycleBehavior behavior, bool didExecute = false)
		{
			// TODO - need to do more stuff to recycle?
			if (!didExecute)
				behavior.Execute(this, true);
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

		public void ReplenishShields(int? amount = null)
		{
			// do nothing
		}

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			// HACK - we reduced max HP of facilities
			if (Hitpoints > MaxHitpoints)
				Repair();

			int damage = hit.NominalDamage;
			var realhit = new Hit(hit.Shot, this, damage);
			var df = realhit.Shot.DamageType.FacilityDamage.Evaluate(realhit);
			var dp = realhit.Shot.DamageType.FacilityPiercing.Evaluate(realhit);
			var factoredDmg = df.PercentOfRounded(damage);
			var piercing = dp.PercentOfRounded(damage);
			var realdmg = Math.Min(Hitpoints, factoredDmg);
			var nominalDamageSpent = (int)(realdmg / ((df + dp) / 100d));
			Hitpoints -= realdmg;
			if (IsDestroyed)
				Dispose();
			return damage - nominalDamageSpent;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}