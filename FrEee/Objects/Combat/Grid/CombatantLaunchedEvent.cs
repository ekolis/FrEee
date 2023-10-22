using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Serialization;
using FrEee.Utility;

namespace FrEee.Objects.Combat.Grid
{
	public class CombatantLaunchedEvent : BattleEvent
	{
		public CombatantLaunchedEvent(Battle battle, ICombatant launcher, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
			Launcher = launcher;
		}

		private ICombatant OurLauncher { get; set; }

		[DoNotSerialize]
		public ICombatant Launcher
		{
			get => OurLauncher ?? Battle?.StartCombatants?[OurLauncher.ID];
			set => OurLauncher = value;
		}

	}
}
