using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Combat
{
	/// <summary>
	/// A seeking missile or torpedo.
	/// </summary>
	public class Seeker : ICombatant
	{
		public Seeker(Sector? sector, Empire owner, ICombatant attacker, Component launcher, ICombatant target)
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
		public int Accuracy => 0;

		/// <summary>
		/// TODO - armored seekers?
		/// </summary>
		public int ArmorHitpoints => 0;

		/// <summary>
		/// The battle in which this seeker was fired.
		/// </summary>
		[DoNotSerialize(false)]
		[Obsolete("Seekers don't need to know what battle they're in; this property is obsolete.")]
		public IBattle? Battle { get; set; }

		public double CombatSpeed { get; set; }

		// TODO - seekers that do different damage based on some sort of abstracted "range"
		public Formula<int> Damage => LaunchingComponent.Template.GetWeaponDamage(1);

		public int DistanceTraveled { get; set; }

		/// <summary>
		/// Seeker evasion is determined by Settings.txt.
		/// TODO - add a field to Components.txt that lets seekers have custom evasion values?
		/// </summary>
		public int Evasion => Mod.Current.Settings.SeekerEvasion;

		public int HitChance => 1;

		/// <summary>
		/// The remaining durability of this seeker.
		/// </summary>
		public int Hitpoints { get; set; }

		public int HullHitpoints => Hitpoints;

		public Image Icon => Portrait;

		public Image Icon32 => Icon.Resize(32);

		public IEnumerable<string> IconPaths => PortraitPaths;

		public long ID { get; set; }

		public bool IsAlive => !IsDestroyed;

		public bool IsDestroyed => Hitpoints <= 0;

		public bool IsDisposed { get; set; }

		public bool IsMemory { get; set; }

		/// <summary>
		/// The combatant which launched the seeker.
		/// </summary>
		public ICombatant? LaunchingCombatant { get; private set; }

		/// <summary>
		/// The component which launched this seeker.
		/// </summary>
		public Component LaunchingComponent { get; private set; }

		public int MaxArmorHitpoints => 0;

		// TODO - let mounts affect seeker HP?
		public int MaxHitpoints => WeaponInfo.SeekerDurability;

		public int MaxHullHitpoints => MaxHitpoints;

		public int MaxNormalShields => 0;

		public int MaxPhasedShields => 0;

		public int MaxShieldHitpoints => MaxNormalShields + MaxPhasedShields;

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
			get => 0;
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
			get => 0;
			set
			{
				// do nothing
			}
		}

		public Image Portrait => Pictures.GetGenericImage<Seeker>(1.0);

		public IEnumerable<string> PortraitPaths
		{
			get
			{
				var paths = new List<string>();

				var shipsetPath = Owner.ShipsetPath;

				if (Mod.Current?.RootPath != null)
				{
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath ?? string.Empty, "Seeker"));
					paths.Add(Path.Combine("Mods", Mod.Current.RootPath, "Pictures", "Races", shipsetPath ?? string.Empty, shipsetPath + "_" + "Seeker"));
				}
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath ?? string.Empty, "Seeker"));
				paths.Add(Path.Combine("Pictures", "Races", shipsetPath ?? string.Empty, shipsetPath + "_" + "Seeker"));
				return paths;
			}
		}

		public Sector? Sector { get; set; }

		public int ShieldHitpoints => NormalShields + PhasedShields;

		public StarSystem? StarSystem => Sector?.StarSystem;

		/// <summary>
		/// The target of the seeker.
		/// </summary>
		public ICombatant Target { get; private set; }

		public double Timestamp { get; set; }

		public SeekingWeaponInfo WeaponInfo => (SeekingWeaponInfo)LaunchingComponent.Template.ComponentTemplate.WeaponInfo;

		/// <summary>
		/// Seekers do not carry other weapons.
		/// </summary>
		public IEnumerable<Component> Weapons => Enumerable.Empty<Component>();

		public WeaponTargets WeaponTargetType => WeaponTargets.Seeker;

		public bool CanTarget(ITargetable target) => target != null && LaunchingComponent.Template.ComponentTemplate.WeaponInfo.Targets.HasFlag(target.WeaponTargetType);

		public Visibility CheckVisibility(Empire emp)
		{
			if (Owner == emp)
				return Visibility.Owned;
			if (Galaxy.Current.Battles.Any(b =>
				(b.Combatants?.Contains(this) ?? false
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
			Galaxy.Current.UnassignID(this);
		}

		public bool IsHostileTo(Empire emp) => Owner != null && Owner.IsEnemyOf(emp, StarSystem);

		public bool IsObsoleteMemory(Empire emp) => Timestamp < Galaxy.Current.Timestamp - 1;

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

		public int TakeDamage(Hit hit, PRNG? dice = null)
		{
			var damage = hit.Shot?.DamageLeft ?? 0;
			damage *= hit.Shot?.DamageType.SeekerDamage.Evaluate(this) ?? 0 / 100;
			var realDamage = Math.Min(Hitpoints, damage);
			Hitpoints -= realDamage;
			return damage - realDamage;
		}

		public override string ToString() => Name;

		public IEnumerable<Component> Components => Enumerable.Empty<Component>();

		public bool FillsCombatTile => false;

		public int Size => 0;
	}
}
