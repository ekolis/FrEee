using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Combat2;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Edits the strategy of a vehicle design.
	/// </summary>
	public class EditStrategyCommand : Command<IDesign>
	{
		public EditStrategyCommand(IDesign design, StrategyObject strategy)
			: base(design)
		{
			Strategy = strategy;
		}

		/// <summary>
		/// The new strategy object to set on the design.
		/// </summary>
		public StrategyObject Strategy { get; set; }

		/// <summary>
		/// Sets the strategy on the design.
		/// </summary>
		public override void Execute()
		{
			Executor.Strategy = Strategy;
		}

		/// <summary>
		/// Contains the strategy, since it might be a new object from the client.
		/// </summary>
		public override IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield return Strategy;
			}
		}
	}
}
