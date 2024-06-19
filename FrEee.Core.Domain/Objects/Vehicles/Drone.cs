using FrEee.Objects.Civilization;
using FrEee.Extensions;
using System;
using FrEee.Objects.Space;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Objects.Vehicles;

[Serializable]
public class Drone : SpaceVehicle, IUnit
{
	public override AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Drone; }
	}

	public override bool CanWarp
	{
		get { return !Owner?.IsMinorEmpire ?? true; }
	}

	ICargoContainer IContainable<ICargoContainer>.Container
	{
		get { return CommonExtensions.FindContainer(this); }
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

	public override WeaponTargets WeaponTargetType
	{
		get { return WeaponTargets.Drone; }
	}

	public override Visibility CheckVisibility(Empire emp)
	{
		var vis = base.CheckVisibility(emp);
		var sobj = Container as ISpaceObject;
		if (sobj != null && sobj.HasVisibility(emp, Visibility.Scanned))
			vis = Visibility.Scanned;
		return vis;
	}

	public override void Place(ISpaceObject target)
	{
		CommonExtensions.Place(this, target);
	}

	public override bool FillsCombatTile => false;
}
