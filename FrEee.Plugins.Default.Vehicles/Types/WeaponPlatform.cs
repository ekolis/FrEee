using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Processes.Combat;
using FrEee.Modding.Abilities;
using FrEee.Vehicles.Types;

namespace FrEee.Plugins.Default.Vehicles.Types;

[Serializable]
public class WeaponPlatform : Vehicle, IUnit
{
	public override AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.WeaponPlatform; }
	}

	public override double CombatSpeed => 0;

	public ICargoContainer Container
	{
		get { return this.FindContainer(); }
	}

	public override int MaxTargets => int.MaxValue;

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

	public override bool ParticipatesInGroundCombat
	{
		get { return false; }
	}

	public override IMobileSpaceObject RecycleContainer
	{
		get { return (this as IUnit).Container as IMobileSpaceObject; }
	}

	public override bool RequiresSpaceYardQueue
	{
		get { return false; }
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

	public override WeaponTargets WeaponTargetType
	{
		// weapon platforms cannot be targeted in space combat
		get { return WeaponTargets.Invalid; }
	}

	public override Visibility CheckVisibility(Empire emp)
	{
		if (Owner == emp)
			return Visibility.Owned;
		var sobj = Container as ISpaceObject;
		if (sobj != null && sobj.HasVisibility(emp, Visibility.Scanned))
			return Visibility.Scanned;
		return Visibility.Unknown;
	}

	public override bool IsObsoleteMemory(Empire emp)
	{
		if (Container == null)
			return this.MemoryOwner() == emp && Timestamp < Game.Current.Timestamp - 1;
		return Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Game.Current.Timestamp - 1;
	}

	public override void Place(ISpaceObject target)
	{
		CommonExtensions.Place(this, target);
	}

	public override bool FillsCombatTile => false;

	public bool CanInvadeAndPoliceColonies => false;

	public bool CanFireIntoSpaceFromPlanetaryCargo => true;
}