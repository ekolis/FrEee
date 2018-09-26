using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Combat.Grid
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

		private GalaxyReference<ICombatant> combatant { get; set; }

		[DoNotSerialize]
		public ICombatant Combatant
		{
			get => combatant?.Value ?? Battle?.StartCombatants?[combatant.ID];
			set => combatant = value.ReferViaGalaxy();
		}

		public IntVector2 EndPosition { get; set; }

		public IntVector2 StartPosition { get; set; }
	}
}
