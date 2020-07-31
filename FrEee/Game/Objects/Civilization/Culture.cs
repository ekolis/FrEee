using FrEee.Game.Interfaces;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;

#nullable enable

namespace FrEee.Game.Objects.Civilization
{
	/// <summary>
	/// An empire's culture. Provides percentage modifiers to various abilities.
	/// </summary>
	[Serializable]
	public class Culture : IModObject
	{
		public int Construction { get; set; }
		public string? Description { get; set; }
		public int GroundCombat { get; set; }
		public int Happiness { get; set; }

		/// <summary>
		/// Resource income percentages based on cultural modifiers.
		/// </summary>
		public ResourceQuantity IncomePercentages
		{
			get
			{
				var result = new ResourceQuantity();
				foreach (var r in Resource.All)
					result += (100 + r.CultureModifier(this)) * r;
				return result;
			}
		}

		public int Intelligence { get; set; }

		public bool IsDisposed =>
				// can't be disposed of
				false;

		public int MaintenanceReduction { get; set; }
		public string? ModID { get; set; }
		public string? Name { get; set; }

		string? INamed.Name => Name;
		public int Production { get; set; }

		public int Repair { get; set; }
		public int Research { get; set; }
		public int SpaceCombat { get; set; }

		/// <summary>
		/// Parameters from the mod meta templates.
		/// </summary>
		public IDictionary<string, object>? TemplateParameters { get; set; }

		public int Trade { get; set; }

		public void Dispose()
		{
			// nothing to do
		}

		public override string ToString() => Name ?? string.Empty;
	}
}
