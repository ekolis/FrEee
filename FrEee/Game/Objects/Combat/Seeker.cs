﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A seeking missile or torpedo.
	/// </summary>
	public class Seeker : ICombatant
	{
		public Seeker(IBattle battle, Empire owner, ICombatant attacker, Component launcher, ICombatant target)
		{
			Battle = battle;
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
			Speed = WeaponInfo.SeekerSpeed;
		}

		/// <summary>
		/// The battle in which this seeker was fired.
		/// </summary>
		public IBattle Battle { get; set; }

		/// <summary>
		/// The name of the seeker.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The combatant which launched the seeker.
		/// </summary>
		public ICombatant LaunchingCombatant { get; private set; }

		/// <summary>
		/// The owner of the seeker.
		/// </summary>
		public Empire Owner { get; set; }

		/// <summary>
		/// The target of the seeker.
		/// </summary>
		public ICombatant Target { get; private set; }

		/// <summary>
		/// The component which launched this seeker.
		/// </summary>
		public Component LaunchingComponent { get; private set; }

		public SeekingWeaponInfo WeaponInfo
		{
			get { return (SeekingWeaponInfo)LaunchingComponent.Template.ComponentTemplate.WeaponInfo; }
		}

		public Formula<int> Damage
		{
			get
			{
				return LaunchingComponent.Template.GetWeaponDamage(1); // TODO - seekers that do different damage based on some sort of abstracted "range"
			}
		}

		/// <summary>
		/// The remaining durability of this seeker.
		/// </summary>
		public int Hitpoints { get; set; }

		public bool CanTarget(ITargetable target)
		{
			return target != null && LaunchingComponent.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType);
		}

		public WeaponTargets WeaponTargetType
		{
			get { return WeaponTargets.Seeker; }
		}

		/// <summary>
		/// Seekers do not carry other weapons.
		/// </summary>
		public IEnumerable<Component> Weapons
		{
			get { return Enumerable.Empty<Component>(); }
		}

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			var damage = hit.Shot.DamageLeft;
			damage *= hit.Shot.DamageType.SeekerDamage.Evaluate(this) / 100;
			var pierced = damage * hit.Shot.DamageType.ComponentPiercing.Evaluate(this);
			int realDamage;
			realDamage = Math.Min(Hitpoints, damage);
			Hitpoints -= realDamage;
			if (IsDestroyed)
				Dispose();
			return damage - realDamage;
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
		}

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
			// do nothing, seekers don't have shields
		}

		public System.Drawing.Image Icon
		{
			get { return Portrait; }
		}

		public System.Drawing.Image Portrait
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

		public IEnumerable<string> IconPaths
		{
			get
			{
				return PortraitPaths;
			}
		}

		public override string ToString()
		{
			return Name;
		}


		public int MaxHitpoints
		{
			get
			{
				return WeaponInfo.SeekerDurability; // TODO - let mounts affect seeker HP?
			}
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
			get { return 1; }
		}


		public bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsEnemyOf(emp, StarSystem);
		}

		/// <summary>
		/// Seekers don't fire so it doesn't really matter...
		/// </summary>
		public int Accuracy
		{
			get { return 0; }
		}

		/// <summary>
		/// Seeker evasion is determined by Settings.txt.
		/// TODO - add a field to Components.txt that lets seekers have custom evasion values?
		/// </summary>
		public int Evasion
		{
			get { return Mod.Current.Settings.SeekerEvasion; }
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Target = null;
			Owner = null;
		}

		public Visibility CheckVisibility(Empire emp)
		{
			if (Owner == emp)
				return Visibility.Owned;
			if (Galaxy.Current.Battles.Any(b => b.Combatants.Contains(this) && b.Combatants.Any(c => c.Owner == emp)))
				return Visibility.Scanned;
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);
			if (vis < Visibility.Fogged)
				Dispose();
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public long ID
		{
			get;
			set;
		}

		public double Timestamp { get; set; }

		public bool IsObsoleteMemory(Empire emp)
		{
			return Timestamp < Galaxy.Current.Timestamp - 1;
		}

		[DoNotSerialize(false)]
		public Sector Sector
		{
			get { return Battle.Sector; }
			set { throw new NotSupportedException("Cannot set the sector of a seeker once it's been initialized."); }
		}

		public StarSystem StarSystem
		{
			get { return Battle.StarSystem; }
		}


		public int ShieldHitpoints
		{
			get { return NormalShields + PhasedShields; }
		}

		/// <summary>
		/// TODO - armored seekers?
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
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxArmorHitpoints
		{
			get { return 0; }
		}

		public int MaxHullHitpoints
		{
			get { return MaxHitpoints; }
		}

		public bool IsDisposed { get; set; }

		public bool IsAlive => !IsDestroyed;

		public int Speed { get; set; }

		public int DistanceTraveled { get; set; }

		public int MaxTargets => 0;
	}
}