using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;

namespace FrEee.Game.Enumerations
{
	public enum EmpirePlacement
	{
		[CanonicalName("Can Start In Same System")]
		CanStartInSameSystem,
		[CanonicalName("Different Systems")]
		DifferentSystems,
		Equidistant
	}
}
