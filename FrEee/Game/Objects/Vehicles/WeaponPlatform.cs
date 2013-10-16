using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class WeaponPlatform : Vehicle, IUnit
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override Enumerations.WeaponTargets WeaponTargetType
		{
			// weapon platforms cannot be targeted in space combat
			get { return Enumerations.WeaponTargets.Invalid; }
		}

		public override void Place(ISpaceObject target)
		{
			CommonExtensions.Place(this, target);
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			if (Owner == emp)
				return Visibility.Owned;
			var sobj = Container as ISpaceObject;
			if (sobj != null && sobj.CheckVisibility(emp) >= Visibility.Scanned)
				return Visibility.Scanned;
			return Visibility.Unknown;
		}

		public override bool ParticipatesInGroundCombat
		{
			get { return false; }
		}

		public ICargoContainer Container
		{
			get { return CommonExtensions.FindContainer(this); }
		}

		public override void Redact(Empire emp)
		{
			var visibility = CheckVisibility(emp);

			// Can't see the platform's components if it's not scanned
			// TODO - let player see design of previously scanned platform if the platform has not been refit
			if (visibility < Visibility.Scanned)
			{
				// create fake design and clear component list
				var d = new Design<SpaceVehicle>();
				d.Hull = (IHull<SpaceVehicle>)Design.Hull;
				d.Owner = Design.Owner;
				Design = d;
				Components.Clear();
			}

			if (visibility < Visibility.Fogged)
				Dispose();
			else if (visibility == Visibility.Fogged)
			{
				var known = emp.Memory[ID];
				if (known != null && known.GetType() == GetType())
					known.CopyTo(this);
			}
		}
	}
}
