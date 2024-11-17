using FrEee.Extensions;
using FrEee.Modding.Abilities;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Processes.Combat;
using System;

namespace FrEee.Vehicles;

[Serializable]
public class Mine : SpaceVehicle, IUnit
{
	public override AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Mine; }
	}

	public override bool CanWarp
	{
		get { return false; }
	}

	ICargoContainer IContainable<ICargoContainer>.Container
	{
		get { return this.FindContainer(); }
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
		// mines cannot be targeted in space combat
		get { return WeaponTargets.Invalid; }
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

	public override void Place(ISpaceObject target)
	{
		CommonExtensions.Place(this, target);
	}

	public override bool FillsCombatTile => false;
}
