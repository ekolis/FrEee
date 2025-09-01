using System;
using System.Collections;
using System.Collections.Generic;
using FrEee.Objects.Civilization;

namespace FrEee.Objects.GameState;

/// <summary>
/// Something that can be referred to from the client side using an ID.
/// </summary>
// TODO: rename IReferrable to IGameObject
public interface IReferrable : IDisposable, IOwnable
{
    long ID { get; set; }

    bool IsDisposed { get; set; }
    
    /// <summary>
    /// Any other referrable objects contained within this referrable.
    /// </summary>
    IEnumerable<IReferrable> Referrables { get; }
}