using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Combat;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// Cargo stored on a colony or ship/base.
	/// </summary>
	public class Cargo : IDamageable
	{
		public Cargo()
		{
			Population = new SafeDictionary<Race, long>();
			Units = new HashSet<IUnit>();
		}

		public int ArmorHitpoints => Units.Sum(u => u.ArmorHitpoints);

		public int HitChance => 1;

		[DoNotSerialize(false)]
		public int Hitpoints
		{
			get
			{
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				return Population.Sum(kvp => (int)Math.Ceiling(kvp.Value * popHPPerPerson)) + Units.Sum(u => u.Hitpoints);
			}
			set
			{
				throw new NotSupportedException("Can't set cargo HP; it's computed.");
			}
		}

		public int HullHitpoints => Units.Sum(u => u.HullHitpoints) + (int)(Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints);

		public bool IsDestroyed => Hitpoints <= 0;

		public int MaxArmorHitpoints => Units.Sum(u => u.MaxArmorHitpoints);

		public int MaxHitpoints
		{
			get
			{
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				return Population.Sum(kvp => (int)Math.Ceiling(kvp.Value * popHPPerPerson)) + Units.Sum(u => u.MaxHitpoints);
			}
		}

		public int MaxHullHitpoints => Units.Sum(u => u.MaxHullHitpoints);

		public int MaxNormalShields => 0;

		public int MaxPhasedShields => 0;

		public int MaxShieldHitpoints => Units.Sum(u => u.MaxShieldHitpoints);

		[DoNotSerialize(false)]
		public int NormalShields
		{
			get
			{
				return 0;
			}
			set
			{
				throw new NotSupportedException("Cargo cannot have shields.");
			}
		}

		[DoNotSerialize(false)]
		public int PhasedShields
		{
			get
			{
				return 0;
			}
			set
			{
				throw new NotSupportedException("Cargo cannot have shields.");
			}
		}

		/// <summary>
		/// The population stored in cargo.
		/// </summary>
		public SafeDictionary<Race, long> Population { get; set; }

		public int ShieldHitpoints => Units.Sum(u => u.ShieldHitpoints);

		/// <summary>
		/// The amount of space taken by this cargo.
		/// </summary>
		public int Size
		{
			get
			{
				if (fakeSize != null)
					return fakeSize.Value;

				// TODO - per race population size?
				return (int)Math.Round(Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationSize) + Units.Sum(u => u.Design.Hull.Size);
			}
		}

		/// <summary>
		/// The units stored in cargo.
		/// </summary>
		public ICollection<IUnit> Units { get; set; }

		private int? fakeSize { get; set; }

		public static Cargo operator +(Cargo c1, Cargo c2)
		{
			var result = new Cargo();
			foreach (var kvp in c1.Population)
				result.Population[kvp.Key] += kvp.Value;
			foreach (var kvp in c2.Population)
				result.Population[kvp.Key] += kvp.Value;
			foreach (var unit in c1.Units.Union(c2.Units))
				result.Units.Add(unit);
			return result;
		}

		/// <summary>
		/// Passes repair on to units.
		/// Tries to repair more-damaged units first.
		/// TODO - repair priorities
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
		public int? Repair(int? amount = null)
		{
			if (amount == null)
			{
				foreach (var u in Units.OrderBy(u => (double)u.Hitpoints / (double)u.MaxHitpoints))
					u.Repair(amount);
			}
			else
			{
				foreach (var u in Units.OrderBy(u => (double)u.Hitpoints / (double)u.MaxHitpoints))
					amount = u.Repair(amount);
			}
			return amount;
		}

		public void ReplenishShields(int? amount = null)
		{
			// do nothing
		}

		/// <summary>
		/// Sets this cargo's fake size to its size (or zero if cargo size can't be seen) and clears the actual population and units.
		/// Used for fog of war.
		/// </summary>
		public void SetFakeSize(bool canSeeCargoSize)
		{
			if (canSeeCargoSize && fakeSize == null)
				fakeSize = Size;
			else if (!canSeeCargoSize)
				fakeSize = 0;

			Population.Clear();
			Units.Clear();
		}

		public int TakeDamage(Hit hit, PRNG? dice = null)
		{
			int damage = hit.NominalDamage;
			if (Population.Any() && Units.Any())
			{
				// for now, have a 50% chance to hit population first and a 50% chance to hit units first
				// TODO - base the chance to hit population vs. units on relative HP or something?
				var coin = RandomHelper.Next(2, dice);
				int leftover;
				if (coin == 0)
					leftover = TakePopulationDamage(hit, damage, dice);
				else
					leftover = TakeUnitDamage(hit, damage, dice);
				if (coin == 0)
					return TakeUnitDamage(hit, leftover, dice);
				else
					return TakePopulationDamage(hit, damage, dice);
			}
			else if (Population.Any())
				return TakePopulationDamage(hit, damage, dice);
			else if (Units.Any())
				return TakeUnitDamage(hit, damage, dice);
			else
				return damage; // nothing to damage
		}

		private int TakePopulationDamage(Hit hit, int damage, PRNG? dice = null)
		{
			int inflicted = 0;
			for (int i = 0; i < damage; i++)
			{
				// pick a race and kill some population
				var race = Population.PickWeighted(dice);
				if (race == null)
					break; // no more population
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				// TODO - don't ceiling the popKilled, just stack it up
				int popKilled = (int)Math.Ceiling(hit.Shot.DamageType.PopulationDamage.Evaluate(hit.Shot) / popHPPerPerson);
				Population[race] -= popKilled;
				if (Population[race] < 0)
					Population[race] = 0;
				inflicted += 1;
			}
			// clear population that was emptied out
			foreach (var race in Population.Where(kvp => kvp.Value <= 0).Select(kvp => kvp.Key).ToArray())
				Population.Remove(race);
			return damage - inflicted;
		}

		private int TakeUnitDamage(Hit hit, int damage, PRNG? dice = null)
		{
			// units with more HP are more likely to get hit first, like with leaky armor
			var units = Units.Where(u => !u.IsDestroyed).ToDictionary(u => u, u => u.MaxHitpoints);
			while (units.Any() && damage > 0)
			{
				var u = units.PickWeighted(dice);
				damage = u.TakeDamage(hit, dice);
				units = units.Where(x => !x.Key.IsDestroyed).ToDictionary(x => x.Key, x => x.Value);
			}
			return damage;
		}
	}
}
