using FrEee.Utility;
using System;
using FrEee.Utility;

namespace FrEee.Setup;

[Flags]
public enum ScoreDisplay
{
    [CanonicalName("Own Only - No Rankings")]
    OwnOnlyNoRankings = 0,

    [CanonicalName("Own Only - Ranked")]
    OwnOnlyRanked = 1,

    [CanonicalName("Allies Only - No Rankings")]
    AlliesOnlyNoRankings = 2,

    [CanonicalName("Allies Only - Ranked")]
    AlliesOnlyRanked = 3,

    All = 7
}