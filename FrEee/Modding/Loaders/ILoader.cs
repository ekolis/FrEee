using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads mod data.
	/// </summary>
	public interface ILoader
	{
		/// <summary>
		/// Loads mod data.
		/// </summary>
		/// <param name="mod">The mod we are loading data into.</param>
		void Load(Mod mod);
	}
}
