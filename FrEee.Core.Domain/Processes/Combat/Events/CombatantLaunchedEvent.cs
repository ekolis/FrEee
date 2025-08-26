using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.Objects.GameState;

namespace FrEee.Processes.Combat.Events;

public class CombatantLaunchedEvent : BattleEvent
{
	public CombatantLaunchedEvent(IBattle battle, ICombatant launcher, ICombatant combatant, Vector2<int> position)
		: base(battle, combatant, position, position)
	{
		Launcher = launcher;
	}

	private GameReference<ICombatant> launcher
	{
		get => Launcher.ReferViaGalaxy();
		set => Launcher = value.Value ?? Battle?.StartCombatants?[value.ID];
	}

	[DoNotSerialize]
	public ICombatant Launcher { get; set; }

}