using System.Collections.Generic;
using System.Drawing;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// Something which has a picture.
	/// </summary>
	public interface IPictorial
	{
		/// <summary>
		/// A small picture.
		/// </summary>
		Image Icon { get; }

		/// <summary>
		/// The icon pre-scaled to 32 pixels by 32 pixels.
		/// </summary>
		Image Icon32 { get; }

		/// <summary>
		/// Paths with fallbacks to the icon, relative to the Pictures folder.
		/// </summary>
		IEnumerable<string> IconPaths { get; }

		/// <summary>
		/// A large picture.
		/// </summary>
		Image Portrait { get; }

		/// <summary>
		/// Paths with fallbacks to the portrait, relative to the Pictures folder.
		/// </summary>
		IEnumerable<string> PortraitPaths { get; }
	}
}