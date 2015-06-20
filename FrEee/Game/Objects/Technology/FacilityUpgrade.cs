using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Technology
{
	public class FacilityUpgrade : IUpgradeable<FacilityUpgrade>
	{
		public FacilityUpgrade(FacilityTemplate old, FacilityTemplate nu)
		{
			Old = old;
			New = nu;
		}

		[DoNotSerialize]
		public FacilityTemplate Old { get { return old; } private set { old = value; } }
		private ModReference<FacilityTemplate> old { get; set; }
		public FacilityTemplate New { get { return nu; } private set { nu = value; } }
		private ModReference<FacilityTemplate> nu { get; set; }

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

		public bool IsObsolescent
		{
			get
			{
				return New.IsObsolescent;
			}
		}


		public IEnumerable<FacilityUpgrade> NewerVersions
		{
			get { return Galaxy.Current.FindSpaceObjects<ISpaceObject>().Select(o => o.ConstructionQueue).ExceptSingle(null).SelectMany(q => q.Orders).Select(o => o.Item).OfType<FacilityUpgrade>().Where(u => u.New.UpgradesTo(New)); }
		}

		public IEnumerable<FacilityUpgrade> OlderVersions
		{
			get { return Galaxy.Current.FindSpaceObjects<ISpaceObject>().Select(o => o.ConstructionQueue).ExceptSingle(null).SelectMany(q => q.Orders).Select(o => o.Item).OfType<FacilityUpgrade>().Where(u => New.UpgradesTo(u.New)); }
		}
	}
}
