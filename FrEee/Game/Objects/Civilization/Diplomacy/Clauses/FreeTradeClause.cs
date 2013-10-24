using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// Gives the receiving empire a quantity of resources proportional to the giving empire's gross income.
	/// Does not deduct any resources from the giving empire.
	/// </summary>
	public class FreeTradeClause : Clause
	{
		public FreeTradeClause(Empire giver, Empire receiver, Resource resource)
			: base(giver, receiver)
		{
			Resource = resource;
			TradePercentage = Mod.Current.Settings.StartTradePercent;
		}

		public override void PerformAction()
		{
			// perform trade
			Receiver.StoredResources[Resource] += (int)(Giver.GrossIncome[Resource] * TradePercentage / 100d);

			// increase trade percentage
			TradePercentage += Mod.Current.Settings.TradePercentPerTurn;
			if (TradePercentage > Mod.Current.Settings.MaxTradePercent)
				TradePercentage = Mod.Current.Settings.MaxTradePercent;
		}

		/// <summary>
		/// The percentage of the giver's income that will be traded.
		/// </summary>
		public double TradePercentage
		{
			get;
			set;
		}

		/// <summary>
		/// The resource being traded.
		/// </summary>
		public Resource Resource
		{
			get;
			set;
		}

		public override string Description
		{
			get
			{
				return Receiver.WeOrName() + " will receive income equal to " + TradePercentage + "% of " + Giver.UsOrName().Possessive() + " gross " + Resource.Name.ToLower() + " income (will increase to " + Mod.Current.Settings.MaxTradePercent + "% over " + Math.Ceiling(Mod.Current.Settings.MaxTradePercent - TradePercentage) / Mod.Current.Settings.TradePercentPerTurn + " turns).";
			}
		}
	}
}
