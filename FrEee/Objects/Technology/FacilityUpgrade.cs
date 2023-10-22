using FrEee.Interfaces;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using System.Collections.Generic;
using System.Linq;
using FrEee.Extensions;
using FrEee.Serialization;

namespace FrEee.Objects.Technology
{
	public class FacilityUpgrade : IUpgradeable<FacilityUpgrade>, IPromotable, INamed
	{
		public FacilityUpgrade(FacilityTemplate old, FacilityTemplate nu)
		{
			Old = old;
			New = nu;
		}

		public ResourceQuantity Cost => New.Cost.Value * The.Mod.Settings.UpgradeFacilityPercentCost / 100;

		/// <summary>
		/// The family of facility being upgraded.
		/// </summary>
		public string Family
		{
			get { return New.Family; }
		}

		public bool IsObsolescent
		{
			get
			{
				return New.IsObsolescent;
			}
		}

		public bool IsObsolete
		{
			get
			{
				return New.IsObsolete;
			}
		}

		public FacilityUpgrade LatestVersion
		{
			get
			{
				if (IsObsolete)
					return new FacilityUpgrade(Old, New.LatestVersion);
				else
					return this;
			}
		}

		public string Name
		{
			get
			{
				return "Upgrade " + Old + " to " + New;
			}
		}

		[ModReference]
		public FacilityTemplate New { get; set; }

		public IEnumerable<FacilityUpgrade> NewerVersions
		{
			get { return The.Galaxy.FindSpaceObjects<IConstructor>().Select(o => o.ConstructionQueue).ExceptSingle(null).SelectMany(q => q.Orders).Select(o => o.Item).OfType<FacilityUpgrade>().Where(u => u.New.UpgradesTo(New)); }
		}

		[ModReference]
		public FacilityTemplate Old { get; set; }

		public IEnumerable<FacilityUpgrade> OlderVersions
		{
			get { return The.Galaxy.FindSpaceObjects<IConstructor>().Select(o => o.ConstructionQueue).ExceptSingle(null).SelectMany(q => q.Orders).Select(o => o.Item).OfType<FacilityUpgrade>().Where(u => New.UpgradesTo(u.New)); }
		}

		public static bool operator !=(FacilityUpgrade x, FacilityUpgrade y)
		{
			return !(x == y);
		}

		public static bool operator ==(FacilityUpgrade x, FacilityUpgrade y)
		{
			return x.SafeEquals(y);
		}

		public override bool Equals(object obj)
		{
			var fu = obj as FacilityUpgrade;
			if (fu == null)
				return false;
			return fu.Old == Old && fu.New == New;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Old, New);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
			}
		}

		/*public bool RequiresColonyQueue
		{
			get
			{
				return true;
			}
		}

		public bool RequiresSpaceYardQueue
		{
			get
			{
				return false;
			}
		}

		public IList<Requirement<Empire>> UnlockRequirements
		{
			get
			{
				return New.UnlockRequirements;
			}
		}

		public string ResearchGroup
		{
			get
			{
				return New.ResearchGroup;
			}
		}

		public string Name
		{
			get
			{
				return "Upgrade to " + New;
			}
		}

		public Image Icon
		{
			get
			{
				return New.Icon;
			}
		}

		public Image Portrait
		{
			get
			{
				return New.Portrait;
			}
		}

		public bool IsMemory
		{
			get; set;
		}

		public double Timestamp
		{
			get; set;
		}

		public long ID
		{
			get; set;
		}

		public bool IsDisposed
		{
			get; set;
		}

		public Empire Owner
		{
			get; private set;
		}

		public bool HasBeenUnlockedBy(Empire emp)
		{
			return New.HasBeenUnlockedBy(emp);
		}

		/// <summary>
		/// You can only see your own facility upgrades.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			// nothing to do, it's only visible if it's yours anyway
		}

		public bool IsObsoleteMemory(Empire emp)
		{
			return false;
		}

		public void Dispose()
		{
			// nothing to dispose of
		}*/
	}
}
