using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// A diplomatic message.
    /// </summary>
    public interface IMessage : IFoggable, IPictorial, IPromotable
    {
        #region Public Properties

        IMessage InReplyTo { get; set; }
        Empire Recipient { get; set; }
        string Text { get; set; }
        int TurnNumber { get; set; }

        #endregion Public Properties
    }
}
