using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using FrEee.Objects.GameState;
using FrEee.Objects.Civilization.CargoStorage;
using FrEee.Serialization;
using FrEee.Processes.Combat;
using FrEee.Ecs;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Objects.Vehicles;

[Serializable]
public class WeaponPlatform : Vehicle, IUnit
{
	public override AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.WeaponPlatform; }
	}

	public override double CombatSpeed => 0;

	public ICargoContainer Entity
	{
		get { return CommonExtensions.FindContainer(this); }
	}

	public override int MaxTargets => int.MaxValue;

	public override IEnumerable<IEntity> Parents
	{
		get
		{
			if (Owner != null)
				yield return Owner;
			if (Entity != null && Entity is IEntity)
				yield return (IEntity)Entity;
		}
	}

	public override bool ParticipatesInGroundCombat
	{
		get { return false; }
	}

	public override IMobileSpaceObject RecycleContainer
	{
		get { return (this as IUnit).Entity as IMobileSpaceObject; }
	}

	public override bool RequiresSpaceYardQueue
	{
		get { return false; }
	}

	[DoNotSerialize]
	public override Sector Sector
	{
		get { return Entity == null ? null : Entity.Sector; }
		set
		{
			//throw new NotSupportedException("Cannot set the sector of a weapon platform.");
		}
	}

	public override StarSystem StarSystem
	{
		get { return Entity?.StarSystem; }
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
		var sobj = Entity as ISpaceObject;
		if (sobj != null && sobj.HasVisibility(emp, Visibility.Scanned))
			return Visibility.Scanned;
		return Visibility.Unknown;
	}

	public override bool IsObsoleteMemory(Empire emp)
	{
		if (Entity == null)
			return this.MemoryOwner() == emp && Timestamp < Galaxy.Current.Timestamp - 1;
		return Entity.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
	}

	public override void Place(ISpaceObject target)
	{
		CommonExtensions.Place(this, target);
	}

	public override bool FillsCombatTile => false;
}