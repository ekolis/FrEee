using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Abilities
{
	/// <summary>
	/// A special ability of some game object, or just a tag used by the AI or by modders.
	/// </summary>
	[Serializable]
	public class Ability : IContainable<object>
	{
		public Ability(object container)
		{
			Container = container;
			Values = new List<Formula<string>>();
		}

		/// <summary>
		/// The name of the ability.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A description of the ability's effects.
		/// </summary>
		public Formula<string> Description { get; set; }

		/// <summary>
		/// Extra data for the ability.
		/// </summary>
		public IList<Formula<string>> Values { get; set; }

		/// <summary>
		/// The first value of the ability. Not all abilities have values, so this might be null!
		/// </summary>
		public Formula<string> Value1
		{
			get
			{
				return Values.ElementAtOrDefault(0);
			}
		}

		/// <summary>
		/// The second value of the ability. Not all abilities have two values, so this might be null!
		/// </summary>
		public Formula<string> Value2
		{
			get
			{
				return Values.ElementAtOrDefault(1);
			}
		}

		public override string ToString()
		{
			return Description ?? (Name + ": " + string.Join(", ", Values.Select(v => v.Value).ToArray()));
		}

		public object Container { get; private set; }
	}
}
