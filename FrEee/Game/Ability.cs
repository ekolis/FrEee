using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A special ability of some game object, or just a tag used by the AI or by modders.
	/// </summary>
	public class Ability
	{
		public Ability()
		{
			Values = new List<string>();
		}

		/// <summary>
		/// The name of the ability.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A description of the ability's effects.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Extra data for the ability.
		/// </summary>
		public IList<string> Values { get; private set; }
	}
}
