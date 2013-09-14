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
		/// Typical stellar objects don't inherit any abilities, so this is usually just the intrinsic abilities.
		/// </summary>
		public virtual IEnumerable<Ability> Abilities
		{
			get { return IntrinsicAbilities; }
		}

		public IEnumerable<Ability> UnstackedAbilities
		{
			get { return Abilities; }
		}

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
			this.FindStarSystem().Remove(this);
			Galaxy.Current.UnassignID(this);
		}

		public StellarSize StellarSize
		{
			get;
			set;
		}


		public virtual bool IsHostileTo(Empire emp)
		{
			return Owner == null ? false : Owner.IsHostileTo(emp);
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


		public ConstructionQueue ConstructionQueue
		{
			get { return null; }
		}

		/// <summary>
		/// Stellar objects can't normally warp.
		/// </summary>
		public virtual bool CanWarp { get { return false; } }

		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			// TODO - cloaking
			if (this.FindStarSystem() == null)
				return Visibility.Unknown;
			var seers = this.FindStarSystem().FindSpaceObjects<ISpaceObject>(sobj => sobj.Owner == emp).Flatten();
			if (!seers.Any())
			{
				if (Galaxy.Current.OmniscientView)
					return Visibility.Visible;
				if (this.FindStarSystem().ExploredByEmpires.Contains(emp))
					return Visibility.Fogged; // TODO - set as unknown if the object was created after the empire last explored the system
				return Visibility.Unknown;
			}
			var scanners = seers.Where(sobj => sobj.GetAbilityValue("Long Range Scanner").ToInt() >= sobj.FindSector().Coordinates.EightWayDistance(this.FindSector().Coordinates));
			if (scanners.Any())
				return Visibility.Scanned;
			return Visibility.Visible;
		}

		public long ID { get; set; }


		public abstract void Redact(Empire emp);
	}
}
