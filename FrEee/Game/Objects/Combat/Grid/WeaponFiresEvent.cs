using FrEee.Game.Interfaces;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat.Grid
{
	public class WeaponFiresEvent : IBattleEvent
	{
		#region Public Constructors

		public WeaponFiresEvent(ICombatant combatant, IntVector2 here, ICombatant target, IntVector2 there, bool isHit)
		{
			Combatant = combatant;
			Target = target;
			StartPosition = here;
			EndPosition = there;
			IsHit = isHit;
		}

		#endregion Public Constructors

		#region Public Properties

		public ICombatant Combatant { get; set; }

		public IntVector2 EndPosition { get; set; }
		public IntVector2 StartPosition { get; set; }
		public ICombatant Target { get; set; }
		public bool IsHit { get; set; }

		#endregion Public Properties
	}
}