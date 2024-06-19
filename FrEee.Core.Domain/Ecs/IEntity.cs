using FrEee.Ecs.Abilities;
using FrEee.Ecs.Abilities.Utility;
using FrEee.Objects.GameState;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Ecs;

/// <summary>
/// An entity which can have abilities.
/// </summary>
public interface IEntity : IEntity<Ability>, IAbilityObject
{
    /// <summary>
    /// Abilities possessed intrinsically by this object.
    /// </summary>
    [Obsolete("Use Abilities.")]
    IEnumerable<Ability> IntrinsicAbilities => Abilities;    
}

/// <summary>
/// A referrable entity which can have abilities.
/// </summary>
public interface IReferrableEntity : IReferrable, IEntity { }
