using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Combat2;
using AutoMapper;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A (typically) naturally occurring, large, immobile space object.
	/// </summary>
	 [Serializable] public abstract class StellarObject : IStellarObject
	{
		public StellarObject()
		{
			IntrinsicAbilities = new List<Ability>();
		}

		/// <summary>
		/// Used for naming.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// Used for naming.
		/// </summary>
		public bool IsUnique { get; set; }

		/// <summary>
		/// The name of this stellar object.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A description of this stellar object.
		/// </summary>
		public string Description { get; set; }
		 
		/// <summary>
		/// Name of the picture used to represent this stellar object, excluding the file extension.
		/// PNG files will be searched first, then BMP.
		/// </summary>
		public string PictureName { get; set; }

		public virtual Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		[DoNotSerialize] public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// Abilities intrinsic to this stellar object.
		/// </summary>
		public IList<Ability> IntrinsicAbilities { get; private set; }

		/// <summary>
		/// Typical stellar objects aren't owned by any empire, so this return null for most types.
		/// </summary>
		public virtual Empire Owner { get { return null; } }

		/// <summary>
		/// Most stellar objects don't need to have any data redacted.
		/// </summary>
		/// <param name="galaxy"></param>
		/// <param name="starSystem"></param>
		/// <param name="visibility"></param>
		public virtual void Redact(Galaxy galaxy, StarSystem starSystem, Visibility visibility)
		{
			if (visibility == Visibility.Unknown)
				throw new ArgumentException("If a stellar object is not visible at all, it should be removed from the player's savegame rather than redacted.", "visibility");
		}

		public override string ToString()
		{
			return Name;
		}
		 

		public void Dispose()
		{
			if (IsDisposed)
				return;
			var sys = this.FindStarSystem();
			if (sys != null)
				sys.Remove(this);
			Galaxy.Current.UnassignID(this);
			this.UpdateEmpireMemories();
		}

		public StellarSize StellarSize
		{
			get;
			set;
		}


		public virtual bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsHostileTo(emp, StarSystem);
		}

		public virtual bool CanBeInFleet
		{
			get { return false; }
		}

		public int SupplyStorage
		{
			get { return this.GetAbilityValue("Supply Storage").ToInt(); }
		}

		public bool HasInfiniteSupplies
		{
			get { return this.HasAbility("Quantum Reactor"); }
		}


		public virtual ConstructionQueue ConstructionQueue
		{
			get { return null; }
		}

		/// <summary>
		/// Stellar objects can't normally warp.
		/// </summary>
		public virtual bool CanWarp { get { return false; } }

		public Visibility CheckVisibility(Empire emp)
		{
			if (this.FindStarSystem() == null)
				return Visibility.Scanned;  // probably a mod definition

			if (emp == Owner)
				return Visibility.Owned;

			// You can always scan stellar objects you are in combat with.
			if (Battle.Current.Any(b => b.Combatants.OfType<StellarObject>().Contains(this) && b.Combatants.Any(c => c.Owner == emp)))
				return Visibility.Scanned;
			if (Battle_Space.Current.Union(Battle_Space.Previous).Any(b => (b.StartCombatants.OfType<StellarObject>().Contains(this) || b.ActualCombatants.OfType<StellarObject>().Contains(this)) && b.ActualCombatants.Any(c => c.Owner == emp)))
				return Visibility.Scanned;

			// TODO - cloaking

			var seers = this.FindStarSystem().FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == emp);
			if (!seers.Any())
			{
				if (Galaxy.Current.OmniscientView)
					return Visibility.Visible;
				var known = emp.Memory[ID];
				if (known != null && this.GetType() == known.GetType())
					return Visibility.Fogged;
				else if (Battle.Previous.Any(b => b.Combatants.OfType<StellarObject>().Contains(this) && b.Combatants.Any(c => c.Owner == emp)))
					return Visibility.Fogged;
				else
					return Visibility.Unknown;
			}
			if (this.HasAbility("Scanner Jammer"))
			{
				var scanners = seers.Where(sobj => 
					sobj.HasAbility("Long Range Scanner") && sobj.GetAbilityValue("Long Range Scanner").ToInt() >= sobj.FindSector().Coordinates.EightWayDistance(this.FindSector().Coordinates)
					|| sobj.HasAbility("Long Range Scanner - System"));
				if (scanners.Any())
					return Visibility.Scanned;
			}
			return Visibility.Visible;
		}

		public long ID { get; set; }


		public abstract void Redact(Empire emp);

		/// <summary>
		/// Stellar objects by default can't be idle, because they can't take orders or build stuff to begin with.
		/// </summary>
		public virtual bool IsIdle
		{
			get { return false; }
		}

		[DoNotSerialize]
		[IgnoreMap]
		public virtual Sector Sector
		{
			get
			{
				return this.FindSector();
			}
			set
			{
				if (value == null)
				{
					if (Sector != null)
						Sector.Remove(this);
				}
				else
					value.Place(this);
			}
		}

		public StarSystem StarSystem
		{
			get { return this.FindStarSystem(); }
		}

		public bool IsMemory
		{
			get;
			set;
		}

		public double Timestamp { get; set; }

		public bool IsObsoleteMemory(Empire emp)
		{
			if (StarSystem == null)
				return Timestamp < Galaxy.Current.Timestamp - 1;
			return StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public abstract AbilityTargets AbilityTarget { get; }

		IEnumerable<Ability> IAbilityObject.IntrinsicAbilities
		{
			get { return IntrinsicAbilities; }
		}

		public IEnumerable<IAbilityObject> Children
		{
			get { yield break; }
		}

		public IAbilityObject Parent
		{
			get { return StarSystem; }
		}

		public string ModID
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }
	}
}
