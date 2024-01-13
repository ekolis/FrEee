using FrEee.Enumerations;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace FrEee.Objects.Combat
{
	/// <summary>
	/// A seeking missile or torpedo.
	/// </summary>
	public class Seeker : ICombatant
	{
		public Seeker(Sector sector, Empire owner, ICombatant attacker, Component launcher, ICombatant target)
		{
			Sector = sector;
			Owner = owner;
			if (launcher.Template.ComponentTemplate.WeaponInfo is SeekingWeaponInfo)
				LaunchingComponent = launcher;
			else
				throw new Exception(launcher + " cannot launch seekers.");
			Name = Owner.Name + " " + launcher.Name;
			if (WeaponInfo.Targets.HasFlag(target.WeaponTargetType))
				Target = target;
			else
				throw new Exception(launcher + " cannot target a " + target.WeaponTargetType + ".");
			Hitpoints = WeaponInfo.SeekerDurability; // TODO - can mounts affect seeker durability?
			CombatSpeed = WeaponInfo.SeekerSpeed;
		}

		/// <summary>
		/// Seekers don't fire so it doesn't really matter...
		/// </summary>
		public int Accuracy
		{
			get { return 0; }
		}

		/// <summary>
		/// TODO - armored seekers?
		/// </summary>
		public int ArmorHitpoints
		{
			get { return 0; }
		}

		/// <summary>
		/// The battle in which this seeker was fired.
		/// </summary>
		[DoNotSerialize(false)]
		[Obsolete("Seekers don't need to know what battle they're in; this property is obsolete.")]
		public IBattle Battle { get; set; }

		public double CombatSpeed { get; set; }

		public Formula<int> Damage
		{
			get
			{
				return LaunchingComponent.Template.GetWeaponDamage(1); // TODO - seekers that do different damage based on some sort of abstracted "range"
			}
		}

		public int DistanceTraveled { get; set; }

		/// <summary>
		/// Seeker evasion is determined by Settings.txt.
		/// TODO - add a field to Components.txt that lets seekers have custom evasion values?
		/// </summary>
		public int Evasion
		{
			get { return Mod.Current.Settings.SeekerEvasion; }
		}

		public int HitChance
		{
			get { return 1; }
		}

		/// <summary>
		/// The remaining durability of this seeker.
		/// </summary>
		public int Hitpoints { get; set; }

		public int HullHitpoints
		{
			get { return Hitpoints; }
		}

		public Image Icon
		{
			get { return Portrait; }
		}

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths
		{
			get
			{
				return PortraitPaths;
			}
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsAlive => !IsDestroyed;

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}

		public bool IsDisposed { get; set; }

		public bool IsMemory
		{
			get;
			set;
		}

		/// <summary>
		/// The combatant which launched the seeker.
		/// </summary>
		public ICombatant LaunchingCombatant { get; private set; }

		/// <summary>
		/// The component which launched this seeker.
		/// </summary>
		public Component LaunchingComponent { get; private set; }

		public int MaxArmorHitpoints
		{
			get { return 0; }
		}

		public int MaxHitpoints
		{
			get
			{
				return WeaponInfo.SeekerDurability; // TODO - let mounts affect seeker HP?
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
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxTargets => 0;

		/// <summary>
		/// The name of the seeker.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Seekers do not have shields.
		/// TODO - maybe let seekers have shields?
		/// </summary>
		public int NormalShields
		{
			get
			{
				return 0;
			}
			set
			{
				// do nothing
			}
		}

		/// <summary>
		/// The owner of the seeker.
		/// </summary>
		public Empire Owner { get; set; }

		/// <summary>
		/// Seekers do not have shields.
		/// TODO - maybe let seekers have shields?
		/// </summary>
		public int PhasedShields
		{
			get
			{
				return 0;
			}
			set
			{
				// do nothing
			}
		}

		public Image Portrait
		{
			// TODO - custom seeker images per shipset
			get { return Pictures.GetGenericImage<Seeker>(1.0); }
		}

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				var paths = new List<string>();

				var shipsetPath = Owner.ShipsetPath;

				if (Mod.Current?.RootPath != null)
				{
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, "Seeker"));
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath, shipsetPath + "_" + "Seeker"));
				}
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath, "Seeker"));
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath, shipsetPath + "_" + "Seeker"));
				return paths;
			}
		}

		public Sector Sector
		{
			get; set;
		}

		public int ShieldHitpoints
		{
			get { return NormalShields + PhasedShields; }
		}

		public StarSystem StarSystem
		{
			get { return Sector.StarSystem; }
		}

		/// <summary>
		/// The target of the seeker.
		/// </summary>
		public ICombatant Target { get; private set; }

		public double Timestamp { get; set; }

		public SeekingWeaponInfo WeaponInfo
		{
			get { return (SeekingWeaponInfo)LaunchingComponent.Template.ComponentTemplate.WeaponInfo; }
		}

		/// <summary>
		/// Seekers do not carry other weapons.
		/// </summary>
		public IEnumerable<Component> Weapons
		{
			get { return Enumerable.Empty<Component>(); }
		}

		public WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Seeker; }
		}

		public bool CanTarget(ITargetable target)
		{
			return target != null && LaunchingComponent.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType);
		}

		public Visibility CheckVisibility(Empire emp)
		{
			if (Owner == emp)
				return Visibility.Owned;
			if (Galaxy.Current.Battles.Any(b =>
				(b.Combatants.Contains(this)
					|| b.StartCombatants.Values.Contains(this)
					|| b.EndCombatants.Values.Contains(this))
				&& b.Combatants.Any(c => c.Owner == emp)))
				return Visibility.Scanned;
			return Visibility.Unknown;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Target = null;
			Galaxy.Current.UnassignID(this);
		}

		public bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsEnemyOf(emp, StarSystem);
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);
			if (vis < Visibility.Fogged)
				Dispose();
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
			// do nothing, seekers don't have shields
		}

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			var damage = hit.Shot.DamageLeft;
			damage *= hit.Shot.DamageType.SeekerDamage.Evaluate(this) / 100;
			var pierced = damage * hit.Shot.DamageType.ComponentPiercing.Evaluate(this);
			int realDamage;
			realDamage = Math.Min(Hitpoints, damage);
			Hitpoints -= realDamage;
			return damage - realDamage;
		}

		public override string ToString()
		{
			return Name;
		}

		public IEnumerable<Component> Components => Enumerable.Empty<Component>();

		public bool FillsCombatTile => false;

		public int Size => 0;
	}
}