using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Ecs.Abilities;

namespace FrEee.Ecs;

/// <summary>
/// Something which can have abilities but is not necessarily an <see cref="IEntity{TAbility}"/>.
/// </summary>
public interface IAbilityObject
{
    /// <summary>
    /// The type of ability target that this object represents.
    /// </summary>
    AbilityTargets AbilityTarget { get; }

    /// <summary>
    /// Child entities that can pass up abilities to this object.
    /// </summary>
    IEnumerable<IAbilityObject> Children { get; }

    /// <summary>
    /// Parent entities from which this object can inherit abilities.
    /// </summary>
    IEnumerable<IAbilityObject> Parents { get; }

    IEnumerable<Ability> IntrinsicAbilities { get; }
}
