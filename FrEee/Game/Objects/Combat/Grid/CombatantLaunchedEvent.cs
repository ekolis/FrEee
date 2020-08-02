using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

#nullable enable

namespace FrEee.Game.Objects.Combat.Grid
{
	public class CombatantLaunchedEvent : BattleEvent
	{
		public CombatantLaunchedEvent(Battle battle, ICombatant launcher, ICombatant combatant, IntVector2 position)
			: base(battle, combatant, position, position)
		{
			Launcher = launcher;
		}

		private GalaxyReference<ICombatant?>? launcher { get; set; }

		[DoNotSerialize]
		public ICombatant? Launcher
		{
			get
			{
				if (launcher?.Value != null)
				{
					return launcher.Value;
				}
				else if (launcher?.ID != null)
				{
					return Battle?.StartCombatants?[launcher.ID];
				}
				return null;
			}

			set => launcher = value.ReferViaGalaxy();
		}

	}
}
