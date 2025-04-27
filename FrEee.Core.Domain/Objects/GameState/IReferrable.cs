using System;
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
}