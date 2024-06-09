using FrEee.Utility;
using FrEee.Utility;
namespace FrEee.Objects.Civilization.Diplomacy;

public enum AllowedTrades
{
    /// <summary>
    /// No trades or gifts are allowed.
    /// </summary>
    None = 0,

    /// <summary>
    /// Anything except technology can be traded or gifted.
    /// </summary>
    [CanonicalName("All But Technology")]
    AllButTechnology = 1,

    /// <summary>
    /// Anything, including technology, can be traded or gifted.
    /// </summary>
    All = 2
}