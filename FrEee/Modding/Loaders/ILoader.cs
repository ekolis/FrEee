using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Modding.Interfaces;

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
		/// <returns>Any mod objects which need IDs generated.</returns>
		IEnumerable<IModObject> Load(Mod mod);

		string ModPath { get; set; }

		string FileName { get; set; }
	}
}
