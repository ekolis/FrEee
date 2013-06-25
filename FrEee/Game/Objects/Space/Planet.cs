using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Modding.Templates;
using FrEee.Modding;
using System.Drawing;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Vehicles;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A planet. Planets can be colonized or mined.
	/// </summary>
	[Serializable]
	public class Planet : StellarObject, ITemplate<Planet>, IOrderable, ICombatSpaceObject, ICargoContainer, IReferrable
	{
		public Planet()
		{
			ResourceValue = new Resources();
			Orders = new List<IOrder<Planet>>();
		}

		/// <summary>
		/// Used for naming.
		/// </summary>
		public Planet MoonOf { get; set; }

		/// <summary>
		/// The PlanetSize.txt entry for this planet's size.
		/// </summary>
		public StellarObjectSize Size { get; set; }

		/// <summary>
		/// The surface composition (e.g. rock, ice, gas) of this planet.
		/// </summary>
		public string Surface { get; set; }

		public string ColonizationAbilityName
		{
			get
			{
				// TODO - custom surface types?
				if (Surface == "Rock")
					return "Colonize Planet - Rock";
				else if (Surface == "Ice")
					return "Colonize Planet - Ice";
				else if (Surface == "Gas Giant")
					return "Colonize Planet - Gas";
				return null;
			}
		}

		/// <summary>
		/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this planet.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// Planet abilities take into account abilities on the colony if one is present.
		/// </summary>
		public override IEnumerable<Ability> Abilities
		{
			get
			{
				return IntrinsicAbilities.Concat(Colony == null ? Enumerable.Empty<Ability>() : Colony.Abilities);
			}
		}

		/// <summary>
		/// The resource value of this planet, in %.
		/// </summary>
		public Resources ResourceValue { get; set; }

		/// <summary>
		/// Just copy the planet's data.
		/// </summary>
		/// <returns>A copy of the planet.</returns>
		public new Planet Instantiate()
		{
			return this.Copy();
		}

		/// <summary>
		/// The empire which has a colony on this planet, if any.
		/// </summary>
		public override Empire Owner
		{
			get
			{
				return Colony == null ? null : Colony.Owner;
			}
		}

		/// <summary>
		/// The colony on this planet, if any.
		/// </summary>
		public Colony Colony { get; set; }

		/// <summary>
		/// Planets need to have their colony redacted if the empire can't see them anymore.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <param name="starSystem"></param>
		/// <param name="visibility"></param>
		public override void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			base.Redact(galaxy, starSystem, visibility);
			if (Colony != null)
			{
				if (visibility < Visibility.Owned)
				{
					if (Colony.ConstructionQueue != null)
					{
						Colony.ConstructionQueue.Orders.Clear();
						Colony.ConstructionQueue.Rate.Clear();
						Colony.ConstructionQueue.UnspentRate.Clear();
					}

					// can only see space used by cargo, not actual cargo
					Colony.Cargo.SetFakeSize();
				}
				if (visibility < Visibility.Scanned)
				{
					var unknownFacilityTemplate = new FacilityTemplate { Name = "Unknown" };
					var facilCount = Colony.Facilities.Count;
					Colony.Facilities.Clear();
					for (int i = 0; i < facilCount; i++)
						Colony.Facilities.Add(new Facility(unknownFacilityTemplate));
				}
				if (visibility < Visibility.Visible)
				{
					Colony = null;
					// TODO - memory sight
				}
			}
		}

		/// <summary>
		/// The resource income from this planet.
		/// </summary>
		public Resources Income
		{
			get
			{
				var income = new Resources();
				var prefix = "Resource Generation - ";
				foreach (var abil in Abilities.ToArray().Where(abil => abil.Name.StartsWith(prefix)))
				{
					var resource = Resource.Find(abil.Name.Substring(prefix.Length));
					int amount;
					int.TryParse(abil.Values[0], out amount);

					// do modifiers to income
					var factor = 1d; // TODO - other modifiers (population, happiness, robotoid factories, etc.)
					amount = Galaxy.Current.StandardMiningModel.GetRate(amount, ResourceValue[resource], factor);

					income.Add(resource, amount);
				}
				prefix = "Point Generation - ";
				foreach (var abil in Abilities.ToArray().Where(abil => abil.Name.StartsWith(prefix)))
				{
					var resource = Resource.Find(abil.Name.Substring(prefix.Length));
					int amount;
					int.TryParse(abil.Values[0], out amount);

					// TODO - modifiers (population, happiness, central computers, etc.)

					income.Add(resource, amount);
				}
				return income;
			}
		}

		/// <summary>
		/// Draws "population bars" on an image of the planet.
		/// Make sure not to draw on an original; make a copy first!
		/// </summary>
		/// <param name="img"></param>
		public void DrawPopulationBars(Image img)
		{
			if (Colony != null)
			{
				// draw population bar
				var g = Graphics.FromImage(img);
				var rect = new Rectangle(img.Width - 21, 1, 20, 8);
				var pen = new Pen(Colony.Owner.Color);
				g.DrawRectangle(pen, rect);
				var brush = new SolidBrush(Colony.Owner.Color);
				var pop = Colony.Population.Sum(kvp => kvp.Value);
				rect.Width = 5;
				rect.X++;
				rect.Y += 2;
				rect.Height -= 3;
				rect.X += 1;
				if (pop > 0)
					g.FillRectangle(brush, rect);
				rect.X += 6;
				if (pop > MaxPopulation / 3)
					g.FillRectangle(brush, rect);
				rect.X += 6;
				if (pop > MaxPopulation * 2 / 3)
					g.FillRectangle(brush, rect);
			}
		}

		public override Image Icon
		{
			get
			{
				var icon = (Image)base.Icon.Clone();
				DrawPopulationBars(icon);
				return icon;
			}
		}

		/// <summary>
		/// Is this planet domed? Domed planets usually have less space for population, facilities, and cargo.
		/// </summary>
		public bool IsDomed
		{
			get
			{
				if (Colony == null)
					return false;
				return Colony.Population.Any(kvp => kvp.Key.NativeAtmosphere != Atmosphere);
			}
		}

		public int MaxFacilities
		{
			get
			{
				if (IsDomed)
					return Size.MaxFacilitiesDomed;
				return Size.MaxFacilities;
			}
		}

		public long MaxPopulation
		{
			get
			{
				if (IsDomed)
					return Size.MaxPopulationDomed;
				return Size.MaxPopulation;
			}
		}

		public int MaxCargo
		{
			get
			{
				if (IsDomed)
					return Size.MaxCargoDomed;
				return Size.MaxCargo;
			}
		}

		IEnumerable<IOrder> IOrderable.Orders
		{
			get { return Orders; }
		}

		public IList<IOrder<Planet>> Orders
		{
			get;
			private set;
		}

		public void ExecuteOrders()
		{
			// TODO - execute planet orders
		}

		public ConstructionQueue ConstructionQueue
		{
			get
			{
				if (Colony != null)
					return Colony.ConstructionQueue;
				return null;
			}
		}

		/// <summary>
		/// Planets get cargo storage both from facilities and intrinsically.
		/// </summary>
		public int CargoStorage
		{
			get
			{
				return MaxCargo + this.GetAbilityValue("Cargo Storage").ToInt();
			}
		}

		public bool CanTarget(ICombatObject target)
		{
			// TODO - weapon platforms on planets
			return false;
		}

		public WeaponTargets WeaponTargetType
		{
			get
			{
				return WeaponTargets.Planet;
			}
		}

		public IEnumerable<Component> Weapons
		{
			get
			{
				// TODO - weapon platforms on planets
				return Enumerable.Empty<Component>();
			}
		}

		public int TakeDamage(DamageType dmgType, int damage, Battle battle)
		{
			if (Colony == null)
				return damage; // uninhabited planets can't take damage

			// TODO - to-hit chances not just based on HP?
			// TODO - per-race population HP?
			// TODO - load population factor and population HP from settings.txt
			var popHP = (int)Math.Ceiling(Colony.Population.Sum(kvp => kvp.Value) * 50 / 1e6);
			var cargoHP = Colony.Cargo.Hitpoints;
			var num = RandomHelper.Next(popHP + cargoHP);
			int leftover;
			if (num >= popHP)
				leftover = TakePopulationDamage(dmgType, damage, battle);
			else
				leftover = Cargo.TakeDamage(dmgType, damage, battle);
			if (num >= popHP)
				return Cargo.TakeDamage(dmgType, leftover, battle);
			else
				return TakePopulationDamage(dmgType, leftover, battle);
		}

		private int TakePopulationDamage(DamageType dmgType, int damage, Battle battle)
		{
			var killed = new SafeDictionary<Race, int>();
			for (int i = 0; i < damage; i++)
			{
				// pick a race and kill some population
				var race = Colony.Population.PickWeighted();
				// TODO - moddable population HP
				int popHPPerMillion = 100;
				int popKilled = (int)1e6 / popHPPerMillion;
				Colony.Population[race] -= popKilled;
				killed[race] += popKilled;
			}
			foreach (var race in killed.Keys)
			{
				battle.LogPopulationDamage(race, killed[race]);
			}
			return damage;
		}

		/// <summary>
		/// Planets can't be destroyed in combat.
		/// </summary>
		public bool IsDestroyed
		{
			get { return false; }
		}

		public int NormalShields
		{
			get;
			set;
		}

		public int PhasedShields
		{
			get;
			set;
		}

		public int MaxNormalShields
		{
			get
			{
				if (Colony == null)
					return 0;
				return Colony.Facilities.GetAbilityValue("Shield Generation").ToInt() + Colony.Facilities.GetAbilityValue("Planet - Shield Generation").ToInt();
			}
		}

		public int MaxPhasedShields
		{
			get
			{
				if (Colony == null)
					return 0;
				return Colony.Facilities.GetAbilityValue("Phased Shield Generation").ToInt();
			}
		}

		public void ReplenishShields()
		{
			NormalShields = MaxNormalShields;
			PhasedShields = MaxPhasedShields;
		}

		public Cargo Cargo
		{
			get { return Colony == null ? null : Colony.Cargo; }
		}

		public void AddOrder(IOrder order)
		{
			if (!(order is IOrder<Planet>))
				throw new Exception("Can't add a " + order.GetType() + " to a planet's orders.");
			Orders.Add((IOrder<Planet>)order);
		}

		public void RemoveOrder(IOrder order)
		{
			if (!(order is IOrder<Planet>))
				throw new Exception("Can't remove a " + order.GetType() + " from a planet's orders.");
			Orders.Remove((IOrder<Planet>)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (!(order is IOrder<Planet>))
				throw new Exception("Can't rearrange a " + order.GetType() + " in a planet's orders.");
			var o = (IOrder<Planet>)order;
			var newpos = Orders.IndexOf(o) + delta;
			Orders.Remove(o);
			Orders.Insert(newpos, o);
		}

		public void Dispose()
		{
			Galaxy.Current.Unregister(this);
			foreach (var emp in Galaxy.Current.Empires)
				Galaxy.Current.Unregister(this, emp);
		}

		/// <summary>
		/// Draws this planet's status icons on a picture.
		/// If the planet has special abilities (such as ruins), a white square will be drawn.
		/// If the planet is uncolonized but colonizable with the current empire's technology, a colonizability circle will also be drawn:
		/// * Green for planets that and breathable by the current empire's primary race.
		/// * Yellow for planets that and breathable by any subjugated population.
		/// * Red for other colonizable planets.
		/// </summary>
		/// <param name="pic"></param>
		public void DrawStatusIcons(Image pic)
		{
			var g = Graphics.FromImage(pic);
			var sizeFactor = 1f / 4f;
			var leftovers = 1f - sizeFactor;
			if (IntrinsicAbilities.Any())
				g.FillRectangle(Brushes.White, pic.Width * leftovers - 1, 0, pic.Width * sizeFactor, pic.Height * sizeFactor);
			if (Colony == null && Empire.Current != null && Empire.Current.UnlockedItems.OfType<ComponentTemplate>().Where(c => c.HasAbility(ColonizationAbilityName)).Any())
			{
				Brush brush;
				if (Atmosphere == Empire.Current.PrimaryRace.NativeAtmosphere)
					brush = Brushes.Green;
				else if (
					Empire.Current.ColonizedPlanets.Any(p => p.Colony.Population.Any(kvp => kvp.Key.NativeAtmosphere == Atmosphere)) ||
					Empire.Current.OwnedSpaceObjects.OfType<ICargoContainer>().Any(cc => cc.Cargo.Population.Any(kvp => kvp.Key.NativeAtmosphere == Atmosphere))
					)
					brush = Brushes.Yellow;
				else
					brush = Brushes.Red;
				g.DrawEllipse(new Pen(brush), pic.Width * leftovers - 1, 0, pic.Width * sizeFactor, pic.Height * sizeFactor);
				g.FillEllipse(brush, pic.Width * (leftovers + sizeFactor / 4f) - 1, pic.Width * sizeFactor / 4f, pic.Width * sizeFactor / 2f, pic.Height * sizeFactor / 2f);
			}
		}

		[DoNotSerialize]
		public int Hitpoints
		{
			get
			{
				if (Colony == null)
					return 0;
				return Cargo.Hitpoints + Colony.Facilities.Sum(f => f.Hitpoints);
			}
			set
			{
				// can't set HP of planet!
			}
		}

		public int MaxHitpoints
		{
			get
			{
				if (Colony == null)
					return 0;
				return Cargo.MaxHitpoints + Colony.Facilities.Sum(f => f.MaxHitpoints);
			}
		}

		/// <summary>
		/// Passes repair on to cargo.
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
		public int Repair(int? amount = null)
		{
			if (Cargo != null)
			{
				return Cargo.Repair(amount);
			}

			// nothing to repair
			if (amount == null)
				return 0;
			return amount.Value;
		}


		public int HitChance
		{
			get { return 1; }
		}

		public StellarSize StellarSize
		{
			get { return Size.StellarSize; }
		}


		public bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsHostileTo(emp);
		}


		public bool CanBeInFleet
		{
			get { return false; }
		}

		public int SupplyStorage
		{
			get { return this.GetAbilityValue("Supply Storage").ToInt(); }
		}

		public bool HasInfiniteSupplies
		{
			get { return false; }
		}


		public int Accuracy
		{
			get
			{
				return Mod.Current.Settings.PlanetAccuracy + this.GetAbilityValue("Combat To Hit Offense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Offense Minus").ToInt();
			}
		}

		public int Evasion
		{
			get
			{
				return Mod.Current.Settings.PlanetEvasion + this.GetAbilityValue("Combat To Hit Defense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Defense Minus").ToInt();
			}
		}
	}
}
