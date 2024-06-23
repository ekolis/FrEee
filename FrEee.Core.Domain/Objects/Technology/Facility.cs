using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Extensions;
using FrEee.Serialization;
using FrEee.Utility;
using FrEee.Processes.Combat;
using FrEee.Ecs;
using FrEee.Ecs.Abilities;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Objects.Technology;

/// <summary>
/// A large immobile installation on a colony.
/// </summary>
[Serializable]
[Obsolete("Use an IEntity wih a FacilityAbility.")]
public class Facility : IEntity, IOwnableEntity, IConstructable, IDamageable, IDisposable, IContainable<Planet>, IFormulaHost, IRecyclable, IUpgradeable<Facility>, IDataObject
{
	public Facility(FacilityTemplate template)
	{
		Template = template;
		ConstructionProgress = new ResourceQuantity();
		Hitpoints = MaxHitpoints;
		Abilities = template.Abilities.Select(q => q.Copy()).ToList();
		this.GetAbility<SemanticScopeAbility>().Container = this;
	}

	public IEnumerable<Ability> Abilities { get; set; }

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

	public IEnumerable<IEntity> Children
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
		get { return Abilities ?? Enumerable.Empty<Ability>(); }
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
		get { return Galaxy.Current.FindSpaceObjects<Planet>().Where(p => p.HasColony).SelectMany(p => p.Colony.Facilities).Cast<Facility>().Where(f => Template.UpgradesTo(f.Template)); }
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
		// TODO: flesh out FacilityAbility so any entity can be a facility, not just a Facility object
		get { return Galaxy.Current.FindSpaceObjects<Planet>().Where(p => p.HasColony).SelectMany(p => p.Colony.Facilities).Cast<Facility>().Where(f => f.Template.UpgradesTo(Template)); }
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

	public IEnumerable<IEntity> Parents
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

	[SerializationPriority(1)]
	private ModReference<FacilityTemplate> template { get; set; }

	public SafeDictionary<string, object> Data
	{
		get
		{
			var dict = new SafeDictionary<string, object>();
			if (ConstructionProgress != Cost)
				dict.Add(nameof(ConstructionProgress), ConstructionProgress);
			if (Hitpoints != MaxHitpoints)
				dict.Add(nameof(Hitpoints), Hitpoints);
			dict.Add(nameof(ID), ID);
			dict.Add(nameof(template), template);
			dict.Add(nameof(Abilities), Abilities);
			return dict;
		}
		set
		{
			template = (ModReference<FacilityTemplate>)value[nameof(template)]; // comes first because other properties depend on its data
			ConstructionProgress = (ResourceQuantity)(value[nameof(ConstructionProgress)] ?? Cost);
			Hitpoints = (int)(value[nameof(Hitpoints)] ?? MaxHitpoints);
			ID = (long)value[nameof(ID)];
			Abilities = (IEnumerable<Ability>)value[nameof(Abilities)];
		}
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		if (Container != null)
		{
			var col = Container.Colony;
			col.FacilityAbilities.Remove(this.GetAbility<SemanticScopeAbility>());
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
			if (planet.Colony.Facilities.Count() >= planet.MaxFacilities)
				planet.Colony.Owner.Log.Add(planet.CreateLogMessage(this + " cannot be constructed at " + planet + " because there is no more space available for facilities there.", LogMessages.LogMessageType.Warning));
			else
				planet.Colony.FacilityAbilities.Add(this.GetAbility<SemanticScopeAbility>());
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