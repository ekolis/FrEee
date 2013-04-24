using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads templates.
	/// </summary>
	public interface ILoader
	{
		/// <summary>
		/// Loads templates.
		/// </summary>
		/// <param name="df">The data file to load.</param>
		/// <param name="mod">The mod we are loading templates into.</param>
		void Load(DataFile df, Mod mod);
	}
}
