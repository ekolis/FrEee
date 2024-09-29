using FrEee.Objects.Civilization.Orders;

namespace FrEee.Gameplay.Commands.Orders;

/// <summary>
/// A command to manipulate an object's order queue.
/// </summary>
public interface IOrderCommand : ICommand
{
    /// <summary>
    /// The specific order being manipulated.
    /// </summary>
    IOrder Order { get; set; }
}