using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// Command to set an empire's research priorities.
	/// </summary>
	public class ResearchCommand : Command<Empire>
	{
		public ResearchCommand()
			: base(Empire.Current)
		{
			spending = new SafeDictionary<Reference<Technology.Technology>, int>();
			queue = new List<Reference<Technology.Technology>>();
		}

		private SafeDictionary<Reference<Technology.Technology>, int> spending { get; set; }

		/// <summary>
		/// Priorities for spending, expressed as percentages of total research output.
		/// </summary>
		[DoNotSerialize]
		public IEnumerable<KeyValuePair<Technology.Technology, int>> Spending
		{
			get
			{
				return spending.Select(kvp => new KeyValuePair<Technology.Technology, int>(kvp.Key.Value, kvp.Value));
			}
		}

		public void SetSpending(Technology.Technology tech, int spendingPct)
		{
			spending[tech.Reference()] = spendingPct;
		}

		public void ClearSpending()
		{
			spending.Clear();
		}

		private List<Reference<Technology.Technology>> queue { get; set; }

		/// <summary>
		/// Queue for usage of leftover points after spending priorities are taken care of.
		/// </summary>
		[DoNotSerialize]
		public IEnumerable<Technology.Technology> Queue
		{
			get
			{
				return queue.Select(t => t.Value);
			}
		}

		public void AddToQueue(Technology.Technology tech)
		{
			queue.Add(tech.Reference());
		}

		public void RemoveFromQueue(Technology.Technology tech)
		{
			queue.Remove(tech.Reference());
		}

		public void ClearQueue()
		{
			queue.Clear();
		}

		public override void Execute()
		{
			// make sure spending is not over 100%
			var totalSpending = Spending.Sum(kvp => kvp.Value);
			if (totalSpending > 100)
			{
				foreach (var kvp in Spending.ToArray())
				{
					SetSpending(kvp.Key, kvp.Value / totalSpending / 100);
				}
			}

			// make sure no techs are prioritized or queued that the empire can't research
			foreach (var kvp in Spending.ToArray())
			{
				if (!Target.HasUnlocked(kvp.Key))
					SetSpending(kvp.Key, 0);
			}
			foreach (Technology.Technology tech in Queue.ToArray())
			{
				if (!Target.HasUnlocked(tech))
					RemoveFromQueue(tech);
			}

			// save to empire
			Target.ResearchSpending.Clear();
			foreach (var kvp in Spending)
				Target.ResearchSpending.Add(kvp);
			Target.ResearchQueue.Clear();
			foreach (var tech in Queue)
				Target.ResearchQueue.Add(tech);
		}
	}
}
