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

		public override void Place(Interfaces.ISpaceObject target)
		{
			// TODO - place units in cargo once we have cargo
			throw new NotImplementedException("Units need to go in cargo when constructed. But we haven't implemented cargo yet...");
		}
	}
}
