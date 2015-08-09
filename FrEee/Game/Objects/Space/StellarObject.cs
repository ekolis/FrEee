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
using FrEee.Modding;


namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A (typically) naturally occurring, large, immobile space object.
	/// </summary>
	[Serializable]
	public abstract class StellarObject : IStellarObject, IAbstractDataObject
	{
		public StellarObject()
		{
			IntrinsicAbilities = new List<Ability>();
			StoredResources = new ResourceQuantity();
		}

		Empire IOwnable.Owner
		{
			get
			{
				return null;
			}
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

		[DoNotSerialize]
		public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// Abilities intrinsic to this stellar object.
		/// </summary>
		public IList<Ability> IntrinsicAbilities { get; private set; }

		// TODO - rename to IntrinsicAbilities in IAbilityContainer and remove DoNotSerialize
		[DoNotSerialize]
		public IList<Ability> Abilities
		{
			get
			{
				return IntrinsicAbilities;
			}
			set
			{
				IntrinsicAbilities = value;
			}
		}

		public override string ToString()
		{
			return Name;
		}


		public virtual void Dispose()
		{
			if (IsDisposed)
				return;
			var sys = this.FindStarSystem();
			if (sys != null)
				sys.Remove(this);
			Galaxy.Current.UnassignID(this);
			if (!IsMemory)
				this.UpdateEmpireMemories();
		}

		public StellarSize StellarSize
		{
			get;
			set;
		}

		/// <summary>
		/// Stellar objects don't typically have owners, so they can't usually be hostile.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public virtual bool IsHostileTo(Empire emp)
		{
			return false;
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
			if ((Sector == null || StarSystem == null) && Mod.Current.StellarObjectTemplates.Contains(this))
				return Visibility.Scanned; // can always see the mod

			return this.CheckSpaceObjectVisibility(emp);
		}

		public long ID { get; set; }


		/// <summary>
		/// Removes any data from this stellar object that the specified empire should not see.
		/// Disposes of the stellar object if it is invisible to the empire.
		/// </summary>
		/// <param name="emp"></param>
		public virtual void Redact(Empire emp)
		{
			var vis = CheckVisibility(emp);
			if (vis < Visibility.Fogged)
				Dispose();
		}

		/// <summary>
		/// Stellar objects by default can't be idle, because they can't take orders or build stuff to begin with.
		/// </summary>
		public virtual bool IsIdle
		{
			get { return false; }
		}

		[DoNotSerialize(false)]
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

		public virtual IEnumerable<IAbilityObject> Children
		{
			get { yield break; }
		}

		public virtual IEnumerable<IAbilityObject> Parents
		{
			get
			{
				if (Sector != null)
					yield return Sector;
			}
		}

		public string ModID
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// Resources stored on this stellar object.
		/// </summary>
		public ResourceQuantity StoredResources { get; private set; }

		public virtual SafeDictionary<string, object> Data
		{
			get
			{
				var dict = new SafeDictionary<string, object>();
				dict[nameof(Index)] = Index;
				dict[nameof(IsUnique)] = IsUnique;
				dict[nameof(Name)] = Name;
				dict[nameof(Description)] = Description;
				dict[nameof(PictureName)] = PictureName;
				dict[nameof(IntrinsicAbilities)] = IntrinsicAbilities;
				dict[nameof(StellarSize)] = StellarSize;
				dict[nameof(ID)] = ID;
				dict[nameof(IsMemory)] = IsMemory;
				dict[nameof(Timestamp)] = Timestamp;
				dict[nameof(ModID)] = ModID;
				dict[nameof(StoredResources)] = StoredResources;
				return dict;
			}
			set
			{
				Index = value[nameof(Index)].Default<int>();
				IsUnique = value[nameof(IsUnique)].Default<bool>();
				Name = value[nameof(Name)].Default<string>();
				Description = value[nameof(Description)].Default<string>();
				PictureName = value[nameof(PictureName)].Default<string>();
				IntrinsicAbilities = value[nameof(IntrinsicAbilities)].Default(new List<Ability>());
				StellarSize = value[nameof(StellarSize)].Default<StellarSize>();
				ID = value[nameof(ID)].Default<long>();
				IsMemory = value[nameof(IsMemory)].Default<bool>();
				Timestamp = value[nameof(Timestamp)].Default<double>();
				ModID = value[nameof(ModID)].Default<string>();
				StoredResources = value[nameof(StoredResources)].Default(new ResourceQuantity());
			}
		}
	}
}
