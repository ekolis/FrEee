using FrEee.Objects.Civilization;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Designs;

/// <summary>
/// A command for an empire to create a design.
/// </summary>
public interface ICreateDesignCommand : ICommand<Empire>
{
    IDesign Design { get; }
}