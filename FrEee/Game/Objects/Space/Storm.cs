using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Game.Objects.Space
{
	/// <summary>
	/// A space storm.
	/// </summary>
	[Serializable]
	public class Storm : StellarObject, ITemplate<Storm>, IDataObject
	{
		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.Storm; }
		}

		public override bool CanBeObscured => false;

		/// <summary>
		/// Some sort of combat image? Where are these stored anyway?
		/// </summary>
		public string CombatTile { get; set; }

		public override SafeDictionary<string, object> Data
		{
			get
			{
				var dict = base.Data;
				dict[nameof(CombatTile)] = CombatTile;
				return dict;
			}
			set
			{
				base.Data = value;
				CombatTile = value[nameof(CombatTile)].Default<string>();
			}
		}

		public Empire Owner
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Just copy the storm's data.
		/// </summary>
		/// <returns>A copy of the storm.</returns>
		public Storm Instantiate()
		{
			var result = this.CopyAndAssignNewID();
			result.ModID = null;
			return result;
		}
	}
}