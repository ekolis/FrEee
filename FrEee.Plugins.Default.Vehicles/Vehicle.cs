using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Vehicles.Types;
using FrEee.Processes.Construction;
using FrEee.Plugins.Default.Vehicles.Types;
using FrEee.Vehicles;

namespace FrEee.Plugins.Default.Vehicles;

/// <summary>
/// A ship, base, or unit.
/// </summary>
[Serializable]
public abstract class Vehicle : INamed, IConstructable, IVehicle, ICombatant, IFoggable
{
	public Vehicle()
	{
		Components = new List<Component>();
		ConstructionProgress = new ResourceQuantity();
	}

	// components can't be directly referenced, only by index
	public virtual IEnumerable<IReferrable> Referrables => [];

	public abstract AbilityTargets AbilityTarget { get; }

	public int Accuracy
	{
		get
		{
			return
				this.GetAbilityValue("Combat To Hit Offense Plus").ToInt()
				- this.GetAbilityValue("Combat To Hit Offense Minus").ToInt()
				+ (Owner == null || Owner.Culture == null ? 0 : Owner.Culture.SpaceCombat)
				+ Sector.GetEmpireAbilityValue(Owner, "Combat Modifier - Sector").ToInt()
				+ StarSystem.GetEmpireAbilityValue(Owner, "Combat Modifier - System").ToInt()
				+ Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
		}
	}

	/// <summary>
	/// Current HP of all armor components.
	/// </summary>
	public int ArmorHitpoints
	{
		get
		{
			return Components.Sum(c => c.ArmorHitpoints);
		}
	}

	public Progress ArmorHitpointsFill
	{
		get { return new Progress(ArmorHitpoints, MaxArmorHitpoints); }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get
		{
			if (Design == null)
				return Components;
			return Components.Cast<IAbilityObject>().Append(Design.Hull);
		}
	}

	public abstract double CombatSpeed { get; }

	/// <summary>
	/// The components on this vehicle.
	/// Needs to be protected, not private, so reflection can find it from a derived class.
	/// Can change CommonExtensions.CopyEnumerableProperties to scan base classes, though...
	/// </summary>
	[DoNotSerialize(false)]
	public IList<Component> Components { get; protected set; }

	public ResourceQuantity ConstructionProgress
	{
		get;
		set;
	}

	public ResourceQuantity Cost
	{
		get
		{
			if (!Components.Any())
				return new ResourceQuantity();
			return Design.Hull.Cost + Components.Select(c => c.Template.Cost).Aggregate((c1, c2) => c1 + c2);
		}
	}

    /// <summary>
    /// Damage that has been applied to this vehicle's components.
    /// </summary>
    /// <remarks>
    /// <see cref="MountedComponentTemplate"/> and <see cref="Component"/>
    /// don't implement <see cref="IReferrable"/> or <see cref="IModObject"/>
    /// so we can't use a <see cref="GameReferenceKeyedDictionary{TKey, TValue}"/>
    /// or <see cref="ModReferenceKeyedDictionary{TKey, TValue}"/> here.
    /// </remarks>
    // TODO: remove this property in favor of DamageList after game is over
	[Obsolete("Use DamageList instead.")]
    public SafeDictionary<MountedComponentTemplate, IList<int>> Damage
    {
        get
        {
            var dict = new SafeDictionary<MountedComponentTemplate, IList<int>>();
            foreach (var c in Components)
            {
                if (c.Hitpoints != c.MaxHitpoints)
                {
                    if (!dict.ContainsKey(c.Template))
                        dict.Add(c.Template, new List<int>());
                    dict[c.Template].Add(c.MaxHitpoints - c.Hitpoints);
                }
            }
            return dict;
        }
        set
        {
			// this crashes, use DamageList instead
			/*
            Components = new List<Component>();
            foreach (var template in Design.Components)
            {
                var component = template.Instantiate();
                Components.Add(component);
            }
            foreach (var kvp in value)
            {
                var template = kvp.Key;
                var damages = kvp.Value;
                for (var i = 0; i < damages.Count; i++)
                {
                    var component = Components.Where(c => c.Template == template).ElementAt(i);
                    component.Hitpoints = component.MaxHitpoints - damages[i];
                }
            }*/
        }
    }

    public List<int> DamageList
	{
		get
		{
			List<int> result = [];
			foreach (var c in Components)
			{
				result.Add(c.MaxHitpoints - c.Hitpoints);
			}
			return result;
		}
		set
		{
			// rebuild component list
			Components = [.. Design.Components.Select(q => q.Instantiate())];

			if (value is not null)
			{
                // apply damage to components
                foreach ((Component component, int damage) in Components.Zip(value))
				{
					component.Hitpoints = component.MaxHitpoints - damage;
				}
			}
		}
	}

	/// <summary>
	/// The design of this vehicle.
	/// </summary>
	[SerializationPriority(1)]
	public IDesign Design { get; set; }

	[DoNotSerialize]
	public int EmergencySpeed { get; set; }

	public int Evasion
	{
		get
		{
			return
				this.GetAbilityValue("Combat To Hit Defense Plus").ToInt()
				- this.GetAbilityValue("Combat To Hit Defense Minus").ToInt()
				+ (Owner == null || Owner.Culture == null ? 0 : Owner.Culture.SpaceCombat)
				+ Sector.GetEmpireAbilityValue(Owner, "Combat Modifier - Sector").ToInt()
				+ StarSystem.GetEmpireAbilityValue(Owner, "Combat Modifier - System").ToInt()
				+ Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
		}
	}

	public int HitChance
	{
		get { return 1; }
	}

	[DoNotSerialize(false)]
	public int Hitpoints
	{
		get
		{
			return Components.Sum(c => c.Hitpoints);
		}
		set
		{
			throw new NotSupportedException("Cannot directly set the hitpoints of a vehicle. Its hitpoints are determined by its components.");
		}
	}

	public IHull Hull { get { return Design.Hull; } }

	/// <summary>
	/// Current HP of all non-armor components.
	/// </summary>
	public int HullHitpoints
	{
		get
		{
			return Components.Sum(c => c.HullHitpoints);
		}
	}

	public Progress HullHitpointsFill
	{
		get { return new Progress(HullHitpoints, MaxHullHitpoints); }
	}

	[DoNotSerialize]
	public Image Icon
	{
		get { return Design.Icon; }
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return Design.IconPaths;
		}
	}

	public long ID
	{
		get;
		set;
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { yield break; }
	}

	public bool IsAlive => !IsDestroyed;

	/// <summary>
	/// Is this vehicle destroyed?
	/// Vehicles are destroyed when all internal (non-armor) components are destroyed, or when they are disposed.
	/// </summary>
	public bool IsDestroyed { get { return IsDisposed || Components.All(c => c.IsDestroyed || c.HasAbility("Armor")); } }

	public bool IsDisposed { get; set; }

	public bool IsMemory
	{
		get;
		set;
	}

	public bool IsObsolescent
	{
		get { return Design.IsObsolescent; }
	}

	public bool IsObsolete
	{
		get { return Design.IsObsolete; }
	}

	public bool IsOurs { get { return Owner == Empire.Current; } }

	/// <summary>
	/// Creates an upgraded version of this vehicle if it can be upgraded.
	/// </summary>
	public IVehicle LatestVersion
	{
		get
		{
			if (IsObsolescent)
				return Design.LatestVersion.Instantiate();
			else
				return this;
		}
	}

	public ResourceQuantity MaintenanceCost
	{
		get
		{
			double pct;
			if (Design.Hull.VehicleType == VehicleTypes.Ship || Design.Hull.VehicleType == VehicleTypes.Base)
				pct = Mod.Current.Settings.ShipBaseMaintenanceRate;
			else
				pct = Mod.Current.Settings.UnitMaintenanceRate;

			if (pct > 0)
			{
				if (Sector != null)
					pct -= Sector.GetEmpireAbilityValue(Owner, "Reduced Maintenance Cost - Sector").ToInt();
				if (StarSystem != null)
					pct -= StarSystem.GetEmpireAbilityValue(Owner, "Reduced Maintenance Cost - System").ToInt();
				if (Owner != null)
				{
					pct -= Owner.GetAbilityValue("Reduced Maintenance Cost - Empire").ToInt();
					pct -= Owner.Culture.MaintenanceReduction;
					if (Owner.PrimaryRace.Aptitudes.ContainsKey(Aptitude.Maintenance.Name))
						pct -= Owner.PrimaryRace.Aptitudes[Aptitude.Maintenance.Name] - 100;
				}
				pct *= (100d + this.GetAbilityValue("Modified Maintenance Cost").ToInt()) / 100d;
				return Cost * pct / 100d;
			}
			else
				return new ResourceQuantity();
		}
	}

	public int MaxArmorHitpoints
	{
		get { return Components.Sum(c => c.MaxArmorHitpoints); }
	}

	public int MaxHitpoints
	{
		get { return Components.Sum(c => c.MaxHitpoints); }
	}

	public int MaxHullHitpoints
	{
		get { return Components.Sum(c => c.MaxHullHitpoints); }
	}

	public int MaxNormalShields
	{
		get
		{
			var shields = MaxUnmodifiedNormalShields;
			var addmods = (double)MaxUnmodifiedNormalShields / (MaxUnmodifiedNormalShields + MaxUnmodifiedPhasedShields) * -Sector.GetAbilityValue("Sector - Shield Disruption").ToInt();
			if (double.IsInfinity(addmods) || double.IsNaN(addmods))
				addmods = 0;
			return Math.Max(0, (100 + ShieldPercentageModifiers).PercentOfRounded(shields)) + (int)addmods;
		}
	}

	public int MaxPhasedShields
	{
		get
		{
			var shields = MaxUnmodifiedPhasedShields;
			var addmods = (double)MaxUnmodifiedPhasedShields / (MaxUnmodifiedNormalShields + MaxUnmodifiedPhasedShields) * -Sector.GetAbilityValue("Sector - Shield Disruption").ToInt();
			if (double.IsInfinity(addmods) || double.IsNaN(addmods))
				addmods = 0;
			return Math.Max(0, (100 + ShieldPercentageModifiers).PercentOfRounded(shields)) + (int)addmods;
		}
	}

	public int MaxShieldHitpoints
	{
		get { return MaxNormalShields + MaxPhasedShields; }
	}

	public abstract int MaxTargets { get; }

	public int MaxUnmodifiedNormalShields
	{
		get
		{
			return
				this.GetAbilityValue("Shield Generation").ToInt()
				+ this.GetAbilityValue("Planet - Shield Generation").ToInt();
		}
	}

	public int MaxUnmodifiedPhasedShields
	{
		get
		{
			return this.GetAbilityValue("Phased Shield Generation").ToInt();
		}
	}

	public double MerchantsRatio => Owner.HasAbility("No Spaceports") ? 1.0 : 0.0;

	/// <summary>
	/// Resource cost per turn to maintain this vehicle.
	/// </summary>
	public int MineralsMaintenance
	{
		get { return MaintenanceCost[Resource.Minerals]; }
	}

	/// <summary>
	/// The name of this vehicle.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Any vehicles that use a newer version of this vehicle's design.
	/// </summary>
	public IEnumerable<IVehicle> NewerVersions
	{
		get
		{
			return Galaxy.Current.FindSpaceObjects<IVehicle>().Where(v => Design.UpgradesTo(v.Design));
		}
	}

	/// <summary>
	/// The current amount of shields.
	/// </summary>
	public int NormalShields { get; set; }

	/// <summary>
	/// Any vehicles that use an older version of this vehicle's design.
	/// </summary>
	public IEnumerable<IVehicle> OlderVersions
	{
		get
		{
			return Galaxy.Current.FindSpaceObjects<IVehicle>().Where(v => v.Design.UpgradesTo(Design));
		}
	}

	public int OrganicsMaintenance
	{
		get { return MaintenanceCost[Resource.Organics]; }
	}

	/// <summary>
	/// The owner of this vehicle.
	/// </summary>
	public Empire Owner { get; set; }

	public virtual IEnumerable<IAbilityObject> Parents
	{
		get { yield return Owner; }
	}

	/// <summary>
	/// Does this vehicle participate in ground combat?
	/// </summary>
	public abstract bool ParticipatesInGroundCombat
	{
		get;
	}

	/// <summary>
	/// The current amount of phased shields.
	/// </summary>
	public int PhasedShields { get; set; }

	[DoNotSerialize]
	public Image Portrait
	{
		get { return Design.Portrait; }
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			return Design.PortraitPaths;
		}
	}

	public int RadioactivesMaintenance
	{
		get { return MaintenanceCost[Resource.Radioactives]; }
	}

	public abstract IMobileSpaceObject RecycleContainer { get; }

	public ResourceQuantity RemoteMiningIncomePercentages
	{
		get
		{
			return Owner.PrimaryRace.IncomePercentages;
		}
	}

	public bool RequiresColonyQueue
	{
		get { return false; }
	}

	public abstract bool RequiresSpaceYardQueue { get; }
	/*public IEnumerable<string> IconPaths
	{
		get { return Design.IconPaths; }
	}

	public IEnumerable<string> PortraitPaths
	{
		get { return Design.PortraitPaths; }
	}*/

	/// <summary>
	/// Vehicles have no resource value.
	/// </summary>
	public ResourceQuantity ResourceValue
	{
		get { return new ResourceQuantity(); }
	}

	public ResourceQuantity ScrapValue
	{
		get
		{
			double ratio;
			if (this is Ship || this is Base)
				ratio = Mod.Current.Settings.ScrapShipOrBaseReturnRate;
			else
				ratio = Mod.Current.Settings.ScrapUnitReturnRate;
			var val = Cost * ratio / 100;
			if (this is ICargoContainer)
			{
				var cc = (ICargoContainer)this;
				if (cc.Cargo != null)
					val += cc.Cargo.Units.Sum(u => u.ScrapValue);
			}
			return val;
		}
	}

	public abstract Sector Sector { get; set; }

	/// <summary>
	/// Total current shield HP.
	/// </summary>
	public int ShieldHitpoints { get { return NormalShields + PhasedShields; } }

	public Progress ShieldHitpointsFill
	{
		get { return new Progress(ShieldHitpoints, MaxShieldHitpoints); }
	}

	public int ShieldPercentageModifiers
	{
		get
		{
			// TODO: make these multiplicative, at least some of them?
			return
				+Sector.GetEmpireAbilityValue(Owner, "Shield Modifier - Sector").ToInt()
				+ StarSystem.GetEmpireAbilityValue(Owner, "Shield Modifier - System").ToInt()
				+ Owner.GetAbilityValue("Shield Modifier - Empire").ToInt();
		}
	}

	public int ShieldAdditiveModifiers
		=> -Sector.GetAbilityValue("Sector - Shield Disruption").ToInt();

	public int Size
	{
		get { return Design.Hull.Size; }
	}

	public ResourceQuantity StandardIncomePercentages
	{
		get
		{
			return Owner.PrimaryRace.IncomePercentages;
		}
	}

	public abstract StarSystem StarSystem { get; }

	/// <summary>
	/// The speed of the vehicle, taking into account hull mass, thrust, and speed bonuses.
	/// </summary>
	public virtual int StrategicSpeed
	{
		get
		{
			// no Engines Per Move rating? then no movement
			if (Design.Hull.ThrustPerMove == 0)
				return 0;

			// can't go anywhere without thrust!
			var thrust = this.GetAbilityValue("Standard Ship Movement").ToInt();
			if (thrust < Design.Hull.ThrustPerMove)
				return 0;

			// take into account base speed plus all bonuses
			return
				thrust / Design.Hull.ThrustPerMove
				+ this.GetAbilityValue("Movement Bonus").ToInt()
				+ this.GetAbilityValue("Extra Movement Generation").ToInt()
				+ this.GetAbilityValue("Vehicle Speed").ToInt()
				+ EmergencySpeed;
		}
	}

	public IConstructionTemplate Template
	{
		get { return Design; }
	}

	public double Timestamp { get; set; }

	/// <summary>
	/// The undamaged weapons installed on this vehicle.
	/// </summary>
	public IEnumerable<Component> Weapons
	{
		get
		{
			return Components.Where(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo != null);
		}
	}

	public abstract WeaponTargets WeaponTargetType { get; }

	public bool CanTarget(ITargetable target)
	{
		// TODO - alliances
		return target.Owner != Owner && Weapons.Any(c => !c.IsDestroyed && c.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType));
	}

	public abstract Visibility CheckVisibility(Empire emp);

	public virtual void Dispose()
	{
		if (IsDisposed)
			return;
		IsDisposed = true;
		Game.Current.UnassignID(this);
		if (!IsMemory)
			this.UpdateEmpireMemories();
		if (this is IUnit u)
			u.Container?.RemoveUnit(u);
	}

	public bool IsHostileTo(Empire emp)
	{
		return Owner == null ? false : Owner.IsEnemyOf(emp, StarSystem);
	}

	public abstract bool IsObsoleteMemory(Empire emp);

	public abstract void Place(ISpaceObject target);

	public void Recycle(IRecycleBehavior behavior, bool didExecute = false)
	{
		// TODO - need to do more stuff to recycle?
		if (!didExecute)
			behavior.Execute(this, true);
	}

	public virtual void Redact(Empire emp)
	{
		var visibility = CheckVisibility(emp);

		// Can't see the ship's components if it's not scanned
		// and can't see the design either if we haven't scanned it before
		if (visibility < Visibility.Scanned)
		{
			// TODO - hide design of vehicle that has never been scanned before, even if we know the design?
			if (Design.CheckVisibility(emp) < Visibility.Scanned)
			{
				// create fake design
				var d = Services.Designs.CreateDesign(Design.VehicleType);
				d.Hull = Design.Hull;
				d.Owner = Design.Owner;
				Design = d;

				// set name of ship so we can't guess what design it is
				Name = (Owner?.Name ?? "Unowned") + " " + Hull.Name;

				// clear component list if design is not known
				Components.Clear();
			}

			// can't see HP of components unless scanned, so pretend the ship is fully repaired
			Repair();
		}

		if (visibility < Visibility.Fogged || visibility < Visibility.Visible && !IsMemory)
			Dispose();
	}

	/// <summary>
	/// Repairs a specified number of components.
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	public int? Repair(int? amount = null)
	{
		if (IsDestroyed)
			return amount; // destroyed vehicles cannot be repaired, keptin!
		if (amount == null)
		{
			foreach (var comp in Components)
				comp.Repair();
		}
		else
		{
			// repair most-damaged components first
			// TODO - other repair priorities
			foreach (var comp in Components.Where(x => x.Hitpoints < x.MaxHitpoints).OrderBy(c => c.Hitpoints / (double)c.MaxHitpoints))
			{
				if (amount <= 0)
					break;
				comp.Repair();
				amount--;
			}
		}
		return amount;
	}

	public virtual void ReplenishShields(int? amount = null)
	{
		if (amount == null)
		{
			NormalShields = MaxNormalShields;
			PhasedShields = MaxPhasedShields;
		}
		else
		{
			PhasedShields += amount.Value;
			if (PhasedShields > MaxPhasedShields)
			{
				var overflow = PhasedShields - MaxPhasedShields;
				PhasedShields = MaxPhasedShields;
				NormalShields += overflow;
				if (NormalShields > MaxNormalShields)
					NormalShields = MaxNormalShields;
			}
		}
	}

	public int TakeDamage(Hit hit, PRNG dice = null)
	{
		int damage = hit.NominalDamage;

		if (IsDestroyed)
			return damage; // she canna take any more!

		// let shields mitigate incoming damage
		damage = this.TakeShieldDamage(hit, damage, dice);

		// TODO - make sure we have components that are not immune to the damage type so we don't get stuck in an infinite loop

		// emissive armor negates a certain amount of damage that penetrates the shields
		// TODO - emissive should be ineffective vs. armor piercing damage
		var emissive = this.GetAbilityValue("Emissive Armor").ToInt();
		var dt = hit.Shot?.DamageType ?? DamageType.Normal;
		damage -= (int)Math.Round(emissive * dt.EmissiveArmor.Evaluate(hit).Percent());

		while (damage > 0 && !IsDestroyed)
		{
			// save off damage counter for shield generation from damage ability
			var sgfdStart = damage;
			var sgfdAbility = this.GetAbilityValue("Shield Generation From Damage").ToInt();

			var comps = Components.Where(c => c.Hitpoints > 0 && dt.ComponentPiercing.Evaluate(new Hit(hit.Shot, c, hit.NominalDamage)) < 100);
			var armor = comps.Where(c => c.HasAbility("Armor"));
			var internals = comps.Where(c => !c.HasAbility("Armor"));
			var canBeHit = armor.Any() ? armor : internals;
			var comp = canBeHit.Where(c =>
				{
					// skip components that are completely pierced by this hit
					var hit2 = new Hit(hit.Shot, c, damage);
					return dt.ComponentPiercing.Evaluate(hit2) < 100;
				}).ToDictionary(c => c, c => c.HitChance).PickWeighted(dice);
			if (comp == null)
				break; // no more components to hit
			var comphit = new Hit(hit.Shot, comp, damage);
			damage = comp.TakeDamage(comphit, dice);

			// shield generation from damage
			var sgfd = dt.ShieldGenerationFromDamage.Evaluate(hit).PercentOfRounded(Math.Min(sgfdStart - damage, sgfdAbility));
			ReplenishShields(sgfd);
		}

		if (IsDestroyed)
		{
			if (this is Ship || this is Base)
			{
				// trigger ship lost happiness changes
				Owner?.TriggerHappinessChange(hm => hm.OurShipLost);
				Owner?.TriggerHappinessChange(StarSystem, hm => hm.OurShipLostInSystem);
			}
			Dispose();
		}

		// update memory sight
		if (!IsMemory)
			this.UpdateEmpireMemories();

		return damage;
	}

	public override string ToString()
	{
		return Name;
	}

	IEnumerable<Component> ICombatant.Components => Components;

	public abstract bool FillsCombatTile { get; }

	/// <summary>
	/// By default, vehicles don't detonate when enemies enter their sector.
	/// </summary>
	public virtual bool DetonatesWhenEnemiesEnterSector => false;
}
