using FrEee.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Objects.Abilities;

/// <summary>
/// An object that can contain different abilities for different empires.
/// </summary>
public interface ICommonAbilityObject : IAbilityObject
{
    /// <summary>
    /// Finds any child ability objects owned by an empire.
    /// </summary>
    /// <param name="emp"></param>
    /// <returns></returns>
    IEnumerable<IAbilityObject> GetContainedAbilityObjects(Empire emp);
}