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
using System.IO;

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
			ResourceValue = new ResourceQuantity();
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
		public ResourceQuantity ResourceValue { get; set; }

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
		/// The resource income from this planet.
		/// </summary>
		public ResourceQuantity Income
		{
			get
			{
				if (Colony == null)
					return new ResourceQuantity(); // no colony? no income!

				var income = new ResourceQuantity();
				var prefix = "Resource Generation - ";
				foreach (var abil in Abilities.ToArray().Where(abil => abil.Name.StartsWith(prefix)))
				{
					var resource = Resource.Find(abil.Name.Substring(prefix.Length));
					int amount;
					int.TryParse(abil.Values[0], out amount);

					// do modifiers to income
					var factor = 1d;
					var totalpop = Colony.Population.Sum(kvp => kvp.Value);
					factor *= Mod.Current.Settings.GetPopulationProductionFactor(totalpop);
					Aptitude aptitude = null;
					if (resource.Name == "Minerals")
						aptitude = Aptitude.Mining;
					if (resource.Name == "Organics")
						aptitude = Aptitude.Farming;
					if (resource.Name == "Radioactives")
						aptitude = Aptitude.Refining;
					if (aptitude != null)
						factor *= Colony.Population.Sum(kvp => (kvp.Key.Aptitudes[aptitude.Name] / 100d + 1d) * (double)kvp.Value / (double)totalpop);
					amount = Galaxy.Current.StandardMiningModel.GetRate(amount, ResourceValue[resource], factor);

					income.Add(resource, amount);
				}
				prefix = "Point Generation - ";
				foreach (var abil in Abilities.ToArray().Where(abil => abil.Name.StartsWith(prefix)))
				{
					var resource = Resource.Find(abil.Name.Substring(prefix.Length));
					int amount;
					int.TryParse(abil.Values[0], out amount);

					var factor = 1d;
					var totalpop = Colony.Population.Sum(kvp => kvp.Value);
					factor *= Mod.Current.Settings.GetPopulationProductionFactor(totalpop);
					Aptitude aptitude = null;
					if (resource.Name == "Research")
						aptitude = Aptitude.Intelligence; // yes, Intelligence aptitude increases Research...
					if (resource.Name == "Intelligence")
						aptitude = Aptitude.Cunning;
					if (aptitude != null)
						factor *= Colony.Population.Sum(kvp => (kvp.Key.Aptitudes[aptitude.Name] / 100d + 1d) * (double)kvp.Value / (double)totalpop);

					income.Add(resource, (int)(amount * factor));
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

		/// <summary>
		/// Fractional turns until the planet has saved up another move point.
		/// </summary>
		public double TimeToNextMove
		{
			get;
			set;
		}

		public void ExecuteOrders()
		{
			if (Galaxy.Current.NextTickSize == double.PositiveInfinity)
				TimeToNextMove = 0;
			else
				TimeToNextMove -= Galaxy.Current.NextTickSize;
			while (TimeToNextMove <= 0 && Orders.Any())
			{
				if (!Orders.Any())
					break;
				Orders.First().Execute(this);
				if (Orders.First().IsComplete)
					Orders.RemoveAt(0);
			}
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
			return Cargo.Units.OfType<WeaponPlatform>().Any(wp => wp.CanTarget(target));
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
				if (Cargo == null)
					return Enumerable.Empty<Component>();
				return Cargo.Units.OfType<WeaponPlatform>().SelectMany(wp => wp.Weapons);
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
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				int popKilled = (int)Math.Ceiling(1d / popHPPerPerson);
				Colony.Population[race] -= popKilled;
				killed[race] += popKilled;
			}
			if (battle != null)
			{
				foreach (var race in killed.Keys)
				{
					battle.LogPopulationDamage(race, killed[race]);
				}
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
			{
				// draw ruins icon
				g.DrawImage(Pictures.GetModImage(Path.Combine("Pictures", "UI", "Map", "ruins")), 0, 0, pic.Width * sizeFactor, pic.Height * sizeFactor);
			}
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

		public override bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsHostileTo(emp);
		}


		public override bool CanBeInFleet
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

		/// <summary>
		/// Expected population change for the upcoming turn due to reproduction, cloning, and plagues.
		/// </summary>
		public long PopulationChangePerTurn
		{
			get
			{
				return PopulationChangePerTurnPerRace.Sum(kvp => kvp.Value);
			}
		}

		/// <summary>
		/// Expected population change for the upcoming turn due to reproduction, cloning, and plagues.
		/// </summary>
		public IDictionary<Race, long> PopulationChangePerTurnPerRace
		{
			get
			{
				var deltapop = new Dictionary<Race, long>();

				if (Colony == null)
					return deltapop;

				foreach (var race in Colony.Population.Keys)
				{
					// TODO - plagued planets should not reproduce, and should lose population each turn
					var sysModifier = this.FindStarSystem().GetAbilityValue(Owner, "Modify Reproduction - System").ToInt();
					var planetModifier = this.GetAbilityValue("Modify Reproduction - Planet").ToInt();
					var reproduction = ((Mod.Current.Settings.Reproduction + (race.Aptitudes["Reproduction"] - 100) + sysModifier + planetModifier) * Mod.Current.Settings.ReproductionMultiplier) / 100d;
					deltapop[race] = (long)(Colony.Population[race] * reproduction);

					// TODO - allow cloning of populations over the max of a 32 bit int?
					var sysCloning = this.FindStarSystem().GetAbilityValue(Owner, "Change Population - System").ToInt();
					var planetCloning = this.GetAbilityValue("Change Population - Planet").ToInt();
					deltapop[race] += (sysCloning + planetCloning) * Mod.Current.Settings.PopulationFactor / Colony.Population.Count; // split cloning across races
				}

				return deltapop;
			}
		}

		/// <summary>
		/// If planets had engines, they could warp...
		/// (Sure, why not?)
		/// </summary>
		public override bool CanWarp { get { return true; } }

		public override void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);

			MoonOf = null; // in case we allow moons to have different visibility than their parent planets

			if (Colony != null)
			{
				if (Colony.CheckVisibility(emp) < Visibility.Visible)
					Colony = null;
				else
					Colony.Redact(emp);
			}

			if (vis < Visibility.Owned)
				Orders.Clear();

			if (vis < Visibility.Fogged)
				Dispose(); // TODO - dispose here if fogged; replace with memory sight cache if present
		}
	}
}