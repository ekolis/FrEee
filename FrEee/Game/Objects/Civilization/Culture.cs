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
	public class Culture : IModObject
	{
		public string Name { get; set; }

		string INamed.Name { get { return Name; } }

		public string Description { get; set; }

		public int Production { get; set; }

		public int Research { get; set; }

		public int Intelligence { get; set; }

		public int Trade { get; set; }

		public int SpaceCombat { get; set; }

		public int GroundCombat { get; set; }

		public int Happiness { get; set; }

		public int MaintenanceReduction { get; set; }

		public int Construction { get; set; }

		public int Repair { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public string ModID { get; set; }
	}
}
