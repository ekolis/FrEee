﻿using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;

namespace FrEee.Objects.Civilization.Diplomacy.Clauses
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
			TradePercentage = The.Mod.Settings.StartTradePercent;
		}

		/// <summary>
		/// The amount of resources traded this turn.
		/// </summary>
		public int Amount { get; set; }

		public override string BriefDescription
		{
			get { return "Free Trade (" + Resource + ")"; }
		}

		public override string FullDescription
		{
			get
			{
				return Receiver.WeOrName() + " will receive income equal to " + TradePercentage + "% of " + Giver.UsOrName().Possessive() + " gross " + Resource.Name.ToLower() + " income (will increase to " + The.Mod.Settings.MaxTradePercent + "% over " + Math.Ceiling(The.Mod.Settings.MaxTradePercent - TradePercentage) / The.Mod.Settings.TradePercentPerTurn + " turns).";
			}
		}

		/// <summary>
		/// The resource being traded.
		/// </summary>
		public Resource Resource
		{
			get;
			set;
		}

		/// <summary>
		/// The percentage of the giver's income that will be traded.
		/// </summary>
		public double TradePercentage
		{
			get;
			set;
		}

		public override void PerformAction()
		{
			// save off amount of trade so the player can see it
			Amount = (int)(Giver.GrossDomesticIncome[Resource] * TradePercentage / 100d);

			// perform trade
			Receiver.StoredResources[Resource] += Amount;

			// cap resources at max storage
			if (Receiver.StoredResources[Resource] > Receiver.ResourceStorage[Resource])
				Receiver.StoredResources[Resource] = Receiver.ResourceStorage[Resource];

			// increase trade percentage
			TradePercentage += The.Mod.Settings.TradePercentPerTurn;
			if (TradePercentage > The.Mod.Settings.MaxTradePercent)
				TradePercentage = The.Mod.Settings.MaxTradePercent;
		}
	}
}