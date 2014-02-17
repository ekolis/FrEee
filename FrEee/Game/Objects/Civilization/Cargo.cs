using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		/// <summary>
		/// The population stored in cargo.
		/// </summary>
		public SafeDictionary<Race, long> Population { get; set; }

		/// <summary>
		/// The units stored in cargo.
		/// </summary>
		public ICollection<IUnit> Units { get; set; }

		private int? fakeSize { get; set; }

		/// <summary>
		/// Sets this cargo's fake size to its size and clears the actual population and units.
		/// Used for fog of war.
		/// </summary>
		public void SetFakeSize()
		{
			fakeSize = Size;
			Population.Clear();
			Units.Clear();
		}

		/// <summary>
		/// The amount of space taken by this cargo.
		/// </summary>
		public int Size
		{
			get
			{
				if (fakeSize != null)
					return fakeSize.Value;

				// TODO - moddable population size, perhaps per race?
				return (int)Math.Round(Population.Sum(kvp => kvp.Value) * 5 / 1e6) + Units.Sum(u => u.Design.Hull.Size);
			}
		}

		[DoNotSerialize]
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

		[DoNotSerialize]
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

		[DoNotSerialize]
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

		public int MaxHitpoints
		{
			get
			{
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				return Population.Sum(kvp => (int)Math.Ceiling(kvp.Value * popHPPerPerson)) + Units.Sum(u => u.MaxHitpoints);
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

		public int TakeDamage(DamageType dmgType, int damage, PRNG dice = null)
		{
			if (Population.Any() && Units.Any())
			{
				// for now, have a 50% chance to hit population first and a 50% chance to hit units first
				// TODO - base the chance to hit population vs. units on relative HP or something?
				var coin = RandomHelper.Next(2);
				int leftover;
				if (coin == 0)
					leftover = TakePopulationDamage(dmgType, damage, dice);
				else
					leftover = TakeUnitDamage(dmgType, damage, dice);
				if (coin == 0)
					return TakeUnitDamage(dmgType, leftover, dice);
				else
					return TakePopulationDamage(dmgType, damage, dice);

			}
			else if (Population.Any())
				return TakePopulationDamage(dmgType, damage, dice);
			else if (Units.Any())
				return TakeUnitDamage(dmgType, damage, dice);
			else
				return damage; // nothing to damage
		}

		private int TakePopulationDamage(DamageType dmgType, int damage, PRNG dice = null)
		{
			int inflicted = 0;
			for (int i = 0; i < damage; i++)
			{
				// pick a race and kill some population
				var race = Population.PickWeighted(dice);
				if (race == null)
					break; // no more population
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				int popKilled = (int)Math.Ceiling(1d / popHPPerPerson);
				Population[race] -= popKilled;
				inflicted += 1;
			}
			// clear population that was emptied out
			foreach (var race in Population.Where(kvp => kvp.Value <= 0).Select(kvp => kvp.Key).ToArray())
				Population.Remove(race);
			return damage - inflicted;
		}

		private int TakeUnitDamage(DamageType dmgType, int damage, PRNG dice = null)
		{
			// units with more HP are more likely to get hit first, like with leaky armor
			var units = Units.ToDictionary(u => u, u => u.MaxHitpoints);
			while (units.Any() && damage > 0)
			{
				var u = units.PickWeighted(dice);
				damage = u.TakeDamage(dmgType, damage, dice);
			}
			return damage;
		}

		public bool IsDestroyed
		{
			get { return Hitpoints <= 0; }
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


		public int HitChance
		{
			get { return 1; }
		}

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


		public int ShieldHitpoints
		{
			get { return Units.Sum(u => u.ShieldHitpoints); }
		}

		public int ArmorHitpoints
		{
			get { return Units.Sum(u => u.ArmorHitpoints); }
		}

		public int HullHitpoints
		{
			get { return Units.Sum(u => u.HullHitpoints) + (int)(Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints); }
		}

		public int MaxShieldHitpoints
		{
			get { return Units.Sum(u => u.MaxShieldHitpoints); }
		}

		public int MaxArmorHitpoints
		{
			get { return Units.Sum(u => u.MaxArmorHitpoints); }
		}

		public int MaxHullHitpoints
		{
			get { return Units.Sum(u => u.MaxHullHitpoints); }
		}
	}
}
