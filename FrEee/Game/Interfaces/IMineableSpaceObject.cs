using FrEee.Utility;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// A space object with a resource value.
    /// </summary>
    public interface IMineableSpaceObject : ISpaceObject
    {
        #region Public Properties

        ResourceQuantity ResourceValue { get; }

        #endregion Public Properties
    }
}
