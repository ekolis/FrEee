using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Combat;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;


namespace FrEee.Objects.Combat.Grid
{
	public abstract class BattleEvent : IBattleEvent
	{
		protected BattleEvent(IBattle battle, ICombatant combatant, IntVector2 startPosition, IntVector2 endPosition)
		{
			Battle = battle;
			Combatant = combatant;
			StartPosition = startPosition;
			EndPosition = endPosition;
		}

		[DoNotCopy]
		public IBattle Battle { get; set; }

		private ICombatant OurCombatant { get; set; }

		[DoNotSerialize]
		public ICombatant Combatant
		{
			get => OurCombatant ?? Battle?.StartCombatants?[OurCombatant.ID];
			set => OurCombatant = value;
		}

		public IntVector2 EndPosition { get; set; }

		public IntVector2 StartPosition { get; set; }
	}
}
