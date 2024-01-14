using System.Collections.Generic;

namespace FrEee.Interfaces;

/// <summary>
/// Something which can be upgraded to a newer version.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IUpgradeable<out T>
{
	/// <summary>
	/// Can this item be upgraded?
	/// </summary>
	bool IsObsolescent { get; }

	/// <summary>
	/// Is this item obsolete?
	/// </summary>
	bool IsObsolete { get; }

	/// <summary>
	/// The latest available version of this item.
	/// </summary>
	T LatestVersion { get; }

	/// <summary>
	/// Any newer versions of this item.
	/// </summary>
	IEnumerable<T> NewerVersions { get; }

	/// <summary>
	/// Any older versions of this item.
	/// </summary>
	IEnumerable<T> OlderVersions { get; }
}