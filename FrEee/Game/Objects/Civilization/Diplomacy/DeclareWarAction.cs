using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// Declares war on the target empire.
	/// </summary>
	public class DeclareWarAction : Action
	{
		public DeclareWarAction(Empire target)
			: base(target)
		{
		}

		public override string Description
		{
			get { return "Declare War"; }
		}

		public override void Execute()
		{
			// TODO - break treaty, once we have treaties
			Executor.Log.Add(Target.CreateLogMessage("We have declared war on the " + Target + "."));
			Target.Log.Add(Executor.CreateLogMessage("The " + Target + " has declared war on us!"));
		}
	}
}
