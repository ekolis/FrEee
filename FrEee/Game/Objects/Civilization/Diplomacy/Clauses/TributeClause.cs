﻿using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Civilization.Diplomacy.Clauses;
using FrEee.Modding;
using FrEee.Utility; using Newtonsoft.Json;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// Gives the receiving empire a quantity of resources proportional to the giving empire's gross income, or a flat quantity of resources.
	/// Deducts the resources from the giving empire.
	/// If the giving empire does not have enough resources, the receiving empire will be notified.
	/// </summary>
	public class TributeClause : Clause
	{
		public TributeClause(Empire giver, Empire receiver, Resource resource, int quantity, bool isPercentage)
			: base(giver, receiver)
		{
			Resource = resource;
			Quantity = quantity;
			IsPercentage = isPercentage;
		}

		public override void PerformAction()
		{
			// check total debt
			var totalDebt = Giver.OfferedTreatyClauses.OfType<TributeClause>().Where(c => c.Resource == Resource).Sum(c => c.Debt);

			// check max amount giveable
			var maxGiveable = Giver.StoredResources[Resource];

			// how much to pay?
			int toPay;
			if (maxGiveable >= totalDebt)
				toPay = Debt;
			else
			{
				// pay proportional to debt to each empire
				toPay = Debt / totalDebt * maxGiveable;
				var unpaid = Debt - toPay;
				Giver.Log.Add(Receiver.CreateLogMessage("We were unable to fulfill our tribute obligations to the " + Receiver + ". We fell short by " + unpaid.ToUnitString(true) + " " + Resource.Name.ToLower() + "."));
				Receiver.Log.Add(Giver.CreateLogMessage("The " + Giver + "was unable to fulfill its tribute obligations. They fell short by " + unpaid.ToUnitString(true) + " " + Resource.Name.ToLower() + "."));
			}

			// transfer resources
			Giver.StoredResources[Resource] -= toPay;
			Receiver.StoredResources[Resource] += toPay;
		}

		/// <summary>
		/// The quantity or percentage of resources or income.
		/// </summary>
		public int Quantity { get; set; }

		/// <summary>
		/// Is this a percentage or a flat rate?
		/// </summary>
		public bool IsPercentage { get; set; }

		/// <summary>
		/// The resource being traded.
		/// </summary>
		public Resource Resource
		{
			get;
			set;
		}

		/// <summary>
		/// The amount of tribute owed.
		/// </summary>
		public int Debt
		{
			get
			{
				if (IsPercentage)
					return Giver.GrossIncome[Resource] * Quantity / 100;
				return Quantity;
			}
		}

		public override string FullDescription
		{
			get
			{
				if (IsPercentage)
					return Giver.WeOrName() + " will pay " + Receiver.UsOrName() + " a tribute of " + Quantity + "% of " + Giver.WeOrName(false).Possessive() + " gross " + Resource.Name.ToLower() + " income.";
				else
					return Giver.WeOrName() + " will pay " + Receiver.UsOrName() + " a tribute of " + Quantity.ToUnitString(true) + " " + Resource.Name.ToLower() + " per turn.";
			}
		}

		public override string BriefDescription
		{
			get
			{
				string qty;
				if (IsPercentage)
					qty = Quantity + "%";
				else
					qty = Quantity.ToUnitString(true);
				return "Tribute (" + Resource + ") - " + qty;
			}
		}
	}
}
