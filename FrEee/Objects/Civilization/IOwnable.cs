using FrEee.Utility;

namespace FrEee.Objects.Civilization;

/// <summary>
/// Something which can be owned by an empire.
/// </summary>
public interface IOwnable
{
    [DoNotCopy]
    Empire Owner { get; }
}

/// <summary>
/// Something whose ownership can be changed.
/// </summary>
public interface ITransferrable : IOwnable
{
    new Empire Owner { get; set; }
}
