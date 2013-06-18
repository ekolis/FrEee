using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire's culture. Provides percentage modifiers to various abilities.
	/// </summary>
	public class Culture : INamed
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int Production { get; set; }

		public int Research { get; set; }

		public int Intelligence { get; set; }

		public int Trade { get; set; }

		public int SpaceCombat { get; set; }

		public int GroundCombat { get; set; }

		public int Happiness { get; set; }

		public int MaintenanceReduction { get; set; }

		/// <summary>
		/// TODO - the data file says SY Rate but I think it's construction even on colonies with no SY. Need to verify...
		/// </summary>
		public int Construction { get; set; }

		public int Repair { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
