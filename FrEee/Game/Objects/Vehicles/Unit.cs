using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	/// <summary>
	/// A vehicle which operates in groups.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class Unit : Vehicle
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}

		public override void Place(ISpaceObject target)
		{
			if (target is ICargoContainer)
			{
				var container = (ICargoContainer)target;
				var cargo = container.Cargo;
				if (cargo.Size + Design.Hull.Size <= container.CargoStorage)
				{
					cargo.Units.Add(this);
					return;
				}
			}
			foreach (var container in target.FindSector().SpaceObjects.OfType<ICargoContainer>().Where(cc => cc.Owner == Owner))
			{
				var cargo = container.Cargo;
				if (cargo.Size + Design.Hull.Size <= container.CargoStorage)
				{
					cargo.Units.Add(this);
					return;
				}
			}
			Owner.Log.Add(this.CreateLogMessage(this + " was lost due to insufficient cargo space at " + target + "."));
		}

		public override Interfaces.ICombatObject CombatObject
		{
			get
			{
				// TODO - unit groups
				throw new NotImplementedException("Units only particpate in combat in groups. Except maybe in ground combat. But anyway, we need unit groups for units to have combat objects.");
			}
		}
	}
}
