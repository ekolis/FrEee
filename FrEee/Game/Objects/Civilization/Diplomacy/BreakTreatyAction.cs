using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// Breaks a treaty with the target empire.
	/// </summary>
	public class BreakTreatyAction : Action
	{
		public BreakTreatyAction(Empire target)
			: base(target)
		{
		}

		public override string Description
		{
			get { return "Break Treaty"; }
		}

		public override void Execute()
		{
			// TODO - break treaty, once we have treaty status
		}
	}
}
