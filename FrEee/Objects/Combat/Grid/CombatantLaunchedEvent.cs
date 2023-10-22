using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Serialization;

namespace FrEee.Objects.Combat.Grid
{
	public class CombatantLaunchedEvent : BattleEvent
	{
		public CombatantLaunchedEvent(Battle battle, ICombatant launcher, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
			Launcher = launcher;
		}

		private GameReference<ICombatant> launcher { get; set; }

		[DoNotSerialize]
		public ICombatant Launcher
		{
			get => launcher?.Value ?? Battle?.StartCombatants?[launcher.ID];
			set => launcher = value.ReferViaGalaxy();
		}

	}
}