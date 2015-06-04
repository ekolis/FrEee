using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Technology
{
	public class FacilityUpgrade : IUpgradeable<FacilityUpgrade>
	{
		public FacilityUpgrade(FacilityTemplate old, FacilityTemplate nu)
		{
			Old = old;
			New = nu;
		}
		public FacilityTemplate Old { get; set; }
		public FacilityTemplate New { get; set; }

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
	}
}
