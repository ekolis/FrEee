using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Technology
{
	public class FacilityUpgrade
	{
		public FacilityUpgrade(FacilityTemplate old, FacilityTemplate nu)
		{
			Old = old;
			New = nu;
		}
		public FacilityTemplate Old { get; set; }
		public FacilityTemplate New { get; set; }
	}
}
