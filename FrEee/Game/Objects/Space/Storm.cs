using System;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Utility;
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
			return this.CopyAndAssignNewID();
		}

		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Storm; }
		}

		[DoNotSerialize(false)]
		public override Empire Owner
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("Cannot set the owner of a storm; it is always null.");
			}
		}
	}
}
