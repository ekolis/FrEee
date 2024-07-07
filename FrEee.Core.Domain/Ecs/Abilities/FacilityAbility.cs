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
using FrEee.Processes.Combat;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Objects.Technology;
using FrEee.Ecs.Stats;

namespace FrEee.Ecs.Abilities;

/// <summary>
/// Marks an entity with <see cref="SemanticScope.Facility"/>
/// and provides any required data for the entity to be a facility
/// on a colony.
/// </summary>
public class FacilityAbility : SemanticScopeAbility,
	IConstructable, IDamageable, IDisposable, IFormulaHost, IRecyclable, IUpgradeable<FacilityAbility>, IDataObject
{
	public FacilityAbility(
		IEntity entity,
		IEntity colony,
		FacilityTemplate template
	) : base(
		entity,
		AbilityRule.Find(SemanticScope.Facility.Name),
		scope: new LiteralFormula<string>(SemanticScope.Facility.Name),
		new LiteralFormula<int>(1) // TODO: variable size facility templates
	)
	{
		Colony = colony;
		Template = template;
		ConstructionProgress = [];
		Hitpoints = MaxHitpoints;
	}
	
	public FacilityAbility(
		IEntity entity,
		AbilityRule rule,
		Formula<string>? description,
		IFormula[] values
	) : this(entity, colony: null, template: null)
	{
	}

	
	public IEntity Colony { get; private set; }

	/// <summary>
	/// TODO - "armor" facilities that are hit before other facilities on a planet?
	/// </summary>
	public int ArmorHitpoints
	{
		get { return 0; }
	}

	public ResourceQuantity ConstructionProgress { get; set; }

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

	public FacilityAbility LatestVersion
	{
		get
		{
			if (IsObsolescent)
				return Template.Instantiate().GetAbility<FacilityAbility>();
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

	public IEnumerable<FacilityAbility> NewerVersions
		=> Galaxy.Current.Find<FacilityAbility>().Where(q => Template.UpgradesTo(q.Template));

	public IEnumerable<FacilityAbility> OlderVersions
		=> Galaxy.Current.Find<FacilityAbility>().Where(q => q.Template.UpgradesTo(Template));

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

	[DoNotSerialize(false)]
	public Empire Owner
	{
		get
		{
			return Colony.GetOwner();
		}
		set
		{
			Colony.SetOwner(value);
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
		get { return Entity.Ancestors().OfType<IMobileSpaceObject>().First(); }
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

	public IDictionary<string, object> Variables
	{
		get
		{
			return new Dictionary<string, object>
			{
				{"colony", Colony},
				// TODO: check colony's world entity
				{"planet", Colony.Parents.OfType<Planet>().First()},
				{"empire", Owner}
			};
		}
	}

	[SerializationPriority(1)]
	private ModReference<FacilityTemplate> template { get; set; }

	public override SafeDictionary<string, object> Data
	{
		get
		{
			var dict = base.Data;
			if (ConstructionProgress != Cost)
				dict.Add(nameof(ConstructionProgress), ConstructionProgress);
			if (Hitpoints != MaxHitpoints)
				dict.Add(nameof(Hitpoints), Hitpoints);
			dict.Add(nameof(ID), ID);
			dict.Add(nameof(template), template);
			dict.Add(nameof(Colony), Colony);
			return dict;
		}
		set
		{
			base.Data = value;
			template = (ModReference<FacilityTemplate>)value[nameof(template)]; // comes first because other properties depend on its data
			ConstructionProgress = (ResourceQuantity)(value[nameof(ConstructionProgress)] ?? Cost);
			Hitpoints = (int)(value[nameof(Hitpoints)] ?? MaxHitpoints);
			ID = (long)value[nameof(ID)];
			Colony = (Colony)value[nameof(Colony)];
		}
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		if (Entity != null)
		{
			var col = Entity.Ancestors().OfType<Colony>().First();
			col.FacilityAbilities.Remove(this);
			col.UpdateEmpireMemories();
		}
	}

	/// <summary>
	/// Places the facility.
	/// </summary>
	/// <param name="sobj">Must be a colonized planet.</param>
	public void Place(ISpaceObject sobj)
	{
		// TODO: allow colony to be placed on any entity with the World semantic scope
		if (sobj is Planet)
		{
			var planet = (Planet)sobj;
			if (planet.Colony == null)
				throw new ArgumentException("Facilities can only be placed on colonized planets.");
			if (planet.Colony.Facilities.Sum(q => q.GetStatValue<int>(StatType.FacilitySize)) >= planet.MaxFacilities)
				planet.Colony.Owner.Log.Add(planet.CreateLogMessage(this + " cannot be constructed at " + planet + " because there is no more space available for facilities there.", Objects.LogMessages.LogMessageType.Warning));
			else
				planet.Colony.FacilityAbilities.Add(this);
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