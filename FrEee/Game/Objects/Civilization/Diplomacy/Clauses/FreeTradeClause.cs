﻿using FrEee.Game.Objects.Civilization;
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
			// save off amount of trade so the player can see it
			Amount = (int)(Giver.GrossDomesticIncome[Resource] * TradePercentage / 100d);

			// perform trade
			Receiver.StoredResources[Resource] += Amount;

			// cap resources at max storage
			if (Receiver.StoredResources[Resource] > Receiver.ResourceStorage[Resource])
				Receiver.StoredResources[Resource] = Receiver.ResourceStorage[Resource];

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

		public override string FullDescription
		{
			get
			{
				return Receiver.WeOrName() + " will receive income equal to " + TradePercentage + "% of " + Giver.UsOrName().Possessive() + " gross " + Resource.Name.ToLower() + " income (will increase to " + Mod.Current.Settings.MaxTradePercent + "% over " + Math.Ceiling(Mod.Current.Settings.MaxTradePercent - TradePercentage) / Mod.Current.Settings.TradePercentPerTurn + " turns).";
			}
		}

		public override string BriefDescription
		{
			get { return "Free Trade (" + Resource + ")"; }
		}

		/// <summary>
		/// The amount of resources traded this turn.
		/// </summary>
		public int Amount { get; set; }
	}
}
