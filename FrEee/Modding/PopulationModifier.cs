using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	/// <summary>
	/// Modifiers to production/construction rate from population.
	/// </summary>
	public class PopulationModifier
	{
		/// <summary>
		/// Maximum amount of population for this modifier; if more population is present, the next better modifier (if present) will be used instead.
		/// </summary>
		public int PopulationAmount { get; set; }

		/// <summary>
		/// Rate of resource/research/intel production as percentage of normal rate.
		/// </summary>
		public int ProductionRate { get; set; }

		/// <summary>
		/// Rate of construction as percentage of normal rate.
		/// </summary>
		public int ConstructionRate { get; set; }
	}
}
