using FrEee.Objects.Civilization;
using System.Collections.Generic;

namespace FrEee.Ecs.Abilities.Utility;

/// <summary>
/// An entity that can contain different abilities for different empires.
/// </summary>
// TODO: make ability sharing an ability in itself?
public interface ICommonEntity
    : IEntity
{
    /// <summary>
    /// All entities contained within this common entity.
    /// </summary>
    IEnumerable<IEntity> Entities { get; }

    /// <summary>
    /// Finds any child ability objects owned by an empire.
    /// </summary>
    /// <param name="emp"></param>
    /// <returns></returns>
    IEnumerable<IEntity> GetContainedEntities(Empire emp);
}