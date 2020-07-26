using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Commands;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// An alliance, which can range from simple non-aggression to a full-fledged military alliance.
	/// </summary>
	public class AllianceClause : Clause
	{
		public AllianceClause(Empire giver, Empire receiver, AllianceLevel level)
			: base(giver, receiver)
		{
			AllianceLevel = level;
		}

		/// <summary>
		/// The level of alliance.
		/// </summary>
		public AllianceLevel AllianceLevel { get; set; }

		public override string BriefDescription => AllianceLevel.ToSpacedString();

		public override string FullDescription
		{
			get
			{
				var list = new List<string>();
				if (AllianceLevel >= AllianceLevel.MilitaryAlliance)
					list.Add(Giver.WeOrName() + " will declare war on anyone whom " + Receiver.WeOrName(false) + " declare" + (Receiver == Empire.Current ? "" : "s") + " war on.");
				if (AllianceLevel >= AllianceLevel.DefensivePact)
					list.Add(Giver.WeOrName() + " will declare war on anyone who declares war on " + Receiver.UsOrName() + ".");
				if (AllianceLevel >= AllianceLevel.NonAggression)
					list.Add(Giver.WeOrName() + " will not attack " + Receiver.UsOrName() + ".");
				else if (AllianceLevel >= AllianceLevel.NeutralZone)
					list.Add(Giver.WeOrName() + " will not attack " + Receiver.UsOrName() + " except in systems colonized by " + Giver.UsOrName() + ".");
				else // no alliance
					list.Add(Giver.WeOrName() + " is hostile to " + Receiver.UsOrName() + ".");
				return string.Join("\n", list.ToArray());
			}
		}

		/// <summary>
		/// Defensive pacts and military alliances will cause war to be delcared automatically.
		/// </summary>
		public override void PerformAction()
		{
			if (AllianceLevel >= AllianceLevel.DefensivePact)
			{
				// declare war on anyone who declares war on the recipient
				foreach (var msg in Receiver.IncomingMessages.OfType<ActionMessage>())
				{
					var action = msg.Action;
					if (action is DeclareWarAction)
						DeclareWar(action.Executor);
				}
			}
			if (AllianceLevel >= AllianceLevel.MilitaryAlliance)
			{
				// declare war on anyone who the recipient declares war on
				foreach (var msg in Receiver.Commands.OfType<ActionMessage>())
				{
					var action = msg.Action;
					if (action is DeclareWarAction)
						DeclareWar(action.Target);
				}
			}
		}

		private void DeclareWar(Empire emp)
		{
			// send declaration of war to the empire in question
			// note that you might not have even encountered them yet, but meh...
			var response = new ActionMessage(emp);
			response.Recipient = emp;
			response.Text = "Our allies have forced our hand. We must declare war!";
			response.Action = new DeclareWarAction(emp);
			var cmd = new SendMessageCommand(response);
			Empire.Current.Commands.Add(cmd);
			Empire.Current.TriggerHappinessChange(hm => hm.TreatyWar);
			emp.TriggerHappinessChange(hm => hm.TreatyWar);
		}
	}
}
