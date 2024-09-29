using FrEee.Objects.Civilization;
using FrEee.Objects.GameState;
using System.Collections.Generic;

namespace FrEee.Gameplay.Commands;

/// <summary>
/// A command to some object.
/// Commands are distinguished from orders by being instantaneous, rather than queued.
/// </summary>
public interface ICommand : IPromotable
{
    IReferrable Executor { get; }

    long ExecutorID { get; }

    /// <summary>
    /// The empire issuing the command.
    /// </summary>
    Empire Issuer { get; }

    /// <summary>
    /// Any new (from the client) objects referred to by this command.
    /// </summary>
    IEnumerable<IReferrable> NewReferrables { get; }

    /// <summary>
    /// Executes the command.
    /// </summary>
    void Execute();
}

/// <summary>
/// A command to some object.
/// </summary>
public interface ICommand<T> : ICommand where T : IReferrable
{
    /// <summary>
    /// The object whose queue is being manipulated.
    /// </summary>
    new T Executor { get; set; }
}
