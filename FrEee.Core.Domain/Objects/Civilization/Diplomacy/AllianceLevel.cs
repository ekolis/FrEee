using FrEee.Utility;
namespace FrEee.Objects.Civilization.Diplomacy;

/// <summary>
/// A level of alliance.
/// </summary>
public enum AllianceLevel
{
    /// <summary>
    /// No alliance. The empire will be attacked by the other empire.
    /// </summary>
    None = 0,

    /// <summary>
    /// The receiver will not be attacked by the giver, except in systems where the giver has a colony.
    /// </summary>
    [CanonicalName("Neutral Zone")]
    NeutralZone = 1,

    /// <summary>
    /// The receiver will not be attacked by the giver.
    /// </summary>
    [CanonicalName("Non-Aggression")]
    [Name("Non Aggression")]
    [Name("Nonaggression")]
    NonAggression = 2,

    /// <summary>
    /// Same as Non-Aggression, but the giver will also declare war on any empire that declares war on the receiver.
    /// </summary>
    [CanonicalName("Defensive Pact")]
    DefensivePact = 3,

    /// <summary>
    /// Same as Defensive Pact, but the giver will also declare war on any empire that the receiver declares war on.
    /// </summary>
    [CanonicalName("Military Alliance")]
    MilitaryAlliance = 4,
}