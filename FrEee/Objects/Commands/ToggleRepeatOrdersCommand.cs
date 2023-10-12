using FrEee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Objects.Commands
{
	public class ToggleRepeatOrdersCommand : Command<IOrderable>
	{
		public ToggleRepeatOrdersCommand(IOrderable target, bool areRepeatOrdersEnabled)
			: base(target)
		{
			AreRepeatOrdersEnabled = areRepeatOrdersEnabled;
		}

		public bool AreRepeatOrdersEnabled { get; set; }

		public override void Execute()
		{
			Executor.AreRepeatOrdersEnabled = AreRepeatOrdersEnabled;
		}
	}
}
