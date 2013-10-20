using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// Declares war on the target empire.
	/// </summary>
	public class DeclarWarAction : Action
	{
		public DeclarWarAction(Empire target)
			: base(target)
		{
		}

		public override string Description
		{
			get { return "Declare War"; }
		}

		public override void Execute()
		{
			// TODO - declare war, once we have treaty status
		}
	}
}
