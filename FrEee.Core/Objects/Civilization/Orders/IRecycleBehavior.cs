using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using System.Collections.Generic;

namespace FrEee.Objects.Civilization.Orders;

/// <summary>
/// A type of recycle behavior (scrap, analyze, etc.)
/// </summary>
public interface IRecycleBehavior
{
    string Verb { get; }

    void Execute(IRecyclable target, bool didRecycle = false);

    IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor, IRecyclable target);
}