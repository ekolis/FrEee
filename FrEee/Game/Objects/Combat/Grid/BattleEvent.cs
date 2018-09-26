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

		public IBattle Battle { get; }

		private GalaxyReference<ICombatant> combatant { get; set; }

		public ICombatant Combatant
		{
			get => combatant?.Value ?? Battle?.StartCombatants?[combatant.ID];
			set => combatant = value.ReferViaGalaxy();
		}

		public IntVector2 EndPosition { get; }

		public IntVector2 StartPosition { get; }
	}
}
