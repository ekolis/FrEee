using FrEee.Objects.Space;

namespace FrEee.Interfaces;

/// <summary>
/// Something which is located in a sector, or is a sector itself.
/// </summary>
public interface ILocated
{
	Sector Sector { get; set; }

	StarSystem StarSystem { get; }
}