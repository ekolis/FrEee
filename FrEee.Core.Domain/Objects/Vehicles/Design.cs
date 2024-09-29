using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;
using FrEee.Modding.Abilities;
using FrEee.Gameplay.Commands;

namespace FrEee.Objects.Vehicles;

/// <summary>
/// Creates designs.
/// </summary>
public static class Design
{
	static Design()
	{
		militiaDesign = new Design<Troop>();
		militiaDesign.BaseName = "Militia";
		var militiaWeapon = new ComponentTemplate();
		militiaWeapon.Durability = Mod.Current.Settings.MilitiaHitpoints;
		militiaWeapon.Name = "Small Arms";
		militiaWeapon.WeaponInfo = new DirectFireWeaponInfo
		{
			Damage = Mod.Current.Settings.MilitiaFirepower,
			MinRange = 0,
			MaxRange = 1,
		};
		militiaDesign.Components.Add(new MountedComponentTemplate(militiaDesign, militiaWeapon));
	}

	public static IDesign Create(VehicleTypes vt)
	{
		IDesign d;
		switch (vt)
		{
			case VehicleTypes.Ship:
				d = new Design<Ship>();
				break;

			case VehicleTypes.Base:
				d = new Design<Base>();
				break;

			case VehicleTypes.Fighter:
				d = new Design<Fighter>();
				break;

			case VehicleTypes.Troop:
				d = new Design<Troop>();
				break;

			case VehicleTypes.Mine:
				d = new Design<Mine>();
				break;

			case VehicleTypes.Satellite:
				d = new Design<Satellite>();
				break;

			case VehicleTypes.Drone:
				d = new Design<Drone>();
				break;

			case VehicleTypes.WeaponPlatform:
				d = new Design<WeaponPlatform>();
				break;

			default:
				throw new Exception("Cannot create a design for vehicle type " + vt + ".");
		}
		d.Owner = Empire.Current;
		return d;
	}

	public static IDesign Create(IHull hull)
	{
		var d = Create(hull.VehicleType);
		d.Hull = hull;
		return d;
	}

	/// <summary>
	/// Imports designs from the library into the game that aren't already in the game.
	/// Requires a current empire. Should only be called client side.
	/// </summary>
	/// <returns>Copied designs imported.</returns>
	public static IEnumerable<IDesign> ImportFromLibrary()
	{
		if (Empire.Current == null)
			throw new InvalidOperationException("Can't import designs without a current empire.");

		var designs = Library.Import<IDesign>(d => d.IsValidInMod && !Empire.Current.KnownDesigns.Any(d2 => d2.Equals(d))).ToArray();

		designs.SafeForeach(d =>
		{
			d.IsNew = true;
			d.Owner = Empire.Current;
			d.TurnNumber = Game.Current.TurnNumber;
			d.Iteration = Empire.Current.KnownDesigns.OwnedBy(Empire.Current).Where(x => x.BaseName == d.BaseName && x.IsUnlocked()).MaxOrDefault(x => x.Iteration) + 1; // auto assign nex available iteration
			d.IsObsolete = d.IsObsolescent;
			Empire.Current.KnownDesigns.Add(d); // only client side, don't need to worry about other players spying :)
		});

		return designs;
	}

	public static Design<Troop> militiaDesign;

	public static Design<Troop> MilitiaDesign => militiaDesign;
}

/// <summary>
/// A vehicle design.
/// </summary>
/// <typeparam name="T">The type of vehicle.</typeparam>
[Serializable]
public class Design<T> : IDesign<T>, ITemplate<T> where T : IVehicle
{
	public Design()
	{
		Components = new List<MountedComponentTemplate>();
		Iteration = 1;
	}

	public IEnumerable<Ability> Abilities
	{
		get { return Hull.Abilities.Concat(Components.SelectMany(c => c.UnstackedAbilities)).Stack(this); }
	}

	public AbilityTargets AbilityTarget
	{
		get { return Hull == null ? AbilityTargets.None : Hull.AbilityTarget; }
	}

	public int Accuracy
	{
		get { return this.GetAbilityValue("Combat To Hit Offense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Offense Minus").ToInt(); }
	}

	public int ArmorHitpoints
	{
		get { return this.Components.Where(c => c.ComponentTemplate.HasAbility("Armor")).Sum(c => c.Durability); }
	}

	public string BaseName { get; set; }

	public int CargoCapacity
	{
		get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
	}

	public int CargoStorage
	{
		get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
	}

	/// <summary>
	/// Designs contain abilities from their
	/// </summary>
	public IEnumerable<Ability> ChildAbilities
	{
		get { throw new NotImplementedException(); }
	}

	public IEnumerable<IAbilityObject> Children
	{
		get { return new IAbilityObject[] { Hull }.Concat(Components); }
	}

	public double CombatSpeed
		=> Mod.Current.Settings.CombatSpeedPercentPerStrategicSpeed.PercentOf(StrategicSpeed)
		+ this.GetAbilityValue("Combat Movement").ToDouble();

	/// <summary>
	/// The components used in this design.
	/// </summary>
	public IList<MountedComponentTemplate> Components
	{
		get;
		private set;
	}

	/// <summary>
	/// The resource cost to build the design.
	/// </summary>
	public ResourceQuantity Cost
	{
		get
		{
			if (!Components.Any())
				return Hull.Cost;
			return Hull.Cost + Components.Select(c => c.Cost).Sum();
		}
	}

	public int Evasion
	{
		get { return this.GetAbilityValue("Combat To Hit Defense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Defense Minus").ToInt(); }
	}

	[DoNotSerialize]
	IHull IDesign.Hull
	{
		get { return Hull; }
		set
		{
			if (value is Hull<T>)
				Hull = (Hull<T>)value;
			else
				throw new Exception("Can't use a " + value.VehicleType + " hull on a " + VehicleType + " design.");
		}
	}

	/// <summary>
	/// The hull used in this design.
	/// </summary>
	[DoNotSerialize]
	public IHull<T> Hull { get { return hull == null ? null : hull.Value; } set { hull = new ModReference<IHull<T>>(value); } }

	public int HullHitpoints
	{
		get { return this.Components.Where(c => !c.ComponentTemplate.HasAbility("Armor")).Sum(c => c.Durability); }
	}

	[DoNotSerialize]
	public Image Icon
	{
		get
		{
			if (Hull == null || Owner == null)
				return Pictures.GetGenericImage(GetType());
			return Hull.GetIcon(Owner.ShipsetPath);
		}
	}

	public Image Icon32 => Icon.Resize(32);

	public IEnumerable<string> IconPaths
	{
		get
		{
			return GetPaths("Mini");
		}
	}

	public long ID
	{
		get;
		set;
	}

	/// <summary>
	/// Designs don't have intrinsic abilities.
	/// </summary>
	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { yield break; }
	}

	public bool IsDisposed { get; set; }

	public bool IsMemory
	{
		get;
		set;
	}

	/// <summary>
	/// Is this a newly created design on the client side that needs to be sent to the server?
	/// </summary>
	[DoNotSerialize]
	public bool IsNew
	{
		get;
		set;
	}

	/// <summary>
	/// Does this design have components or hull which can be upgraded?
	/// </summary>
	/// <remarks>
	/// Once we have upgradeable hulls and mounts, those will be checked here as well.
	/// </remarks>
	public bool IsObsolescent
	{
		get
		{
			return Hull.IsObsolescent || Components.Any(c => c.IsObsolescent);
		}
	}

	/// <summary>
	/// Has this design been marked as obsolete by the player or an AI minister?
	/// </summary>
	public bool IsObsolete { get; set; }

	public bool IsValidInMod => hull.HasValue && Components.All(q => q.IsValidInMod);

	public int Iteration { get; set; }

	/// <summary>
	/// The latest iteration of this design.
	/// </summary>
	public IDesign<T> LatestVersion
	{
		get
		{
			return Owner.KnownDesigns.OfType<IDesign<T>>().Where(q =>
				q.Owner == Owner
				&& q.Name == Name
			).MaxBy(q => q.Iteration) ?? this;
		}
	}

	/// <summary>
	/// Creates an upgraded version of this design, with the latest upgraded components, etc.
	/// </summary>
	/// <returns></returns>
	public IDesign<T> Upgrade()
	{
		var copy = this.CopyAndAssignNewID();
		copy.Hull = Hull.LatestVersion;
		copy.TurnNumber = Game.Current.TurnNumber;
		copy.Owner = Empire.Current;
		copy.Iteration = Empire.Current.KnownDesigns.OwnedBy(Empire.Current).Where(x => x.BaseName == BaseName && x.IsUnlocked()).MaxOrDefault(x => x.Iteration) + 1; // auto assign nex available iteration
		copy.VehiclesBuilt = 0;
		copy.IsObsolete = false;

		// use real component templates and mounts from mod, not copies!
		copy.Components.Clear();
		foreach (var mct in Components)
		{
			// reuse templates so components appear "condensed" on vehicle designer
			var mount = mct.Mount == null ? null : mct.Mount.LatestVersion;
			var ct = mct.ComponentTemplate.LatestVersion;
			var same = copy.Components.FirstOrDefault(x => x.ComponentTemplate == ct && x.Mount == mount);
			if (same == null)
				copy.Components.Add(new MountedComponentTemplate(copy, ct, mount));
			else
				copy.Components.Add(same);
		}
		return copy;
	}

	public ResourceQuantity MaintenanceCost
	{
		get
		{
			double pct;
			if (Hull.VehicleType == VehicleTypes.Ship || Hull.VehicleType == VehicleTypes.Base)
				pct = Mod.Current.Settings.ShipBaseMaintenanceRate;
			else
				pct = Mod.Current.Settings.UnitMaintenanceRate;

			if (pct > 0)
			{
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

	IDesign IUpgradeable<IDesign>.LatestVersion
	{
		get { return LatestVersion; }
	}

	public string Name
	{
		get
		{
			if (Iteration == 1)
				return BaseName;
			else
				return BaseName + " " + Iteration.ToRomanNumeral();
		}
	}

	IEnumerable<IDesign> IUpgradeable<IDesign>.NewerVersions
	{
		get { return NewerVersions; }
	}

	public IEnumerable<IDesign<T>> NewerVersions
	{
		get
		{
			// TODO - check design library?
			return Game.Current.Referrables.OfType<IDesign<T>>().Where(d => d.Owner == Owner && d.BaseName == BaseName && d.Iteration > Iteration);
		}
	}

	IEnumerable<IDesign> IUpgradeable<IDesign>.OlderVersions
	{
		get { return OlderVersions; }
	}

	public IEnumerable<IDesign<T>> OlderVersions
	{
		get
		{
			// TODO - check design library?
			return Game.Current.Referrables.OfType<IDesign<T>>().Where(d => d.Owner == Owner && d.BaseName == BaseName && d.Iteration < Iteration);
		}
	}

	/// <summary>
	/// The empire which created this design.
	/// </summary>
	[DoNotSerialize]
	public Empire Owner { get { return owner; } set { owner = value; } }

	public IEnumerable<Ability> ParentAbilities
	{
		get { throw new NotImplementedException(); }
	}

	public IEnumerable<IAbilityObject> Parents
	{
		get
		{
			yield return Owner;
		}
	}

	[DoNotSerialize]
	public Image Portrait
	{
		get
		{
			if (Hull == null || Owner == null)
				return Pictures.GetGenericImage(GetType());
			return Hull.GetPortrait(Owner.ShipsetPath);
		}
	}

	public IEnumerable<string> PortraitPaths
	{
		get
		{
			return GetPaths("Portrait");
		}
	}

	public IEnumerable<T> RealVehicles
	{
		get { return Vehicles.Where(v => !v.IsMemory); }
	}

	public bool RequiresColonyQueue
	{
		get { return false; }
	}

	public bool RequiresSpaceYardQueue
	{
		get { return VehicleType == VehicleTypes.Ship || VehicleType == VehicleTypes.Base; }
	}

	public string ResearchGroup
	{
		get { return VehicleTypeName; }
	}

	/// <summary>
	/// The ship's role (design type in SE4).
	/// </summary>
	public string Role { get; set; }

	public int ShieldHitpoints
	{
		get { return this.GetAbilityValue("Planet - Shield Generation").ToInt() + this.GetAbilityValue("Shield Generation").ToInt() + this.GetAbilityValue("Phased Shield Generation").ToInt(); }
	}

	public int ShieldRegeneration
	{
		get { return this.GetAbilityValue("Shield Regeneration").ToInt(); }
	}

	/// <summary>
	/// Unused space on the design.
	/// </summary>
	public int SpaceFree
	{
		get
		{
			var hullsize = Hull == null ? 0 : Hull.Size;
			return hullsize - Components.Sum(comp => comp.Size);
		}
	}

	public int StrategicSpeed
	{
		get
		{
			// no Engines Per Move rating? then no movement
			if (Hull.ThrustPerMove == 0)
				return 0;
			var thrust = this.GetAbilityValue("Standard Ship Movement").ToInt();
			// TODO - make sure that Movement Bonus and Extra Movement are not in fact affected by Engines Per Move in SE4
			return thrust / Hull.ThrustPerMove + this.GetAbilityValue("Movement Bonus").ToInt() + this.GetAbilityValue("Extra Movement Generation").ToInt() + this.GetAbilityValue("Vehicle Speed").ToInt();
		}
	}

	public int SupplyStorage
	{
		get { return this.GetAbilityValue("Supply Storage").ToInt(); }
	}

	public int SupplyUsagePerSector
	{
		get
		{
			return Components.Where(comp => comp.HasAbility("Standard Ship Movement") || comp.HasAbility("Extra Movement Generation") || comp.HasAbility("Movement Bonus")).Sum(comp => comp.SupplyUsage);
		}
	}

	public double Timestamp
	{
		get;
		set;
	}

	/// <summary>
	/// The turn this design was created (for our designs) or discovered (for alien designs).
	/// </summary>
	public int TurnNumber { get; set; }

	public IList<Requirement<Empire>> UnlockRequirements
	{
		get
		{
			var list = new List<Requirement<Empire>>();
			foreach (var req in Hull.UnlockRequirements)
				list.Add(req);
			foreach (var req in Components.SelectMany(c => c.ComponentTemplate.UnlockRequirements))
				list.Add(req);
			foreach (var req in Components.SelectMany(c => c.Mount == null ? Enumerable.Empty<Requirement<Empire>>() : c.Mount.UnlockRequirements))
				list.Add(req);
			return list;
		}
	}

	public IEnumerable<Ability> UnstackedAbilities
	{
		get { return Hull.Abilities.Concat(Components.SelectMany(c => c.UnstackedAbilities)); }
	}

	public IEnumerable<T> Vehicles
	{
		get { return Game.Current.Referrables.OfType<T>().Where(v => v.Design == this); }
	}

	/// <summary>
	/// How many vehicles have been built using this design?
	/// Should not be visible for enemy designs.
	/// </summary>
	public int VehiclesBuilt { get; set; }

	public VehicleTypes VehicleType
	{
		get
		{
			if (typeof(T) == typeof(Ship))
				return VehicleTypes.Ship;
			if (typeof(T) == typeof(Base))
				return VehicleTypes.Base;
			if (typeof(T) == typeof(Fighter))
				return VehicleTypes.Fighter;
			if (typeof(T) == typeof(Satellite))
				return VehicleTypes.Satellite;
			if (typeof(T) == typeof(Troop))
				return VehicleTypes.Troop;
			if (typeof(T) == typeof(Drone))
				return VehicleTypes.Drone;
			if (typeof(T) == typeof(Mine))
				return VehicleTypes.Mine;
			if (typeof(T) == typeof(WeaponPlatform))
				return VehicleTypes.WeaponPlatform;
			throw new Exception("Invalid vehicle type " + typeof(T) + ".");
		}
	}

	public string VehicleTypeName
	{
		get
		{
			return VehicleType.ToSpacedString();
		}
	}

	public IEnumerable<string> Warnings
	{
		get
		{
			if (string.IsNullOrWhiteSpace(Name))
				yield return "You must give your design a name.";
			if (string.IsNullOrEmpty(Role))
				yield return "You must give your design a role.";
			if (Hull == null)
				yield return "You must select a hull for your design.";
			if (!Owner.HasUnlocked(Hull))
				yield return "You have not unlocked the " + Hull + ".";
			var comps = Components.Select(comp => comp.ComponentTemplate);
			if (Hull.NeedsBridge && (!comps.Any(comp => comp.HasAbility("Ship Bridge")) && !comps.Any(comp => comp.HasAbility("Master Computer"))))
				yield return "This hull requires a bridge or master computer.";
			if (comps.Count(comp => comp.HasAbility("Ship Bridge")) > 1)
				yield return "A vehicle can have no more than one bridge";
			if (!Hull.CanUseAuxiliaryControl && comps.Any(comp => comp.HasAbility("Ship Auxiliary Control")))
				yield return "This hull cannot use auxiliary control.";
			if (comps.Count(comp => comp.HasAbility("Ship Auxiliary Control")) > 1)
				yield return "A vehicle can have no more than one auxiliary control.";
			if (comps.Count(comp => comp.HasAbility("Master Computer")) > 1)
				yield return "A vehicle can have no more than one master computer.";
			if (comps.Count(comp => comp.HasAbility("Standard Ship Movement")) > Hull.MaxEngines)
				yield return "This hull can only support " + Hull.MaxEngines + " engines.";
			if (comps.Count(comp => comp.HasAbility("Ship Life Support")) < Hull.MinLifeSupport && !comps.Any(comp => comp.HasAbility("Master Computer")))
				yield return "This hull requires at least " + Hull.MinCrewQuarters + " life support modules or a Master Computer.";
			if (comps.Count(comp => comp.HasAbility("Ship Crew Quarters")) < Hull.MinCrewQuarters && !comps.Any(comp => comp.HasAbility("Master Computer")))
				yield return "This hull requires at least " + Hull.MinCrewQuarters + " crew quarters or a Master Computer.";
			if ((double)Components.Where(comp => comp.HasAbility("Cargo Storage")).Sum(comp => comp.Size) / (double)Hull.Size * 100d < Hull.MinPercentCargoBays)
				yield return "This hull requires at least " + Hull.MinPercentCargoBays + "% of its space to be used by cargo-class components.";
			if ((double)Components.Where(comp => comp.HasAbility("Launch/Recover Fighters")).Sum(comp => comp.Size) / (double)Hull.Size * 100d < Hull.MinPercentFighterBays)
				yield return "This hull requires at least " + Hull.MinPercentFighterBays + "% of its space to be used by fighter bays.";
			if ((double)Components.Where(comp => comp.HasAbility("Colonize Planet - Rock") || comp.HasAbility("Colonize Planet - Ice") || comp.HasAbility("Colonize Planet - Gas")).Sum(comp => comp.Size) / (double)Hull.Size * 100d < Hull.MinPercentColonyModules)
				yield return "This hull requires at least " + Hull.MinPercentColonyModules + "% of its space to be used by colony modules.";
			foreach (var g in comps.GroupBy(comp => comp.Family))
			{
				var limited = g.Where(comp => comp.MaxPerVehicle != null);
				if (limited.Any())
				{
					var limit = limited.Min(comp => comp.MaxPerVehicle.Value);
					var name = g.First(comp => comp.MaxPerVehicle == limit).Name;
					if (limit < g.Count())
						yield return "The " + name + " family of components is limited to " + limit + " per vehicle.";
				}
			}
			if (SpaceFree < 0)
				yield return "You are over the hull size limit by " + (-SpaceFree).Kilotons() + ".";
			foreach (var c in comps.Distinct())
			{
				if (!c.VehicleTypes.HasFlag(VehicleType))
					yield return "The " + c.Name + " cannot be placed on this vehicle type.";
			}
			foreach (var comp in Components.GroupBy(mct => mct.ComponentTemplate).Select(g => g.Key))
			{
				if (!Owner.HasUnlocked(comp))
					yield return "You have not unlocked the " + comp + ".";
			}
			foreach (var mount in Components.GroupBy(mct => mct.Mount).Select(g => g.Key))
			{
				if (!Hull.CanUseMount(mount))
					yield return "This hull cannot use the " + mount + ".";
				if (!Owner.HasUnlocked(mount))
					yield return "You have not unlocked the " + mount + ".";
			}
			foreach (var mct in Components.GroupBy(mct => mct).Select(g => g.Key))
			{
				if (!mct.ComponentTemplate.CanUseMount(mct.Mount))
					yield return "The " + mct.ComponentTemplate + " cannot use the " + mct.Mount + ".";
			}
		}
	}

	private ModReference<IHull<T>> hull { get; set; }

	/// <summary>
	/// For serialization and client safety
	/// </summary>
	private GameReference<Empire> owner { get; set; }

	public void AddComponent(ComponentTemplate ct, Mount m = null)
	{
		Components.Add(new MountedComponentTemplate(this, ct, m));
	}

	public Visibility CheckVisibility(Empire emp)
	{
		if (Owner == emp)
			return Visibility.Owned;
		// do we already know the design? or did we engage in combat with it this turn?
		// TODO - "battle manager" so we're not tied to a specific combat implementation
		else if (emp.KnownDesigns.Contains(this) || Game.Current.Battles.Any(b =>
			b.Combatants.Any(c => c.Owner == emp) &&
				(b.Combatants.OfType<IVehicle>().Any(v => v.Design == this)
				|| b.Combatants.OfType<ICargoContainer>().Any(c => c.Cargo?.Units?.Any(u => u.Design == this) ?? false))))
			return Visibility.Scanned;
		return Visibility.Unknown;
	}

	public void Clean()
	{
		// make sure this design's components actually belong to this design!
		foreach (var mct in Components)
			mct.Container = this;
	}

	public IConstructionOrder CreateConstructionOrder(ConstructionQueue queue)
	{
		var dtype = GetType();
		var vtype = dtype.GetGenericArguments()[0];
		var ordertype = typeof(ConstructionOrder<,>).MakeGenericType(vtype, dtype);
		var o = (IConstructionOrder)Activator.CreateInstance(ordertype);
		o.GetType().GetProperty("Template").SetValue(o, this, new object[] { });
		return o;
	}

	public ICreateDesignCommand CreateCreationCommand()
	{
		return new CreateDesignCommand<T>(this);
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		Game.Current.UnassignID(this);
		foreach (var emp in Game.Current.Empires.Where(e => e != null))
			emp.KnownDesigns.Remove(this);
	}

	public override bool Equals(object? obj)
	{
		if (obj is Design<T> d)
		{
			if (d.BaseName != BaseName)
				return false;
			if (d.Hull.ModID != Hull.ModID)
				return false;
			if (d.Components.Count != Components.Count)
				return false;
			for (var i = 0; i < Components.Count; i++)
			{
				if (d.Components[i].ComponentTemplate.ModID != Components[i].ComponentTemplate.ModID)
					return false;
				if (d.Components[i].Mount?.ModID != Components[i].Mount?.ModID)
					return false;
			}
			return true;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return HashCodeMasher.Mash(BaseName, Hull, HashCodeMasher.MashList(Components));
	}

	/// <summary>
	/// A design is unlocked if its hull and all used mounts/components are unlocked.
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	public bool HasBeenUnlockedBy(Empire emp)
	{
		return emp.HasUnlocked(Hull) && Components.All(c => emp.HasUnlocked(c.ComponentTemplate) && emp.HasUnlocked(c.Mount));
	}

	/// <summary>
	/// Has the empire unlocked the hull, components, and mounts used on this design?
	/// </summary>
	/// <param name="emp"></param>
	/// <returns></returns>
	public bool HasEmpireUnlocked(Empire emp)
	{
		return emp.HasUnlocked(Hull) && Components.All(mct => emp.HasUnlocked(mct.ComponentTemplate) && emp.HasUnlocked(mct.Mount));
	}

	public T Instantiate()
	{
		var t = Activator.CreateInstance<T>();
		t.Design = this;
		foreach (var mct in Components)
		{
			var c = mct.Instantiate();
			t.Components.Add(c);
			c.Container = t;
		}
		VehiclesBuilt++;
		t.Name = Name + " " + VehiclesBuilt;
		return t;
	}

	IVehicle IDesign.Instantiate()
	{
		return Instantiate();
	}

	/*public Combat2.StrategyObject Strategy { get; set; }

	public Tactic Tactic { get; set; }*/

	public bool IsObsoleteMemory(Empire emp)
	{
		return false;
	}

	public void Redact(Empire emp)
	{
		// can't see "obsoleteness" of foreign designs unless you've seen a newer iteration of the same design
		if (CheckVisibility(emp) < Visibility.Owned && !emp.KnownDesigns.Any(d => d.BaseName == BaseName && d.Iteration > Iteration))
			IsObsolete = false;

		if (CheckVisibility(emp) < Visibility.Fogged)
			Dispose();
	}

	public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
	{
		if (done == null)
			done = new HashSet<IPromotable>();
		if (!done.Contains(this))
		{
			done.Add(this);
			foreach (var mct in Components)
				mct.ReplaceClientIDs(idmap, done);
		}
	}

	public override string ToString()
	{
		return Name;
	}

	private IEnumerable<string> GetPaths(string pathtype)
	{
		var shipsetPath = Owner.ShipsetPath;
		if (shipsetPath == null)
			shipsetPath = "Default";
		if (!Hull.PictureNames.Any())
			return Enumerable.Empty<string>();
		var paths = new List<string>();

		foreach (var s in Hull.PictureNames)
		{
			if (Mod.Current.RootPath != null)
			{
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, pathtype + "_" + s));
				paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_" + pathtype + "_" + s)); // for SE4 shipset compatibility
			}
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, pathtype + "_" + s));
			paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_" + pathtype + "_" + s)); // for SE4 shipset compatibility
		}
		return paths;
	}

	IDesign IDesign.Upgrade()
		=> Upgrade();
}
