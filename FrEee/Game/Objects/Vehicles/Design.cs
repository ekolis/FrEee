using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;
using System.Drawing;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// Creates designs.
	/// </summary>
	public static class Design
	{
		public static IDesign Create(VehicleTypes vt)
		{
			switch (vt)
			{
				case VehicleTypes.Ship:
					return new Design<Ship>();
				case VehicleTypes.Base:
					return new Design<Base>();
				case VehicleTypes.Fighter:
					return new Design<Fighter>();
				case VehicleTypes.Troop:
					return new Design<Troop>();
				case VehicleTypes.Mine:
					return new Design<Mine>();
				case VehicleTypes.Satellite:
					return new Design<Satellite>();
				case VehicleTypes.Drone:
					return new Design<Drone>();
				case VehicleTypes.WeaponPlatform:
					return new Design<WeaponPlatform>();
				default:
					throw new Exception("Cannot create a design for vehicle type " + vt + ".");
			}
		}
	}

	/// <summary>
	/// A vehicle design.
	/// </summary>
	/// <typeparam name="T">The type of vehicle.</typeparam>
	public class Design<T> : IDesign, ITemplate<T> where T : Vehicle<T>, new()
	{
		public Design()
		{
			Components = new List<MountedComponentTemplate>();
		}

		public string Name { get; set; }

		/// <summary>
		/// The empire which created this design.
		/// </summary>
		public Empire Owner { get; set; }

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
		public Hull<T> Hull { get; set; }

		/// <summary>
		/// The components used in this design.
		/// </summary>
		public IList<MountedComponentTemplate> Components
		{
			get;
			private set;
		}

		public T Instantiate()
		{
			var t = new T();
			t.Design = this;
			foreach (var mct in Components)
				t.Components.Add(mct.Instantiate());
			return t;
		}

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
				if (VehicleType == VehicleTypes.WeaponPlatform)
					return "Weapon Platform"; // add the space
				return VehicleType.ToString();
			}
		}

		/// <summary>
		/// The ship's role (design type in SE4).
		/// </summary>
		public string Role { get; set; }

		/// <summary>
		/// The turn this design was created (for our designs) or discovered (for alien designs).
		/// </summary>
		public int TurnNumber { get; set; }

		public bool IsObsolete { get; set; }

		public IEnumerable<string> Warnings
		{
			get
			{
				if (Hull == null)
					yield return "You must select a hull for your design.";
				var comps = Components.Select(comp => comp.ComponentTemplate);
				if (Hull.NeedsBridge && !comps.Any(comp => comp.HasAbility("Ship Bridge")))
					yield return "This hull requires a bridge.";
				if (comps.Count(comp => comp.HasAbility("Ship Bridge")) > 1)
					yield return "A vehicle can have no more than one bridge.";
				if (!Hull.CanUseAuxiliaryControl && comps.Any(comp => comp.HasAbility("Ship Auxiliary Control")))
					yield return "This hull cannot use auxiliary control.";
				if (comps.Count(comp => comp.HasAbility("Ship Auxiliary Control")) > 1)
					yield return "A vehicle can have no more than one auxiliary control.";
				if (comps.Count(comp => comp.HasAbility("Standard Ship Movement")) > Hull.MaxEngines)
					yield return "This hull can only support " + Hull.MaxEngines + " engines.";
				if (comps.Count(comp => comp.HasAbility("Ship Life Support")) < Hull.MinLifeSupport)
					yield return "This hull requires at least " + Hull.MinCrewQuarters + " life support modules.";
				if (comps.Count(comp => comp.HasAbility("Ship Crew Quarters")) < Hull.MinCrewQuarters)
					yield return "This hull requires at least " + Hull.MinCrewQuarters + " crew quarters.";
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

				// TODO - warnings for illegal mount usage
			}
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

		/// <summary>
		/// The resource cost to build the design.
		/// </summary>
		public Resources Cost
		{
			get
			{
				if (!Components.Any())
					return Hull.Cost;
				return Hull.Cost + Components.Select(c => c.Cost).Sum();
			}
		}

		public int Speed
		{
			get
			{
				// no Engines Per Move rating? then no movement
				if (Hull.Mass == 0)
					return 0;
				var thrust = this.GetAbilityValue("Standard Ship Movement").ToInt();
				// TODO - make sure that Movement Bonus and Extra Movement are not in fact affected by Engines Per Move in SE4
				return thrust / Hull.Mass + this.GetAbilityValue("Movement Bonus").ToInt() + this.GetAbilityValue("Extra Movement").ToInt();
			}
		}

		public IEnumerable<Abilities.Ability> Abilities
		{
			get { return Hull.Abilities.Concat(Components.SelectMany(c => c.Abilities)).Stack(); }
		}

		public int SupplyUsage
		{
			get
			{
				return Speed * Components.Where(comp => comp.HasAbility("Standard Ship Movement") || comp.HasAbility("Extra Movement") || comp.HasAbility("Movement Bonus")).Sum(comp => comp.SupplyUsage);
			}
		}


		public int ShieldHitpoints
		{
			get { return this.GetAbilityValue("Planet - Shield Generation").ToInt() + this.GetAbilityValue("Shield Generation").ToInt() + this.GetAbilityValue("Phased Shield Generation").ToInt(); }
		}

		public int ShieldRegeneration
		{
			get { return this.GetAbilityValue("Shield Regeneration").ToInt(); }
		}

		public int ArmorHitpoints
		{
			get { return this.Components.Where(c => c.ComponentTemplate.HasAbility("Armor")).Sum(c => c.Durability); }
		}

		public int HullHitpoints
		{
			get { return this.Components.Where(c => !c.ComponentTemplate.HasAbility("Armor")).Sum(c => c.Durability); }
		}

		public int Accuracy
		{
			get { return this.GetAbilityValue("Combat To Hit Offense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Offense Minus").ToInt(); }
		}

		public int Evasion
		{
			get { return this.GetAbilityValue("Combat To Hit Defense Plus").ToInt() - this.GetAbilityValue("Combat To Hit Defense Minus").ToInt(); }
		}

		public int CargoCapacity
		{
			get { return this.GetAbilityValue("Cargo Storage").ToInt(); }
		}

		public Image Icon
		{
			get
			{
				return Hull.GetIcon(Owner.ShipsetPath);
			}
		}

		public Image Portrait
		{
			get
			{
				return Hull.GetPortrait(Owner.ShipsetPath);
			}
		}
	}
}