﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
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

		public override bool IsObsoleteMemory(Empire emp)
		{
			if (Container == null)
				return this.MemoryOwner() == emp && Timestamp < Galaxy.Current.Timestamp - 1;
			return Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
		}

		public override AbilityTargets AbilityTarget
		{
			get { return AbilityTargets.WeaponPlatform; }
		}

		[DoNotSerialize]
		public override Sector Sector
		{
			get { return Container == null ? null : Container.Sector; }
			set
			{
				//throw new NotSupportedException("Cannot set the sector of a weapon platform.");
			}
		}

		public override StarSystem StarSystem
		{
			get { return Container?.StarSystem; }
		}

		public override IMobileSpaceObject RecycleContainer
		{
			get { return (this as IUnit).Container as IMobileSpaceObject; }
		}

		public override IEnumerable<IAbilityObject> Parents
		{
			get
			{
				if (Owner != null)
					yield return Owner;
				if (Container != null && Container is IAbilityObject)
					yield return (IAbilityObject)Container;
			}
		}
	}
}
