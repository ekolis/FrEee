using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Vehicles
{
	public class Fighter : Vehicle<Fighter>
	{
		public override bool RequiresSpaceYardQueue
		{
			get { return false; }
		}
	}
}
