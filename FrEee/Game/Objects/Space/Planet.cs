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
	public class Planet : StellarObject, ITemplate<Planet>, IOrderable, ICombatSpaceObject, ICargoTransferrer, IReferrable, IMobileSpaceObject<Planet>, IMineableSpaceObject, IIncomeProducer, IDataObject, ITransferrable
	{
		public Planet()
		{
			ResourceValue = new ResourceQuantity();
			Orders = new List<IOrder<Planet>>();
		}

		/// <summary>
		/// Used for naming.
		/// </summary>
		[DoNotSerialize]
		internal Planet MoonOf { get; set; }

		/// <summary>
		/// The PlanetSize.txt entry for this asteroid field's size.
		/// </summary>
		[DoNotSerialize]
		public StellarObjectSize Size { get { return size; } set { size = value; } }

		private ModReference<StellarObjectSize> size { get; set; }

		/// <summary>
		/// The surface composition (e.g. rock, ice, gas) of this planet.
		/// </summary>
		public string Surface { get; set; }

		public string ColonizationAbilityName
		{
			get
			{
				return "Colonize Planet - " + Surface;
			}
		}

		/// <summary>
		/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this planet.
		/// </summary>
		public string Atmosphere { get; set; }

		/// <summary>
		/// The resource value of this planet, in %.
		/// </summary>
		public ResourceQuantity ResourceValue { get; set; }

		/// <summary>
		/// Just copy the planet's data.
		/// </summary>
		/// <returns>A copy of the planet.</returns>
		public Planet Instantiate()
		{
			return this.CopyAndAssignNewID();
		}

		/// <summary>
		/// The empire which has a colony on this planet, if any.
		/// </summary>
		public Empire Owner
		{
			get
			{
				return Colony == null ? null : Colony.Owner;
			}
			set
			{
				if (Colony == null && value != null)
					Colony = new Colony { Owner = value };
				else if (Colony != null && value == null)
				{
					// TODO - unowned colonies?
					Colony.Dispose();
					Colony = null;
				}
				else if (Colony != null && value != null)
					Colony.Owner = value;
			}
		}

		public override bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsEnemyOf(emp, StarSystem);
		}

		/// <summary>
		/// The colony on this planet, if any.
		/// </summary>
		public Colony Colony { get; set; }

		/// <summary>
		/// The resource income from this planet, not taking into account presence or lack of a spaceport.
		/// </summary>
		public ResourceQuantity GrossIncomeIgnoringSpaceport
		{
			get
			{
				if (Colony == null)
					return new ResourceQuantity(); // no colony? no income!


				// TODO - solar resource generation (maybe even other resources than min/org/rad?)

				return this.StandardIncome() + this.RawResourceIncome();
			}
		}

		/*/// <summary>
		/// The planet's gross income, taking into presence presence or lack of a spaceport.
		/// </summary>
		public override ResourceQuantity GrossIncome
		{
			get
			{
				if (Colony == null)
					return new ResourceQuantity();

				var sys = StarSystem;
				if (sys.HasAbility("Spaceport", Owner))
					return GrossIncomeIgnoringSpaceport;
				else
					return GrossIncomeIgnoringSpaceport * Colony.MerchantsRatio;
			}
		}*/

		/// <summary>
		/// Base resource generation, not taking into account value.
		/// </summary>
		public ResourceQuantity ResourceGeneration
		{
			get
			{
				if (Colony == null)
					return new ResourceQuantity();

				var sys = StarSystem;
				if (sys.HasAbility("Spaceport", Owner))
					return GrossIncomeIgnoringSpaceport;
				else
					return GrossIncomeIgnoringSpaceport * Colony.MerchantsRatio;
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
				g.FillRectangle(Brushes.Black, rect);
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
				// TODO - abilities that modify max pop
				if (IsDomed)
					return Size.MaxPopulationDomed;
				return Size.MaxPopulation;
			}
		}

		public int MaxCargo
		{
			get
			{
				// TODO - cargo facilities and such
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

		public bool ExecuteOrders()
		{
			return this.ExecuteMobileSpaceObjectOrders();
		}

		public override ConstructionQueue ConstructionQueue
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

		public bool CanTarget(ITargetable target)
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
				return Cargo.Units.OfType<WeaponPlatform>().SelectMany(wp => wp.Weapons).Where(x => !x.IsDestroyed);
			}
		}

		public int TakeDamage(Hit hit, PRNG dice = null)
		{
			var damage = hit.NominalDamage;

			if (Colony == null)
				return damage; // uninhabited planets can't take damage

			// TODO - to-hit chances not just based on HP?
			// TODO - per-race population HP?
			var popHP = (int)Math.Ceiling(Colony.Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints);
			var cargoHP = Colony.Cargo.MaxHitpoints;
			var facilHP = Colony.Facilities.Sum(f => f.MaxHitpoints);
			var order = new int[] { 0, 1, 2 }.Shuffle(dice);
			foreach (var num in order)
			{
				if (num == 0)
					damage = TakePopulationDamage(hit, dice);
				else if (num == 1)
					damage = Cargo.TakeDamage(hit, dice);
				else if (num == 2)
					damage = TakeFacilityDamage(hit, dice);
				hit = new Hit(hit.Shot, this, damage);

			}

			// if planet was completely glassed, remove the colony
			if (!Colony.Population.Any(p => p.Value > 0) && !Cargo.Units.Any() && !Cargo.Population.Any(p => p.Value > 0) && !Colony.Facilities.Any())
				Colony.Dispose();

			// update memory sight
			if (!IsMemory)
				this.UpdateEmpireMemories();

			return damage;
		}

		public void TakePopulationDamage(int popFactorsKilled, PRNG dice = null)
		{
			if (Colony == null)
				return;
			long popKilled = popFactorsKilled * Mod.Current.Settings.PopulationFactor;
			long totalPop = Colony.Population.Sum(kvp => kvp.Value);
			long dead = 0;
			foreach (var race in Colony.Population.Keys)
			{
				long ourDead = popKilled / totalPop;
				Colony.Population[race] -= ourDead;
				dead += ourDead;
			}
			for (long i = 0; i < popKilled - dead; i++)
			{
				// leftover deaths get allocated randomly
				var race = Colony.Population.PickWeighted(dice);
				Colony.Population[race]--;
			}
		}

		private int TakePopulationDamage(Hit hit, PRNG dice = null)
		{
			if (Colony == null)
				return hit.NominalDamage;
			int damage = hit.NominalDamage;
			int inflicted = 0;
			for (int i = 0; i < damage; i++)
			{
				// pick a race and kill some population
				var race = Colony.Population.PickWeighted(dice);
				if (race == null)
					break; // no more population
				double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
				// TODO - don't ceiling the popKilled, just stack it up
				int popKilled = (int)Math.Ceiling(hit.Shot.DamageType.PopulationDamage.Evaluate(hit.Shot) / popHPPerPerson);
				Colony.Population[race] -= popKilled;
				if (Colony.Population[race] < 0)
					Colony.Population[race] = 0;
				inflicted += 1;
			}
			// clear population that was emptied out
			foreach (var race in Colony.Population.Where(kvp => kvp.Value <= 0).Select(kvp => kvp.Key).ToArray())
				Colony.Population.Remove(race);
			return damage - inflicted;
		}

		private int TakeFacilityDamage(Hit hit, PRNG dice = null)
		{
			if (Colony == null)
				return hit.NominalDamage;
			int damage = hit.NominalDamage;
			// TODO - take into account damage types, and make sure we have facilities that are not immune to the damage type so we don't get stuck in an infinite loop
			while (damage > 0 && Colony.Facilities.Any())
			{
				var facil = Colony.Facilities.Where(f =>
				{
					// skip facilities that are completely pierced by this hit
					var hit2 = new Hit(hit.Shot, f, damage);
					return hit2.Shot.DamageType.ComponentPiercing.Evaluate(hit2) < 100;
				}).ToDictionary(f => f, f => f.HitChance).PickWeighted(dice);
				if (facil == null)
					break; // no more facilities to hit
				var facilhit = new Hit(hit.Shot, facil, damage);
				damage = facil.TakeDamage(facilhit, dice);
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
				var shields = MaxUnmodifiedNormalShields;
				var modifiers = ShieldModifiers;
				if (modifiers >= 0)
					return shields; // positive modifiers go to phased shields
				else
					return Math.Max(0, shields + modifiers); // negative modifiers go to normal shields first
			}
		}

		public int MaxUnmodifiedNormalShields
		{
			get
			{
				if (Colony == null)
					return 0;
				return
					Colony.Facilities.GetAbilityValue("Shield Generation", this).ToInt()
					+ Colony.Facilities.GetAbilityValue("Planet - Shield Generation", this).ToInt();
			}
		}

		public int ShieldModifiers
		{
			get
			{
				return
						-Sector.GetAbilityValue("Sector - Shield Disruption").ToInt()
						+ Sector.GetEmpireAbilityValue(Owner, "Shield Modifier - Sector").ToInt()
						+ StarSystem.GetEmpireAbilityValue(Owner, "Shield Modifier - System").ToInt()
						+ Owner.GetAbilityValue("Shield Modifier - Empire").ToInt();
			}
		}

		public int MaxUnmodifiedPhasedShields
		{
			get
			{
				if (Colony == null)
					return 0;
				return Colony.Facilities.GetAbilityValue("Phased Shield Generation", this).ToInt();
			}
		}

		public int MaxPhasedShields
		{
			get
			{
				if (Colony == null)
					return 0;
				var shields = MaxUnmodifiedPhasedShields;
				var modifiers = ShieldModifiers;
				if (modifiers >= 0)
					return shields + modifiers; // positive modifiers go to phased shields
				else
					return Math.Max(0, shields + modifiers + MaxUnmodifiedNormalShields); // negative modifiers go to normal shields first
			}
		}

		public void ReplenishShields(int? amount = null)
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
			if (order != null && !(order is IOrder<Planet>))
				return; // order can't exist here anyway
			Orders.Remove((IOrder<Planet>)order);
		}

		public void RearrangeOrder(IOrder order, int delta)
		{
			if (!(order is IOrder<Planet>))
				throw new Exception("Can't rearrange a " + order.GetType() + " in a planet's orders.");
			var o = (IOrder<Planet>)order;
			var newpos = Orders.IndexOf(o) + delta;
			if (newpos < 0)
				newpos = 0;
			Orders.Remove(o);
			if (newpos >= Orders.Count)
				Orders.Add(o);
			else
				Orders.Insert(newpos, o);
		}

		/// <summary>
		/// Draws this planet's status icons on a picture.
		/// If the planet has special abilities (such as ruins), a ruins icon will be drawn.
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
				var path = Path.Combine("Pictures", "UI", "Map", "ruins");
				var img = Pictures.GetModImage(path);
				if (img == null)
					throw new FileNotFoundException("Could not load ruins icon: " + Path.GetFullPath(path) + ".");
				g.DrawImage(img, 0, 0, pic.Width * sizeFactor, pic.Height * sizeFactor);
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
		public int? Repair(int? amount = null)
		{
			if (Cargo != null)
			{
				return Cargo.Repair(amount);
			}

			// nothing to repair
			if (amount == null)
				return amount;
			return amount.Value;
		}


		public int HitChance
		{
			get { return 1; }
		}

		public override bool CanBeInFleet
		{
			get { return false; }
		}

		public int Accuracy
		{
			get
			{
				return
					Mod.Current.Settings.PlanetAccuracy
					+ this.GetAbilityValue("Combat To Hit Offense Plus").ToInt()
					- this.GetAbilityValue("Combat To Hit Offense Minus").ToInt()
					+ (Owner == null || Owner.Culture == null ? 0 : Owner.Culture.SpaceCombat)
					+ Sector.GetEmpireAbilityValue(Owner, "Combat Modifier - Sector").ToInt()
					+ StarSystem.GetEmpireAbilityValue(Owner, "Combat Modifier - System").ToInt()
					+ Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
			}
		}

		public int Evasion
		{
			get
			{
				return
					Mod.Current.Settings.PlanetEvasion
					+ this.GetAbilityValue("Combat To Hit Defense Plus").ToInt()
					- this.GetAbilityValue("Combat To Hit Defense Minus").ToInt()
					+ (Owner == null || Owner.Culture == null ? 0 : Owner.Culture.SpaceCombat)
					+ Sector.GetEmpireAbilityValue(Owner, "Combat Modifier - Sector").ToInt()
					+ StarSystem.GetEmpireAbilityValue(Owner, "Combat Modifier - System").ToInt()
					+ Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
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
					var sys = this.FindStarSystem();
					var sysModifier = sys == null ? 0 : sys.GetEmpireAbilityValue(Owner, "Modify Reproduction - System").ToInt();
					var planetModifier = this.GetAbilityValue("Modify Reproduction - Planet").ToInt();
					var reproduction = ((Mod.Current.Settings.Reproduction + (race.Aptitudes["Reproduction"] - 100) + sysModifier + planetModifier) * Mod.Current.Settings.ReproductionMultiplier) / 100d;
					deltapop[race] = (long)(Colony.Population[race] * reproduction);

					// TODO - allow cloning of populations over the max of a 32 bit int?
					var sysCloning = sys == null ? 0 : sys.GetEmpireAbilityValue(Owner, "Change Population - System").ToInt();
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
			base.Redact(emp);

			var vis = CheckVisibility(emp);

			MoonOf = null; // in case we allow moons to have different visibility than their parent planets

			if (Colony != null && Colony.CheckVisibility(emp) < Visibility.Fogged)
				Colony = null;

			if (vis < Visibility.Owned)
				Orders.Clear();
		}

		public double MineralsValue { get { return ResourceValue[Resource.Minerals]; } }
		public double OrganicsValue { get { return ResourceValue[Resource.Organics]; } }
		public double RadioactivesValue { get { return ResourceValue[Resource.Radioactives]; } }

		public override bool IsIdle
		{
			get
			{
				if (Colony == null)
					return false;
				return ConstructionQueue != null && ConstructionQueue.IsIdle;
			}
		}

		public long PopulationStorageFree
		{
			get { return MaxPopulation - (Colony == null ? 0L : Colony.Population.Sum(kvp => kvp.Value)); }
		}

		public long AddPopulation(Race race, long amount)
		{
			if (Colony == null)
				return amount; // can't add population with no colony
			var canPop = Math.Min(amount, PopulationStorageFree);
			amount -= canPop;
			Colony.Population[race] += canPop;
			var canCargo = Math.Min(amount, (long)(this.CargoStorageFree() / Mod.Current.Settings.PopulationSize));
			amount -= canCargo;
			Colony.Cargo.Population[race] += canCargo;
			return amount;
		}

		public long RemovePopulation(Race race, long amount)
		{
			if (Colony == null)
				return amount; // can't remove population with no colony
			var canCargo = Math.Min(amount, Colony.Cargo.Population[race]);
			amount -= canCargo;
			Colony.Cargo.Population[race] -= canCargo;
			var canPop = Math.Min(amount, Colony.Population[race]);
			amount -= canPop;
			Colony.Population[race] -= canPop;
			return amount;
		}

		public bool AddUnit(IUnit unit)
		{
			if (this.CargoStorageFree() >= unit.Design.Hull.Size)
			{
				Colony.Cargo.Units.Add(unit);
				return true;
			}
			return false;
		}

		public bool RemoveUnit(IUnit unit)
		{
			if (Colony.Cargo.Units.Contains(unit))
			{
				Colony.Cargo.Units.Remove(unit);
				return true;
			}
			return false;
		}

		public IDictionary<Race, long> AllPopulation
		{
			get
			{
				var dict = new SafeDictionary<Race, long>();
				if (Colony != null)
				{
					foreach (var kvp in Colony.Population)
						dict[kvp.Key] += kvp.Value;
					foreach (var kvp in Colony.Cargo.Population)
						dict[kvp.Key] += kvp.Value;
				}
				return dict;
			}
		}

		/// <summary>
		/// Planets can't currently move, but they can execute orders at the end of the turn.
		/// </summary>
		public double TimePerMove
		{
			get { return 1; }
		}

		public int MovementRemaining
		{
			get;
			set;
		}

		/// <summary>
		/// TODO - planetary engines? but how would we do engines per move?
		/// </summary>
		public int Speed
		{
			get { return 0; }
		}

		public int SupplyRemaining
		{
			get
			{
				return 0;
			}
			set
			{
				// HACK - can't throw an exception, it breaks cloning objects, so just do nothing
			}
		}

		/// <summary>
		/// When a planet spends time, all its space units in cargo spend time too.
		/// TODO - It should also perform construction here...
		/// </summary>
		/// <param name="timeElapsed"></param>
		public void SpendTime(double timeElapsed)
		{
			TimeToNextMove += timeElapsed;
			if (Cargo != null)
			{
				foreach (var u in Cargo.Units.OfType<IMobileSpaceObject>())
					u.SpendTime(timeElapsed);
			}
		}

		[DoNotSerialize]
		public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap
		{
			get;
			set;
		}

		public Fleet Container
		{
			get { return Galaxy.Current.FindSpaceObjects<Fleet>(f => f.Vehicles.Contains(this)).SingleOrDefault(); }
		}


		public IEnumerable<IUnit> AllUnits
		{
			get { return Cargo == null ? Enumerable.Empty<IUnit>() : Cargo.Units; }
		}

		[DoNotSerialize(false)]
		public override Sector Sector
		{
			get
			{
				return base.Sector;
			}
			set
			{
				base.Sector = value;
				/*if (Cargo != null)
				{
					foreach (var v in Cargo.Units.OfType<IMobileSpaceObject>().ToArray())
						v.Sector = value;
				}*/
			}
		}

		int? IMobileSpaceObject.Size
		{
			get { return null; }
		}

		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Planet; }
		}


		public int ShieldHitpoints
		{
			get { return NormalShields + PhasedShields; }
		}

		/// <summary>
		/// TODO - planetary "armor" facilities that soak damage first?
		/// </summary>
		public int ArmorHitpoints
		{
			get { return Cargo == null ? 0 : Cargo.ArmorHitpoints; }
		}

		public int HullHitpoints
		{
			get { return Colony == null ? 0 : (Colony.Facilities.Sum(f => f.Hitpoints) + (int)(Colony.Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints) + Cargo.HullHitpoints); }
		}

		public int MaxShieldHitpoints
		{
			get { return MaxNormalShields + MaxPhasedShields; }
		}

		public int MaxArmorHitpoints
		{
			get { return Cargo == null ? 0 : Cargo.MaxArmorHitpoints; }
		}

		public int MaxHullHitpoints
		{
			get { return Colony == null ? 0 : (Colony.Facilities.Sum(f => f.MaxHitpoints) + (int)(Colony.Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints) + Cargo.MaxHullHitpoints); }
		}

		/// <summary>
		/// Planets currently cost nothing to maintain.
		/// TODO - moddable unit/population/facility maintenance
		/// </summary>
		public ResourceQuantity MaintenanceCost
		{
			get { return new ResourceQuantity(); }
		}

		IEnumerable<Ability> IAbilityObject.IntrinsicAbilities
		{
			get { return IntrinsicAbilities; }
		}

		public override IEnumerable<IAbilityObject> Children
		{
			get
			{
				if (Colony != null)
					yield return Colony;
			}
		}

		public Progress PopulationFill
		{
			get
			{
				var pop = 0L;
				if (Colony != null)
					pop = Colony.Population.Sum(kvp => kvp.Value);
				return new Progress(pop, MaxPopulation);
			}
		}

		public int MineralsIncome { get { return this.GrossIncome()[Resource.Minerals]; } }
		public int OrganicsIncome { get { return this.GrossIncome()[Resource.Organics]; } }
		public int RadioactivesIncome { get { return this.GrossIncome()[Resource.Radioactives]; } }
		public int ResearchIncome { get { return this.GrossIncome()[Resource.Research]; } }
		public int IntelligenceIncome { get { return this.GrossIncome()[Resource.Intelligence]; } }

		public bool HasColony { get { return Colony != null; } }
		public bool HasSpaceYard { get { return this.HasAbility("Space Yard"); } }

		public Progress FacilityFill
		{
			get
			{
				var facils = 0;
				if (Colony != null)
					facils = Colony.Facilities.Count;
				return new Progress(facils, MaxFacilities);
			}
		}

		public Progress CargoFill
		{
			get
			{
				var cargo = 0;
				if (Colony != null)
					cargo = Colony.Cargo.Size;
				return new Progress(cargo, MaxCargo);
			}
		}

		/// <summary>
		/// Planets don't have to worry about supplies.
		/// </summary>
		public void BurnMovementSupplies()
		{
			// do nothing
		}

		public override void Dispose()
		{
			if (IsDisposed)
				return;
			var sys = this.FindStarSystem();
			if (sys != null)
				sys.Remove(this);
			if (Cargo != null)
			{
				foreach (var u in Cargo.Units)
					u.Dispose();
			}
			if (Colony != null)
				Colony.Dispose();
			Galaxy.Current.UnassignID(this);
			if (!IsMemory)
				this.UpdateEmpireMemories();
			IsDisposed = true;
		}

		/// <summary>
		/// Deletes this planet and spawns an asteroid field with the same name, sector, size, and value as this planet.
		/// If there are no appropriate asteroid field templates in the mod, simply deletes the planet.
		/// </summary>
		public void ConvertToAsteroidField()
		{
			var sector = Sector;
			var size = Size;
			var value = ResourceValue;
			var name = Name;

			var astTemplates = Mod.Current.StellarObjectTemplates.OfType<AsteroidField>().Where(a => a.Size == size);
			if (astTemplates.Any())
			{
				var astTemplate = astTemplates.PickRandom();
				var ast = astTemplate.Instantiate();
				ast.Sector = Sector;
				ast.ResourceValue = value;
				ast.Name = name;
				ast.UpdateEmpireMemories();
			}

			Dispose();
		}

		public ResourceQuantity StandardIncomePercentages
		{
			get
			{
				if (HasColony)
					return Colony.StandardIncomePercentages;
				return new ResourceQuantity();
			}
		}

		public ResourceQuantity RemoteMiningIncomePercentages
		{
			get
			{
				if (HasColony)
					return Colony.RemoteMiningIncomePercentages;
				return new ResourceQuantity();
			}
		}

		public override IEnumerable<IAbilityObject> Parents
		{
			get
			{
				if (Sector != null)
					yield return Sector;
				if (Owner != null)
					yield return Owner;
			}
		}

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var dict = base.Data;
				dict[nameof(size)] = size;
				dict[nameof(Surface)] = Surface;
				dict[nameof(Atmosphere)] = Atmosphere;
				dict[nameof(ResourceValue)] = ResourceValue;
				dict[nameof(Colony)] = Colony;
				dict[nameof(Orders)] = Orders;
				return dict;
			}
			set
			{
				base.Data = value;
				size = value[nameof(size)].Default<ModReference<StellarObjectSize>>();
				Surface = value[nameof(Surface)].Default<string>();
				Atmosphere = value[nameof(Atmosphere)].Default<string>();
				ModID = value[nameof(ModID)].Default<string>();
				ResourceValue = value[nameof(ResourceValue)].Default(new ResourceQuantity());
				Colony = value[nameof(Colony)].Default<Colony>();
				Orders = value[nameof(Orders)].Default(new List<IOrder<Planet>>());
			}
		}

		public override bool CanBeObscured => true;

		public double MerchantsRatio
		{
			get
			{
				var totalpop = AllPopulation.Sum(x => x.Value);
				return AllPopulation.Sum(x => (double)x.Value / (double)totalpop * (x.Key.HasAbility("No Spaceports") ? 1d : 0d));
			}
		}
	}
}