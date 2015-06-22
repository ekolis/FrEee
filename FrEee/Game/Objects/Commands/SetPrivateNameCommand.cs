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
	/// Sets the private name for an object.
	/// </summary>
	public class SetPrivateNameCommand : Command<Empire>
	{
		public SetPrivateNameCommand(Empire empire, INameable target, string name)
			: base(empire)
		{
			Target = target;
			
		}

		/// <summary>
		/// What are we clearing the name on?
		/// </summary>
		[DoNotSerialize]
		public INameable Target { get { return target.Value; } set { target = value.ReferViaGalaxy(); } }

		private GalaxyReference<INameable> target { get; set; }

		/// <summary>
		/// The name to set.
		/// </summary>
		public string Name { get; set; }

		public override void Execute()
		{
			Executor.PrivateNames[target] = Name;
		}
	}
}
