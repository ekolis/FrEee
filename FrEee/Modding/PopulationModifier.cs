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
		/// Rate of resource/research/intel production as percentage of normal rate.
		/// </summary>
		public int ProductionRate { get; set; }

		/// <summary>
		/// Rate of construction as percentage of normal rate.
		/// </summary>
		public int ConstructionRate { get; set; }
	}
}
