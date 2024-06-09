using FrEee.Objects.Civilization;
using FrEee.Extensions;
using System;
using FrEee.Objects.Space;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.Abilities;
using FrEee.Processes.Combat;

namespace FrEee.Objects.Vehicles;

[Serializable]
public class Satellite : SpaceVehicle, IUnit
{
	public override AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Satellite; }
	}

	public override bool CanWarp
	{
		get { return false; }
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
		get { return WeaponTargets.Satellite; }
	}

	public override Visibility CheckVisibility(Empire emp)
	{
		var vis = base.CheckVisibility(emp);
		var sobj = Container as ISpaceObject;
		if (vis < Visibility.Scanned && sobj != null && sobj.HasVisibility(emp, Visibility.Scanned))
			vis = Visibility.Scanned;
		return vis;
	}

	public override void Place(ISpaceObject target)
	{
		CommonExtensions.Place(this, target);
	}

	public override bool FillsCombatTile => false;
}
