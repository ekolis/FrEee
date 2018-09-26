﻿using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Combat.Grid
{
	/// <summary>
	/// When combatants collide - seeker detonation or ship ramming
	/// </summary>
	/// <seealso cref="FrEee.Game.Objects.Combat.Grid.IBattleEvent" />
	public class CombatantsCollideEvent : BattleEvent
	{
		public CombatantsCollideEvent(Battle battle, ICombatant combatant, ICombatant target, IntVector2 location, int combatantDamage, int targetDamage, bool wasCombatantDisarmed, bool wasTargetDisarmed)
			: base(battle, combatant, location, location)
		{
			Target = target;
			CombatantDamage = combatantDamage;
			TargetDamage = targetDamage;
			WasCombatantDisarmed = wasCombatantDisarmed;
			WasTargetDisarmed = wasTargetDisarmed;
		}


		private GalaxyReference<ICombatant> target { get; set; }

		public ICombatant Target
		{
			get => target?.Value ?? Battle?.StartCombatants?[target.ID];
			set => target = value.ReferViaGalaxy();
		}

		public int CombatantDamage { get; set; }
		public int TargetDamage { get; set; }

		public bool WasCombatantDisarmed { get; set; }
		public bool WasTargetDisarmed { get; set; }
	}
}