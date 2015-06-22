using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Sets the name of an object.
	/// </summary>
	public class SetPublicNameCommand : Command<INameable>
	{
		public SetPublicNameCommand(INameable target, string name)
			: base(target)
		{
			Name = name;
		}

		/// <summary>
		/// The name to set.
		/// </summary>
		public string Name { get; set; }

		public override void Execute()
		{
			Executor.Name = Name;
		}
	}
}
