using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	[Serializable]
	public class Satellite : SpaceUnit
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}
	}
}
