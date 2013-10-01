using System;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility; using FrEee.Utility.Serialization;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Civilization;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A space storm.
	/// </summary>
	[Serializable]
	public class Storm : StellarObject, ITemplate<Storm>
	{
		/// <summary>
		/// Some sort of combat image? Where are these stored anyway?
		/// </summary>
		public string CombatTile { get; set; }

		/// <summary>
		/// Just copy the storm's data.
		/// </summary>
		/// <returns>A copy of the storm.</returns>
		public Storm Instantiate()
		{
			return this.Copy();
		}

		public override void Redact(Empire emp)
		{
			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose(); // TODO - dispose here if fogged; replace with memory sight cache if present
		}
	}
}
