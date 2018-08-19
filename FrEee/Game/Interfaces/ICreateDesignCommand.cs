using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// A command for an empire to create a design.
    /// </summary>
    public interface ICreateDesignCommand : ICommand<Empire>
    {
        #region Public Properties

        IDesign Design { get; }

        #endregion Public Properties
    }
}
