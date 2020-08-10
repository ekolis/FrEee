using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace FrEee.Game.Objects.Commands
{
	public class ToggleOrdersOnHoldCommand : Command<IOrderable>
	{
		public ToggleOrdersOnHoldCommand(IOrderable target, bool areOrdersOnHold)
			: base(target)
		{
			AreOrdersOnHold = areOrdersOnHold;
		}

		public bool AreOrdersOnHold { get; set; }

		public override void Execute()
		{
			if (Executor is null)
				return;
			Executor.AreOrdersOnHold = AreOrdersOnHold;
		}
	}
}
