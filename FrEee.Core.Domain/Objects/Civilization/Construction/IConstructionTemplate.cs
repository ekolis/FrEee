using FrEee.Objects.GameState;
using FrEee.Objects.Technology;
using FrEee.Utility;
namespace FrEee.Objects.Civilization.Construction;

/// <summary>
/// Template for constructable items.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IConstructionTemplate : IReferrable, IPictorial, IResearchable
{
    /// <summary>
    /// The cost to build it.
    /// </summary>
    ResourceQuantity Cost { get; }

    /// <summary>
    /// Does this template require a colony to build it?
    /// </summary>
    bool RequiresColonyQueue { get; }

    /// <summary>
    /// Does this template require a space yard to build it?
    /// </summary>
    bool RequiresSpaceYardQueue { get; }

    /// <summary>
    /// Has the empire unlocked this construction template?
    /// </summary>
    /// <param name="emp"></param>
    /// <returns></returns>
    bool HasBeenUnlockedBy(Empire emp);
}