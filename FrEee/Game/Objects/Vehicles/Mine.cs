using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Mine : SpaceVehicle
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override Enumerations.WeaponTargets WeaponTargetType
		{
			// mines cannot be targeted in space combat
			get { return Enumerations.WeaponTargets.Invalid; }
		}

		public override bool CanWarp
		{
			get { return false; }
		}

		/// <summary>
		/// Mines are invisible to everyone except their owner.
		/// </summary>
		/// <param name="emp"></param>
		/// <returns></returns>
		public override Visibility CheckVisibility(Empire emp)
		{
			if (emp == Owner)
				return Visibility.Owned;
			return Visibility.Unknown;
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}
	}
}
