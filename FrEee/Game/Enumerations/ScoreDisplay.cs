using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility;

namespace FrEee.Game.Enumerations
{
	public enum ScoreDisplay
	{
		[CanonicalName("Own Only - No Rankings")]
		OwnOnlyNoRankings,
		[CanonicalName("Own Only - Ranked")]
		OwnOnlyRanked,
		[CanonicalName("Allies Only - No Rankings")]
		AlliesOnlyNoRankings,
		[CanonicalName("Allies Only - No Rankings")]
		AlliesOnlyRanked,
		All
	}
}
