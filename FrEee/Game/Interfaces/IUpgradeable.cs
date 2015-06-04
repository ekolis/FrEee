using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which can be upgraded to a newer version.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IUpgradeable<out T>
	{
		/// <summary>
		/// Is this item obsolete?
		/// </summary>
		bool IsObsolete { get; }

		/// <summary>
		/// The latest available version of this item.
		/// </summary>
		T LatestVersion { get; }
	}
}
