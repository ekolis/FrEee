using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire's culture. Provides percentage modifiers to various abilities.
	/// </summary>
	[Serializable]
	public class Culture : INamed
	{
		public Formula<string> Name { get; set; }

		string INamed.Name { get { return Name; } }

		public Formula<string> Description { get; set; }

		public Formula<int> Production { get; set; }

		public Formula<int> Research { get; set; }

		public Formula<int> Intelligence { get; set; }

		public Formula<int> Trade { get; set; }

		public Formula<int> SpaceCombat { get; set; }

		public Formula<int> GroundCombat { get; set; }

		public Formula<int> Happiness { get; set; }

		public Formula<int> MaintenanceReduction { get; set; }

		public Formula<int> Construction { get; set; }

		public Formula<int> Repair { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
