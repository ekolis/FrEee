using FrEee.Objects.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Combat;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
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
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Civilization.Construction;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Extensions;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Objects.Space;

/// <summary>
/// A planet. Planets can be colonized or mined.
/// </summary>
[Serializable]
public class Planet : StellarObject, ITemplate<Planet>, IOrderable, ICombatSpaceObject, ICargoTransferrer, IReferrable, IMobileSpaceObject<Planet>, IMineableSpaceObject, IIncomeProducer, IDataObject, ITransferrable, ITargetable, IConstructor
{
	public Planet()
	{
		ResourceValue = new ResourceQuantity();
		Orders = new List<IOrder>();
	}

	public override AbilityTargets AbilityTarget => AbilityTargets.Planet;

	public int Accuracy
	{
		get =>
			Mod.Current.Settings.PlanetAccuracy
			+ this.GetAbilityValue("Combat To Hit Offense Plus").ToInt()
			- this.GetAbilityValue("Combat To Hit Offense Minus").ToInt()
			+ (Owner == null || Owner.Culture == null ? 0 : Owner.Culture.SpaceCombat)
			+ Sector.GetEmpireAbilityValue(Owner, "Combat Modifier - Sector").ToInt()
			+ StarSystem.GetEmpireAbilityValue(Owner, "Combat Modifier - System").ToInt()
			+ Owner.GetAbilityValue("Combat Modifier - Empire").ToInt();
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

	public IEnumerable<IUnit> AllUnits => Cargo?.Units ?? Enumerable.Empty<IUnit>();

	/// <summary>
	/// Are this object's orders on hold?
	/// </summary>
	public bool AreOrdersOnHold { get; set; }

	/// <summary>
	/// Should this object's orders repeat once they are completed?
	/// </summary>
	public bool AreRepeatOrdersEnabled { get; set; }

	/// <summary>
	/// TODO - planetary "armor" facilities that soak damage first?
	/// </summary>
	public int ArmorHitpoints => Cargo?.ArmorHitpoints ?? 0;

	/// <summary>
	/// The atmospheric composition (e.g. methane, oxygen, carbon dioxide) of this planet.
	/// </summary>
	public string Atmosphere { get; set; }

	public override bool CanBeInFleet => false;

	public override bool CanBeObscured => true;

	/// <summary>
	/// If planets had engines, they could warp...
	/// (Sure, why not?)
	/// </summary>
	public override bool CanWarp => !Owner?.IsMinorEmpire ?? true;

	public Cargo Cargo => Colony?.Cargo;

	public Progress CargoFill => new Progress(Colony?.Cargo.Size ?? 0, MaxCargo);

	/// <summary>
	/// Planets get cargo storage both from facilities and intrinsically.
	/// </summary>
	public int CargoStorage => MaxCargo + this.GetAbilityValue("Cargo Storage").ToInt();

	public override IEnumerable<IAbilityObject> Children
	{
		get
		{
			if (Colony != null)
				yield return Colony;
		}
	}

	/// <summary>
	/// The environmental conditions of this planet. Affects reproduction rate of populations.
	/// </summary>
	public Conditions Conditions => Mod.Current.Settings.ConditionsThresholds.Where(x => x.Value <= ConditionsAmount).WithMax(x => x.Value).Single().Key;

	public Progress ConditionsProgress => new Progress(ConditionsAmount, Mod.Current.Settings.MaxConditions);

	/// <summary>
	/// Numeric representation of plantery conditions.
	/// </summary>
	public int ConditionsAmount { get; set; }

	public string ColonizationAbilityName => "Colonize Planet - " + Surface;

	/// <summary>
	/// The colony on this planet, if any.
	/// </summary>
	public Colony Colony { get; set; }

	public double CombatSpeed => 0;

	public override ConstructionQueue ConstructionQueue => Colony?.ConstructionQueue;

	public Fleet Container { get; set; }

	public override SafeDictionary<string, object> Data
	{
		get
		{
			var dict = base.Data;
			dict[nameof(size)] = size;
			dict[nameof(Surface)] = Surface;
			dict[nameof(Atmosphere)] = Atmosphere;
			dict[nameof(ConditionsAmount)] = ConditionsAmount;
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
			ConditionsAmount = value[nameof(ConditionsAmount)].Default<int>();
			ModID = value[nameof(ModID)].Default<string>();
			ResourceValue = value[nameof(ResourceValue)].Default(new ResourceQuantity());
			Colony = value[nameof(Colony)].Default<Colony>();
			Orders = value[nameof(Orders)].Default(new List<IOrder>());
		}
	}

	[DoNotSerialize]
	public IDictionary<PathfinderNode<Sector>, ISet<PathfinderNode<Sector>>> DijkstraMap
	{
		get;
		set;
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

	public bool HasColony => Colony != null;

	public bool HasSpaceYard => this.HasAbility("Space Yard");

	public int HitChance => 1;

	[DoNotSerialize]
	public int Hitpoints
	{
		get
		{
			if (Colony == null)
				return 0;
			return Cargo.Hitpoints + Colony.Facilities.Sum(f => f.Hitpoints) + (int)AllPopulation.Sum(kvp => kvp.Value * Mod.Current.Settings.PopulationHitpoints);
		}
		set
		{
			// can't set HP of planet!
		}
	}

	public int HullHitpoints
	{
		get { return Colony == null ? 0 : (Colony.Facilities.Sum(f => f.Hitpoints) + (int)(Colony.Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints) + Cargo.HullHitpoints); }
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

	public int IntelligenceIncome => this.GrossIncome()[Resource.Intelligence];

	IEnumerable<Ability> IAbilityObject.IntrinsicAbilities => IntrinsicAbilities;

	public bool IsAlive => Colony != null;

	/// <summary>
	/// Planets can't be destroyed in combat.
	/// </summary>
	public bool IsDestroyed => false;

	/// <summary>
	/// Is this planet domed? Domed planets usually have less space for population, facilities, and cargo.
	/// </summary>
	public bool IsDomed => Colony?.Population.Any(kvp => kvp.Key.NativeAtmosphere != Atmosphere) ?? false;

	public override bool IsIdle => Colony?.ConstructionQueue?.IsIdle ?? false;

	/// <summary>
	/// Planets currently cost nothing to maintain.
	/// TODO - moddable unit/population/facility maintenance
	/// </summary>
	public ResourceQuantity MaintenanceCost => new ResourceQuantity();

	public int MaxArmorHitpoints => Cargo?.MaxArmorHitpoints ?? 0;

	public int MaxCargo => IsDomed ? Size.MaxCargoDomed : Size.MaxCargo;

	public int MaxFacilities => IsDomed ? Size.MaxFacilitiesDomed : Size.MaxFacilities;

	public int MaxHitpoints
	{
		get
		{
			if (Colony == null)
				return 0;
			return Cargo.MaxHitpoints + Colony.Facilities.Sum(f => f.MaxHitpoints);
		}
	}

	public int MaxHullHitpoints
	{
		get { return Colony == null ? 0 : (Colony.Facilities.Sum(f => f.MaxHitpoints) + (int)(Colony.Population.Sum(kvp => kvp.Value) * Mod.Current.Settings.PopulationHitpoints) + Cargo.MaxHullHitpoints); }
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

	public long MaxPopulation => IsDomed ? Size.MaxPopulationDomed : Size.MaxPopulation;

	public int MaxShieldHitpoints
	{
		get { return MaxNormalShields + MaxPhasedShields; }
	}

	public int MaxTargets => int.MaxValue;

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

	public int MaxUnmodifiedPhasedShields
	{
		get
		{
			if (Colony == null)
				return 0;
			return Colony.Facilities.GetAbilityValue("Phased Shield Generation", this).ToInt();
		}
	}

	public double MerchantsRatio
	{
		get
		{
			var totalpop = AllPopulation.Sum(x => x.Value);
			return AllPopulation.Sum(x => (double)x.Value / (double)totalpop * (x.Key.HasAbility("No Spaceports") ? 1d : 0d));
		}
	}

	public int MineralsIncome => this.GrossIncome()[Resource.Minerals];

	public double MineralsValue => ResourceValue[Resource.Minerals];

	public int MovementRemaining { get; set; }

	public int NormalShields { get; set; }

	public IList<IOrder> Orders { get; private set; }

	IEnumerable<IOrder> IOrderable.Orders
		=> Orders;

	public int OrganicsIncome => this.GrossIncome()[Resource.Organics];

	public double OrganicsValue => ResourceValue[Resource.Organics];

	/// <summary>
	/// The empire which has a colony on this planet, if any.
	/// </summary>
	public Empire Owner
	{
		get => Colony?.Owner;
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

	public int PhasedShields { get; set; }

	/// <summary>
	/// Expected population change for the upcoming turn due to reproduction, cloning, and plagues.
	/// </summary>
	public long PopulationChangePerTurn => PopulationChangePerTurnPerRace.Sum(kvp => kvp.Value);

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
				var moodModifier = Mod.Current.Settings.MoodReproductionModifiers[Colony.Moods[race]];
				var conditionsModifier = Mod.Current.Settings.ConditionsReproductionModifiers[Conditions];
				var reproduction =
					(Mod.Current.Settings.Reproduction + (race.Aptitudes["Reproduction"] - 100)
						+ sysModifier + planetModifier + moodModifier + conditionsModifier)
					* Mod.Current.Settings.ReproductionMultiplier
					/ 100d;
				if (reproduction < 0)
					reproduction = 0;
				deltapop[race] = (long)(Colony.Population[race] * reproduction);

				// TODO - allow cloning of populations over the max of a 32 bit int?
				var sysCloning = sys == null ? 0 : sys.GetEmpireAbilityValue(Owner, "Change Population - System").ToInt();
				var planetCloning = this.GetAbilityValue("Change Population - Planet").ToInt();
				deltapop[race] += (sysCloning + planetCloning) * Mod.Current.Settings.PopulationFactor / Colony.Population.Count; // split cloning across races
			}

			return deltapop;
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

	public long PopulationStorageFree => MaxPopulation - (Colony?.Population.Sum(kvp => kvp.Value) ?? 0);

	public int RadioactivesIncome => this.GrossIncome()[Resource.Radioactives];

	public double RadioactivesValue => ResourceValue[Resource.Radioactives];

	public ResourceQuantity RemoteMiningIncomePercentages => Colony?.RemoteMiningIncomePercentages ?? new ResourceQuantity();

	public int ResearchIncome => this.GrossIncome()[Resource.Research];

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
	/// The resource value of this planet, in %.
	/// </summary>
	public ResourceQuantity ResourceValue { get; set; }

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

	public int ShieldHitpoints => NormalShields + PhasedShields;

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

	/// <summary>
	/// The PlanetSize.txt entry for this asteroid field's size.
	/// </summary>
	[DoNotSerialize]
	public StellarObjectSize Size { get => size; set => size = value; }

	int ICombatant.Size => int.MaxValue;

	public ResourceQuantity StandardIncomePercentages => Colony?.StandardIncomePercentages ?? new ResourceQuantity();

	/// <summary>
	/// TODO - planetary engines? but how would we do engines per move?
	/// </summary>
	public int StrategicSpeed => 0;

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
	/// The surface composition (e.g. rock, ice, gas) of this planet.
	/// </summary>
	public string Surface { get; set; }

	/// <summary>
	/// Planets can't currently move, but they can execute orders at the end of the turn.
	/// </summary>
	public double TimePerMove => 1;

	/// <summary>
	/// Fractional turns until the planet has saved up another move point.
	/// </summary>
	public double TimeToNextMove { get; set; }

	public IEnumerable<Component> Weapons
	{
		get
		{
			if (Cargo == null)
				return Enumerable.Empty<Component>();
			return Cargo.Units.OfType<WeaponPlatform>().SelectMany(wp => wp.Weapons).Where(x => !x.IsDestroyed);
		}
	}

	public WeaponTargets WeaponTargetType => WeaponTargets.Planet;

	/// <summary>
	/// Used for naming.
	/// </summary>
	[DoNotSerialize]
	internal Planet MoonOf { get; set; }

	private ModReference<StellarObjectSize> size { get; set; }

	public void AddOrder(IOrder order)
	{
		if (!(order is IOrder))
			throw new Exception("Can't add a " + order.GetType() + " to a planet's orders.");
		Orders.Add((IOrder)order);
	}

	public long AddPopulation(Race race, long amount)
	{
		// make sure planet has a colony
		if (Colony == null)
			return amount; // can't add population with no colony

		// put population in planetary population storage
		var canPop = Math.Min(amount, PopulationStorageFree);
		amount -= canPop;
		Colony.Population[race] += canPop;

		// don't put population in planetary cargo storage, that's just confusing
		/*var canCargo = Math.Min(amount, (long)(this.CargoStorageFree() / Mod.Current.Settings.PopulationSize));
		amount -= canCargo;
		Colony.Cargo.Population[race] += canCargo;*/

		// apply anger to population
		if (!Colony.Anger.ContainsKey(race))
			Colony.Anger[race] = Mod.Current.Settings.StartPopulationAnger;

		// return leftover population
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

	/// <summary>
	/// Planets don't have to worry about supplies.
	/// </summary>
	public void BurnMovementSupplies()
	{
		// do nothing
	}

	public bool CanTarget(ITargetable target) => Cargo?.Units.OfType<WeaponPlatform>().Any(wp => wp.CanTarget(target)) ?? false;

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
	/// Draws "population bars" on an image of the planet.
	/// Make sure not to draw on an original; make a copy first!
	/// </summary>
	/// <param name="img"></param>
	public void DrawPopulationBars(Image img, int? expectedSize = null)
	{
		if (Colony != null)
		{
			// draw population bar
			var g = Graphics.FromImage(img);
			double scaleFactor = 1;
			if (expectedSize != null)
				scaleFactor = (double)expectedSize / Math.Max(img.Width, img.Height);
			var rect = new Rectangle((int)(img.Width - 21 / scaleFactor), (int)(1 / scaleFactor), (int)(20 / scaleFactor), (int)(8 / scaleFactor));
			var pen = new Pen(Colony.Owner.Color, 1 / (float)scaleFactor);
			g.FillRectangle(Brushes.Black, rect);
			g.DrawRectangle(pen, rect);
			var brush = new SolidBrush(Colony.Owner.Color);
			var pop = Colony.Population.Sum(kvp => kvp.Value);
			rect.Width = (int)(5 / scaleFactor);
			rect.X += (int)(1 /scaleFactor);
			rect.Y += (int)(2 / scaleFactor);
			rect.Height -= (int)(3 / scaleFactor);
			rect.X += (int)(1 / scaleFactor);
			if (pop > 0)
				g.FillRectangle(brush, rect);
			rect.X += (int)(6 /scaleFactor);
			if (pop > MaxPopulation / 3)
				g.FillRectangle(brush, rect);
			rect.X += (int)(6 / scaleFactor);
			if (pop > MaxPopulation * 2 / 3)
				g.FillRectangle(brush, rect);
		}
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

	public bool ExecuteOrders() => this.ExecuteMobileSpaceObjectOrders();

	/// <summary>
	/// Just copy the planet's data.
	/// </summary>
	/// <returns>A copy of the planet.</returns>
	public Planet Instantiate()
	{
		var result = this.CopyAndAssignNewID();
		result.ModID = null;
		return result;
	}

	public override bool IsHostileTo(Empire emp) => Owner?.IsEnemyOf(emp, StarSystem) ?? false;

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

	public void RearrangeOrder(IOrder order, int delta)
	{
		if (!(order is IOrder))
			throw new Exception("Can't rearrange a " + order.GetType() + " in a planet's orders.");
		var o = (IOrder)order;
		var newpos = Orders.IndexOf(o) + delta;
		if (newpos < 0)
			newpos = 0;
		Orders.Remove(o);
		if (newpos >= Orders.Count)
			Orders.Add(o);
		else
			Orders.Insert(newpos, o);
	}

	public override void Redact(Empire emp)
	{
		base.Redact(emp);

		var vis = CheckVisibility(emp);

		MoonOf = null; // in case we allow moons to have different visibility than their parent planets

		if (Colony != null && Colony.CheckVisibility(emp) < Visibility.Fogged)
			Colony = null;

		if (vis < Visibility.Owned)
		{
			// TODO - espionage?
			Orders.Clear();
			AreOrdersOnHold = false;
			AreRepeatOrdersEnabled = false;
		}
	}

	public void RemoveOrder(IOrder order)
	{
		if (order != null && !(order is IOrder))
			return; // order can't exist here anyway
		Orders.Remove((IOrder)order);
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

	public bool RemoveUnit(IUnit unit)
	{
		if (Colony.Cargo.Units.Contains(unit))
		{
			Colony.Cargo.Units.Remove(unit);
			return true;
		}
		return false;
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

	public int TakeDamage(Hit hit, PRNG dice = null)
	{
		var damage = hit.NominalDamage;

		if (Colony == null)
			return damage; // uninhabited planets can't take damage

		// let shields mitigate incoming damage
		damage = this.TakeShieldDamage(hit, damage, dice);

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
			if (hit.NominalDamage <= 0)
				break;
		}

		// if planet was completely glassed, remove the colony
		if (!Colony.Population.Any(p => p.Value > 0) && !Cargo.Units.Any() && !Cargo.Population.Any(p => p.Value > 0) && !Colony.Facilities.Any())
		{
			if (Colony.IsHomeworld)
				Colony.Owner.TriggerHappinessChange(hm => hm.OurHomeworldLost);
			Colony.Owner.TriggerHappinessChange(hm => hm.OurPlanetLost);
			Colony.Dispose();
		}

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

	private int TakePopulationDamage(Hit hit, PRNG dice = null)
	{
		if (Colony == null)
			return hit.NominalDamage;
		int damage = hit.NominalDamage;
		int inflicted = 0;
		long totalPopKilled = 0;
		for (int i = 0; i < damage; i++)
		{
			// pick a race and kill some population
			var race = Colony.Population.PickWeighted(dice);
			if (race == null)
				break; // no more population
			double popHPPerPerson = Mod.Current.Settings.PopulationHitpoints;
			// TODO - don't ceiling the popKilled, just stack it up
			int popKilled = (int)Math.Ceiling(hit.Shot.DamageType.PopulationDamage.Evaluate(hit.Shot) / 100 / popHPPerPerson);
			totalPopKilled += popKilled;
			Colony.Population[race] -= popKilled;
			if (Colony.Population[race] < 0)
				Colony.Population[race] = 0;
			inflicted += 1;
		}
		Colony.TriggerHappinessChange(hm => (int)(hm.OneMillionPopulationKilled * totalPopKilled / 1_000_000));
		// clear population that was emptied out
		foreach (var race in Colony.Population.Where(kvp => kvp.Value <= 0).Select(kvp => kvp.Key).ToArray())
			Colony.Population.Remove(race);
		return damage - inflicted;
	}

	public Progress AngerProgress => new Progress(Colony?.AverageAnger ?? 0, Mod.Current.Settings.MaxAnger);

	public IEnumerable<Component> Components => Cargo.Units.OfType<WeaponPlatform>().SelectMany(q => q.Components);

	public bool FillsCombatTile => true;
}
