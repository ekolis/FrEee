using FrEee.Objects.GameState;

namespace FrEee.Modding;

/// <summary>
/// General info about a mod.
/// </summary>
public class ModInfo : INamed
{
	/// <summary>
	/// The mod's author.
	/// </summary>
	public string Author { get; set; }

	/// <summary>
	/// Description of the mod.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// The author's email address.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// The folder that the mod was loaded from (relative to the Mods folder), or null if it's the stock mod.
	/// </summary>
	public string Folder { get; set; }

	/// <summary>
	/// Is this mod version obsolete?
	/// </summary>
	public bool IsObsolete { get; set; }

	/// <summary>
	/// The name of this mod.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// The mod version.
	/// </summary>
	public string Version { get; set; }

	/// <summary>
	/// The mod's website address.
	/// </summary>
	public string Website { get; set; }

	public override string ToString()
	{
		return Name + " " + Version;
	}
}