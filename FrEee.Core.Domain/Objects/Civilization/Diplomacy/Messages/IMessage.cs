using FrEee.Objects.GameState;

namespace FrEee.Objects.Civilization.Diplomacy.Messages;

/// <summary>
/// A diplomatic message.
/// </summary>
public interface IMessage : IFoggable, IPictorial, IPromotable, IReferrable
{
    IMessage InReplyTo { get; set; }
    Empire Recipient { get; set; }
    string Text { get; set; }
    int TurnNumber { get; set; }
}