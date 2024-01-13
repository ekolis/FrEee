using FrEee.Interfaces;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;

namespace FrEee.Objects.Combat.Grid
{
	public class CombatantLaunchedEvent : BattleEvent
	{
		public CombatantLaunchedEvent(Battle battle, ICombatant launcher, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
			Launcher = launcher;
		}

		private GalaxyReference<ICombatant> launcher { get; set; }

		[DoNotSerialize]
		public ICombatant Launcher
		{
			get => launcher?.Value ?? Battle?.StartCombatants?[launcher.ID];
			set => launcher = value.ReferViaGalaxy();
		}

	}
}