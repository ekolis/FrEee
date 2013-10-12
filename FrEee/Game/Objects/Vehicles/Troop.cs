using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Troop : Vehicle, IUnit
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override WeaponTargets WeaponTargetType
		{
			// troops cannot be targeted in space combat
			get { return Enumerations.WeaponTargets.Invalid; }
		}
	
		public override void Place(ISpaceObject target)
		{
			if (target is ICargoContainer)
			{
				var cc = (ICargoContainer)target;
				if (cc.AddUnit(this))
					return;
			}
			// cargo was full? then try other space objects
			foreach (var cc in target.Sector.SpaceObjects.Where(sobj => sobj.Owner == target.Owner).OfType<ICargoContainer>())
			{
				if (cc.AddUnit(this))
					return;
			}
			target.Owner.Log.Add(this.CreateLogMessage(this + " could not be placed in cargo at " + target + " because there is not enough cargo space available."));
		}

		public override Visibility CheckVisibility(Empire emp)
		{
			if (Owner == emp)
				return Visibility.Owned;
			// TODO - show enemy troops in a ground invasion
			return Visibility.Unknown;
		}

		/// <summary>
		/// Troops participate in ground combat.
		/// </summary>
		public override bool ParticipatesInGroundCombat
		{
			get { return true; }
		}

		public ICargoContainer Container
		{
			get { return Galaxy.Current.FindSpaceObjects<ICargoTransferrer>().Flatten().Flatten().SingleOrDefault(cc => cc.Cargo.Units.Contains(this)); }
		}

		public override void Redact(Empire emp)
		{
			var visibility = CheckVisibility(emp);

			// Can't see the troop's components if it's not scanned
			// TODO - let player see design of previously scanned troop if the troop has not been refit
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
